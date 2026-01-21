// Copyright 2012,2024 texolution GmbH
// 
// Licensed under the Apache License, Version 2.0 (the "License"); you may not 
// use this file except in compliance with the License. You may obtain a copy of 
// the License at
// 
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software 
// distributed under the License is distributed on an "AS IS" BASIS, WITHOUT 
// WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the 
// License for the specific language governing permissions and limitations under 
// the License.

using C4ServerConnector.Exceptions;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace C4ServerConnector
{
    class HttpInterface
    {
        private readonly HttpClient _shortTimeoutClient;
        private readonly HttpClient _longTimeoutClient;
        private readonly string _sessionLogFn;

        private const string BOUNDARY = "u7g89dsaanu43g279dfs";
        private const int DOWNLOAD_BUFFER_SIZE = 1048576;

        private string _ticket;
        public string Ticket
        {
            get => _ticket;
            set
            {
                _ticket = value;
                SetTicketHeaders(_ticket); // updates both HttpClients safely
            }
        }
        public HttpInterface(
            string localCertFile,
            string ticket,
            bool writeSessionLog,
            long dataTimeoutSeconds,
            long contentTimeoutSeconds,
            string tempPath)
        {
            _sessionLogFn = writeSessionLog
                ? Path.Combine(tempPath, "C4_Session_Log_" + DateTime.Now.ToString("yyyy-MM-dd_HH:mm:ss.fff") + ".log")
                : null;

            X509Certificate2 certificate = null;
            if (!string.IsNullOrEmpty(localCertFile))
            {
                certificate = new X509Certificate2(localCertFile);
            }

            // IMPORTANT: do NOT share one handler instance across multiple HttpClient instances.
            _shortTimeoutClient = new HttpClient(CreateHandler(certificate))
            {
                Timeout = TimeSpan.FromSeconds(dataTimeoutSeconds)
            };

            _longTimeoutClient = new HttpClient(CreateHandler(certificate))
            {
                Timeout = TimeSpan.FromSeconds(contentTimeoutSeconds)
            };

            if (!string.IsNullOrEmpty(ticket))
            {
                Ticket = ticket;
                SetTicketHeaders(ticket);
            }
        }

        private static HttpClientHandler CreateHandler(X509Certificate2 certificate)
        {
            return new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
                {
                    if (certificate == null)
                    {
                        return sslPolicyErrors == SslPolicyErrors.None;
                    }

                    return cert != null && cert.GetCertHashString() == certificate.GetCertHashString();
                },
                MaxResponseHeadersLength = 1000000
            };
        }

        private static T Sync<T>(Task<T> task) =>
            task.ConfigureAwait(false).GetAwaiter().GetResult();

        private static void Sync(Task task) =>
            task.ConfigureAwait(false).GetAwaiter().GetResult();

        private void SetTicketHeaders(string ticket)
        {
            SetTicketHeader(_shortTimeoutClient, ticket);
            SetTicketHeader(_longTimeoutClient, ticket);
        }

        private static void SetTicketHeader(HttpClient client, string ticket)
        {
            client.DefaultRequestHeaders.Remove("ticket");
            if (!string.IsNullOrEmpty(ticket))
            {
                client.DefaultRequestHeaders.Add("ticket", ticket);
            }
        }

        public string GetCommand(string cmdUrl)
        {
            if (_sessionLogFn != null)
            {
                File.AppendAllText(_sessionLogFn, $"GET {cmdUrl}{Environment.NewLine}");
            }

            using var result = Sync(_shortTimeoutClient.GetAsync(cmdUrl, HttpCompletionOption.ResponseHeadersRead));
            // Optional: result.EnsureSuccessStatusCode();
            return Sync(result.Content.ReadAsStringAsync());
        }

        public string PostConnectCommand(string cmdUrl, XmlDocument requestBody)
        {
            using var requestMessage = new HttpRequestMessage(HttpMethod.Post, cmdUrl)
            {
                Content = new StringContent(requestBody.OuterXml, Encoding.UTF8, "application/xml")
            };

            if (_sessionLogFn != null)
            {
                File.AppendAllText(_sessionLogFn, $"POST {cmdUrl}\n{requestBody.OuterXml}\n");
            }

            using var responseMessage = Sync(_shortTimeoutClient.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead));
            // Optional: responseMessage.EnsureSuccessStatusCode();

            string responseString = Sync(responseMessage.Content.ReadAsStringAsync());

            if (_sessionLogFn != null)
            {
                File.AppendAllText(_sessionLogFn, $"  Response: {responseString}\n");
            }

            XmlDocument responseXml = new XmlDocument();
            responseXml.LoadXml(responseString);

            CheckForConnectErrors(responseXml);

            XmlNode ticketN = responseXml.DocumentElement.SelectSingleNode("cinnamonConnection/ticket");
            if (ticketN != null)
            {
                Ticket = ticketN.InnerText;
                SetTicketHeaders(Ticket);
                return Ticket;
            }

            return null;
        }

        public XmlDocument PostCommand(string cmdUrl, XmlDocument requestBody, bool longTimeout = false)
        {
            using var requestMessage = new HttpRequestMessage(HttpMethod.Post, cmdUrl)
            {
                Content = new StringContent(requestBody.OuterXml, Encoding.UTF8, "application/xml")
            };

            if (_sessionLogFn != null)
            {
                File.AppendAllText(_sessionLogFn, $"POST {cmdUrl}\n{requestBody.OuterXml}\n");
            }

            var client = longTimeout ? _longTimeoutClient : _shortTimeoutClient;

            HttpResponseMessage responseMessage;
            try
            {
                responseMessage = Sync(client.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead));
            }
            catch (TaskCanceledException ex)
            {
                // HttpClient timeout commonly manifests as TaskCanceledException.
                throw new TimeoutException($"HTTP request timed out after {client.Timeout} for {cmdUrl}", ex);
            }

            using (responseMessage)
            {
                // Optional: responseMessage.EnsureSuccessStatusCode();

                string responseString = Sync(responseMessage.Content.ReadAsStringAsync());

                if (_sessionLogFn != null)
                {
                    File.AppendAllText(_sessionLogFn, $"  Response: {responseString}\n");
                }

                XmlDocument responseXml = new XmlDocument();
                responseXml.LoadXml(responseString);

                CheckForErrors(cmdUrl, requestBody, responseXml);
                return responseXml;
            }
        }

        public XmlDocument PostCommandFileUpload(string cmdUrl, XmlDocument requestBody, string filename)
        {
            using var multipartContent = new MultipartFormDataContent(BOUNDARY);
            multipartContent.Headers.ContentType.MediaType = "multipart/form-data";
            multipartContent.Add(new StringContent(requestBody.OuterXml, Encoding.UTF8, "application/xml"), "cinnamonRequest");

            if (_sessionLogFn != null)
            {
                File.AppendAllText(_sessionLogFn, $"POST {cmdUrl}\n{requestBody.OuterXml}\n");
            }

            if (filename == null)
            {
                using var requestMessage = new HttpRequestMessage(HttpMethod.Post, cmdUrl)
                {
                    Content = multipartContent
                };

                using var responseMessage = Sync(_shortTimeoutClient.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead));
                // Optional: responseMessage.EnsureSuccessStatusCode();

                string responseString = Sync(responseMessage.Content.ReadAsStringAsync());

                XmlDocument responseXml = new XmlDocument();
                responseXml.LoadXml(responseString);

                CheckForErrors(cmdUrl, requestBody, responseXml);
                return responseXml;
            }

            using (FileStream fs = File.OpenRead(filename))
            {
                var fileContent = new StreamContent(fs);
                multipartContent.Add(fileContent, "file", Path.GetFileName(filename));

                using var requestMessage = new HttpRequestMessage(HttpMethod.Post, cmdUrl)
                {
                    Content = multipartContent
                };

                HttpResponseMessage responseMessage;
                try
                {
                    responseMessage = Sync(_longTimeoutClient.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead));
                }
                catch (TaskCanceledException ex)
                {
                    throw new TimeoutException($"HTTP upload timed out after {_longTimeoutClient.Timeout} for {cmdUrl}", ex);
                }

                using (responseMessage)
                {
                    // Optional: responseMessage.EnsureSuccessStatusCode();

                    string responseString = Sync(responseMessage.Content.ReadAsStringAsync());

                    XmlDocument responseXml = new XmlDocument();
                    responseXml.LoadXml(responseString);

                    CheckForErrors(cmdUrl, requestBody, responseXml);
                    return responseXml;
                }
            }
        }

        public void PostDisconnectCommand(string cmdUrl)
        {
            if (_sessionLogFn != null)
            {
                File.AppendAllText(_sessionLogFn, $"POST {cmdUrl}{Environment.NewLine}");
            }

            using var c = new StringContent("", Encoding.UTF8);

            using var result = Sync(_shortTimeoutClient.PostAsync(cmdUrl, c));
            // Optional: result.EnsureSuccessStatusCode();

            XmlDocument resp = new XmlDocument();
            resp.LoadXml(Sync(result.Content.ReadAsStringAsync()));

            if (resp.DocumentElement.SelectSingleNode("disconnectSuccessful")?.InnerText == "true")
            {
                Ticket = null;
                SetTicketHeaders(null);
            }
        }

        public void PostCommandFileDownload(string cmdUrl, XmlDocument requestBody, string contentFn)
        {
            using var content = new StringContent(requestBody.OuterXml, Encoding.UTF8);

            if (_sessionLogFn != null)
            {
                File.AppendAllText(_sessionLogFn, $"POST {cmdUrl}{Environment.NewLine}{requestBody.OuterXml}{Environment.NewLine}");
            }

            HttpResponseMessage respMsg;
            try
            {
                respMsg = Sync(_longTimeoutClient.PostAsync(cmdUrl, content));
            }
            catch (TaskCanceledException ex)
            {
                throw new TimeoutException($"HTTP download request timed out after {_longTimeoutClient.Timeout} for {cmdUrl}", ex);
            }

            using (respMsg)
            {
                // Optional: respMsg.EnsureSuccessStatusCode();

                using Stream respStream = Sync(respMsg.Content.ReadAsStreamAsync());

                using var fst = new FileStream(contentFn, FileMode.Create, FileAccess.Write, FileShare.None);

                var buffer = new byte[DOWNLOAD_BUFFER_SIZE];
                int read;
                while ((read = respStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    fst.Write(buffer, 0, read);
                }
            }
        }

        public void PostCommandFileDownloadToStream(string cmdUrl, XmlDocument requestBody, Stream outputStream)
        {
            using var content = new StringContent(requestBody.OuterXml, Encoding.UTF8);

            if (_sessionLogFn != null)
            {
                File.AppendAllText(_sessionLogFn, $"POST {cmdUrl}{Environment.NewLine}{requestBody.OuterXml}{Environment.NewLine}");
            }

            HttpResponseMessage respMsg;
            try
            {
                respMsg = Sync(_longTimeoutClient.PostAsync(cmdUrl, content));
            }
            catch (TaskCanceledException ex)
            {
                throw new TimeoutException($"HTTP download request timed out after {_longTimeoutClient.Timeout} for {cmdUrl}", ex);
            }

            using (respMsg)
            {
                // Optional: respMsg.EnsureSuccessStatusCode();

                using Stream respStream = Sync(respMsg.Content.ReadAsStreamAsync());

                var buffer = new byte[DOWNLOAD_BUFFER_SIZE];
                int read;
                while ((read = respStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    outputStream.Write(buffer, 0, read);
                }

                outputStream.Flush();
            }
        }

        private void CheckForErrors(string cmdUrl, XmlDocument requestBody, XmlDocument resp)
        {
            XmlNodeList errors = resp.DocumentElement.SelectNodes("errors/error");
            if (errors.Count == 0) return;

            if (errors.Count == 1)
            {
                XmlElement errorEl = errors[0] as XmlElement;
                string code = errorEl.SelectSingleNode("code").InnerText;

                if (code == "INTERNAL_SERVER_ERROR_TRY_AGAIN_LATER")
                    throw new InternalServerErrorException(errorEl.SelectSingleNode("message").InnerText);
                if (code == "AUTHENTICATION_FAIL_SESSION_EXPIRED")
                    throw new SessionExpiredException(errorEl.SelectSingleNode("message").InnerText);
                if (code == "FOLDER_NOT_FOUND") return; /* do nothing */
                if (code == "OBJECT_NOT_FOUND") return; /* do nothing */

                throw new ApplicationException(string.Join("\n",
                    "Message:" + errorEl.SelectSingleNode("message").InnerText,
                    "Command: " + cmdUrl,
                    "Request: " + requestBody.OuterXml));
            }

            StringBuilder sb = new StringBuilder();
            bool throwable = false;

            foreach (XmlElement errorEl in errors)
            {
                string code = errorEl.SelectSingleNode("code").InnerText;

                if (code == "INTERNAL_SERVER_ERROR_TRY_AGAIN_LATER")
                    throw new InternalServerErrorException(errorEl.SelectSingleNode("message").InnerText);
                if (code == "AUTHENTICATION_FAIL_SESSION_EXPIRED")
                    throw new SessionExpiredException(errorEl.SelectSingleNode("message").InnerText);
                if (code == "FOLDER_NOT_FOUND") continue; /* do nothing */
                if (code == "OBJECT_NOT_FOUND") continue; /* do nothing */

                throwable = true;
                sb.AppendLine(code + ": " + errorEl.SelectSingleNode("message").InnerText);
            }

            if (throwable) throw new ApplicationException(sb.ToString());
        }

        private void CheckForConnectErrors(XmlDocument resp)
        {
            XmlNodeList errors = resp.DocumentElement.SelectNodes("errors/error");
            if (errors.Count == 0) return;

            if (errors.Count == 1)
            {
                XmlElement errorEl = errors[0] as XmlElement;
                string code = errorEl.SelectSingleNode("code").InnerText;
                if (code == "CONNECTION_FAIL_WRONG_PASSWORD")
                    throw new ConnectionFailedException(errorEl.SelectSingleNode("message").InnerText);
                return;
            }

            foreach (XmlElement errorEl in errors)
            {
                string code = errorEl.SelectSingleNode("code").InnerText;
                if (code == "CONNECTION_FAIL_WRONG_PASSWORD")
                    throw new ConnectionFailedException(errorEl.SelectSingleNode("message").InnerText);
            }
        }
    }
}

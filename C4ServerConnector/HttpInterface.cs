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
using System;
using System.Net.Http;
using System.Text;
using System.Xml;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using C4ServerConnector.Exceptions;

namespace C4ServerConnector
{
    class HttpInterface
    {
        private HttpClient _shortTimeoutClient;
        private HttpClient _longTimeoutClient;
        private string _sessionLogFn;
        public string Ticket { get; set; }
        
        public HttpInterface(string localCertFile, string ticket, bool writeSessionLog, long dataTimeoutSeconds, long contentTimeoutSeconds, string tempPath)
        {
            _sessionLogFn = (writeSessionLog?Path.Combine(tempPath, "C4_Session_Log_"+DateTime.Now.ToString("yyyy-MM-dd_HH:mm:ss.fff"))+".log":null);
            X509Certificate2 certificate = null;
            if (localCertFile!=null)
            {
                certificate= new X509Certificate2(localCertFile);
            }
            HttpClientHandler handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
                {
                    if (certificate == null) return sslPolicyErrors == SslPolicyErrors.None;
                    else return cert.GetCertHashString() == certificate.GetCertHashString();
                },
                MaxResponseHeadersLength = 1000000
        };

            _shortTimeoutClient = new HttpClient(handler);
            _shortTimeoutClient.Timeout = TimeSpan.FromSeconds(dataTimeoutSeconds);
            if (ticket != null) 
            {
                Ticket = ticket;
                _shortTimeoutClient.DefaultRequestHeaders.Add("ticket", Ticket);
            }

            _longTimeoutClient = new HttpClient(handler);
            _longTimeoutClient.Timeout = TimeSpan.FromSeconds(contentTimeoutSeconds);
            if (ticket != null)
            {
                Ticket = ticket;
                _longTimeoutClient.DefaultRequestHeaders.Add("ticket", Ticket);
            }
        }
        public string GetCommand(string cmdUrl)
        {
            HttpResponseMessage result = _shortTimeoutClient.GetAsync(cmdUrl).Result;
            if(_sessionLogFn!=null) File.AppendAllText(_sessionLogFn, string.Concat("GET ",cmdUrl,Environment.NewLine));
            return result.Content.ReadAsStringAsync().Result;
        }
        public string PostConnectCommand(string cmdUrl, XmlDocument requestBody)
        {
            HttpContent content = new StringContent(requestBody.OuterXml, Encoding.UTF8, "application/xml");
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, cmdUrl)
            {
                Content = content
            };
            if (_sessionLogFn != null)
            {
                File.AppendAllText(_sessionLogFn, $"POST {cmdUrl}\n{requestBody.OuterXml}\n");
            }
            HttpResponseMessage responseMessage = _shortTimeoutClient.SendAsync(requestMessage).Result;
            string responseString = responseMessage.Content.ReadAsStringAsync().Result;
            if (_sessionLogFn != null)
            {
                File.AppendAllText(_sessionLogFn, $"  Response: {responseString}\n");
            }
            XmlDocument responseXml = new XmlDocument();
            responseXml.LoadXml(responseString);
            CheckForConnectErrors(responseXml);
            XmlNode ticketN = responseXml.DocumentElement.SelectSingleNode("cinnamonConnection/ticket");
            if (ticketN!=null)
            {
                Ticket = ticketN.InnerText;
                _shortTimeoutClient.DefaultRequestHeaders.Add("ticket", Ticket);
                _longTimeoutClient.DefaultRequestHeaders.Add("ticket", Ticket);
                return Ticket;
            }
            return null;
        }
        public XmlDocument PostCommand(string cmdUrl, XmlDocument requestBody)
        {
            HttpContent content = new StringContent(requestBody.OuterXml, Encoding.UTF8, "application/xml");
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, cmdUrl)
            {
                Content = content
            };
            if (_sessionLogFn != null)
            {
                File.AppendAllText(_sessionLogFn, $"POST {cmdUrl}\n{requestBody.OuterXml}\n");
            }
            HttpResponseMessage responseMessage = _shortTimeoutClient.SendAsync(requestMessage).Result;
            string responseString = responseMessage.Content.ReadAsStringAsync().Result;
            if (_sessionLogFn != null)
            {
                File.AppendAllText(_sessionLogFn, $"  Response: {responseString}\n");
            }

            // WORKAROUND FOR SERVER BUG (invalid trigger response with echo)
            //if(responseString.StartsWith("<trigger_request>")) return FixServerResponse(responseString);

            XmlDocument responseXml = new XmlDocument();
            responseXml.LoadXml(responseString);
            CheckForErrors(cmdUrl, requestBody, responseXml);
            return responseXml;
        }
        //public async Task<XmlDocument> PostCommandFileUploadFromStreamAsync(string cmdUrl, XmlDocument requestBody, Stream stream)
        //{
        //    MultipartFormDataContent multipartContent = new MultipartFormDataContent(Constants.BOUNDARY);
        //    multipartContent.Headers.ContentType.MediaType = "multipart/form-data";
        //    multipartContent.Add(new StringContent(requestBody.OuterXml, Encoding.UTF8, "application/xml"), "cinnamonRequest");

        //    StreamContent fileContent = new StreamContent(stream);
        //    multipartContent.Add(fileContent, "file", "upload.tmp");

        //    HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, cmdUrl)
        //    {
        //        Content = multipartContent
        //    };

        //    HttpResponseMessage responseMessage = await _longTimeoutClient.SendAsync(requestMessage);
        //    string responseString = await responseMessage.Content.ReadAsStringAsync();

        //    XmlDocument responseXml = new XmlDocument();
        //    responseXml.LoadXml(responseString);
        //    CheckForErrors(cmdUrl, requestBody, responseXml);
        //    return responseXml;
        //}
        public XmlDocument PostCommandFileUpload(string cmdUrl, XmlDocument requestBody, string filename)
        {
            //requestBody.DocumentElement.AppendChild(requestBody.CreateElement("ticket")).InnerText = Ticket;
            MultipartFormDataContent multipartContent = new MultipartFormDataContent(Constants.BOUNDARY);
            multipartContent.Headers.ContentType.MediaType = "multipart/form-data";
            multipartContent.Add(new StringContent(requestBody.OuterXml, Encoding.UTF8, "application/xml"), "cinnamonRequest");
            if (_sessionLogFn != null)
            {
                File.AppendAllText(_sessionLogFn, $"POST {cmdUrl}\n{requestBody.OuterXml}\n");
            }
            if (filename==null)
            {
                HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, cmdUrl)
                {
                    Content = multipartContent
                };
                HttpResponseMessage responseMessage = _shortTimeoutClient.SendAsync(requestMessage).Result;
                string responseString = responseMessage.Content.ReadAsStringAsync().Result;
                XmlDocument responseXml = new XmlDocument();
                responseXml.LoadXml(responseString);
                CheckForErrors(cmdUrl, requestBody, responseXml);
                return responseXml;
            }
            else
            {
                using (FileStream fs = File.OpenRead(filename))
                {
                    StreamContent fileContent = new StreamContent(fs);
                    multipartContent.Add(fileContent, "file", Path.GetFileName(filename));
                    HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, cmdUrl)
                    {
                        Content = multipartContent
                    };
                    HttpResponseMessage responseMessage = _longTimeoutClient.SendAsync(requestMessage).Result;
                    string responseString = responseMessage.Content.ReadAsStringAsync().Result;
                    XmlDocument responseXml = new XmlDocument();
                    responseXml.LoadXml(responseString);
                    CheckForErrors(cmdUrl, requestBody, responseXml);
                    return responseXml;
                }

            }
        }
        public void PostDisconnectCommand(string cmdUrl)
        {
            HttpContent c = new StringContent("", Encoding.UTF8);
            if (_sessionLogFn != null) File.AppendAllText(_sessionLogFn, string.Concat("POST ", cmdUrl, Environment.NewLine));
            HttpResponseMessage result = _shortTimeoutClient.PostAsync(cmdUrl, c).Result;

            XmlDocument resp = new XmlDocument();
            resp.LoadXml(result.Content.ReadAsStringAsync().Result);
            if (resp.DocumentElement.SelectSingleNode("disconnectSuccessful").InnerText=="true")
            {
                Ticket = null;
                _shortTimeoutClient.DefaultRequestHeaders.Remove("ticket");
                _longTimeoutClient.DefaultRequestHeaders.Remove("ticket");
                return;
            }
        }
        public void PostCommandFileDownload(string cmdUrl, XmlDocument requestBody, string contentFn)
        {
            // Add ticket if needed to the request body
            // requestBody.DocumentElement.AppendChild(requestBody.CreateElement("ticket")).InnerText = Ticket;

            HttpContent content = new StringContent(requestBody.OuterXml, Encoding.UTF8);

            if (_sessionLogFn != null)
            {
                File.AppendAllText(_sessionLogFn, string.Concat("POST ", cmdUrl, Environment.NewLine, requestBody.OuterXml, Environment.NewLine));
            }

            HttpResponseMessage respMsg = _longTimeoutClient.PostAsync(cmdUrl, content).Result;

            // Ensure response stream and file streams are properly disposed of
            using (Stream respStream = respMsg.Content.ReadAsStreamAsync().Result)
            using (BinaryReader reader = new BinaryReader(respStream))
            using (FileStream fst = new FileStream(contentFn, FileMode.Create, FileAccess.Write, FileShare.None))
            using (BinaryWriter writer = new BinaryWriter(fst))
            {
                int bufSize = Constants.DOWNLOAD_BUFFER_SIZE;
                while (bufSize > 0)
                {
                    byte[] buffer = reader.ReadBytes(bufSize);
                    if (buffer.Length == 0)
                    {
                        break; // End of stream
                    }
                    writer.Write(buffer);
                    bufSize = buffer.Length;
                }
            }

            // No need to explicitly call Close(), as `using` handles resource cleanup.
        }
        public void PostCommandFileDownloadToStream(string cmdUrl, XmlDocument requestBody, Stream outputStream)
        {
            // Add ticket if needed to the request body
            // requestBody.DocumentElement.AppendChild(requestBody.CreateElement("ticket")).InnerText = Ticket;

            HttpContent content = new StringContent(requestBody.OuterXml, Encoding.UTF8);

            if (_sessionLogFn != null)
            {
                File.AppendAllText(_sessionLogFn, string.Concat("POST ", cmdUrl, Environment.NewLine, requestBody.OuterXml, Environment.NewLine));
            }

            HttpResponseMessage respMsg = _longTimeoutClient.PostAsync(cmdUrl, content).Result;

            // Ensure response stream and outputStream are properly handled
            using (Stream respStream = respMsg.Content.ReadAsStreamAsync().Result)
            {
                int bufSize = Constants.DOWNLOAD_BUFFER_SIZE;
                byte[] buffer = new byte[bufSize];
                int bytesRead;
                while ((bytesRead = respStream.Read(buffer, 0, bufSize)) > 0)
                {
                    outputStream.Write(buffer, 0, bytesRead);
                }
            }

            // Ensure outputStream is properly flushed if necessary
            outputStream.Flush();
        }
        //public void PostCommandFileDownload(string cmdUrl, XmlDocument requestBody, string contentFn)
        //{
        //    //requestBody.DocumentElement.AppendChild(requestBody.CreateElement("ticket")).InnerText = Ticket;
        //    HttpContent c = new StringContent(requestBody.OuterXml, Encoding.UTF8);
        //    if (_sessionLogFn != null) File.AppendAllText(_sessionLogFn, string.Concat("POST ", cmdUrl, Environment.NewLine, requestBody.OuterXml, Environment.NewLine));
        //    HttpResponseMessage respMsg = _longTimeoutClient.PostAsync(cmdUrl, c).Result;
        //    Stream respStream = respMsg.Content.ReadAsStreamAsync().Result;
        //    BinaryReader reader = new BinaryReader(respStream);
        //    FileStream fst = new FileStream(contentFn, FileMode.Create, FileAccess.Write);
        //    BinaryWriter wrt = new BinaryWriter(fst);
        //    int bufSize = Constants.DOWNLOAD_BUFFER_SIZE;
        //    while (bufSize > 0)
        //    {
        //        byte[] buffer = reader.ReadBytes(bufSize);
        //        wrt.Write(buffer);
        //        bufSize = buffer.Length;
        //    }
        //    respStream.Close();
        //    wrt.Close();
        //    fst.Close();
        //    reader.Close();
        //    return;
        //}
        public Stream PostCommandFileDownloadAsStream(string cmdUrl, XmlDocument requestBody)
        {
            HttpContent content = new StringContent(requestBody.OuterXml, Encoding.UTF8, "application/xml");
            if (_sessionLogFn != null) File.AppendAllText(_sessionLogFn, string.Concat("POST ", cmdUrl, Environment.NewLine, requestBody.OuterXml, Environment.NewLine));
            HttpResponseMessage respMsg = _longTimeoutClient.PostAsync(cmdUrl, content).Result;
            respMsg.EnsureSuccessStatusCode();
            Stream respStream = respMsg.Content.ReadAsStreamAsync().Result;
            return respStream;
        }
        private void CheckForErrors(string cmdUrl, XmlDocument requestBody, XmlDocument resp)
        {
            XmlNodeList errors = resp.DocumentElement.SelectNodes("errors/error");
            if(errors.Count == 0) return;
            else if(errors.Count == 1)
            {
                XmlElement errorEl = errors[0] as XmlElement;
                string code = errorEl.SelectSingleNode("code").InnerText;
                if (code == "INTERNAL_SERVER_ERROR_TRY_AGAIN_LATER") throw new InternalServerErrorException(errorEl.SelectSingleNode("message").InnerText);
                else if (code == "AUTHENTICATION_FAIL_SESSION_EXPIRED") throw new SessionExpiredException(errorEl.SelectSingleNode("message").InnerText);
                else if (code == "FOLDER_NOT_FOUND") ; /* do nothing*/
                else if (code == "OBJECT_NOT_FOUND") ; /* do nothing*/
                else throw new ApplicationException(string.Join("\n","Message:" + errorEl.SelectSingleNode("message").InnerText,"Command: "+cmdUrl, "Request: " + requestBody.OuterXml));
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                bool throwable = false;
                foreach (XmlElement errorEl in errors)
                {
                    string code = errorEl.SelectSingleNode("code").InnerText;
                    if (code == "INTERNAL_SERVER_ERROR_TRY_AGAIN_LATER") throw new InternalServerErrorException(errorEl.SelectSingleNode("message").InnerText);
                    else if (code == "AUTHENTICATION_FAIL_SESSION_EXPIRED") throw new SessionExpiredException(errorEl.SelectSingleNode("message").InnerText);
                    else if (code == "FOLDER_NOT_FOUND") ; /* do nothing*/
                    else if (code == "OBJECT_NOT_FOUND") ; /* do nothing*/
                    else
                    {
                        throwable = true;
                        sb.AppendLine(code + ": " + errorEl.SelectSingleNode("message").InnerText);
                    }
                }
                if(throwable) throw new ApplicationException(sb.ToString());
            }
        }
        private void CheckForConnectErrors(XmlDocument resp)
        {
            XmlNodeList errors = resp.DocumentElement.SelectNodes("errors/error");
            if (errors.Count == 0) return;
            else if (errors.Count == 1)
            {
                XmlElement errorEl = errors[0] as XmlElement;
                string code = errorEl.SelectSingleNode("code").InnerText;
                if (code == "CONNECTION_FAIL_WRONG_PASSWORD") throw new ConnectionFailedException(errorEl.SelectSingleNode("message").InnerText);
            }
            else
            {
                //StringBuilder sb = new StringBuilder();
                foreach (XmlElement errorEl in errors)
                {
                    string code = errorEl.SelectSingleNode("code").InnerText;
                    if (code == "CONNECTION_FAIL_WRONG_PASSWORD") throw new ConnectionFailedException(errorEl.SelectSingleNode("message").InnerText);
                    //sb.AppendLine(code + ": " + errorEl.SelectSingleNode("message").InnerText);
                }
                //throw new ApplicationException(sb.ToString());
            }
        }
    }
}

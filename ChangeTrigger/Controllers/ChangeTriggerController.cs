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
using ChangeTriggerLib.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Serilog;

[ApiController]
[Route("[controller]")]
public class ChangeTriggerController : ControllerBase
{
    private readonly TriggerActionService _triggerActionService;
    private readonly ILogger<ChangeTriggerController> _logger;

    public ChangeTriggerController(TriggerActionService triggerActionService, ILogger<ChangeTriggerController> logger)
    {
        _triggerActionService = triggerActionService;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> Post()
    {
        try
        {
            _logger.LogInformation($"Entered POST");
            string actionParameter = HttpContext.Request.Query["action"].ToString();
            _logger.LogInformation($"Action: {actionParameter}");

            string requestBody = string.Empty;
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                requestBody = await reader.ReadToEndAsync();
            }
            _logger.LogInformation($"Request body: {requestBody}");
            XmlDocument requestData = null;
            if(requestBody.Length>0)
            {
                // pre-trigger or post-commit-trigger
                requestData = new XmlDocument();
                requestData.LoadXml(requestBody);
            }

            //string ticketHeader = Request.Headers["ticket"].ToString();
            string ticketHeader = _triggerActionService.ServiceSession.Ticket;

            XmlDocument requestToCinnamon = null;
            if (Request.Headers.TryGetValue("cinnamon-request", out StringValues cinnamonRequestHeader))
            {
                _logger.LogInformation($"CinnamonRequest: {cinnamonRequestHeader}");
                if (cinnamonRequestHeader.ToString().Trim().Length>0)
                {
                    try
                    {
                        requestToCinnamon = new XmlDocument();
                        requestToCinnamon.LoadXml(cinnamonRequestHeader);
                    }
                    catch (XmlException ex)
                    {
                        _logger.LogError(ex, "Invalid XML in cinnamon-request header.");
                        return BadRequest("Invalid XML in cinnamon-request header.");
                    }
                }
            }

            _logger.LogInformation($"Ticket: {ticketHeader}");
            if(requestToCinnamon==null) _logger.LogInformation($"CinnamonRequest: missing");
            else 
            _logger.LogInformation($"Request to Cinnamon: {requestToCinnamon.OuterXml}");

            XmlDocument resp = null;
            try
            {
                resp = await _triggerActionService.GetAction(actionParameter, _logger).ExecuteAsync(ticketHeader, requestData, requestToCinnamon, Request.Headers);
            }
            catch(SessionExpiredException ex)
            {
                _logger.LogError(ex, "Session expired - trying to reconnect.");
                _triggerActionService.ReconnectServiceSession();
                resp = await _triggerActionService.GetAction(actionParameter, _logger).ExecuteAsync(ticketHeader, requestData, requestToCinnamon, Request.Headers);
            }

            // TODO: evaluate response for success
            _logger.LogInformation($"Trigger action response: {resp.OuterXml}");

            _logger.LogInformation("Successfully processed request.");
            return Ok(resp.OuterXml);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing request.");
            return BadRequest();
        }
    }
}
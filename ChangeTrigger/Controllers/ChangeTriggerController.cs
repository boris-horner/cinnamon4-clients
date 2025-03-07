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
using System.Text;
using System.Xml;
using Serilog;

[ApiController]
[Route("[controller]")]
public class ChangeTriggerController : ControllerBase
{
    private readonly TriggerActionService _triggerActionService;
    private readonly Serilog.ILogger _logger;

    public ChangeTriggerController(TriggerActionService triggerActionService, Serilog.ILogger logger)
    {
        _triggerActionService = triggerActionService;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> Post()
    {
        try
        {
            _logger.Information($"Entered POST");
            string actionParameter = HttpContext.Request.Query["action"].ToString();
            _logger.Information($"Action: {actionParameter}");

            string requestBody = string.Empty;
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                requestBody = await reader.ReadToEndAsync();
            }
            //_logger.Information($"Request body: {requestBody}");
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
                _logger.Information($"CinnamonRequest: {cinnamonRequestHeader}");
                if (cinnamonRequestHeader.ToString().Trim().Length>0)
                {
                    try
                    {
                        requestToCinnamon = new XmlDocument();
                        requestToCinnamon.LoadXml(cinnamonRequestHeader);
                    }
                    catch (XmlException ex)
                    {
                        _logger.Error(ex, "Invalid XML in cinnamon-request header.");
                        return BadRequest("Invalid XML in cinnamon-request header.");
                    }
                }
            }

            _logger.Information($"Ticket: {ticketHeader}");
            if(requestToCinnamon==null) _logger.Information($"CinnamonRequest: missing");
            //else _logger.Information($"Request to Cinnamon: {requestToCinnamon.OuterXml}");

            XmlDocument resp = null;
            if(actionParameter=="nop")
            {
                // actionParameter passed by the change trigger config in Cinnamon is "nop". Then, trigger_request has attribute "type" - NopAction is chosen by that type parameter
                string reqType = requestToCinnamon.DocumentElement.GetAttribute("type");
                try
                {
                    resp = await _triggerActionService.GetNopAction(reqType, _logger).ExecuteAsync(ticketHeader, requestData, requestToCinnamon, Request.Headers);
                }
                catch (SessionExpiredException ex)
                {
                    _logger.Error(ex, "Session expired - trying to reconnect.");
                    _triggerActionService.ReconnectServiceSession();
                    resp = await _triggerActionService.GetNopAction(reqType, _logger).ExecuteAsync(ticketHeader, requestData, requestToCinnamon, Request.Headers);
                }
            }
            else
            {
                // actionParameter passed by the change trigger config in Cinnamon is not "nop", so the trigger request is some standard Cinnamon API structure - Action is chosen by the action parameter of the URL in the change trigger config
                try
                {
                    resp = await _triggerActionService.GetAction(actionParameter, _logger).ExecuteAsync(ticketHeader, requestData, requestToCinnamon, Request.Headers);
                }
                catch(SessionExpiredException ex)
                {
                    _logger.Error(ex, "Session expired - trying to reconnect.");
                    _triggerActionService.ReconnectServiceSession();
                    resp = await _triggerActionService.GetAction(actionParameter, _logger).ExecuteAsync(ticketHeader, requestData, requestToCinnamon, Request.Headers);
                }
            }


            // TODO: evaluate response for success
            _logger.Information($"Trigger action response: {resp.OuterXml}");

            _logger.Information("Successfully processed request.");
            return Ok(resp.OuterXml);

        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Error processing request.");
            return BadRequest();
        }
    }
}
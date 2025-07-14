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
using ChangeTriggerLib.Services;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Serilog;
using System.Net;
using System.Reflection;
using System.Xml;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.Limits.MaxRequestHeadersTotalSize = 1048576; // Increase to 1 MB
});

XmlDocument config = new XmlDocument();
string assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
config.Load(Path.Combine(assemblyPath, "ct.config.xml"));
string logFn = config.DocumentElement.SelectSingleNode("logfile").InnerText;
if(!Directory.Exists(Path.GetDirectoryName(logFn))) Directory.CreateDirectory(Path.GetDirectoryName(logFn));

// Configure Serilog early
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.File(Path.Combine(Path.GetTempPath(), logFn), rollingInterval: RollingInterval.Day)
    .CreateLogger();

// Register Serilog
builder.Host.UseSerilog(Log.Logger);

Log.Information("Application starting up");

string pfxFile = string.IsNullOrEmpty(builder.Configuration["Https:Pfx"]) ? null : Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), builder.Configuration["Https:Pfx"]); 
string pfxSecret = (builder.Configuration["Https:Secret"].Length == 0 ? null : builder.Configuration["Https:Secret"]);
int httpsPort = (builder.Configuration["Https:Port"].Length == 0 ? 8080 : int.Parse(builder.Configuration["Https:Port"]));

// TODO: if statement to execute this only if the config contains such data
if (!string.IsNullOrEmpty(pfxFile) && !string.IsNullOrEmpty(pfxSecret))
{
    builder.WebHost.ConfigureKestrel(serverOptions =>
    {
        serverOptions.Listen(IPAddress.Any, httpsPort, listenOptions =>
        {
            listenOptions.UseHttps(pfxFile, pfxSecret);
        });
    });
}

// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddSerilog();

builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.Limits.MaxRequestBodySize = null;
}); 
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var triggerActionService = new TriggerActionService(config, Log.Logger); // Use Serilog directly here
triggerActionService.InitCustomServices();
triggerActionService.InitFactories();

builder.Services.AddSingleton(triggerActionService);

// Add other custom singleton services here
builder.Services.AddHttpClient("ShortTimeoutClient", client =>
{
    client.Timeout = TimeSpan.FromSeconds(30);
});

builder.Services.AddHttpClient("LongTimeoutClient", client =>
{
    client.Timeout = TimeSpan.FromHours(1);
}); 

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
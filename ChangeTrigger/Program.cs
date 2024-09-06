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
using Serilog;
using System.Net;
using System.Reflection;
using System.Xml;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Configure Serilog early
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .CreateLogger();

// Register Serilog
builder.Host.UseSerilog(Log.Logger);

Log.Information("Application starting up");

string pfxFile = (builder.Configuration["Https:Pfx"].Length == 0 ? null : Path.Combine(Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath), builder.Configuration["Https:Pfx"]));
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

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

XmlDocument config = new XmlDocument();
string assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
config.Load(Path.Combine(assemblyPath, "ct.config.xml"));

builder.Services.AddSingleton<TriggerActionService>(serviceProvider =>
{
    var logger = serviceProvider.GetRequiredService<ILogger<TriggerActionService>>();
    var triggerActionService = new TriggerActionService(config, logger);
    triggerActionService.InitCustomServices();
    triggerActionService.InitFactories();
    return triggerActionService;
});

// Add other custom singleton services here

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
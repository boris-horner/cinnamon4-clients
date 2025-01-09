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
using C4ServerConnector;
using ChangeTriggerLib.Interfaces;
using System.Reflection;
using System.Xml;
using Serilog;

namespace ChangeTriggerLib.Services
{
    public class TriggerActionService
    {
        private Dictionary<string, ITriggerActionFactory> _factories;
        private Dictionary<string, ITriggerActionFactory> _nopFactories;
        private Dictionary<string, ICustomService> _customServices;
        private XmlDocument _config;
        public ILogger Logger;
        public C4Session ServiceSession { get; private set; }
        public C4SessionConfiguration SessionConfiguration { get; private set; }

        public TriggerActionService(XmlDocument config, ILogger logger)
        {
            try
            {
                _config = config;
                Logger = logger;
                XmlNode localCertFileN = _config.DocumentElement.SelectSingleNode("server/local_cert_file");
                XmlNode localTempPathN = _config.DocumentElement.SelectSingleNode("server/local_temp_path");
                string username = _config.DocumentElement.SelectSingleNode("server/username").InnerText;
                ServiceSession = new C4Session(_config.DocumentElement.SelectSingleNode("server/url").InnerText,
                                              username,
                                              _config.DocumentElement.SelectSingleNode("server/password").InnerText,
                                              null,
                                              localCertFileN == null ? null : localCertFileN.InnerText,
                                              false,
                                              localTempPathN == null ? null : localTempPathN.InnerText);
                SessionConfiguration = new C4SessionConfiguration(ServiceSession, username, ServiceSession.LocalTempPath, null, null);

            }
            catch(Exception ex)
            {
                Logger.Error(string.Join("\n",ex.GetType().ToString(), ex.Message, ex.StackTrace));
                throw ex;
            }


        }
        public void InitCustomServices()
        {
            Logger.Information("InitCustomServices");
            _customServices = new Dictionary<string, ICustomService>();
            foreach (XmlElement sngServiceEl in _config.DocumentElement.SelectNodes("singleton_services/singleton_service"))
            {
                string name = sngServiceEl.GetAttribute("name");
                string assembly = sngServiceEl.GetAttribute("assembly");
                string type = sngServiceEl.GetAttribute("type");
                Assembly asm = Assembly.LoadFrom(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), assembly + ".dll"));
                Logger.Information("Loading: " + Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), assembly + ".dll"));
                Logger.Information("Assembly is " + (asm == null ? "null" : "not null"));
                ICustomService? customService = (ICustomService)asm.CreateInstance(assembly + "." + type);
                Logger.Information("Custom service is " + (customService == null ? "null" : "not null"));
                customService.Init(sngServiceEl);
                _customServices.Add(name, customService);
                Logger.Information("Added: " + name);
            }

        }
        public void InitFactories()
        {
            _factories = new Dictionary<string, ITriggerActionFactory>();
            _nopFactories = new Dictionary<string, ITriggerActionFactory>();

            Logger.Information("InitFactories");
            foreach(XmlElement triggerActionFactoryEl in _config.DocumentElement.SelectNodes("trigger_action_factories/trigger_action_factory"))
            {
                string name = triggerActionFactoryEl.GetAttribute("name");
                string assembly = triggerActionFactoryEl.GetAttribute("assembly");
                string type = triggerActionFactoryEl.GetAttribute("type");
                bool nopFactory = triggerActionFactoryEl.HasAttribute("mode") && triggerActionFactoryEl.GetAttribute("mode") == "nop";
                Assembly asm = Assembly.LoadFrom(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), assembly + ".dll"));
                Logger.Information("Loading: "+ Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), assembly + ".dll"));
                Logger.Information("Assembly is "+(asm==null?"null":"not null"));
                ITriggerActionFactory? factory = (ITriggerActionFactory)asm.CreateInstance(assembly + "." + type);
                Logger.Information("Factory is " + (factory == null ? "null" : "not null"));
                factory.Init(triggerActionFactoryEl, this);
                if(nopFactory) _nopFactories.Add(name, factory);
                else _factories.Add(name, factory);
                Logger.Information("Added: "+name);
            }

        }

        public ITriggerAction GetAction(string command, ILogger logger)
        {
            Logger.Information($"Getting action for command: {command}");
            if (_factories.TryGetValue(command, out ITriggerActionFactory? factory))
            {
                return factory.CreateAction(logger);
            }

            throw new ArgumentException($"No action found for command: {command}", nameof(command));
        }
        public ITriggerAction GetNopAction(string command, ILogger logger)
        {
            Logger.Information($"Getting action for command: {command}");
            if (_nopFactories.TryGetValue(command, out ITriggerActionFactory? factory))
            {
                return factory.CreateAction(logger);
            }

            throw new ArgumentException($"No action found for command: {command}", nameof(command));
        }

        public ICustomService GetCustomService(string name)
        {
            if (_customServices.ContainsKey(name)) return _customServices[name];

            throw new ArgumentException($"No custom service found for name: {name}", nameof(name));
        }

        public void ReconnectServiceSession()
        {
            XmlNode localCertFileN = _config.DocumentElement.SelectSingleNode("server/local_cert_file");
            XmlNode localTempPathN = _config.DocumentElement.SelectSingleNode("server/local_temp_path");
            string username = _config.DocumentElement.SelectSingleNode("server/username").InnerText;
            ServiceSession = new C4Session(_config.DocumentElement.SelectSingleNode("server/url").InnerText,
                                          username,
                                          _config.DocumentElement.SelectSingleNode("server/password").InnerText,
                                          null,
                                          localCertFileN == null ? null : localCertFileN.InnerText,
                                          false,
                                          localTempPathN == null ? null : localTempPathN.InnerText);
            SessionConfiguration = new C4SessionConfiguration(ServiceSession, username, ServiceSession.LocalTempPath, null, null);
        }
    }
}

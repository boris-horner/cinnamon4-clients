﻿// Copyright 2012,2024 texolution GmbH
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
using CDCplusLib.Interfaces;
using System.Xml;
using C4ObjectApi.Interfaces;
using C4ObjectApi.Repository;

namespace CDCplusLib.Common
{
    public class PreChangeTriggerActorsCollection : ISessionExtension
    {
        public Dictionary<string,IPreChangeTriggerActor> ActorByCommand { get; private set; }
        public Dictionary<string, IPreChangeTriggerActor> ActorByType { get; private set; }
        public void Init(CmnSession s, XmlElement configEl)
        {
            System.Diagnostics.Debug.Print("Init");
            ActorByCommand = new Dictionary<string, IPreChangeTriggerActor>();
            ActorByType = new Dictionary<string, IPreChangeTriggerActor>();
            foreach (XmlElement actorEl in configEl.SelectNodes("actor"))
            {
                string asm = actorEl.GetAttribute("assembly");
                string tp = actorEl.GetAttribute("type");
                string key = asm + "." + tp;
                IPreChangeTriggerActor pcta = (ActorByType.ContainsKey(key))?ActorByType[key]:(IPreChangeTriggerActor)s.SessionConfig.GetAssembly(asm).CreateInstance(key);
                pcta.Init(s, actorEl);
                foreach(string cmd in pcta.ValidCommands)
                {
                    if (!ActorByCommand.ContainsKey(cmd)) ActorByCommand.Add(cmd, pcta);
                }
            }
        }


    }
}
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
using System.Collections.Generic;
using System.IO;

namespace C4ObjectApi.Global
{
    public class LocksManager
    {
        private string _locksDir;
        public Dictionary<long, Lock> Locks;
        public string ServerId { get; }
        public LocksManager(string locksDir, string serverId)
        {
            _locksDir = locksDir;
            if(!Directory.Exists(_locksDir)) Directory.CreateDirectory(_locksDir);
            Locks=new Dictionary<long, Lock>();
            ServerId = serverId;
            foreach (string fn in Directory.GetFiles(_locksDir, string.Concat(ServerId, ".*.xml")))
            {
                string fnOnly = Path.GetFileNameWithoutExtension(fn);
                long id;
                string idString = fnOnly.Split('.')[1];
                if(long.TryParse(idString, out id))
                {
                    Locks.Add(id, new Lock(id, _locksDir, ServerId));
                }
            }
        }
        public void RemoveLock(long lockId)
        {
            if(Locks.ContainsKey(lockId)) File.Delete(Locks[lockId].Filename);
            Locks.Remove(lockId);
        }
        public void AddLock(long id, string contentFilename, Dictionary<long, string> childIdsToFn)
        {
            if (Locks.ContainsKey(id)) RemoveLock(id);
            Lock l = new Lock(id, contentFilename, _locksDir, ServerId);
            foreach (long childId in childIdsToFn.Keys)
            {
                if(id!=childId)
                {
                    l.DependenciesById.Add(childId, childIdsToFn[childId]);
                    l.DependenciesByFilename.Add(childIdsToFn[childId], childId);
                }
            }
            Locks.Add(l.Id, l);
            l.Save();
        }
    }
}

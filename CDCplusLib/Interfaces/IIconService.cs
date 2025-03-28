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
using C4ObjectApi.Interfaces;
using C4ObjectApi.Repository;
using C4ServerConnector;

namespace CDCplusLib.Interfaces
{
    public interface IIconService
    {
        void Init(CmnSession s, System.Xml.XmlElement configEl);
        string GetIconKey(IRepositoryNode ow);
        string GetIconKey(IC4Node c4n);
        ImageList GlobalSmallImageList { get; }
        ImageList GlobalLargeImageList { get; }
    }
}

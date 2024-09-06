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

namespace C4ObjectApi.Repository
{
    public class VersionComparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            string[] xSegs = x.Split('.');
            string[] ySegs = y.Split('.');

            int i = 0;
            while (true)
            {
                if (xSegs.Length > i && ySegs.Length > i)
                {
                    // both next segments exist (and all previous ones are identical)
                    // compare the segment - if identical, continue - else, return the compare result
                    if (xSegs[i] != ySegs[i])
                    {
                        if (i == 0)
                        {
                            // first segment - no dash
                            int xInt = int.Parse(xSegs[i]);
                            int yInt = int.Parse(ySegs[i]);
                            return xInt - yInt;
                        }
                        else
                        {
                            // both segments contain dashes - split and compare the values numerically
                            string[] xSegSegs = xSegs[i].Split('-');
                            string[] ySegSegs = ySegs[i].Split('-');

                            // first segment
                            int xInt = int.Parse(xSegSegs[0]);
                            int yInt = int.Parse(ySegSegs[0]);
                            if (xInt != yInt) return xInt - yInt;

                            // second segment
                            xInt = int.Parse(xSegSegs[1]);
                            yInt = int.Parse(ySegSegs[1]);
                            if (xInt != yInt) return xInt - yInt;
                        }
                    }
                }
                else if (xSegs.Length > i)
                {
                    // only left segment exists (and all previous ones are identical)
                    return 1;
                }
                else if (ySegs.Length > i)
                {
                    // only right segment exists (and all previous ones are identical)
                    return -1;
                }
                else
                {
                    // none of the segments exist (and all previous ones are identical) - this shouldn't happen if versions of the same object are compared
                    return 0;
                }
                ++i;
            }
        }
    }
}

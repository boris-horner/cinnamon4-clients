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
using System.Xml;
using CDCplusLib.Interfaces;

namespace CDCplusLib.TabControls.SearchEditorNodes
{
    public partial class SearchOperatorGui : UserControl
    {
        public enum OperatorTypes { And, Or };
        public OperatorTypes OperatorType;

        public event EventHandler ControlChanged;
        public SearchOperatorGui()
        {
            InitializeComponent();
            OperatorType = OperatorTypes.And;
            optOpTypeAnd.Checked = true;
            optOpTypeOr.Checked = false;
        }
        public SearchOperatorGui(XmlElement operatorEl)
        {
            InitializeComponent();
            OperatorType = operatorEl.GetAttribute("type") == "and" ? OperatorTypes.And : OperatorTypes.Or;
            optOpTypeAnd.Checked = OperatorType == OperatorTypes.And;
            optOpTypeOr.Checked = !optOpTypeAnd.Checked;
        }

        private void optOpTypeAnd_CheckedChanged(object sender, EventArgs e)
        {
            OperatorType = optOpTypeAnd.Checked ? OperatorTypes.And : OperatorTypes.Or;
            ControlChanged?.Invoke(this, new EventArgs());
        }

        public void AppendToQuery(XmlElement parentClauseEl, TreeNode operatorNode)
        {
            if(OperatorType==OperatorTypes.And)
            {
                // all children must be true
                // negation: none of the children must be true
                XmlElement bqEl = (XmlElement)parentClauseEl.AppendChild(parentClauseEl.OwnerDocument.CreateElement("BooleanQuery"));
                foreach(TreeNode childNode in operatorNode.Nodes)
                {
                    XmlElement clEl = (XmlElement)bqEl.AppendChild(parentClauseEl.OwnerDocument.CreateElement("Clause"));
                    clEl.SetAttribute("occurs", "must");
                    object tag = childNode.Tag;
                    if (tag.GetType() == typeof(SearchOperatorGui))
                    {
                        ((SearchOperatorGui)tag).AppendToQuery(clEl, childNode);
                    }
                    else // term
                    {
                        ((ISearchTermGui)tag).AppendToQuery(clEl);
                    }
                }
            }
            else // or
            {
                // at least one child must be true
                XmlElement bqEl = (XmlElement)parentClauseEl.AppendChild(parentClauseEl.OwnerDocument.CreateElement("BooleanQuery"));
                bqEl.SetAttribute("minimumNumberShouldMatch", "1");
                foreach (TreeNode childNode in operatorNode.Nodes)
                {
                    XmlElement clEl = (XmlElement)bqEl.AppendChild(parentClauseEl.OwnerDocument.CreateElement("Clause"));
                    clEl.SetAttribute("occurs", "should");
                    object tag = childNode.Tag;
                    if (tag.GetType() == typeof(SearchOperatorGui))
                    {
                        ((SearchOperatorGui)tag).AppendToQuery(clEl, childNode);
                    }
                    else // term
                    {
                        ((ISearchTermGui)tag).AppendToQuery(clEl);
                    }
                }
            }
        }
    }
}

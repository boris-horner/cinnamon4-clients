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
namespace CDCplusLib.Common.GUI
{
    public partial class EditStringValue : Form
    {
        public EditStringValue(string title, string valueLabel, string value, string regex=null)
        {
            InitializeComponent();
            vtxtValue.RegularExpression = regex;
            vtxtValue.Text = value;
            lblValue.Text = valueLabel;
            Text = title;
            vtxtValue.Select();
        }

        private void vtxtValue_TextChanged(object sender, EventArgs e)
        {
            cmdOk.Enabled = vtxtValue.IsValid;
        }

        private void cmdOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
        public string Value 
        { 
            get
            {
                return vtxtValue.Text;
            } 
        }
    }
}

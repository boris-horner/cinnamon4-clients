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
using CDCplusLib.DataModel;

namespace CDCplusLib.Common.GUI
{
	public partial class EditListValue : Form
	{
		public EditListValue(string title, string valueLabel, string selectedLabel, HashSet<SimpleDisplayItem> values)
		{
			InitializeComponent();
			foreach (SimpleDisplayItem item in values) cboListValues.Items.Add(item);
			if (selectedLabel != null) cboListValues.Text = selectedLabel;
			else cboListValues.SelectedIndex = 0;
			lblValue.Text = valueLabel;
			Text = title;
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

		private void cboListValues_SelectedIndexChanged(object sender, EventArgs e)
		{
			cmdOk.Enabled = cboListValues.SelectedItem != null;
		}

		public SimpleDisplayItem Value
		{
			get
			{
				return (SimpleDisplayItem)cboListValues.SelectedItem;
			}
		}

	}
}

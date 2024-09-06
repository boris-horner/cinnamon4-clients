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
    public partial class AddRemoveList : UserControl
    {
        public event ListChangedEventHandler ListChanged;

        public delegate void ListChangedEventHandler();
        public AddRemoveList()
        {
            InitializeComponent();
        }
        public void Init(IList<object> notAssignedItems, IList<object> assignedItems)
        {
            NotAssigned = notAssignedItems;
            Assigned = assignedItems;
        }
        public IList<object> Assigned
        {
            get
            {
                IList<object> result = new List<object>();
                foreach (object it in lbAssigned.Items)
                    result.Add(it);
                return result;
            }
            set
            {
                lbAssigned.Items.Clear();
                foreach (object it in value)
                    lbAssigned.Items.Add(it);
                ActivateControls();
            }
        }
        public IList<object> NotAssigned
        {
            get
            {
                IList<object> result = new List<object>();
                foreach (object it in lbNotAssigned.Items)
                    result.Add(it);
                return result;
            }
            set
            {
                lbNotAssigned.Items.Clear();
                foreach (object it in value)
                    lbNotAssigned.Items.Add(it);
                ActivateControls();
            }
        }
        private void ActivateControls()
        {
            cmdRemove.Enabled = lbAssigned.SelectedItems.Count > 0;
            cmdAdd.Enabled = lbNotAssigned.SelectedItems.Count > 0;
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            AssignItems();
        }

        private void cmdRemove_Click(object sender, EventArgs e)
        {
            UnassignItems();
        }
        private void AssignItems()
        {
            IList<object> del = new List<object>();
            foreach (object it in lbNotAssigned.SelectedItems)
            {
                lbAssigned.Items.Add(it);
                del.Add(it);
            }
            foreach (object it in del)
                lbNotAssigned.Items.Remove(it);
            ActivateControls();
            ListChanged?.Invoke();
        }
        private void UnassignItems()
        {
            IList<object> del = new List<object>();
            foreach (object it in lbAssigned.SelectedItems)
            {
                lbNotAssigned.Items.Add(it);
                del.Add(it);
            }
            foreach (object it in del)
                lbAssigned.Items.Remove(it);
            ActivateControls();
            ListChanged?.Invoke();
        }
        public void AssignAllItems()
        {
            IList<object> del = new List<object>();
            foreach (object it in lbNotAssigned.Items)
            {
                lbAssigned.Items.Add(it);
                del.Add(it);
            }
            foreach (object it in del)
                lbNotAssigned.Items.Remove(it);
            ActivateControls();
            ListChanged?.Invoke();
        }
        public void UnassignAllItems()
        {
            IList<object> del = new List<object>();
            foreach (object it in lbAssigned.Items)
            {
                lbNotAssigned.Items.Add(it);
                del.Add(it);
            }
            foreach (object it in del)
                lbAssigned.Items.Remove(it);
            ActivateControls();
            ListChanged?.Invoke();
        }
        public void AssignItem(string text)
        {
            foreach (object it in lbNotAssigned.Items)
            {
                if ((it.ToString() ?? "") == (text ?? ""))
                {
                    lbAssigned.Items.Add(it);
                    lbNotAssigned.Items.Remove(it);
                    break;
                }
            }
            ActivateControls();
            ListChanged?.Invoke();
        }
        public void UnassignItem(string text)
        {
            foreach (object it in lbAssigned.Items)
            {
                if ((it.ToString() ?? "") == (text ?? ""))
                {
                    lbNotAssigned.Items.Add(it);
                    lbAssigned.Items.Remove(it);
                    break;
                }
            }
            ActivateControls();
            ListChanged?.Invoke();
        }

        private void lbAssigned_DoubleClick(object sender, EventArgs e)
        {
            UnassignItems();
        }
        private void lbAssigned_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActivateControls();
        }

        private void lbNotAssigned_DoubleClick(object sender, EventArgs e)
        {
            AssignItems();
        }

        private void lbNotAssigned_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActivateControls();
        }
    }
}

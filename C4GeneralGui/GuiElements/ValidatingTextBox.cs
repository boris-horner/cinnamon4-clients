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
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace C4GeneralGui.GuiElements
{
    public partial class ValidatingTextBox : TextBox
    {
        public string _regularExpression;
        public Color _validColor;
        public Color _validReadOnlyColor;
        public Color _invalidReadOnlyColor;
        public Color _internalInvalidColor;
        public Color _externalInvalidColor;
        public Color _bothInvalidColor;
        public bool? _externalValidation;
        public string RegularExpression { get { return _regularExpression; } set { _regularExpression = value; GuiActions(); } }   // null: no internal validation
        public Color ValidColor { get { return _validColor; } set { _validColor = value; GuiActions(); } }
        public Color ValidReadOnlyColor { get { return _validReadOnlyColor; } set { _validReadOnlyColor = value; GuiActions(); } }
        public Color InvalidReadOnlyColor { get { return _invalidReadOnlyColor; } set { _invalidReadOnlyColor = value; GuiActions(); } }
        public Color InternalInvalidColor { get { return _internalInvalidColor; } set { _internalInvalidColor = value; GuiActions(); } }
        public Color ExternalInvalidColor { get { return _externalInvalidColor; } set { _externalInvalidColor = value; GuiActions(); } }
        public Color BothInvalidColor { get { return _bothInvalidColor; } set { _bothInvalidColor = value; GuiActions(); } }
        public bool? ExternalValidation { get { return _externalValidation; } set { _externalValidation = value; GuiActions(); } }   // true: valid, false: invalid, null: no external validation
        public bool IsValid { get; private set; }
        public ValidatingTextBox() : base()
        {
            InitializeComponent();

            this.TextChanged += new System.EventHandler(this.ValidatingTextBox_TextChanged);
            RegularExpression = null;
            ValidColor = Color.White;
            ValidReadOnlyColor = SystemColors.Control;
            InvalidReadOnlyColor = Color.LightYellow;
            BothInvalidColor = Color.Red;
            ExternalInvalidColor = Color.Orange;
            InternalInvalidColor = Color.Yellow;
            ExternalValidation = null;
            GuiActions();
        }

        private void GuiActions()
        {
            bool regexValid = RegularExpression==null || Regex.IsMatch(Text, RegularExpression);
            bool extValid = ExternalValidation == null || ExternalValidation == true;
            IsValid = regexValid && extValid;
            if(ReadOnly) BackColor = (regexValid && extValid) ? ValidReadOnlyColor : InvalidReadOnlyColor;
            else
            {
                if (regexValid) BackColor = extValid ? ValidColor : ExternalInvalidColor;
                else BackColor = extValid ? InternalInvalidColor : BothInvalidColor;
            }
        }

        private void ValidatingTextBox_TextChanged(object sender, EventArgs e)
        {
            GuiActions();
        }
    }
}

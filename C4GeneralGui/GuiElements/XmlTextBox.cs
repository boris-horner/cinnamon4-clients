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
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;

namespace C4GeneralGui.GuiElements
{
    public partial class XmlTextBox : RichTextBox
    {
        private readonly Regex _tagRx;
        private readonly Regex _tagEndRx;
        private readonly Regex _attrRx;
        private readonly Regex _attrValRx;
        private readonly Regex _commentRx;
        private readonly Regex _cdataStartRx;
        private readonly Regex _cdataEndRx;
        private bool _dirty;

        public XmlTextBox()
        {
            TextChanged += XmlTextBox_TextChanged;
            _dirty = false;
            _tagRx = new Regex(@"(</?[a-z]*)\s?>?");
            _tagEndRx = new Regex("(/>)");
            _attrRx = new Regex(@"\s\w+=");
            _attrValRx = new Regex("(\"[^\"]*\")");
            _commentRx = new Regex("(<!--.*-->)");
            _cdataStartRx = new Regex(@"(\<!\[CDATA\[).*");
            _cdataEndRx = new Regex(".*(]]>)");
            // TODO: text inside tags
        }

        public void SetText(string txt)
        {
            Text = txt;
            Indent();
            Highlight();
            _dirty = false;
        }

        public void Indent()
        {
            if (Text.Length > 0)
            {
                try
                {
                    MemoryStream ms = new MemoryStream();
                    XmlTextWriter xtw = new XmlTextWriter(ms, Encoding.Unicode);
                    xtw.Formatting = Formatting.Indented;
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(Text);
                    doc.WriteContentTo(xtw);
                    xtw.Flush();
                    ms.Seek(0L, SeekOrigin.Begin);
                    StreamReader sr = new StreamReader(ms);
                    Text = sr.ReadToEnd();
                }
                catch (Exception ex)
                {
                    // ignore
                }
            }
        }

        public void Highlight()
        {
            if (TextLength < 1000000) // if less then 1 MB (otherwise it might take a while)
            {
                ElementaryHighlight(_cdataStartRx, Color.Gray);
                ElementaryHighlight(_cdataEndRx, Color.Gray);
                ElementaryHighlight(_tagRx, Color.DarkRed);
                ElementaryHighlight(_attrRx, Color.Red);
                ElementaryHighlight(_tagEndRx, Color.DarkRed);
                ElementaryHighlight(_attrValRx, Color.Blue);
                ElementaryHighlight(_commentRx, Color.Green);
            }
            else
            {
                Select(0, TextLength);
                SelectionColor = Color.Black;
            }
        }

        private void ElementaryHighlight(Regex rx, Color c)
        {
            // Dim pos As Integer = 0
            foreach (Match m in rx.Matches(Text))
            {
                Select(m.Index, m.Length);
                SelectionColor = c;
                // Me.SelectionStart = pos

            }
        }

        private void XmlTextBox_TextChanged(object sender, EventArgs e)
        {
            _dirty = true;
        }

        public bool Dirty
        {
            get
            {
                return _dirty;
            }
        }
    }
}
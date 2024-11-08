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
using System.Reflection;
using System.Windows.Forms;
using System.IO;
using CDCplus.Properties;

namespace CDCplus.Windows
{
    partial class About : Form
    {
        public About()
        {
            InitializeComponent();
            //this.Text = String.Format("About {0}", AssemblyTitle);
            System.Version v = Assembly.GetExecutingAssembly().GetName().Version;
            lblVersion.Text = string.Format(Resources.lblVersion, v.Major.ToString() + "." + v.Minor.ToString());
            //lblBuild.Text = string.Format(Resources.lblBuild, v.Build.ToString() + "." + v.Revision.ToString());
            //DateTime dt = GetLastUpdate();
            //lblLastUpdate.Text = string.Format(Resources.lblLastUpdate, dt.ToShortDateString() + " " + dt.ToLongTimeString());
        }

        #region Assembly Attribute Accessors

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion

        private void OKButton_Click(object sender, EventArgs e)
        {
            Close();
        }
        private DateTime GetLastUpdate()
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            string asmPath = Path.GetDirectoryName(new Uri(asm.CodeBase).LocalPath);
            DateTime result = DateTime.MinValue;
            foreach (string fn in Directory.GetFiles(asmPath, "*.exe"))
            {
                DateTime lts = RetrieveLinkerTimestamp(fn);
                if (lts > result) result = lts;
            }
            foreach (string fn in Directory.GetFiles(asmPath, "*.dll"))
            {
                DateTime lts = RetrieveLinkerTimestamp(fn);
                if (lts > result) result = lts;
            }
            return result;
        }
        private static DateTime RetrieveLinkerTimestamp(string filePath)
        {
            const int peHeaderOffset = 60;
            const int linkerTimestampOffset = 8;
            byte[] b = new byte[2048];
            FileStream s = null;
            try
            {
                s = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                s.Read(b, 0, 2048);
            }
            finally
            {
                if (s != null)
                    s.Close();
            }
            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(System.BitConverter.ToInt32(b, System.BitConverter.ToInt32(b, peHeaderOffset) + linkerTimestampOffset));
            return dt.AddHours(TimeZone.CurrentTimeZone.GetUtcOffset(dt).Hours);
        }

        private void About_Paint(object sender, PaintEventArgs e)
        {
           LogoPictureBox.Height = LogoPictureBox.Width * 59 / 198; 
        }
    }
}

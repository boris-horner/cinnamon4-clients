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
using C4GeneralGui.GuiElements;
using System;
using System.Diagnostics;
using System.Net;
using System.Windows.Forms;

namespace CDCplus
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Add global exception handlers
            Application.ThreadException += (sender, args) =>
            {
                // Log or handle thread-specific exceptions
                Debug.WriteLine($"ThreadException: {args.Exception}");
                //StandardMessage.ShowMessage("An unexpected error occurred.", StandardMessage.Severity.ErrorMessage, null, args.Exception);
            };

            AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
            {
                // Log or handle app domain-level unhandled exceptions
                Debug.WriteLine($"UnhandledException: {args.ExceptionObject}");
                // Optional: Show a message or log the error
            };

            ServicePointManager.DefaultConnectionLimit = 20;
            Application.EnableVisualStyles();
            Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
            //Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ServerHub());
        }
    }
}

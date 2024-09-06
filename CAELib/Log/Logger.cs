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
namespace CAELib.Log
{
    public class Logger
    {
        string _fn;
        bool _console;
        public enum Severity { Info, Warning, Error, Fatal }
        public Logger(bool console,string fn=null )
        {
            _fn = fn;
			if (_fn != null && !Directory.Exists(Path.GetDirectoryName(_fn))) Directory.CreateDirectory(Path.GetDirectoryName(_fn));
			_console = console;
        }
        public void Log(string line)
        {
            Log(line, Severity.Info, true, null);
        }

        public void Log(string message, Severity s, bool withTimestamp=true, Exception ex=null)
        {
            string sev = null;
            switch(s)
            {
                case Severity.Info: sev = "[I]"; break;
                case Severity.Warning: sev = "[W]"; break;
                case Severity.Error: sev = "[E]"; break;
                case Severity.Fatal: sev = "[F]"; break;
            }
            string line = withTimestamp ? DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") : "";
            line += sev + " " + message;
            if (ex != null) line += "\nException: " + ex.GetType().ToString() + "\n" + ex.Message + "\n" + ex.StackTrace;
            if (_console) Console.WriteLine(line);

			if (_fn != null) File.AppendAllText(_fn, line + Environment.NewLine);
        }
        public bool ConsoleLogging { get { return _console; } }
        public string Logfile { get { return _fn; } }
    }
}

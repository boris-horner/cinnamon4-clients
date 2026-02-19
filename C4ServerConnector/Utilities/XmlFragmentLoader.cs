using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace C4ServerConnector.Utilities
{
    public static class XmlFragmentLoader
    {
        public static void LoadXmlFromInnerText(XmlDocument targetDoc, string innerText)
        {
            // Wrap the string without allocating a truncated copy
            using var tr = new SkippingStringReader(innerText);

            var settings = new XmlReaderSettings
            {
                ConformanceLevel = ConformanceLevel.Fragment,
                DtdProcessing = DtdProcessing.Prohibit
            };

            using var xr = XmlReader.Create(tr, settings);
            xr.MoveToContent();
            targetDoc.Load(xr);
        }

        /// <summary>
        /// TextReader over an existing string, but skips a leading XML declaration
        /// (including invalid version="1.1") and leading whitespace/BOM.
        /// No substring allocation.
        /// </summary>
        private sealed class SkippingStringReader : TextReader
        {
            private readonly string _s;
            private int _pos;
            private readonly int _len;

            public SkippingStringReader(string s)
            {
                _s = s ?? "";
                _len = _s.Length;
                _pos = 0;

                SkipBomAndWhitespace();
                SkipLeadingXmlDeclarationIfPresent();
                SkipBomAndWhitespace();
            }

            public override int Peek() => _pos < _len ? _s[_pos] : -1;

            public override int Read() => _pos < _len ? _s[_pos++] : -1;

            public override int Read(char[] buffer, int index, int count)
            {
                if (buffer == null) throw new ArgumentNullException(nameof(buffer));
                if (index < 0 || count < 0 || index + count > buffer.Length) throw new ArgumentOutOfRangeException();

                int remaining = _len - _pos;
                if (remaining <= 0) return 0;

                int toCopy = remaining < count ? remaining : count;
                _s.CopyTo(_pos, buffer, index, toCopy);
                _pos += toCopy;
                return toCopy;
            }

            private void SkipBomAndWhitespace()
            {
                // BOM (U+FEFF) can appear at the beginning of a string
                while (_pos < _len)
                {
                    char c = _s[_pos];
                    if (c == '\uFEFF' || char.IsWhiteSpace(c)) _pos++;
                    else break;
                }
            }

            private void SkipLeadingXmlDeclarationIfPresent()
            {
                // Must be at the very beginning (after whitespace/BOM).
                // Recognize "<?xml" (case sensitive per spec).
                if (!StartsWith("<?xml")) return;

                int end = _s.IndexOf("?>", _pos, StringComparison.Ordinal);
                if (end < 0) return; // malformed; let XML reader fail later

                _pos = end + 2; // skip past "?>"
            }

            private bool StartsWith(string token)
            {
                if (_pos + token.Length > _len) return false;
                for (int i = 0; i < token.Length; i++)
                    if (_s[_pos + i] != token[i]) return false;
                return true;
            }
        }
    }
}

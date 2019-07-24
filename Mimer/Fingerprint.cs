using System;
using System.Collections.Generic;
using System.Globalization;

namespace Mimer
{
    // TODO: [snowinmars] add offset for header and footer
    internal sealed class Fingerprint
    {
        public Fingerprint(string header, string footer, string stringMimeType, DetectedMimeType mimeType)
        {
            MimeType = mimeType;
            StringMimeType = stringMimeType;

            Header = ParseArray(header);
            Footer = ParseArray(footer);
        }

        private static byte[] ParseArray(string header)
        {
            var list = new List<byte>();
            var stringValue = header.Replace(" ", string.Empty).Replace("_", string.Empty);

            if (stringValue.Length % 2 != 0)
            {
                throw new
                    Exception($"Array length should be divided by 2, but '{stringValue}' (length: {stringValue.Length}) was provided");
            }

            for (var i = 0; i < stringValue.Length; i += 2)
            {
                var stringByte = $"{stringValue[i]}{stringValue[i + 1]}";

                if (!byte.TryParse(stringByte, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var @byte))
                {
                    throw new Exception($"Can't parse byte value from {stringByte}");
                }

                list.Add(@byte);
            }

            return list.ToArray();
        }

        public byte[] Header { get; }

        public byte[] Footer { get; }

        public DetectedMimeType MimeType { get; }

        public string StringMimeType { get; }
    }
}
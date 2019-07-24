using System;
using System.IO;
using System.Linq;

namespace Mimer
{
    // ReSharper disable once AllowPublicClass
    public static class MimeDetector
    {
        public static DetectedMimeType Detect(string filepath)
        {
            if (Directory.Exists(filepath))
            {
                throw new ArgumentException("File path was expected but a directory path was provided");
            }

            if (!File.Exists(filepath))
            {
                throw new FileNotFoundException();
            }

            using (var stream = File.OpenRead(filepath))
            {
                const long count = 256;

                var header = new byte[count];
                var bottom = new byte[count];

                stream.Read(header, 0, header.Length);
                stream.Seek(count, SeekOrigin.End);
                stream.Read(bottom, 0, bottom.Length);

                foreach (var fingerprint in MimeRepository.Fingerprints)
                {
                    var currentHeader = header.Take(fingerprint.Header.Length);
                    var currentFooter = bottom.Take(fingerprint.Footer.Length);

                    if (fingerprint.Header.SequenceEqual(currentHeader) &&
                        fingerprint.Footer.SequenceEqual(currentFooter))
                    {
                        return fingerprint.MimeType;
                    }
                }

                return default;
            }
        }

        public static string MimeToString(this DetectedMimeType mime)
        {
            if (!Enum.IsDefined(typeof(DetectedMimeType), mime))
            {
                throw new ArgumentOutOfRangeException(nameof(mime), mime, "Enum value is out of its range");
            }

            return MimeRepository.Fingerprints.First(x => x.MimeType == mime).StringMimeType;
        }
    }
}
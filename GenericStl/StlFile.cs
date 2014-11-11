using System;
using System.IO;
using System.Text;

namespace GenericStl
{
    public static class StlFile
    {
        public static bool IsBinaryFile(string filename)
        {
            if (!File.Exists(filename))
            {
                throw new FileNotFoundException("filename could not be found.", filename);
            }

            using (var fs = File.OpenRead(filename))
            {
                return IsBinary(fs);
            }
        }

        public static bool IsBinary(Stream stream)
        {
            try
            {
                using(var r = new BinaryReader(stream, Encoding.UTF8, true))
                {
                    var firstChars = new string(r.ReadChars(5));

                    if (!string.Equals(firstChars, "solid", StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }

                    var numberOfCharsToReadAtEnd = "endsolid".Length + 300;
                    numberOfCharsToReadAtEnd = numberOfCharsToReadAtEnd > stream.Length ? (int)stream.Length : numberOfCharsToReadAtEnd;

                    stream.Seek(-numberOfCharsToReadAtEnd , SeekOrigin.End);

                    var lastChars = new string(r.ReadChars(numberOfCharsToReadAtEnd));

                    return !lastChars.Contains("endsolid");
                }
            }
            finally
            {
                stream.Seek(0, SeekOrigin.Begin);
            }
        }
    }
}
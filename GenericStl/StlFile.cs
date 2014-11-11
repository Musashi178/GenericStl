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
                using (var r = new StreamReader(stream, Encoding.UTF8, true, 1024, true))
                {
                    var buf = new char[20];
                    r.ReadBlock(buf, 0, 20);
                    var start = new string(buf);
                    return !start.TrimStart().StartsWith("solid", StringComparison.OrdinalIgnoreCase);
                }
            }
            finally
            {
                stream.Seek(0, SeekOrigin.Begin);
            }
        }
    }
}
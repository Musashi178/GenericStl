using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GenericStl
{
    public class StlReader<TTriangle, TVertex, TNormal> : StlReaderBase<TTriangle, TVertex, TNormal>
    {
        public StlReader(Func<TVertex, TVertex, TVertex, TNormal, TTriangle> createTriangle, Func<float, float, float, TVertex> createVertex, Func<float, float, float, TNormal> createNormal) : base(createTriangle, createVertex, createNormal)
        {
        }

        public override IEnumerable<TTriangle> ReadFromFile(string fileName)
        {
            using (var fs = File.OpenRead(fileName))
            {
                foreach (var triangle in ReadFromStream(fs))
                {
                    yield return triangle;
                }
            }
        }

        public override IEnumerable<TTriangle> ReadFromStream(Stream s)
        {
            if (IsBinaryStl(s))
            {
                return new BinaryStlReader<TTriangle, TVertex, TNormal>(CreateTriangle, CreateVertex, CreateNormal).ReadFromStream(s);
            }

            return new AsciiStlReader<TTriangle, TVertex, TNormal>(CreateTriangle, CreateVertex, CreateNormal).ReadFromStream(s);
        }

        public static bool IsBinaryStl(Stream stream)
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
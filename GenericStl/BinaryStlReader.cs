using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace GenericStl
{
    public class BinaryStlReader<TTriangle, TVertex, TNormal> : StlReaderBase<TTriangle, TVertex, TNormal>
    {
        public BinaryStlReader(Func<TVertex, TVertex, TVertex, TNormal, TTriangle> createTriangle, Func<float, float, float, TVertex> createVertex, Func<float, float, float, TNormal> createNormal) : base(createTriangle, createVertex, createNormal)
        {

        }

        public override IEnumerable<TTriangle> ReadFromFile(string fileName)
        {
            using (var fs = File.OpenRead(fileName))
            {
                foreach (var triangle in ReadFromStream(fs)) yield return triangle;
            }
        }

        public override IEnumerable<TTriangle> ReadFromStream(Stream s)
        {
            if (s == null)
            {
                throw new ArgumentNullException("s");
            }

            using (var reader = new BinaryReader(s, Encoding.UTF8, true))
            {
                reader.ReadBytes(80); //header

                var numTriangles = reader.ReadInt32();

                for (var i = 0; i < numTriangles; ++i)
                {
                    yield return ReadTriangle(reader);
                }

                Debug.Assert(s.Position == s.Length);
            }
        }


        private TTriangle ReadTriangle(BinaryReader reader)
        {
            Debug.Assert(reader != null);

            var n = ReadNormal(reader);
            var v1 = ReadVertex(reader);
            var v2 = ReadVertex(reader);
            var v3 = ReadVertex(reader);
            reader.ReadBytes(2); // attribute byte count

            return CreateTriangle(v1, v2, v3, n);
        }

        private TVertex ReadVertex(BinaryReader reader)
        {
            return CreateVertex(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
        }

        private TNormal ReadNormal(BinaryReader reader)
        {
            return CreateNormal(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
        }

        public IEnumerable<TTriangle> Read(byte[] data)
        {
            using (var s = new MemoryStream(data, false))
            {
                foreach (var triangle in ReadFromStream(s)) yield return triangle;
            }    
        }
    }
}

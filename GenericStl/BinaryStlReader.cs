using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace GenericStl
{
    public class BinaryStlReader<TTriangle, TVector, TVertex> : StlReaderBase<TTriangle, TVector, TVertex>
    {
        public BinaryStlReader(Func<TVertex, TVertex, TVertex, TVector, TTriangle> createTriangle, Func<float, float, float, TVertex> createVertex, Func<float, float, float, TVector> createNormal) : base(createTriangle, createVertex, createNormal)
        {

        }

        public override IEnumerable<TTriangle> ReadFile(string fileName)
        {
            using (var fs = File.OpenRead(fileName))
            {
                foreach (var triangle in Read(fs)) yield return triangle;
            }
        }

        private IEnumerable<TTriangle> Read(Stream s)
        {
            using (var reader = new BinaryReader(s))
            {
                reader.ReadBytes(80); //header

                var numTriangles = reader.ReadInt32();

                for (int i = 0; i < numTriangles; ++i)
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

        private TVector ReadNormal(BinaryReader reader)
        {
            return CreateNormal(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
        }

        public IEnumerable<TTriangle> Read(byte[] data)
        {
            using (var s = new MemoryStream(data, false))
            {
                foreach (var triangle in Read(s)) yield return triangle;
            }    
        }
    }
}

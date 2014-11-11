using System;
using System.Collections.Generic;
using System.IO;

namespace GenericStl
{
    public class StlReader<TTriangle, TVertex, TNormal> : StlReaderBase<TTriangle, TVertex, TNormal>
    {
        public StlReader(Func<TVertex, TVertex, TVertex, TNormal, TTriangle> createTriangle, Func<float, float, float, TVertex> createVertex, Func<float, float, float, TNormal> createNormal) : base(createTriangle, createVertex, createNormal)
        {
        }

        public StlReader(IDataStructureCreator<TTriangle, TVertex, TNormal> dataStructureCreator) : base(dataStructureCreator)
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
            if (s == null)
            {
                throw new ArgumentNullException("s", "Stream must not be null.");
            }

            var reader = GetReaderFor(s);
            return reader.ReadFromStream(s);
        }

        private IStlReader<TTriangle> GetReaderFor(Stream s)
        {
            if (StlFile.IsBinary(s))
            {
                return new BinaryStlReader<TTriangle, TVertex, TNormal>(CreateTriangle, CreateVertex, CreateNormal);
            }

            return new AsciiStlReader<TTriangle, TVertex, TNormal>(CreateTriangle, CreateVertex, CreateNormal);
        }
    }
}
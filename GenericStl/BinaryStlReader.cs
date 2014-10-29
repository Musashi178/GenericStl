using System;
using System.Collections.Generic;

namespace GenericStl
{
    public class BinaryStlReader<TTriangle, TVector, TVertex> : StlReaderBase<TTriangle, TVector, TVertex>
    {
        public BinaryStlReader(Func<TVertex, TVertex, TVertex, TVector, TTriangle> createTriangle, Func<float, float, float, TVertex> createVertex, Func<float, float, float, TVector> createNormal) : base(createTriangle, createVertex, createNormal)
        {

        }

        public override IEnumerable<TTriangle> ReadFile(string fileName)
        {
            return null;
        }
    }
}

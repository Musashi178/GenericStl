using System;
using System.Collections.Generic;

namespace GenericStl
{
    public abstract class StlReaderBase<TTriangle, TVector, TVertex> : IStlReader<TTriangle>
    {
        protected readonly Func<float, float, float, TVector> CreateNormal;
        protected readonly Func<TVertex, TVertex, TVertex, TVector, TTriangle> CreateTriangle;
        protected readonly Func<float, float, float, TVertex> CreateVertex;

        protected StlReaderBase(Func<TVertex, TVertex, TVertex, TVector, TTriangle> createTriangle, Func<float, float, float, TVertex> createVertex, Func<float, float, float, TVector> createNormal)
        {
            if (createTriangle == null)
            {
                throw new ArgumentNullException("createTriangle");
            }
            if (createVertex == null)
            {
                throw new ArgumentNullException("createVertex");
            }
            if (createNormal == null)
            {
                throw new ArgumentNullException("createNormal");
            }

            CreateTriangle = createTriangle;
            CreateNormal = createNormal;
            CreateVertex = createVertex;
        }

        public abstract IEnumerable<TTriangle> ReadFile(string fileName);
    }
}
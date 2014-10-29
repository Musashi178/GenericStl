using System;
using System.Collections.Generic;

namespace GenericStl
{
    public abstract class StlReaderBase<TTriangle, TNormal, TVertex> : IStlReader<TTriangle>
    {
        protected readonly Func<float, float, float, TNormal> CreateNormal;
        protected readonly Func<TVertex, TVertex, TVertex, TNormal, TTriangle> CreateTriangle;
        protected readonly Func<float, float, float, TVertex> CreateVertex;

        protected StlReaderBase(Func<TVertex, TVertex, TVertex, TNormal, TTriangle> createTriangle, Func<float, float, float, TVertex> createVertex, Func<float, float, float, TNormal> createNormal)
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
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace GenericStl
{
    [SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes")]
    public abstract class StlReaderBase<TTriangle, TVertex, TNormal> : IStlReader<TTriangle>
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

        protected StlReaderBase(IDataReaderFactory<TTriangle, TVertex, TNormal> dataReaderFactory)
        {
            if (dataReaderFactory == null)
            {
                throw new ArgumentNullException("dataReaderFactory");
            }

            CreateTriangle = dataReaderFactory.CreateTriangle;
            CreateNormal = dataReaderFactory.CreateNormal;
            CreateVertex = dataReaderFactory.CreateVertex;
        }

        public abstract IEnumerable<TTriangle> ReadFromFile(string fileName);
        public abstract IEnumerable<TTriangle> ReadFromStream(Stream s);
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace GenericStl
{
    [SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes")]
    public abstract class StlWriterBase<TTriangle, TVertex, TNormal> : IStlWriter<TTriangle>
    {
        protected readonly Func<TNormal, Tuple<float, float, float>> ExtractNormal;
        protected readonly Func<TTriangle, Tuple<TVertex, TVertex, TVertex, TNormal>> ExtractTriangle;
        protected readonly Func<TVertex, Tuple<float, float, float>> ExtractVertex;

        protected StlWriterBase(Func<TTriangle, Tuple<TVertex, TVertex, TVertex, TNormal>> extractTriangle, Func<TVertex, Tuple<float, float, float>> extractVertex, Func<TNormal, Tuple<float, float, float>> extractNormal)
        {
            if (extractTriangle == null)
            {
                throw new ArgumentNullException("extractTriangle");
            }

            if (extractVertex == null)
            {
                throw new ArgumentNullException("extractVertex");
            }

            if (extractNormal == null)
            {
                throw new ArgumentNullException("extractNormal");
            }

            ExtractTriangle = extractTriangle;
            ExtractVertex = extractVertex;
            ExtractNormal = extractNormal;
        }

        public abstract void WriteToFile(string fileName, IEnumerable<TTriangle> data);
        public abstract void WriteToStream(Stream s, IEnumerable<TTriangle> triangles);
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace GenericStl
{
    /// <summary>
    /// A base class for stl writers.
    /// </summary>
    /// <typeparam name="TTriangle">The type of the data structure representing a triangle.</typeparam>
    /// <typeparam name="TVertex">The type of the data structure representing the vertices.</typeparam>
    /// <typeparam name="TNormal">The type of the data structure representing the normal.</typeparam>
    [SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes")]
    public abstract class StlWriterBase<TTriangle, TVertex, TNormal> : IStlWriter<TTriangle>
    {
        protected readonly Func<TNormal, Tuple<float, float, float>> ExtractNormal;
        protected readonly Func<TTriangle, Tuple<TVertex, TVertex, TVertex, TNormal>> ExtractTriangle;
        protected readonly Func<TVertex, Tuple<float, float, float>> ExtractVertex;

        /// <summary>
        /// Constructs a basic stl writer from three factory functions.
        /// </summary>
        /// <param name="extractTriangle">Factoryfunc for extracting the three vertices and the normal from a triangle. Must not be null.</param>
        /// <param name="extractVertex">Factoryfunc for extracting three coords from a vertex. Must not be null.</param>
        /// <param name="extractNormal">Factoryfunc for extracting the three float from a normal. Must not be null.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="extractTriangle"/>, <paramref name="extractVertex"/> or <paramref name="extractTriangle"/> is null.</exception>
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

        /// <summary>
        /// Constructs a basic stl writer from an <paramref name="extractor"/>.
        /// </summary>
        /// <param name="extractor">The factory used for extracting data from the data structure. Must not be null</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="extractor"/> is null.</exception>
        protected StlWriterBase(IDataStructureExtractor<TTriangle, TVertex, TNormal> extractor)
        {
            if (extractor == null)
            {
                throw new ArgumentNullException("extractor");
            }

            ExtractTriangle = extractor.ExtractTriangle;
            ExtractVertex = extractor.ExtractVertex;
            ExtractNormal = extractor.ExtractNormal;
        }

        /// <summary>
        /// Writes all triangles in <paramref name="triangles"/> to <paramref name="fileName"/>. 
        /// <remarks>An existing file will be overwritten.</remarks>
        /// </summary>
        /// <param name="fileName">The file to store the data in.</param>
        /// <param name="triangles">The triangles to store.</param>
        public abstract void WriteToFile(string fileName, IEnumerable<TTriangle> triangles);

        /// <summary>
        /// Writes all triangles in <paramref name="triangles"/> to <paramref name="s"/>. 
        /// </summary>
        /// <param name="s">The stream to write the data to.</param>
        /// <param name="triangles">The triangles to store.</param>
        public abstract void WriteToStream(Stream s, IEnumerable<TTriangle> triangles);
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace GenericStl
{
    /// <summary>
    /// A base class for stl readers.
    /// </summary>
    /// <typeparam name="TTriangle">The type of the data structure representing a triangle.</typeparam>
    /// <typeparam name="TVertex">The type of the data structure representing the vertices.</typeparam>
    /// <typeparam name="TNormal">The type of the data structure representing the normal.</typeparam>
    [SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes")]
    public abstract class StlReaderBase<TTriangle, TVertex, TNormal> : IStlReader<TTriangle>
    {
        protected readonly Func<float, float, float, TNormal> CreateNormal;
        protected readonly Func<TVertex, TVertex, TVertex, TNormal, TTriangle> CreateTriangle;
        protected readonly Func<float, float, float, TVertex> CreateVertex;

        /// <summary>
        /// Constructs a basic stl reader from three factory functions.
        /// </summary>
        /// <param name="createTriangle">Factoryfunc for creating a triangle from of three vertices and a normal. Must not be null.</param>
        /// <param name="createVertex">Factoryfunc for creating a vertex out of three floats. Must not be null.</param>
        /// <param name="createNormal">Factoryfunc for creating a normal from three float. Must not be null.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="createTriangle"/>, <paramref name="createVertex"/> or <paramref name="createTriangle"/> is null.</exception>
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

        /// <summary>
        /// Constructs a basic stl read from an <paramref name="dataStructureCreator"/>.
        /// </summary>
        /// <param name="dataStructureCreator">The factory used for creating the data structure. Must not be null</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="dataStructureCreator"/> is null.</exception>
        protected StlReaderBase(IDataStructureCreator<TTriangle, TVertex, TNormal> dataStructureCreator)
        {
            if (dataStructureCreator == null)
            {
                throw new ArgumentNullException("dataStructureCreator");
            }

            CreateTriangle = dataStructureCreator.CreateTriangle;
            CreateNormal = dataStructureCreator.CreateNormal;
            CreateVertex = dataStructureCreator.CreateVertex;
        }

        /// <summary>
        /// Reads all triangles from the file referenced by <paramref name="fileName"/>. The reading is done on the fly, so do not delete the file before all data is read.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>All triangle data in <paramref name="fileName"/></returns>
        public abstract IEnumerable<TTriangle> ReadFromFile(string fileName);

        /// <summary>
        /// Reads all triangles from the stream referenced by <paramref name="s"/>. The caller is responsible for closing the stream, but do not close it before you read all the triangles that you need.
        /// </summary>
        /// <param name="s">The stream to read the data from.</param>
        /// <returns>All triangle data in <paramref name="s"/></returns>
        public abstract IEnumerable<TTriangle> ReadFromStream(Stream s);
    }
}
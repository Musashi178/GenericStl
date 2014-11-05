using System;
using FluentAssertions;
using GenericStl.Tests.TestDataStructures;
using NUnit.Framework;

namespace GenericStl.Tests
{
    public abstract class StlReaderBaseTests<TStlReaderImplementation> where TStlReaderImplementation : StlReaderBase<Triangle, Vertex, Normal>
    {
        protected readonly Func<Vertex, Vertex, Vertex, Normal, Triangle>[] CreateTriangleFuncData = new Func<Vertex, Vertex, Vertex, Normal, Triangle>[]
        {
            null,
            TestDataStructureHelpers.CreateTriangle
        };

        protected readonly Func<float, float, float, Vertex>[] CreateVertexFuncData = new Func<float, float, float, Vertex>[]
        {
            null,
            TestDataStructureHelpers.CreateVertex
        };

        protected readonly Func<float, float, float, Normal>[] CreateNormalFuncData = new Func<float, float, float, Normal>[]
        {
            null,
            TestDataStructureHelpers.CreateNormal
        };

        [Test]
        public void Ctor_WithNullCreator_ThrowsArgumentNullException()
        {
            var call = new Action(() => CreateReader(null));

            call.ShouldThrow<ArgumentNullException>();
        }

        [Theory]
        public void Ctor_WithNullFunc_ThrowArgumentNullException(
            [ValueSource("CreateTriangleFuncData")]Func<Vertex, Vertex, Vertex, Normal, Triangle> createTriangle,
            [ValueSource("CreateVertexFuncData")]Func<float, float, float, Vertex> createVertex,
            [ValueSource("CreateNormalFuncData")]Func<float, float, float, Normal> createNormal)
        {
            Assume.That(createTriangle == null || createVertex==null || createNormal == null);

            var call = new Action(() => CreateReader(createTriangle, createVertex, createNormal));

            call.ShouldThrow<ArgumentNullException>();
        }

        protected abstract TStlReaderImplementation CreateReader(Func<Vertex, Vertex, Vertex, Normal, Triangle> createTriangle, Func<float, float, float, Vertex> createVertex, Func<float, float, float, Normal> createNormal);
        protected abstract TStlReaderImplementation CreateReader(IDataStructureCreator<Triangle, Vertex, Normal> structureCreator);

    }
}
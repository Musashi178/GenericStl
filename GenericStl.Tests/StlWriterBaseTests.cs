using System;
using FluentAssertions;
using GenericStl.Tests.TestDataStructures;
using NUnit.Framework;

namespace GenericStl.Tests
{
    public abstract class StlWriterBaseTests<TStlWriterImplementation> where TStlWriterImplementation : StlWriterBase<Triangle, Vertex, Normal>
    {
        protected readonly Func<Normal, Tuple<float, float, float>>[] ExtractNormalFuncData = new Func<Normal, Tuple<float, float, float>>[]
        {
            null,
            TestDataStructureHelpers.ExtractNormal
        };

        protected readonly Func<Triangle, Tuple<Vertex, Vertex, Vertex, Normal>>[] ExtractTriangleFuncData = new Func<Triangle, Tuple<Vertex, Vertex, Vertex, Normal>>[]
        {
            null,
            TestDataStructureHelpers.ExtractTriangle
        };

        protected readonly Func<Vertex, Tuple<float, float, float>>[] ExtractVertexFuncData = new Func<Vertex, Tuple<float, float, float>>[]
        {
            null,
            TestDataStructureHelpers.ExtractVertex
        };

        [Test]
        public void Ctor_WithNullCreator_ThrowsArgumentNullException()
        {
            var call = new Action(() => CreateWriter(null));

            call.ShouldThrow<ArgumentNullException>();
        }

        [Theory]
        public void Ctor_WithNullFunc_ThrowArgumentNullException(
            [ValueSource("ExtractTriangleFuncData")] Func<Triangle, Tuple<Vertex, Vertex, Vertex, Normal>> extractTriangle,
            [ValueSource("ExtractVertexFuncData")] Func<Vertex, Tuple<float, float, float>> extractVertex,
            [ValueSource("ExtractNormalFuncData")] Func<Normal, Tuple<float, float, float>> extractNormal)
        {
            Assume.That(extractTriangle == null || extractVertex == null || extractNormal == null);

            var call = new Action(() => CreateWriter(extractTriangle, extractVertex, extractNormal));

            call.ShouldThrow<ArgumentNullException>();
        }

        protected abstract TStlWriterImplementation CreateWriter(Func<Triangle, Tuple<Vertex, Vertex, Vertex, Normal>> extractTriangle, Func<Vertex, Tuple<float, float, float>> extractVertex, Func<Normal, Tuple<float, float, float>> extractNormal);
        protected abstract TStlWriterImplementation CreateWriter(IDataStructureExtractor<Triangle, Vertex, Normal> extractor);
    }
}
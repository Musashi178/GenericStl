using System;
using System.IO;
using FluentAssertions;
using GenericStl.Tests.TestDataStructures;
using Xunit;

namespace GenericStl.Tests
{
    public abstract class StlWriterBaseTests<TWriterImpl> where TWriterImpl : StlWriterBase<Triangle, Vertex, Normal>
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

        protected TWriterImpl ObjectUnderTest;

        protected StlWriterBaseTests()
        {
            ObjectUnderTest = CreateWriter(TestDataStructureHelpers.ExtractTriangle, TestDataStructureHelpers.ExtractVertex, TestDataStructureHelpers.ExtractNormal);
        }

        [Fact]
        public void Ctor_WithNullCreator_ThrowsArgumentNullException()
        {
            var call = new Action(() => CreateWriter(null));

            call.ShouldThrow<ArgumentNullException>();
        }

        //[Theory, CombinatorialData]
        //public void Ctor_WithNullFunc_ThrowArgumentNullException(
        //    [CombinatorialValues(null)] Func<Triangle, Tuple<Vertex, Vertex, Vertex, Normal>> extractTriangle,
        //    [CombinatorialValues(null)] Func<Vertex, Tuple<float, float, float>> extractVertex,
        //    [CombinatorialValues(null)] Func<Normal, Tuple<float, float, float>> extractNormal)
        //{
        //    Assume.That(extractTriangle == null || extractVertex == null || extractNormal == null);

        //    var call = new Action(() => CreateWriter(extractTriangle, extractVertex, extractNormal));

        //    call.ShouldThrow<ArgumentNullException>();
        //}

        [Fact]
        public void WriteToStream_WithNullStream_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => ObjectUnderTest.WriteToStream(null, TestHelpers.BlockExpectedResult));
        }

        [Fact]
        public void WriteToStream_WithNullTriangles_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                using (var ms = new MemoryStream())
                {
                    ObjectUnderTest.WriteToStream(ms, null);
                }
            });
        }

        protected abstract TWriterImpl CreateWriter(Func<Triangle, Tuple<Vertex, Vertex, Vertex, Normal>> extractTriangle, Func<Vertex, Tuple<float, float, float>> extractVertex, Func<Normal, Tuple<float, float, float>> extractNormal);
        protected abstract TWriterImpl CreateWriter(IDataStructureExtractor<Triangle, Vertex, Normal> extractor);
    }
}
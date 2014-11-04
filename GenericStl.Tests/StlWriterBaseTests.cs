using System;
using System.IO;
using FluentAssertions;
using GenericStl.Tests.TestDataStructures;
using NUnit.Framework;

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

        [SetUp]
        public void SetUp()
        {
            ObjectUnderTest = CreateWriter(TestDataStructureHelpers.ExtractTriangle, TestDataStructureHelpers.ExtractVertex, TestDataStructureHelpers.ExtractNormal);
        }

        [TearDown]
        public void TearDown()
        {
            ObjectUnderTest = null;
        }

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

        [Test]
        [ExpectedException(typeof (ArgumentNullException))]
        public void WriteToStream_WithNullStream_ThrowsArgumentNullException()
        {
            ObjectUnderTest.WriteToStream(null, TestHelpers.BlockExpectedResult);
        }

        [Test]
        [ExpectedException(typeof (ArgumentNullException))]
        public void WriteToStream_WithNullTriangles_ThrowsArgumentNullException()
        {
            using (var ms = new MemoryStream())
            {
                ObjectUnderTest.WriteToStream(ms, null);
            }
        }

        protected abstract TWriterImpl CreateWriter(Func<Triangle, Tuple<Vertex, Vertex, Vertex, Normal>> extractTriangle, Func<Vertex, Tuple<float, float, float>> extractVertex, Func<Normal, Tuple<float, float, float>> extractNormal);
        protected abstract TWriterImpl CreateWriter(IDataStructureExtractor<Triangle, Vertex, Normal> extractor);
    }
}
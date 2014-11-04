using System;
using System.IO;
using ApprovalTests;
using ApprovalTests.Reporters;
using GenericStl.Tests.TestDataStructures;
using NUnit.Framework;

namespace GenericStl.Tests
{
    [TestFixture]
    [UseReporter(typeof (NUnitReporter))]
    public class BinaryStlWriterTests : StlWriterBaseTests<BinaryStlWriter<Triangle, Vertex, Normal>>
    {
        [SetUp]
        public void SetUp()
        {
            _objectUnderTest = new BinaryStlWriter<Triangle, Vertex, Normal>(TestDataStructureHelpers.ExtractTriangle, TestDataStructureHelpers.ExtractVertex, TestDataStructureHelpers.ExtractNormal);
        }

        private BinaryStlWriter<Triangle, Vertex, Normal> _objectUnderTest;

        protected override BinaryStlWriter<Triangle, Vertex, Normal> CreateWriter(Func<Triangle, Tuple<Vertex, Vertex, Vertex, Normal>> extractTriangle, Func<Vertex, Tuple<float, float, float>> extractVertex, Func<Normal, Tuple<float, float, float>> extractNormal)
        {
            return new BinaryStlWriter<Triangle, Vertex, Normal>(extractTriangle, extractVertex, extractNormal);
        }

        protected override BinaryStlWriter<Triangle, Vertex, Normal> CreateWriter(IDataStructureExtractor<Triangle, Vertex, Normal> extractor)
        {
            return new BinaryStlWriter<Triangle, Vertex, Normal>(extractor);
        }

        [Test]
        public void WriteFile_WithBlock_ReturnsExpectedResult()
        {
            var namer = Approvals.GetDefaultNamer();
            var file = Path.Combine(namer.SourcePath, namer.Name + ".received.stl");
            _objectUnderTest.WriteToFile(file, TestHelpers.BlockExpectedResult);
            Approvals.Verify(new FileInfo(file));
        }

        [Test]
        public void Write_WithBlock_ReturnsExpectedResult()
        {
            var result = _objectUnderTest.Write(TestHelpers.BlockExpectedResult);
            Approvals.VerifyBinaryFile(result, ".stl");
        }
    }
}
using System;
using System.IO;
using System.Linq;
using System.Text;
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
            ObjectUnderTest.WriteToFile(file, TestHelpers.BlockExpectedResult);
            Approvals.Verify(new FileInfo(file));
        }

        [Test]
        public void Write_WithBlock_ReturnsExpectedResult()
        {
            var result = ObjectUnderTest.Write(TestHelpers.BlockExpectedResult);
            Approvals.VerifyBinaryFile(result, ".stl");
        }

        [Test]
        public void Write_WithSmallHeader_ReturnsExpectedResult()
        {
            var hdr = new ASCIIEncoding().GetBytes("short header");
            var result = ObjectUnderTest.Write(TestHelpers.BlockExpectedResult, hdr);
            Approvals.VerifyBinaryFile(result, ".stl");
        }

        [Test]
        public void Write_WithLargeHeader_ReturnsExpectedResult()
        {
            var hdr = string.Join("", Enumerable.Repeat("abcdefghijklmnopqrstuvwxyz1234567890", 10));
            var hdrBytes = new ASCIIEncoding().GetBytes(hdr);

            var result = ObjectUnderTest.Write(TestHelpers.BlockExpectedResult, hdrBytes);
            Approvals.VerifyBinaryFile(result, ".stl");
        }
    }
}
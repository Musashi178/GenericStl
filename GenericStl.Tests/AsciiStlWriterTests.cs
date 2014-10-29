using System.IO;
using ApprovalTests;
using ApprovalTests.Reporters;
using GenericStl.Tests.TestDataStructures;
using NUnit.Framework;

namespace GenericStl.Tests
{
    [TestFixture]
    [UseReporter(typeof (NUnitReporter))]
    public class AsciiStlWriterTests
    {
        [SetUp]
        public void SetUp()
        {
            _objectUnderTest = new AsciiStlWriter<Triangle, Vertex, Normal>(TestHelpers.ExtractTriangle, TestHelpers.ExtractVertex, TestHelpers.ExtractNormal);
        }

        private AsciiStlWriter<Triangle, Vertex, Normal> _objectUnderTest;

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
            Approvals.Verify(new ApprovalTextWriter(result, ".stl"));
        }
    }
}
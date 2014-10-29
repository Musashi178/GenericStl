using ApprovalTests;
using ApprovalTests.Reporters;
using NUnit.Framework;

using Vertex = System.Tuple<float, float, float>;
using Normal = System.Tuple<float, float, float>;
using Triangle = System.Tuple<System.Tuple<float, float, float>, System.Tuple<float, float, float>, System.Tuple<float, float, float>, System.Tuple<float, float, float>>;

namespace GenericStl.Tests
{
    [TestFixture]
    [UseReporter(typeof(NUnitReporter))]
    public class AsciiStlWriterTests
    {
        private AsciiStlWriter<Triangle, Vertex, Vertex> _objectUnderTest;

        [SetUp]
        public void SetUp()
        {
            this._objectUnderTest = new AsciiStlWriter<Triangle, Vertex, Normal>(TestHelpers.ExtractTriangle, TestHelpers.ExtractVertex, TestHelpers.ExtractNormal);
        }

        [Test]
        public void Write_WithBlock_ReturnsExpectedResult()
        {
            var result = this._objectUnderTest.Write(TestHelpers.BlockExpectedResult);
            Approvals.Verify(new ApprovalTextWriter(result,".stl"));
        }
    }
}

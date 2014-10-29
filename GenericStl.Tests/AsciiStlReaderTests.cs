using System.IO;
using FluentAssertions;
using NUnit.Framework;
using Vertex = System.Tuple<float, float, float>;
using Normal = System.Tuple<float, float, float>;
using Triangle = System.Tuple<System.Tuple<float, float, float>, System.Tuple<float, float, float>, System.Tuple<float, float, float>, System.Tuple<float, float, float>>;

namespace GenericStl.Tests
{
    [TestFixture]
    public class AsciiStlReaderTests
    {
        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            this._expectedCubeResult = new Triangle[]
            {
                new Triangle(new Vertex(.0f, 100.0f, 100.0f), new Vertex(.0f, 100.0f, .0f), new Vertex(.0f, .0f, 100.0f), new Normal(-1.0f, .0f, .0f)),
                new Triangle(new Vertex(.0f, .0f, 100.0f), new Vertex(.0f, 100.0f, .0f), new Vertex(.0f, 0.0f, .0f), new Normal(-1.0f, .0f, .0f)),
                new Triangle(new Vertex(100.0f, 100.0f, 100.0f), new Vertex(.0f, 100.0f, 100.0f), new Vertex(100.0f, .0f, 100.0f), new Normal(0.0f, .0f, 1.0f)),
                new Triangle(new Vertex(100.0f, .0f, 100.0f), new Vertex(.0f, 100.0f, 100.0f), new Vertex(0.0f, .0f, 100.0f), new Normal(0.0f, .0f, 1.0f)),
                new Triangle(new Vertex(100.0f, 100.0f, .0f), new Vertex(100.0f, 100.0f, 100.0f), new Vertex(100.0f, .0f, .0f), new Normal(1.0f, .0f, .0f)),
                new Triangle(new Vertex(100.0f, .0f, .0f), new Vertex(100.0f, 100.0f, 100.0f), new Vertex(100.0f, .0f, 100.0f), new Normal(1.0f, .0f, .0f)),
                new Triangle(new Vertex(.0f, 100.0f, .0f), new Vertex(100.0f, 100.0f, .0f), new Vertex(.0f, .0f, .0f), new Normal(.0f, .0f, -1.0f)),
                new Triangle(new Vertex(.0f, .0f, .0f), new Vertex(100.0f, 100.0f, .0f), new Vertex(100.0f, .0f, .0f), new Normal(.0f, .0f, -1.0f)),
                new Triangle(new Vertex(100.0f, 100.0f, 100.0f), new Vertex(100.0f, 100.0f, .0f), new Vertex(.0f, 100.0f, 100.0f), new Normal(.0f, 1.0f, .0f)),
                new Triangle(new Vertex(.0f, 100.0f, 100.0f), new Vertex(100.0f, 100.0f, .0f), new Vertex(.0f, 100.0f, 0.0f), new Normal(.0f, 1.0f, .0f)),
                new Triangle(new Vertex(100.0f, .0f, .0f), new Vertex(100.0f, .0f, 100.0f), new Vertex(.0f, .0f, .0f), new Normal(.0f, -1.0f, .0f)),
                new Triangle(new Vertex(.0f, .0f, .0f), new Vertex(100.0f, .0f, 100.0f), new Vertex(.0f, .0f, 100.0f), new Normal(.0f, -1.0f, .0f)),
            };
        }

        [SetUp]
        public void SetUp()
        {
            _objectUnderTest = new AsciiStlReader<Triangle, Vertex, Normal>(TestHelpers.CreateTriangle, TestHelpers.CreateVertex, TestHelpers.CreateNormal);
        }

        private const string AsciiTestFile = @".\TestData\ascii_block.stl";
        private AsciiStlReader<Triangle, Vertex, Normal> _objectUnderTest;
        private Triangle[] _expectedCubeResult;

        [Test]
        public void ReadFile_WithAsciiBlockFile_ReturnsExpectedTriangles()
        {
            var result = _objectUnderTest.ReadFile(AsciiTestFile);

            result.Should().BeEquivalentTo(this._expectedCubeResult);
        }

        [Test]
        public void Read_WithAsciiBlockFile_ReturnsTheExpectedTriangles()
        {
            var stlFileContent = File.ReadAllLines(AsciiTestFile);

            var result = _objectUnderTest.Read(stlFileContent);

            result.Should().BeEquivalentTo(this._expectedCubeResult);
        }
    }
}
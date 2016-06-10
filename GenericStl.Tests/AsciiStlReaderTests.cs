using System;
using System.IO;
using System.Runtime.InteropServices;
using FluentAssertions;
using GenericStl.Tests.TestDataStructures;
using Xunit;
using Ploeh.AutoFixture;

namespace GenericStl.Tests
{
    
    public class AsciiStlReaderTests : StlReaderBaseTests<AsciiStlReader<Triangle, Vertex, Normal>>
    {
        public  AsciiStlReaderTests()
        {
            _fixture = new Fixture();
            _objectUnderTest = new AsciiStlReader<Triangle, Vertex, Normal>(TestDataStructureHelpers.CreateTriangle, TestDataStructureHelpers.CreateVertex, TestDataStructureHelpers.CreateNormal);
        }

        private const string AsciiTestFile = @".\TestData\ascii_block.stl";
        private readonly AsciiStlReader<Triangle, Vertex, Normal> _objectUnderTest;
        private Fixture _fixture;

        protected override AsciiStlReader<Triangle, Vertex, Normal> CreateReader(Func<Vertex, Vertex, Vertex, Normal, Triangle> createTriangle, Func<float, float, float, Vertex> createVertex, Func<float, float, float, Normal> createNormal)
        {
            return new AsciiStlReader<Triangle, Vertex, Normal>(createTriangle, createVertex, createNormal);
        }

        protected override AsciiStlReader<Triangle, Vertex, Normal> CreateReader(IDataStructureCreator<Triangle, Vertex, Normal> structureCreator)
        {
            return new AsciiStlReader<Triangle, Vertex, Normal>(structureCreator);
        }

        [Fact]
        public void ReadFile_WithAsciiBlockFile_ReturnsExpectedTriangles()
        {
            var result = _objectUnderTest.ReadFromFile(AsciiTestFile);

            result.Should().BeEquivalentTo(TestHelpers.BlockExpectedResult);
        }

        [Fact]
        public void Read_WithAsciiBlockFile_ReturnsTheExpectedTriangles()
        {
            var stlFileContent = File.ReadAllLines(AsciiTestFile);

            var result = _objectUnderTest.Read(stlFileContent);

            result.Should().BeEquivalentTo(TestHelpers.BlockExpectedResult);
        }

        [Test]
        public void GetNormal_WithLineSeparatingTheTokensWithMultipleWhitespaces_ReturnsTheExpectedNormal()
        {
            var expectedNormal = _fixture.Create<Normal>();
            var line = string.Format(" facet  normal    {0}  {1} {2}", expectedNormal.X, expectedNormal.Y, expectedNormal.Z);

            var result = _objectUnderTest.GetNormal(line);

            result.Should().Be(expectedNormal);
        }

        [Test]
        public void GetVertex_WithLineSeparatingTheTokensWithMultipleWhitespaces_ReturnsTheExpectedVertex()
        {
            var expectedVertex = _fixture.Create<Vertex>();
            var line = string.Format("vertex    {0}  {1} {2}", expectedVertex.X, expectedVertex.Y, expectedVertex.Z);

            var result = _objectUnderTest.GetVertex(line);

            result.Should().Be(expectedVertex);
        }
    }
}
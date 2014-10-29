using System.IO;
using FluentAssertions;
using GenericStl.Tests.TestDataStructures;
using NUnit.Framework;

namespace GenericStl.Tests
{
    [TestFixture]
    public class AsciiStlReaderTests
    {
        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
           
        }

        [SetUp]
        public void SetUp()
        {
            _objectUnderTest = new AsciiStlReader<Triangle, Vertex, Normal>(TestHelpers.CreateTriangle, TestHelpers.CreateVertex, TestHelpers.CreateNormal);
        }

        private const string AsciiTestFile = @".\TestData\ascii_block.stl";
        private AsciiStlReader<Triangle, Vertex, Normal> _objectUnderTest;

        [Test]
        public void ReadFile_WithAsciiBlockFile_ReturnsExpectedTriangles()
        {
            var result = _objectUnderTest.ReadFromFile(AsciiTestFile);

            result.Should().BeEquivalentTo(TestHelpers.BlockExpectedResult);
        }

        [Test]
        public void Read_WithAsciiBlockFile_ReturnsTheExpectedTriangles()
        {
            var stlFileContent = File.ReadAllLines(AsciiTestFile);

            var result = _objectUnderTest.Read(stlFileContent);

            result.Should().BeEquivalentTo(TestHelpers.BlockExpectedResult);
        }
    }
}
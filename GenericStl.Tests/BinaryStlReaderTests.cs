using System.IO;
using FluentAssertions;
using GenericStl.Tests.TestDataStructures;
using NUnit.Framework;

namespace GenericStl.Tests
{
    [TestFixture]
    public class BinaryStlReaderTests
    {
        [SetUp]
        public void SetUp()
        {
            _objectUnderTest = new BinaryStlReader<Triangle, Vertex, Normal>(TestHelpers.CreateTriangle, TestHelpers.CreateVertex, TestHelpers.CreateNormal);
        }

        private BinaryStlReader<Triangle, Vertex, Normal> _objectUnderTest;
        private const string BinaryTestFile = @".\TestData\binary_block.stl";

        [Test]
        public void ReadFile_WithBinaryBlockStl_ReturnsExpectedTriangles()
        {
            var result = _objectUnderTest.ReadFromFile(BinaryTestFile);

            result.Should().BeEquivalentTo(TestHelpers.BlockExpectedResult);
        }

        [Test]
        public void Read_WithBinaryBlockStl_ReturnsExpectedTriangles()
        {
            var stlFileContent = File.ReadAllBytes(BinaryTestFile);

            var result = _objectUnderTest.Read(stlFileContent);

            result.Should().BeEquivalentTo(TestHelpers.BlockExpectedResult);
        }
    }
}
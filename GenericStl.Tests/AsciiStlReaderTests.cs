using System;
using System.IO;
using FluentAssertions;
using GenericStl.Tests.TestDataStructures;
using NUnit.Framework;

namespace GenericStl.Tests
{
    [TestFixture]
    public class AsciiStlReaderTests : StlReaderBaseTests<AsciiStlReader<Triangle, Vertex, Normal>>
    {
        [SetUp]
        public void SetUp()
        {
            _objectUnderTest = new AsciiStlReader<Triangle, Vertex, Normal>(TestDataStructureHelpers.CreateTriangle, TestDataStructureHelpers.CreateVertex, TestDataStructureHelpers.CreateNormal);
        }

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
        }

        private const string AsciiTestFile = @".\TestData\ascii_block.stl";
        private AsciiStlReader<Triangle, Vertex, Normal> _objectUnderTest;

        protected override AsciiStlReader<Triangle, Vertex, Normal> CreateReader(Func<Vertex, Vertex, Vertex, Normal, Triangle> createTriangle, Func<float, float, float, Vertex> createVertex, Func<float, float, float, Normal> createNormal)
        {
            return new AsciiStlReader<Triangle, Vertex, Normal>(createTriangle, createVertex, createNormal);
        }

        protected override AsciiStlReader<Triangle, Vertex, Normal> CreateReader(IDataStructureCreator<Triangle, Vertex, Normal> structureCreator)
        {
            return new AsciiStlReader<Triangle, Vertex, Normal>(structureCreator);
        }

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
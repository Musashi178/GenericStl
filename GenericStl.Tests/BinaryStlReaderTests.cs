using System;
using System.IO;
using FluentAssertions;
using GenericStl.Tests.TestDataStructures;
using Xunit;

namespace GenericStl.Tests
{
    
    public class BinaryStlReaderTests : StlReaderBaseTests<BinaryStlReader<Triangle, Vertex, Normal>>
    {
        public BinaryStlReaderTests()
        {
            _objectUnderTest = new BinaryStlReader<Triangle, Vertex, Normal>(TestDataStructureHelpers.CreateTriangle, TestDataStructureHelpers.CreateVertex, TestDataStructureHelpers.CreateNormal);
        }

        private readonly BinaryStlReader<Triangle, Vertex, Normal> _objectUnderTest;
        private const string BinaryTestFile = @".\TestData\binary_block.stl";

        protected override BinaryStlReader<Triangle, Vertex, Normal> CreateReader(Func<Vertex, Vertex, Vertex, Normal, Triangle> createTriangle, Func<float, float, float, Vertex> createVertex, Func<float, float, float, Normal> createNormal)
        {
            return new BinaryStlReader<Triangle, Vertex, Normal>(createTriangle, createVertex, createNormal);
        }

        protected override BinaryStlReader<Triangle, Vertex, Normal> CreateReader(IDataStructureCreator<Triangle, Vertex, Normal> structureCreator)
        {
            return new BinaryStlReader<Triangle, Vertex, Normal>(structureCreator);
        }

        [Fact]
        public void ReadFile_WithBinaryBlockStl_ReturnsExpectedTriangles()
        {
            var result = _objectUnderTest.ReadFromFile(BinaryTestFile);

            result.Should().BeEquivalentTo(TestHelpers.BlockExpectedResult);
        }

        [Fact]
        public void Read_WithBinaryBlockStl_ReturnsExpectedTriangles()
        {
            var stlFileContent = File.ReadAllBytes(BinaryTestFile);

            var result = _objectUnderTest.Read(stlFileContent);

            result.Should().BeEquivalentTo(TestHelpers.BlockExpectedResult);
        }
    }
}
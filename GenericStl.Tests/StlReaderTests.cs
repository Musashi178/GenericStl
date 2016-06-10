using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using GenericStl.Tests.TestDataStructures;
using Xunit;

namespace GenericStl.Tests
{
    public class StlReaderTests
    {
        public StlReaderTests()
        {
            _objectUnderTest = new StlReader<Triangle, Vertex, Normal>(TestDataStructureHelpers.CreateTriangle, TestDataStructureHelpers.CreateVertex, TestDataStructureHelpers.CreateNormal);
        }

        private readonly StlReader<Triangle, Vertex, Normal> _objectUnderTest;

        [Theory]
        [InlineData(@".\TestData\ascii_block.stl")]
        [InlineData(@".\TestData\binary_block.stl")]
        public void ReadFromFile_ReturnsExpectedTriangles(string fileName)
        {
            var result = _objectUnderTest.ReadFromFile(fileName);

            result.Should().BeEquivalentTo(TestHelpers.BlockExpectedResult);
        }

    }
}

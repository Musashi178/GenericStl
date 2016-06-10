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
    
    public class StlFileTests
    {
        [Theory]
        [InlineData(@".\TestData\ascii_block.stl", false)]
        [InlineData(@".\TestData\binary_block.stl", true)]
        [InlineData(@".\TestData\binary_block_with_solid_header.stl", true)]
        public void IsBinaryFile_ReturnsExpected(string filename, bool expectedResult)
        {
            StlFile.IsBinaryFile(filename).Should().Be(expectedResult);
        }
    }
}

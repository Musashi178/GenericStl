using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using GenericStl.Tests.TestDataStructures;
using NUnit.Framework;

namespace GenericStl.Tests
{
    public class StlReaderTests
    {
        [SetUp]
        public void SetUp()
        {
            _objectUnderTest = new StlReader<Triangle, Vertex, Normal>(TestHelpers.CreateTriangle, TestHelpers.CreateVertex, TestHelpers.CreateNormal);
        }

        public IEnumerable StlFiles
        {
            get
            {
                yield return new TestCaseData(@".\TestData\ascii_block.stl").SetName("Ascii file");
                yield return new TestCaseData(@".\TestData\binary_block.stl").SetName("Binary file");
            }
        } 
        private StlReader<Triangle, Vertex, Normal> _objectUnderTest;

        [Test]
        [TestCaseSource("StlFiles")]
        public void ReadFromFile_ReturnsExpectedTriangles(string fileName)
        {
            var result = _objectUnderTest.ReadFromFile(fileName);

            result.Should().BeEquivalentTo(TestHelpers.BlockExpectedResult);
        }

    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenericStl.Tests.TestDataStructures;
using NUnit.Framework;

namespace GenericStl.Tests
{
    [TestFixture]
    public class StlFileTests
    {
        public IEnumerable IsBinaryFileTestCases
        {
            get
            {
                yield return new TestCaseData(@".\TestData\ascii_block.stl").SetName("Ascii file").Returns(false);
                yield return new TestCaseData(@".\TestData\binary_block.stl").SetName("Binary block file").Returns(true);
                yield return new TestCaseData(@".\TestData\binary_block_with_solid_header.stl").SetName("Binary file with solid header").Returns(true);
            }
        }

        [TestCaseSource("IsBinaryFileTestCases")]
        public bool IsBinaryFile_ReturnsExpected(string filename)
        {
            return StlFile.IsBinaryFile(filename);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using FluentAssertions;
using GenericStl;
using System.IO;
using Vertex = System.Tuple<float, float, float>;
using Normal = System.Tuple<float, float, float>;
using Triangle = System.Tuple<System.Tuple<float, float, float>, System.Tuple<float, float, float>, System.Tuple<float, float, float>, System.Tuple<float, float, float>>;

namespace GenericStl.Tests
{
    public static class TestHelpers
    {
        public static Vertex CreateVertex(float x, float y, float z)
        {
            return new Vertex(x, y, z);
        }

        public static Normal CreateNormal(float x, float y, float z)
        {
            return new Normal(x, y, z);
        }

        public static Triangle CreateTriangle(Vertex a, Vertex b, Vertex c, Normal n)
        {
            return new Triangle(a, b, c, n);
        }
    } 

   

    [TestFixture]
    public class AsciiStlReaderTests
    {
        private static string asciiTestFile = @".\TestData\ascii_block.stl";
        private AsciiStlReader<Triangle, Vertex, Normal> _objectUnderTest;

        

        [SetUp]
        public void SetUp()
        {
            this._objectUnderTest = new AsciiStlReader<Triangle, Vertex, Normal>(TestHelpers.CreateTriangle, TestHelpers.CreateVertex, TestHelpers.CreateNormal);
        }

        [Test]
        public void Read_ReturnsExpected()
        {
            var stlFileContent = File.ReadAllLines(asciiTestFile);

            var result = this._objectUnderTest.Read(stlFileContent);

            result.Should().HaveCount(12);
        }
    }
}

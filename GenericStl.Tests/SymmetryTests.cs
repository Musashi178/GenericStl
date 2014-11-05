using System;
using System.Collections;
using System.IO;
using FluentAssertions;
using GenericStl.Tests.TestDataStructures;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace GenericStl.Tests
{
    [TestFixture]
    public class SymmetryTests
    {
        private readonly Random _randomizer = new Random(DateTime.Now.Millisecond);

        public static IEnumerable ReaderWriters
        {
            get
            {
                yield return new TestCaseData(new AsciiStlReader<Triangle, Vertex, Normal>(TestDataStructureHelpers.CreateTriangle, TestDataStructureHelpers.CreateVertex, TestDataStructureHelpers.CreateNormal), new AsciiStlWriter<Triangle, Vertex, Normal>(TestDataStructureHelpers.ExtractTriangle, TestDataStructureHelpers.ExtractVertex, TestDataStructureHelpers.ExtractNormal)).SetName("Ascii");
                yield return new TestCaseData(new BinaryStlReader<Triangle, Vertex, Normal>(TestDataStructureHelpers.CreateTriangle, TestDataStructureHelpers.CreateVertex, TestDataStructureHelpers.CreateNormal), new BinaryStlWriter<Triangle, Vertex, Normal>(TestDataStructureHelpers.ExtractTriangle, TestDataStructureHelpers.ExtractVertex, TestDataStructureHelpers.ExtractNormal)).SetName("Binary");
            }
        }

        [Test]
        [TestCaseSource("ReaderWriters")]
        public void WriteRead_ShouldReturnTheInputData(IStlReader<Triangle> r, IStlWriter<Triangle> w)
        {
            var fixture = new Fixture()
            {
                RepeatCount = _randomizer.Next(1, 1000)
            };

            var muliplier = (float) _randomizer.NextDouble();
            fixture.Customize<float>(c => c.FromFactory<int>(i => i*muliplier));

            var expected = fixture.Create<Triangle[]>();

            using (var ms = new MemoryStream())
            {
                w.WriteToStream(ms, expected);

                ms.Seek(0, SeekOrigin.Begin);

                var actual = r.ReadFromStream(ms);

                actual.Should().BeEquivalentTo(expected);

                new Action(() => ms.Seek(0, SeekOrigin.Begin)).ShouldNotThrow<ObjectDisposedException>("ReadFromStream should not close the stream.");
            }
        }
    }
}
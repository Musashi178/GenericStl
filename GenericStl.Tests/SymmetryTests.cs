using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using FluentAssertions;
using GenericStl.Tests.TestDataStructures;
using Xunit;
using Ploeh.AutoFixture;
using Xunit.Extensions;

namespace GenericStl.Tests
{
    
    public class SymmetryTests
    {
        private readonly Random _randomizer = new Random(DateTime.Now.Millisecond);

        public static IEnumerable<object[]> ReaderWriters
        {
            get
            {
                yield return new object[]{new AsciiStlReader<Triangle, Vertex, Normal>(TestDataStructureHelpers.CreateTriangle, TestDataStructureHelpers.CreateVertex, TestDataStructureHelpers.CreateNormal), new AsciiStlWriter<Triangle, Vertex, Normal>(TestDataStructureHelpers.ExtractTriangle, TestDataStructureHelpers.ExtractVertex, TestDataStructureHelpers.ExtractNormal)};
                yield return new object[]{new BinaryStlReader<Triangle, Vertex, Normal>(TestDataStructureHelpers.CreateTriangle, TestDataStructureHelpers.CreateVertex, TestDataStructureHelpers.CreateNormal), new BinaryStlWriter<Triangle, Vertex, Normal>(TestDataStructureHelpers.ExtractTriangle, TestDataStructureHelpers.ExtractVertex, TestDataStructureHelpers.ExtractNormal)};
            }
        }

        [Theory]
        [MemberData("ReaderWriters")]
        public void WriteRead_ShouldReturnTheInputData(IStlReader<Triangle> r, IStlWriter<Triangle> w)
        {
            var fixture = new Fixture()
            {
                RepeatCount = _randomizer.Next(1, 1000)
            };

            var multiplier = (float) _randomizer.NextDouble();
            fixture.Customize<float>(c => c.FromFactory<int>(i => i*multiplier));

            var expected = fixture.Create<Triangle[]>();

            using (var ms = new MemoryStream())
            {
                w.WriteToStream(ms, expected);

                ms.Seek(0, SeekOrigin.Begin);

                var actual = r.ReadFromStream(ms);

                actual.Should().BeEquivalentTo(expected);

                var actionOnStream = new Action(() => ms.Seek(0, SeekOrigin.Begin));
                actionOnStream.ShouldNotThrow<ObjectDisposedException>("ReadFromStream should not close the stream.");
            }
        }
    }
}
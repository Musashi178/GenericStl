using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using MoreLinq;

namespace GenericStl
{
    [SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes")]
    public class AsciiStlReader<TTriangle, TVertex, TNormal> : StlReaderBase<TTriangle, TVertex, TNormal>
    {
        private const int DefaultBufferSize = 1024;
        private readonly Func<string, float> _parseFloat;

        public AsciiStlReader(Func<TVertex, TVertex, TVertex, TNormal, TTriangle> createTriangle, Func<float, float, float, TVertex> createVertex, Func<float, float, float, TNormal> createNormal)
            : base(createTriangle, createVertex, createNormal)
        {
            _parseFloat = f => float.Parse(f, CultureInfo.InvariantCulture);
        }

        public AsciiStlReader(IDataStructureCreator<TTriangle, TVertex, TNormal> structureCreator)
            : base(structureCreator)
        {
        }

        public IEnumerable<TTriangle> Read(IEnumerable<string> fileContent)
        {
            return fileContent.Select(l => l.Trim())
                .Where(l => !string.IsNullOrEmpty(l))
                .SkipWhile(l => l.StartsWith("solid", StringComparison.OrdinalIgnoreCase))
                .TakeWhile(l => !l.StartsWith("endsolid", StringComparison.OrdinalIgnoreCase))
                .Split(l => string.Equals(l, "endfacet", StringComparison.OrdinalIgnoreCase))
                .Select(ToTriangle);
        }

        public override IEnumerable<TTriangle> ReadFromFile(string fileName)
        {
            using (var fs = File.OpenRead(fileName))
            {
                foreach (var triangle in ReadFromStream(fs))
                {
                    yield return triangle;
                }
            }
        }

        public override IEnumerable<TTriangle> ReadFromStream(Stream s)
        {
            return Read(ReadLines(s));
        }

        private static IEnumerable<string> ReadLines(Stream s)
        {
            var r = new StreamReader(s, Encoding.UTF8, true, DefaultBufferSize); // do not dispose this reader as it would dispose the stream
            while (!r.EndOfStream)
            {
                yield return r.ReadLine();
            }
        }

        private TTriangle ToTriangle(IEnumerable<string> triangleChunk)
        {
            var chunk = triangleChunk.ToArray();

            var normal = GetNormal(chunk[0]);

            var v1 = GetVertex(chunk[2]);
            var v2 = GetVertex(chunk[3]);
            var v3 = GetVertex(chunk[4]);

            return CreateTriangle(v1, v2, v3, normal);
        }

        public TNormal GetNormal(string line)
        {
            var tokens = Tokenize(line);

            Debug.Assert(string.Equals(tokens[0], "facet", StringComparison.InvariantCultureIgnoreCase));
            Debug.Assert(string.Equals(tokens[1], "normal", StringComparison.InvariantCultureIgnoreCase));

            return CreateNormal(_parseFloat(tokens[2]), _parseFloat(tokens[3]), _parseFloat(tokens[4]));
        }

        private static string[] Tokenize(string line)
        {
            return line.Split(' ').Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();
        }


        public TVertex GetVertex(string line)
        {
            var tokens = Tokenize(line);

            Debug.Assert(string.Equals(tokens[0], "vertex", StringComparison.InvariantCultureIgnoreCase));

            return CreateVertex(_parseFloat(tokens[1]), _parseFloat(tokens[2]), _parseFloat(tokens[3]));
        }
    }
}
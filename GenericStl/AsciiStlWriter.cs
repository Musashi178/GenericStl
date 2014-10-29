using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace GenericStl
{
    public class AsciiStlWriter<TTriangle, TNormal, TVertex> : StlWriterBase<TTriangle, TNormal, TVertex>
    {
        public AsciiStlWriter(Func<TTriangle, Tuple<TVertex, TVertex, TVertex, TNormal>> extractTriangle, Func<TVertex, Tuple<float, float, float>> extractVertex, Func<TNormal, Tuple<float, float, float>> extractNormal) : base(extractTriangle, extractVertex, extractNormal)
        {
        }

        public override void WriteFile(IEnumerable<TTriangle> data, string filename)
        {
            using (var fs = File.CreateText(filename))
            {
                WriteTo(data, fs);
            }
        }

        public string Write(IEnumerable<TTriangle> triangles)
        {
            using (var w = new StringWriter(CultureInfo.InvariantCulture))
            {
                WriteTo(triangles, w);
                return w.ToString();
            }
        }

        private void WriteTo(IEnumerable<TTriangle> triangles, TextWriter w)
        {
            w.WriteLine("solid");

            foreach (var triangle in triangles)
            {
                WriteTriangle(w, triangle);
            }

            w.WriteLine("endsolid");
        }

        private void WriteTriangle(TextWriter w, TTriangle triangle)
        {
            var triangleData = ExtractTriangle(triangle);

            w.Write("facet ");
            WriteNormal(w, triangleData.Item4);
            w.WriteLine("outer loop");
            WriteVertex(w, triangleData.Item1);
            WriteVertex(w, triangleData.Item2);
            WriteVertex(w, triangleData.Item3);
            w.WriteLine("endloop");
            w.WriteLine("endfacet");
        }

        private void WriteVertex(TextWriter w, TVertex v)
        {
            var vertexData = ExtractVertex(v);
            w.Write("vertex ");
            WriteTripleFloat(w, vertexData);
            w.WriteLine();
        }

        private static void WriteTripleFloat(TextWriter w, Tuple<float, float, float> t)
        {
            w.Write(t.Item1);
            w.Write(" ");
            w.Write(t.Item2);
            w.Write(" ");
            w.Write(t.Item3);
        }

        private void WriteNormal(TextWriter w, TNormal n)
        {
            var normalData = ExtractNormal(n);
            w.Write("normal ");
            WriteTripleFloat(w, normalData);
            w.WriteLine();
        }
    }
}
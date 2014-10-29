using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace GenericStl
{
    public class AsciiStlWriter<TTriangle, TVector, TVertex>
    {
        private readonly Func<TVector, Tuple<float, float, float>> _extractNormal;
        private readonly Func<TTriangle, Tuple<TVertex, TVertex, TVertex, TVector>> _extractTriangle;
        private readonly Func<TVertex, Tuple<float, float, float>> _extractVertex;

        public AsciiStlWriter(Func<TTriangle, Tuple<TVertex, TVertex, TVertex, TVector>> extractTriangle, Func<TVertex, Tuple<float, float, float>> extractVertex, Func<TVector, Tuple<float, float, float>> extractNormal)
        {
            _extractTriangle = extractTriangle;
            _extractVertex = extractVertex;
            _extractNormal = extractNormal;
        }

        public string Write(IEnumerable<TTriangle> triangles)
        {
            using (var w = new StringWriter(CultureInfo.InvariantCulture))
            {
                w.WriteLine("solid");

                foreach (var triangle in triangles)
                {
                    WriteTriangle(w, triangle);
                }

                w.WriteLine("endsolid");
                return w.ToString();
            }
        }

        private void WriteTriangle(TextWriter w, TTriangle triangle)
        {
            var triangleData = _extractTriangle(triangle);

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
            var vertexData = _extractVertex(v);
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

        private void WriteNormal(TextWriter w, TVector n)
        {
            var normalData = _extractNormal(n);
            w.Write("normal ");
            WriteTripleFloat(w, normalData);
            w.WriteLine();
        }
    }
}
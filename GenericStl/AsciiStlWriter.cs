using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Text;

namespace GenericStl
{
    [SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes")]
    public class AsciiStlWriter<TTriangle, TVertex, TNormal> : StlWriterBase<TTriangle, TVertex, TNormal>
    {
        public AsciiStlWriter(Func<TTriangle, Tuple<TVertex, TVertex, TVertex, TNormal>> extractTriangle, Func<TVertex, Tuple<float, float, float>> extractVertex, Func<TNormal, Tuple<float, float, float>> extractNormal) 
            : base(extractTriangle, extractVertex, extractNormal)
        {
        }

        public AsciiStlWriter(IDataStructureExtractor<TTriangle, TVertex, TNormal> extractor)
            : base(extractor)
        {
            
        }

        public override void WriteToFile(string fileName, IEnumerable<TTriangle> triangles)
        {
            if (triangles == null)
            {
                throw new ArgumentNullException("triangles");
            }

            using (var fs = File.CreateText(fileName))
            {
                WriteTo(fs, triangles);
            }
        }

        public override void WriteToStream(Stream s, IEnumerable<TTriangle> triangles)
        {
            if (s == null)
            {
                throw new ArgumentNullException("s");
            }

            if (triangles == null)
            {
                throw new ArgumentNullException("triangles");
            }

            using (var w = new StreamWriter(s, new UTF8Encoding(false, true), 1024, true))
            {
                WriteTo(w, triangles);
            }
        }

        public string Write(IEnumerable<TTriangle> triangles)
        {
            using (var w = new StringWriter(CultureInfo.InvariantCulture))
            {
                WriteTo(w, triangles);
                return w.ToString();
            }
        }

        private void WriteTo(TextWriter w, IEnumerable<TTriangle> triangles)
        {
            Debug.Assert(w != null, "w != null");

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
            w.Write(t.Item1.ToString("r", CultureInfo.InvariantCulture));
            w.Write(" ");
            w.Write(t.Item2.ToString("r", CultureInfo.InvariantCulture));
            w.Write(" ");
            w.Write(t.Item3.ToString("r", CultureInfo.InvariantCulture));
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
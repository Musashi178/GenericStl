using System;
using System.Collections.Generic;
using System.IO;

namespace GenericStl
{
    public class BinaryStlWriter<TTriangle, TNormal, TVertex> : StlWriterBase<TTriangle, TNormal, TVertex>
    {
        private const int HeaderLengthInByte = 80;

        public BinaryStlWriter(Func<TTriangle, Tuple<TVertex, TVertex, TVertex, TNormal>> extractTriangle, Func<TVertex, Tuple<float, float, float>> extractVertex, Func<TNormal, Tuple<float, float, float>> extractNormal) : base(extractTriangle, extractVertex, extractNormal)
        {
        }

        public override void WriteFile(IEnumerable<TTriangle> data, string filename)
        {
            WriteFile(data, null, filename);
        }

        public void WriteFile(IEnumerable<TTriangle> data, byte[] header, string filename)
        {
            using (var fs = File.Create(filename))
            {
                Write(fs, data, header);
            }
        }

        public byte[] Write(IEnumerable<TTriangle> triangles, byte[] header = null)
        {
            using (var s = new MemoryStream())
            {
                Write(s, triangles, header);

                return s.ToArray();
            }
        }

        private void Write(Stream s, IEnumerable<TTriangle> triangles, byte[] header = null)
        {
            if (header != null && header.Length != 80)
            {
                throw new ArgumentException("header must have a size of 80 bytes.", "header");
            }

            using (var w = new BinaryWriter(s))
            {
                WriteHeader(w, header ?? new byte[HeaderLengthInByte]);
                WriteLength(w, 0);

                var length = 0;

                foreach (var triangle in triangles)
                {
                    length++;

                    WriteTriangle(w, triangle);
                }

                s.Seek(HeaderLengthInByte, SeekOrigin.Begin);
                WriteLength(w, length);
            }
        }

        private void WriteTriangle(BinaryWriter w, TTriangle t)
        {
            var triangleData = ExtractTriangle(t);

            WriteNormal(w, triangleData.Item4);
            WriteVertex(w, triangleData.Item1);
            WriteVertex(w, triangleData.Item2);
            WriteVertex(w, triangleData.Item3);

            w.Write(new byte[2]);
        }

        private void WriteVertex(BinaryWriter w, TVertex v)
        {
            var vertexData = ExtractVertex(v);

            WriteTriple(w, vertexData);
        }

        private static void WriteTriple(BinaryWriter w, Tuple<float, float, float> t)
        {
            w.Write(t.Item1);
            w.Write(t.Item2);
            w.Write(t.Item3);
        }

        private void WriteNormal(BinaryWriter w, TNormal n)
        {
            var normalData = ExtractNormal(n);

            WriteTriple(w, normalData);
        }

        private static void WriteLength(BinaryWriter w, int length)
        {
            w.Write(length);
        }

        private static void WriteHeader(BinaryWriter w, byte[] hdr)
        {
            w.Write(hdr);
        }
    }
}
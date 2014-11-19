using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;

namespace GenericStl
{
    [SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes")]
    public class BinaryStlWriter<TTriangle, TVertex, TNormal> : StlWriterBase<TTriangle, TVertex, TNormal>
    {
        private const int HeaderLengthInByte = 80;

        public BinaryStlWriter(Func<TTriangle, Tuple<TVertex, TVertex, TVertex, TNormal>> extractTriangle, Func<TVertex, Tuple<float, float, float>> extractVertex, Func<TNormal, Tuple<float, float, float>> extractNormal) : base(extractTriangle, extractVertex, extractNormal)
        {
        }

        public BinaryStlWriter(IDataStructureExtractor<TTriangle, TVertex, TNormal> extractor)
            : base(extractor)
        {
        }


        public override void WriteToFile(string fileName, IEnumerable<TTriangle> triangles)
        {
            WriteToFile(fileName, triangles, null);
        }

        public override void WriteToStream(Stream s, IEnumerable<TTriangle> triangles)
        {
            WriteToStream(s, triangles, null);
        }

        public void WriteToFile(string fileName, IEnumerable<TTriangle> triangles, byte[] header)
        {
            using (var fs = File.Create(fileName))
            {
                WriteToStream(fs, triangles, header);
            }
        }

        public byte[] Write(IEnumerable<TTriangle> triangles)
        {
            return Write(triangles, null);
        }

        public byte[] Write(IEnumerable<TTriangle> triangles, byte[] header)
        {
            using (var s = new MemoryStream())
            {
                WriteToStream(s, triangles, header);
                return s.ToArray();
            }
        }

        public void WriteToStream(Stream s, IEnumerable<TTriangle> triangles, byte[] header)
        {
            if (triangles == null)
            {
                throw new ArgumentNullException("triangles");
            }

            var hdr = PrepareHeader(header);

            var w = new BinaryWriter(s, new UTF8Encoding(false, true)); // do not dispose this reader as it would dispose the stream
            WriteHeader(w, hdr);
            WriteLength(w, 0);

            var length = 0;

            foreach (var triangle in triangles)
            {
                length++;

                WriteTriangle(w, triangle);
            }

            s.Seek(HeaderLengthInByte, SeekOrigin.Begin);
            WriteLength(w, length);
            w.Flush();
        }

        private static byte[] PrepareHeader(byte[] header)
        {
            if (header == null)
            {
                return new byte[HeaderLengthInByte];
            }

            if (header.Length == HeaderLengthInByte)
            {
                return header;
            }

            var newHdr = new byte[HeaderLengthInByte];

            if (header.Length < HeaderLengthInByte)
            {
                Array.Copy(header, newHdr, header.Length);
            }
            else if (header.Length > HeaderLengthInByte)
            {
                Array.Copy(header, newHdr, newHdr.Length);
            }

            return newHdr;
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
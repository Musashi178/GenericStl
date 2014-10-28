using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using MoreLinq;

namespace GenericStl
{
    public class AsciiStlReader<TTriangle, TVector, TVertex>
    {
        private readonly Func<float, float, float, TVertex> _createVertex;
        private readonly Func<float, float, float, TVector> _createNormal;
        private readonly Func<TVertex, TVertex, TVertex, TVector, TTriangle> _createTriangle;
        private readonly Func<string, float> _parseFloat;

        public AsciiStlReader(Func<TVertex, TVertex, TVertex, TVector, TTriangle> createTriangle, Func<float, float, float, TVertex> createVertex, Func<float, float, float, TVector> createNormal)
        {

            this._createTriangle = createTriangle;
            this._createNormal = createNormal;
            this._createVertex = createVertex;

            this._parseFloat = new Func<string, float>(f => float.Parse(f, CultureInfo.InvariantCulture));
        }

        public IEnumerable<TTriangle> Read(string[] fileContent)
        {

            return fileContent.Select(l => l.Trim())
                .Where(l => !string.IsNullOrEmpty(l))
                .SkipWhile(l => l.StartsWith("solid", StringComparison.InvariantCultureIgnoreCase))
                .TakeWhile(l => !l.StartsWith("endsolid", StringComparison.InvariantCultureIgnoreCase))
                .Split(l => string.Equals(l, "endfacet", StringComparison.InvariantCultureIgnoreCase))
                .Select(ToTriangle);
        }

        private TTriangle ToTriangle(IEnumerable<string> triangleChunk)
        {
            var chunk = triangleChunk.ToArray();

            var normal = GetNormal(chunk[0]);

            var v1 = GetVertex(chunk[2]);
            var v2 = GetVertex(chunk[3]);
            var v3 = GetVertex(chunk[4]);

            return _createTriangle(v1, v2, v3, normal);
        }

        private TVector GetNormal(string line)
        {
            var segs = line.Split(' ');

            Debug.Assert(string.Equals(segs[0], "facet", StringComparison.InvariantCultureIgnoreCase));
            Debug.Assert(string.Equals(segs[1], "normal", StringComparison.InvariantCultureIgnoreCase));

            return _createNormal(_parseFloat(segs[2]), _parseFloat(segs[3]), _parseFloat(segs[4]));  
        }

        

        private TVertex GetVertex(string line)
        {
            var segs = line.Split(' ');

            Debug.Assert(string.Equals(segs[0], "vertex", StringComparison.InvariantCultureIgnoreCase));
         
            return _createVertex(_parseFloat(segs[1]), _parseFloat(segs[2]), _parseFloat(segs[3]));  
        }

    }

    
}

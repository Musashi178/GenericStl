using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericStl
{
    public class AsciiStlReader<TTriangle, TVector, TVertex>
    {
        private readonly Func<float, float, float, TVertex> _createVertex;
        private readonly Func<float, float, float, TVector> _createNormal;
        private readonly Func<TVertex, TVertex, TVertex, TVector, TTriangle> _createTriangle;

        public AsciiStlReader(Func<TVertex, TVertex, TVertex, TVector, TTriangle> createTriangle, Func<float, float, float, TVertex> createVertex, Func<float, float, float, TVector> createNormal)
        {

            this._createTriangle = createTriangle;
            this._createNormal = createNormal;
            this._createVertex = createVertex;
        }

        public TTriangle[] Read(string[] fileContent)
        {
            var stlContent = string.Join("\n", fileContent.Select(l => l.Trim())
                .SkipWhile(l => l.StartsWith("solid", StringComparison.InvariantCultureIgnoreCase))
                .TakeWhile(l => !l.StartsWith("endsolid", StringComparison.InvariantCultureIgnoreCase))
                .ToArray() );

            System.Console.WriteLine(stlContent);

            return null;
                    
        }
    }
}

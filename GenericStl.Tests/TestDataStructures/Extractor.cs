using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericStl.Tests.TestDataStructures
{
    public class Extractor : IDataStructureExtractor<Triangle, Vertex, Normal>
    {
        public Tuple<Vertex, Vertex, Vertex, Normal> ExtractTriangle(Triangle t)
        {
            return TestDataStructureHelpers.ExtractTriangle(t);
        }

        public Tuple<float, float, float> ExtractVertex(Vertex vertex)
        {
            return TestDataStructureHelpers.ExtractVertex(vertex);
        }

        public Tuple<float, float, float> ExtractNormal(Normal normal)
        {
            return TestDataStructureHelpers.ExtractNormal(normal);
        }
    }
}

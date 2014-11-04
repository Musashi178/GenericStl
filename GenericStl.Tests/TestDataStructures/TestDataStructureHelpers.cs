using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericStl.Tests.TestDataStructures
{
    public static class TestDataStructureHelpers
    {
        public static Vertex CreateVertex(float x, float y, float z)
        {
            return new Vertex(x, y, z);
        }

        public static Normal CreateNormal(float x, float y, float z)
        {
            return new Normal(x, y, z);
        }

        public static Triangle CreateTriangle(Vertex a, Vertex b, Vertex c, Normal n)
        {
            return new Triangle(a, b, c, n);
        }

        public static Tuple<Vertex, Vertex, Vertex, Normal> ExtractTriangle(Triangle t)
        {
            return new Tuple<Vertex, Vertex, Vertex, Normal>(t.V1, t.V2, t.V3, t.N);
        }

        public static Tuple<float, float, float> ExtractVertex(Vertex v)
        {
            return new Tuple<float, float, float>(v.X, v.Y, v.Z);
        }

        public static Tuple<float, float, float> ExtractNormal(Normal n)
        {
            return new Tuple<float, float, float>(n.X, n.Y, n.Z);
        }
    }
}

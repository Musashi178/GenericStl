using System;
using GenericStl.Tests.TestDataStructures;

namespace GenericStl.Tests
{
    public static class TestHelpers
    {
        public static readonly Triangle[] BlockExpectedResult =
        {
            new Triangle(new Vertex(.0f, 100.0f, 100.0f), new Vertex(.0f, 100.0f, .0f), new Vertex(.0f, .0f, 100.0f), new Normal(-1.0f, .0f, .0f)),
            new Triangle(new Vertex(.0f, .0f, 100.0f), new Vertex(.0f, 100.0f, .0f), new Vertex(.0f, 0.0f, .0f), new Normal(-1.0f, .0f, .0f)),
            new Triangle(new Vertex(100.0f, 100.0f, 100.0f), new Vertex(.0f, 100.0f, 100.0f), new Vertex(100.0f, .0f, 100.0f), new Normal(0.0f, .0f, 1.0f)),
            new Triangle(new Vertex(100.0f, .0f, 100.0f), new Vertex(.0f, 100.0f, 100.0f), new Vertex(0.0f, .0f, 100.0f), new Normal(0.0f, .0f, 1.0f)),
            new Triangle(new Vertex(100.0f, 100.0f, .0f), new Vertex(100.0f, 100.0f, 100.0f), new Vertex(100.0f, .0f, .0f), new Normal(1.0f, .0f, .0f)),
            new Triangle(new Vertex(100.0f, .0f, .0f), new Vertex(100.0f, 100.0f, 100.0f), new Vertex(100.0f, .0f, 100.0f), new Normal(1.0f, .0f, .0f)),
            new Triangle(new Vertex(.0f, 100.0f, .0f), new Vertex(100.0f, 100.0f, .0f), new Vertex(.0f, .0f, .0f), new Normal(.0f, .0f, -1.0f)),
            new Triangle(new Vertex(.0f, .0f, .0f), new Vertex(100.0f, 100.0f, .0f), new Vertex(100.0f, .0f, .0f), new Normal(.0f, .0f, -1.0f)),
            new Triangle(new Vertex(100.0f, 100.0f, 100.0f), new Vertex(100.0f, 100.0f, .0f), new Vertex(.0f, 100.0f, 100.0f), new Normal(.0f, 1.0f, .0f)),
            new Triangle(new Vertex(.0f, 100.0f, 100.0f), new Vertex(100.0f, 100.0f, .0f), new Vertex(.0f, 100.0f, 0.0f), new Normal(.0f, 1.0f, .0f)),
            new Triangle(new Vertex(100.0f, .0f, .0f), new Vertex(100.0f, .0f, 100.0f), new Vertex(.0f, .0f, .0f), new Normal(.0f, -1.0f, .0f)),
            new Triangle(new Vertex(.0f, .0f, .0f), new Vertex(100.0f, .0f, 100.0f), new Vertex(.0f, .0f, 100.0f), new Normal(.0f, -1.0f, .0f)),
        };

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
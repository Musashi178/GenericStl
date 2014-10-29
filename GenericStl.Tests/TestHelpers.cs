using System;
using Vertex = System.Tuple<float, float, float>;
using Normal = System.Tuple<float, float, float>;
using Triangle = System.Tuple<System.Tuple<float, float, float>, System.Tuple<float, float, float>, System.Tuple<float, float, float>, System.Tuple<float, float, float>>;

namespace GenericStl.Tests
{
    public static class TestHelpers
    {
        public static Tuple<float, float, float> CreateVertex(float x, float y, float z)
        {
            return new Tuple<float, float, float>(x, y, z);
        }

        public static Tuple<float, float, float> CreateNormal(float x, float y, float z)
        {
            return new Tuple<float, float, float>(x, y, z);
        }

        public static Tuple<Tuple<float, float, float>, Tuple<float, float, float>, Tuple<float, float, float>, Tuple<float, float, float>> CreateTriangle(Tuple<float, float, float> a, Tuple<float, float, float> b, Tuple<float, float, float> c, Tuple<float, float, float> n)
        {
            return new Tuple<Tuple<float, float, float>, Tuple<float, float, float>, Tuple<float, float, float>, Tuple<float, float, float>>(a, b, c, n);
        }

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
    }
}
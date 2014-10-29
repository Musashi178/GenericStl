using Vertex = System.Tuple<float, float, float>;
using Normal = System.Tuple<float, float, float>;
using Triangle = System.Tuple<System.Tuple<float, float, float>, System.Tuple<float, float, float>, System.Tuple<float, float, float>, System.Tuple<float, float, float>>;

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

        public static Vertex CreateNormal(float x, float y, float z)
        {
            return new Vertex(x, y, z);
        }

        public static Triangle CreateTriangle(Vertex a, Vertex b, Vertex c, Vertex n)
        {
            return new Triangle(a, b, c, n);
        }

        public static Vertex ExtractVertex(Vertex v)
        {
            return v;
        }

        public static Vertex ExtractNormal(Vertex n)
        {
            return n;
        }

        public static Triangle ExtractTriangle(Triangle t)
        {
            return t;
        }
    }
}
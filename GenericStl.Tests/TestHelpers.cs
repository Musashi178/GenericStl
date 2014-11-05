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
    }
}
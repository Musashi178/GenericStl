namespace GenericStl.Tests.TestDataStructures
{
    public class Creator : IDataStructureCreator<Triangle, Vertex, Normal>
    {
        public Triangle CreateTriangle(Vertex v1, Vertex v2, Vertex v3, Normal n)
        {
            return TestDataStructureHelpers.CreateTriangle(v1, v2, v3, n);
        }

        public Normal CreateNormal(float x, float y, float z)
        {
            return TestDataStructureHelpers.CreateNormal(x,y,z);
        }

        public Vertex CreateVertex(float x, float y, float z)
        {
            return TestDataStructureHelpers.CreateVertex(x, y, z);
        }
    }
}
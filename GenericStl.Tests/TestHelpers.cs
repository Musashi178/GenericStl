using System;

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
    }
}
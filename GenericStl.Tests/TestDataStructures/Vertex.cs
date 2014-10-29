using System;

namespace GenericStl.Tests.TestDataStructures
{
    public class Vertex : IEquatable<Vertex>
    {
        private readonly float _x;
        private readonly float _y;
        private readonly float _z;

        public Vertex(float x, float y, float z)
        {
            _x = x;
            _y = y;
            _z = z;
        }

        public float X
        {
            get { return _x; }
        }

        public float Y
        {
            get { return _y; }
        }

        public float Z
        {
            get { return _z; }
        }

        public bool Equals(Vertex other)
        {
            if (other == null)
            {
                return false;
            }

            return Math.Abs(other.X - X) < float.Epsilon && Math.Abs(other.Y - Y) < float.Epsilon && Math.Abs(other.Z - Z) < float.Epsilon;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = X.GetHashCode();
                hashCode = (hashCode*397) ^ Y.GetHashCode();
                hashCode = (hashCode*397) ^ Z.GetHashCode();
                return hashCode;
            }
        }

        public override bool Equals(object other)
        {
            return Equals(other as Vertex);
        }
    }
}
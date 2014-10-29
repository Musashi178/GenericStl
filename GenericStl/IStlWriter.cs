using System.Collections.Generic;
using System.IO;

namespace GenericStl
{
    public interface IStlWriter<TTriangle>
    {
        void WriteToFile(string fileName, IEnumerable<TTriangle> data);
        void WriteToStream(Stream s, IEnumerable<TTriangle> triangles);
    }
}
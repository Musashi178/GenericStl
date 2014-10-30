using System.Collections.Generic;
using System.IO;

namespace GenericStl
{
    public interface IStlReader<TTriangle>
    {
        IEnumerable<TTriangle> ReadFromFile(string fileName);

        IEnumerable<TTriangle> ReadFromStream(Stream s);
    }
}
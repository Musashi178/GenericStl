using System.Collections.Generic;

namespace GenericStl
{
    public interface IStlReader<TTriangle>
    {
        IEnumerable<TTriangle> ReadFile(string fileName);
    }
}
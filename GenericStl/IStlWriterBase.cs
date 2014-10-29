using System.Collections.Generic;

namespace GenericStl
{
    public interface IStlWriterBase<TTriangle>
    {
        void WriteFile(IEnumerable<TTriangle> data, string fileName);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericStl
{
    public interface IDataReaderFactory<TTriangle, TVertex, TNormal>
    {
        TTriangle CreateTriangle(TVertex v1, TVertex v2, TVertex v3, TNormal n);
        TNormal CreateNormal(float x, float y, float z);
        TVertex CreateVertex(float x, float y, float z);
    }
}

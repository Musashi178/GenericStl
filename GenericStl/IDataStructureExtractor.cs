using System;

namespace GenericStl
{
    public interface IDataStructureExtractor<TTriangle, TVertex, TNormal>
    {
        Tuple<TVertex, TVertex, TVertex, TNormal> ExtractTriangle(TTriangle triangle1);
        Tuple<float, float, float> ExtractVertex(TVertex vertex);
        Tuple<float, float, float> ExtractNormal(TNormal normal);
    }
}
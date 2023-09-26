using Basics;
using UnityEngine;

namespace Extensions
{
    public static class TransformExtensions
    {
        public static Transform LocalPositionBy(this Transform tr, Vector3 graphPosition, FunctionLibrary.Function function)
        {
            tr.localPosition = function(graphPosition.x, graphPosition.z, Time.time);
            return tr;
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Basics
{
    public class GraphComponent : MonoBehaviour
    {
        [SerializeField] private Transform _pointTransformPrefab;
        [SerializeField, Range(10, 100)] private int _resolution = 10;

        private List<Transform> _points = new();

        private void Start()
        {
            this._points = Enumerable.Range(0, this._resolution).ToList().Select(InstantiatePointFrom).ToList();
        }

        private Transform InstantiatePointFrom(int index)
        {
            var positionOffset = 2.0f / this._resolution;
            var newPosition = new Vector3((index + 0.5f) * positionOffset - 1.0f, 0.0f, 0.0f);

            return InstantiatePointFrom(index, newPosition, positionOffset);
        }

        private Transform InstantiatePointFrom(int index, Vector3 newPosition, float positionOffset)
        {
            var newPoint = Instantiate(this._pointTransformPrefab, this.transform, false);
            newPoint.localPosition = newPosition;
            newPoint.localScale = Vector3.one * positionOffset;
            newPoint.name = $"{this._pointTransformPrefab.name}_{index}";

            return newPoint;
        }

        private void Update()
        {
            this._points.ForEach(UpdatePositionOf);
        }

        private static void UpdatePositionOf(Transform transform)
        {
            var localPosition = transform.localPosition;
            localPosition.y = Mathf.Sin(2 * (localPosition.x + Time.time));
            transform.localPosition = localPosition;
        }
    }
}

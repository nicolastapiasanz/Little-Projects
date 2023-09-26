using System;
using System.Collections.Generic;
using System.Linq;
using Extensions;
using UnityEngine;

namespace Basics
{
    public class GraphComponent : MonoBehaviour
    {
        [SerializeField] private Transform _pointTransformPrefab;
        [SerializeField, Range(10, 100)] private int _resolution = 10;
        [SerializeField] private FunctionLibrary.FunctionName _function;

        private List<Transform> _points = new();

        private float PositionOffset => 2.0f / this._resolution;
        

        private void Start()
        {
            this._points = Enumerable.Range(0, this._resolution * this._resolution).
                Select(index => InstantiatePointFrom(index % this._resolution, index / this._resolution))
                .ToList();
        }

        private Transform InstantiatePointFrom(int x, int z)
        {
            var newPosition = GraphPositionFrom(x, 0, z);
            return InstantiatePointFrom($"{x}_{z}", newPosition);
        }

        private Vector3 GraphPositionFrom(int x, int y, int z)
        {
            var positionOffset = this.PositionOffset;
            return new Vector3((x + 0.5f) * positionOffset - 1.0f, y, (z + 0.5f) * positionOffset - 1.0f);
        }

        private Transform InstantiatePointFrom(string index, Vector3 newPosition)
        {
            var newPoint = Instantiate(this._pointTransformPrefab, this.transform, false);
            newPoint.localPosition = newPosition;
            newPoint.localScale = Vector3.one * this.PositionOffset;
            newPoint.name = $"{this._pointTransformPrefab.name}_{index}";

            return newPoint;
        }

        private void Update()
        {
            var function = FunctionLibrary.FunctionFrom(this._function);
            this._points = this._points.Select((x, i) => LocalPositionBy(i, x, function)).ToList();
        }

        private Transform LocalPositionBy(int i, Transform x, FunctionLibrary.Function function)
        {
            var pointPosition = GraphPositionFrom(i % this._resolution, 0, i / this._resolution);
            return x.LocalPositionBy(pointPosition, function);
        }
    }
}

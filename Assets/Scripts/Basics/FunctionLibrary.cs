using UnityEngine;

namespace Basics
{
    public static class FunctionLibrary
    {
        public delegate Vector3 Function(float u, float v, float time);

        private static readonly Function[] Functions = { SinV3, MultiSinV3, RippleV3, Sphere, Torus };

        public enum FunctionName
        {
            Sin,
            MultiSin,
            Ripple,
            Sphere,
            Torus
        }

        public static Function FunctionFrom(FunctionName functionName)
        {
            return Functions[(int)functionName];
        }

        public static float Sin(float u, float v, float time)
        {
            return Mathf.Sin(Mathf.PI * (u + v + time));
        }

        public static float MultiSin(float u, float v, float time)
        {
            var y = Mathf.Sin(Mathf.PI * (u + time * 0.5f));
            y += Mathf.Sin(2.0f * Mathf.PI * (v + time)) * 0.5f;
            y += Mathf.Sin(Mathf.PI * (u + v + 0.25f * time));
            return y * (1.0f / 2.5f);
        }

        public static float Ripple(float u, float v, float time)
        {
            var d = Mathf.Sqrt(u * u + v * v);
            var y = Mathf.Sin(Mathf.PI * (4.0f * d - time));
            return y / (1.0f + 10.0f * d);
        }

        public static Vector3 SinV3(float u, float v, float time)
        {
            return new Vector3(
                u,
                Mathf.Sin(Mathf.PI * (u + v + time)),
                v);
        }

        public static Vector3 MultiSinV3(float u, float v, float time)
        {
            var y = Mathf.Sin(Mathf.PI * (u + time * 0.5f));
            y += Mathf.Sin(2.0f * Mathf.PI * (v + time)) * 0.5f;
            y += Mathf.Sin(Mathf.PI * (u + v + 0.25f * time));
            y *= (1.0f / 2.5f);
            return new Vector3(
                u,
                y,
                v);
        }

        public static Vector3 RippleV3(float u, float v, float time)
        {
            var d = Mathf.Sqrt(u * u + v * v);
            var y = Mathf.Sin(Mathf.PI * (4.0f * d - time));
            y /= (1.0f + 10.0f * d);
            return new Vector3(
                u,
                y,
                v);
        }

        public static Vector3 Sphere(float u, float v, float time)
        {
            var r = 0.9f + 0.1f * Mathf.Sin(Mathf.PI * (6.0f * u + 4.0f * v + time));
            var s = r * Mathf.Cos(0.5f * Mathf.PI * v);
            return new Vector3(
                s * Mathf.Sin(Mathf.PI * u),
                r * Mathf.Sin(Mathf.PI * 0.5f * v),
                s * Mathf.Cos(Mathf.PI * u)
            );
        }

        public static Vector3 Torus(float u, float v, float time)
        {
            var r1 = 0.7f + 0.1f * Mathf.Sin(Mathf.PI * (6.0f * u + 0.5f * time));
            var r2 = 0.15f + 0.05f * Mathf.Sin(Mathf.PI * (8.0f * u + 4.0f * v + 2.0f * time));
            var s = r1 + r2 * Mathf.Cos(Mathf.PI * v);
            return new Vector3(
                s * Mathf.Sin(Mathf.PI * u),
                r2 * Mathf.Sin(Mathf.PI * v),
                s * Mathf.Cos(Mathf.PI * u)
            );
        }
    }
}

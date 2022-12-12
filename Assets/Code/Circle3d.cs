using UnityEngine;

namespace VineGeneration
{
    /// <summary>
    /// Generates points along a circle in 3D space
    /// Restricted to x/z plane at y = 0
    /// </summary>
    public class Circle3d
    {
        public static Vector3[] GetCircleVertices(float radius, int divisions)
        {
            float angle = 360.0f / divisions;

            Vector3[] vertices = new Vector3[divisions];

            for (int j = 0; j < divisions; j++)
            {
                vertices[j] = GetPoint(radius, j * angle, 0);
            }

            return vertices;
        }

        public static Vector3 GetPoint(float radius, float angle, float z)
        {
            float myCos = Mathf.Cos(angle * Mathf.Deg2Rad) * radius;
            float mySin = Mathf.Sin(angle * Mathf.Deg2Rad) * radius;

            return new Vector3(myCos, z, mySin);
        }
    }
}
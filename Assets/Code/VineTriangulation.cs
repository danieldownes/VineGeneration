using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class VineTriangulation : MonoBehaviour
{
    public MeshFilter Filter;

    public SplineContainer Spline;
    public float LengthSubdivisionQuality;
    public int RadialSubdivisions;
    public float Radius;

    private List<Vector3> vertices;
    private List<int> triangles;


    public Mesh Generate()
    {
        vertices = new List<Vector3>();
        triangles = new List<int>();

        for (float l = 0; l < 1; l += LengthSubdivisionQuality)
        {
            Vector3 p = Spline.EvaluatePosition(l);

            Vector3[] v = Circle3d.GetCircleVertices(Radius, RadialSubdivisions);

            for (int i = 0; i < v.Length; i++)
                RotatePointAroundPivot(v[i], p, Spline.EvaluatePosition(l));

            vertices.AddRange(v);
        }

        int heightSegments = Mathf.RoundToInt(1 / LengthSubdivisionQuality);
        int radialSegments = RadialSubdivisions;

        for (int j = 1; j <= heightSegments; j++)
        {
            for (int i = 1; i <= radialSegments; i++)
            {
                //TODO: Quad Triangulation 
            }
        }

        Mesh mesh = new Mesh();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        return mesh;
    }

    public Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles)
    {
        return Quaternion.Euler(angles) * (point - pivot) + pivot;
    }

}

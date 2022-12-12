using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class VineTriangulation
{
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
            addStack(l);
        }
        addStack(1);

        // Connect vertices
        int heightSegments = Mathf.RoundToInt(1 / LengthSubdivisionQuality);
        int radialSegments = RadialSubdivisions;

        for (int y = 1; y <= heightSegments; y++)
        {
            for (int x = 1; x < radialSegments; x++)
            {
                createQuad(
                    radialSegments * y + x,
                    radialSegments * (y - 1) + x,
                    radialSegments * (y - 1) + (x - 1),
                    radialSegments * y + (x - 1));

            }
        }

        Mesh mesh = new Mesh();
        mesh.name = "ProceduralVine";
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        return mesh;
    }

    private void createQuad(int a, int b, int c, int d)
    {
        triangles.Add(b); triangles.Add(c); triangles.Add(a);
        triangles.Add(c); triangles.Add(d); triangles.Add(a);
    }

    private void addStack(float l)
    {
        Vector3 p = Spline.EvaluatePosition(l);
        Vector3[] v = Circle3d.GetCircleVertices(Radius, RadialSubdivisions, p.y);

        vertices.AddRange(v);
    }

    public Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles)
    {
        return Quaternion.Euler(angles) * (point - pivot) + pivot;
    }
}

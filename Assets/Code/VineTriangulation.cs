using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class VineTriangulation
{
    public SplineContainer Spline;
    public float LengthSubdivisionQuality;
    public int Slices;
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
        int stacks = Mathf.RoundToInt(1 / LengthSubdivisionQuality);
        int slices = Slices;

        for (int y = 1; y <= stacks; y++)
        {
            for (int x = 1; x < slices; x++)
            {
                createQuad(
                    slices * y + x,
                    slices * (y - 1) + x,
                    slices * (y - 1) + (x - 1),
                    slices * y + (x - 1));

            }

            createQuad(
                slices * y,
                slices * (y - 1),
                slices * y - 1,
                slices * (y + 1) - 1);
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
        Vector3[] v = Circle3d.GetCircleVertices(Radius, Slices);

        for (int i = 0; i < v.Length; i++)
        {
            v[i] += p;
            v[i] = RotatePointAroundPivot(v[i], p, Quaternion.FromToRotation(Vector3.up, Spline.EvaluateTangent(l)));
        }

        vertices.AddRange(v);
    }

    public Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Quaternion angles)
    {
        return angles * (point - pivot) + pivot;
    }
}

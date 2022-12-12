using UnityEngine;
using UnityEngine.Splines;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
[ExecuteInEditMode]
public class Vine : MonoBehaviour
{
    [Range(2, 100)]
    public float Age = 12;

    [Range(0, 4)]
    public float Roughness = 4;

    [Range(0.5f, 10)]
    public float Thickness = 0.5f;

    private MeshFilter filter;
    [SerializeField] private VineTriangulation vineTriangulation;
    [SerializeField] private SplineContainer splineContainer;

    void Start()
    {
        filter = GetComponent<MeshFilter>();

        Rebuild();
    }

    public void Rebuild()
    {
        if (filter.sharedMesh != null)
        {
            if (Application.isPlaying)
                Destroy(filter.sharedMesh);
            else
                DestroyImmediate(filter.sharedMesh);
        }

        vineTriangulation = new VineTriangulation();

        SplineDistribution.Distribute(splineContainer, Age,
            3 + Mathf.RoundToInt(Age / 2), Roughness, Vector3.up);

        vineTriangulation.Spline = splineContainer;
        vineTriangulation.LengthSubdivisionQuality = 1 / (Age * 2f); // 1 / 4; // Mathf.RoundToInt(Age * Roughness);
        vineTriangulation.Slices = 12;
        vineTriangulation.Radius = Thickness;

        filter.sharedMesh = vineTriangulation.Generate();
    }

}

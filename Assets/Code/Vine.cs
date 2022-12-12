using UnityEngine;
using UnityEngine.Splines;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
[ExecuteInEditMode]
public class Vine : MonoBehaviour
{
    public float Age = 12;
    public float Roughness = 4;
    public float Thickness = 0.5f;

    private MeshFilter filter;
    [SerializeField] private VineTriangulation vineTriangulation;
    [SerializeField] private SplineContainer splineContainer;

    void Start()
    {
        filter = GetComponent<MeshFilter>();
        vineTriangulation = new VineTriangulation();

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

        SplineDistribution.Distribute(splineContainer, Age, Mathf.RoundToInt(Age / 2), Vector3.up);

        vineTriangulation.Spline = splineContainer;
        vineTriangulation.LengthSubdivisionQuality = 0.1f;
        vineTriangulation.RadialSubdivisions = 6;
        vineTriangulation.Radius = Thickness;

        filter.sharedMesh = vineTriangulation.Generate();
    }

}

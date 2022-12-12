using UnityEngine;
using UnityEngine.Splines;

/// <summary>
/// Generate points along a vector that represent the stroke of a vine.
/// </summary>
public class SplineDistribution : MonoBehaviour
{
    [SerializeField] private SplineContainer splineContainer;
    public Vector3 AlongAxis;
    public int Length;
    public int ControlPointCount;

    private void Awake()
    {
        if (ControlPointCount < 2)
            return;

        float stepSize = Length / ControlPointCount;

        Spline spline = splineContainer.AddSpline();
        spline.Clear();

        Vector3 pos;
        for (int i = 0; i < ControlPointCount; i++)
        {
            pos = AlongAxis.normalized * stepSize * i;

            // Add some perpendicular random offset / roughness
            // Offset is reduced the higher we go
            float offset = 1 / ControlPointCount * i;
            pos += Vector3.Cross(Random.insideUnitSphere * offset, AlongAxis).normalized;

            spline.Add(new BezierKnot(pos), TangentMode.AutoSmooth);
        }
    }
}

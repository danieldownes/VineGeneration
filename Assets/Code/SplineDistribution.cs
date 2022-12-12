using UnityEngine;
using UnityEngine.Splines;

/// <summary>
/// Generate points along a vector that represent the stroke of a vine.
/// </summary>
public class SplineDistribution
{
    public static void Distribute(SplineContainer SplineContainer,
        float Length, int ControlPointCount, Vector3 AlongAxis)
    {
        if (ControlPointCount < 2)
            return;

        float stepSize = Length / ControlPointCount;

        while (SplineContainer.Splines.Count > 0)
            SplineContainer.RemoveSpline(SplineContainer.Splines[0]);

        Spline spline = SplineContainer.AddSpline();

        Vector3 pos;
        for (int i = 0; i < ControlPointCount; i++)
        {
            pos = AlongAxis.normalized * stepSize * i;

            // Add some perpendicular random offset / roughness
            // Offset is reduced the higher we go
            float offset = 1; // / ControlPointCount * i;
            pos += Vector3.Cross(Random.insideUnitSphere * offset, AlongAxis).normalized;

            spline.Add(new BezierKnot(pos), TangentMode.AutoSmooth);
        }
    }
}

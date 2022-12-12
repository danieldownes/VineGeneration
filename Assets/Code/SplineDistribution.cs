using UnityEngine;
using UnityEngine.Splines;

namespace VineGeneration
{
    /// <summary>
    /// Generate points along a vector that represent the stroke of a vine.
    /// </summary>
    public class SplineDistribution
    {
        public static void Distribute(SplineContainer SplineContainer,
            float Length, int ControlPointCount,
            float Roughness, Vector3 AlongAxis)
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
                float offset = 1 - ((float)i / ControlPointCount);
                offset *= Roughness;
                Vector3 radialOffset = Random.insideUnitSphere;
                radialOffset.Scale(new Vector3(offset, 0, offset));

                pos += Vector3.Cross(radialOffset, AlongAxis);

                spline.Add(new BezierKnot(pos), TangentMode.AutoSmooth);
            }
        }
    }
}
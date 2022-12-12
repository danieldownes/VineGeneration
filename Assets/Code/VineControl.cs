using UnityEngine;
using UnityEngine.Splines;

namespace VineGeneration
{
    /// <summary>
    /// Control class that uses VineTriangulation and SplineDistribution Models
    /// to generate a spline and related mesh with the provided fidelity config.
    /// </summary>
    public class VineControl
    {
        private VineTriangulation vineTriangulation;

        // Configs
        private const float sliceSubdivisionsQuality = 2f;
        private const float stackSubdivisionsQuality = 2f;

        public Mesh Generate(SplineContainer splineContainer, float Age, float Roughness, float Thickness)
        {
            vineTriangulation = new VineTriangulation();
            SplineDistribution.Distribute(splineContainer, Age,
                3 + Mathf.RoundToInt(Age / sliceSubdivisionsQuality), Roughness, Vector3.up);

            vineTriangulation.Spline = splineContainer;
            vineTriangulation.LengthSubdivisionQuality = 1 / (Age * stackSubdivisionsQuality);
            vineTriangulation.Slices = 12;
            vineTriangulation.Radius = Thickness;

            return vineTriangulation.Generate();
        }
    }
}

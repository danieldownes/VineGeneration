using UnityEngine;
using UnityEngine.Splines;

namespace VineGeneration
{
    /// <summary>
    /// Vine View class to handling changing of inspector and pass on
    /// to control, and then updates filter mesh with generated mesh.
    /// </summary>
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

        private VineControl control;
        [SerializeField] private SplineContainer splineContainer;

        void Start()
        {
            filter = GetComponent<MeshFilter>();

            Rebuild();
        }

        public void Rebuild()
        {
            var vineControl = new VineControl();

            if (filter.sharedMesh != null)
            {
                if (Application.isPlaying)
                    Destroy(filter.sharedMesh);
                else
                    DestroyImmediate(filter.sharedMesh);
            }

            filter.sharedMesh = vineControl.Generate(splineContainer, Age, Roughness, Thickness);
        }

    }
}
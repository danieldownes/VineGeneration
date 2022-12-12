using UnityEditor;
using VineGeneration;

[CustomEditor(typeof(Vine), true)]
public class VineEditor : Editor
{
    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();
        base.OnInspectorGUI();
        if (EditorGUI.EndChangeCheck())
        {
            var pm = target as Vine;
            pm.Rebuild();
        }
    }

}

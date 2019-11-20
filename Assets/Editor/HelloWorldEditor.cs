using UnityEditor;

[CustomEditor(typeof(HelloWorld))]
public class HelloWorldEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var script = target as HelloWorld;
        DrawDefaultInspector();
        if (script != null)
        {
            EditorGUILayout.LabelField("Custom Message", script.message);
        }
    }
}
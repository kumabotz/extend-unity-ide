using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(HelloWorld))]
public class HelloWorldEditor : Editor
{
    private bool visible;

    public override void OnInspectorGUI()
    {
        var script = target as HelloWorld;
        if (script == null)
        {
            return;
        }
        script.speed = EditorGUILayout.Slider("Speed", script.speed, 0, 10);
        script.target = EditorGUILayout.ObjectField("Target", script.target, typeof(HelloWorld), true) as HelloWorld;

        visible = EditorGUILayout.Foldout(visible, "Options");
        if (visible)
        {
            EditorGUI.indentLevel++;
            var props = new[] { "startPos", "target", "message" };
            foreach (var prop in props)
            {
                var sProp = serializedObject.FindProperty(prop);
                var guiContent = new GUIContent { text = sProp.displayName };
                EditorGUILayout.PropertyField(sProp, guiContent);
            }
            EditorGUI.indentLevel--;
        }

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("A Button");
        if (GUILayout.Button("Open Test Window"))
        {
            EditorWindow.GetWindow(typeof(TestWindow));
        }
        EditorGUILayout.EndHorizontal();
    }
}
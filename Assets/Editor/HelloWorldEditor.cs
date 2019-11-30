using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(HelloWorld))]
public class HelloWorldEditor : AbstractEditor
{
    private bool visible;
    protected override string title => "Test override title";
    protected override string description => "Test override description";

    public override void OnInspectorGUI()
    {
        var script = target as HelloWorld;
        if (script == null)
        {
            return;
        }
        base.OnInspectorGUI();

        EditorGUILayout.BeginVertical("box");
        EditorGUILayout.Space();
        script.speed = EditorGUILayout.Slider("Speed", script.speed, 0, 10);
        EditorGUILayout.Space();
        EditorGUILayout.EndVertical();

        EditorGUILayout.Space();
        EditorGUILayout.BeginVertical("box");
        EditorGUI.indentLevel++;
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
                DisplayPropError(sProp);
            }
            EditorGUI.indentLevel--;
        }
        EditorGUI.indentLevel--;
        EditorGUILayout.EndVertical();

        EditorGUILayout.Space();
        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField("A Button");
        if (GUILayout.Button("Open Test Window"))
        {
            EditorWindow.GetWindow(typeof(TestWindow));
        }
        EditorGUILayout.EndVertical();

        serializedObject.ApplyModifiedProperties();
    }

    private void DisplayPropError(SerializedProperty prop)
    {
        var empty = false;
        switch (prop.propertyType)
        {
            case SerializedPropertyType.String:
                empty = prop.stringValue == "";
                break;
            case SerializedPropertyType.ObjectReference:
                empty = prop.objectReferenceValue == null;
                break;
            case SerializedPropertyType.Vector3:
                empty = prop.vector3Value == Vector3.zero;
                break;
        }
        if (empty)
        {
            DisplayError(prop.displayName);
        }
    }

    private void DisplayError(string name)
    {
        var message = $"{name} field cannot be empty";
        EditorGUILayout.HelpBox(message, MessageType.Error);
    }
}
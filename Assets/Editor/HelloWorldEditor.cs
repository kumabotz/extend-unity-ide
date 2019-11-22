using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(HelloWorld))]
public class HelloWorldEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var script = target as HelloWorld;
        if (script == null)
        {
            return;
        }
        script.speed = EditorGUILayout.Slider("Speed", script.speed, 0, 10);
        script.target = EditorGUILayout.ObjectField("Target", script.target, typeof(HelloWorld), true) as HelloWorld;

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("A Button");
        if (GUILayout.Button("Click Me"))
        {
            Debug.Log(script.message);
        }
        EditorGUILayout.EndHorizontal();
        //DrawDefaultInspector();
        //EditorGUILayout.LabelField("Custom Message", script.message);
    }
}
using NUnit.Framework.Internal;
using UnityEditor;
using UnityEngine;

public class TestWindow : EditorWindow
{
    private GameObject currentSelection;

    [MenuItem("Window/Test Window")]
    private static void Init()
    {
        var window = GetWindow(typeof(TestWindow)) as TestWindow;
        if (window == null) return;
        var content = new GUIContent();
        var icon = new Texture2D(16, 16);
        content.text = "Test Window";
        content.image = icon;
        window.titleContent = content;
    }

    private void OnFocus()
    {
        currentSelection = Selection.activeGameObject;
    }

    private void OnLostFocus()
    {
        currentSelection = null;
    }

    private void OnGUI()
    {
        if (currentSelection != null)
        {
            EditorGUILayout.BeginVertical();
            EditorGUILayout.LabelField("Currently Selected GameObject");
            EditorGUILayout.LabelField(currentSelection.name);
            currentSelection.transform.position = EditorGUILayout.Vector3Field("At Position", currentSelection.transform.position);
            EditorGUILayout.EndVertical();
        }
        else
        {
            EditorGUILayout.LabelField("Select a GameObject first then click here.");
        }

        DropAreaGUI();
    }

    private void DropAreaGUI()
    {
        var e = Event.current.type;
        if (e == EventType.DragUpdated)
        {
            DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
        }
        else if (e == EventType.DragPerform)
        {
            DragAndDrop.AcceptDrag();

            foreach (var draggedObject in DragAndDrop.objectReferences)
            {
                if (draggedObject is GameObject)
                {
                    Debug.Log(draggedObject.name);
                }
            }
        }
    }
}
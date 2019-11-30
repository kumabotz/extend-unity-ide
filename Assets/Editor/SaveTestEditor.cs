using System.IO;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Networking;

[CustomEditor(typeof(SaveTest))]
public class SaveTestEditor : AbstractEditor
{
    private ReorderableList list;
    private SaveTest saveTestScript;

    private void OnEnable()
    {
        saveTestScript = Selection.activeGameObject.GetComponent<SaveTest>();
        list = new ReorderableList(serializedObject, serializedObject.FindProperty("items"), true, true, true, true);
        list.onRemoveCallback += RemoveCallback;
        list.drawElementCallback += OnDrawCallback;
    }

    private void OnDisable()
    {
        if (list != null)
        {
            list.onRemoveCallback -= RemoveCallback;
            list.drawElementCallback -= OnDrawCallback;
        }
    }

    private void RemoveCallback(ReorderableList list)
    {
        if (EditorUtility.DisplayDialog("Warning!", "Are you sure?", "Yes", "No"))
        {
            ReorderableList.defaultBehaviours.DoRemoveButton(list);
        }
    }

    private void OnDrawCallback(Rect rect, int index, bool isActive, bool isFocused)
    {
        var item = list.serializedProperty.GetArrayElementAtIndex(index);
        EditorGUI.PropertyField(
            new Rect(rect.x, rect.y, 60, EditorGUIUtility.singleLineHeight), 
            item.FindPropertyRelative("name"),
            GUIContent.none);
        EditorGUI.PropertyField(
            new Rect(rect.x + 80, rect.y, 150, EditorGUIUtility.singleLineHeight),
            item.FindPropertyRelative("pos"),
            GUIContent.none);
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        list.DoLayoutList();
        EditorGUILayout.BeginVertical();
        if (GUILayout.Button("Save"))
        {
            var text = saveTestScript.Save();
            WriteData(text);
        }

        if (GUILayout.Button("Load"))
        {
            var data = ReadDataFromFile();
            saveTestScript.Load(data);
        }
        EditorGUILayout.EndVertical();
        serializedObject.ApplyModifiedProperties();
    }

    private void WriteData(string data)
    {
        var path = EditorUtility.SaveFilePanel("Save Data", "", "data.txt", "txt");
        using (var fs = new FileStream(path, FileMode.Create))
        {
            using (var writer = new StreamWriter(fs))
            {
                writer.Write(data);
            }
        }
        AssetDatabase.Refresh();
    }

    private string ReadDataFromFile()
    {
        var path = EditorUtility.OpenFilePanel("Load Data", "", "txt");
        var uri = $"file:///{path}";
        using (var www = UnityWebRequest.Get(uri))
        {
            var request = www.SendWebRequest();
            while (!request.isDone || !www.isDone)
            {
            }
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.LogWarning($"WWW {uri} has error: {www.error}");
                return null;
            }
            return www.downloadHandler.text;
        }
    }
}
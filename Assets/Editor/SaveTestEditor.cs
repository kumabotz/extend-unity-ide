using System.IO;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Networking;

[CustomEditor(typeof(SaveTest))]
public class SaveTestEditor : Editor
{
    private ReorderableList list;
    private SaveTest saveTestScript;

    private void OnEnable()
    {
        saveTestScript = Selection.activeGameObject.GetComponent<SaveTest>();
        list = new ReorderableList(serializedObject, serializedObject.FindProperty("items"), true, true, true, true);
    }

    public override void OnInspectorGUI()
    {
        //DrawDefaultInspector();
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
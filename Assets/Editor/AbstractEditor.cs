using UnityEditor;

public abstract class AbstractEditor : Editor
{
    protected virtual string title => GetType().ToString();

    protected virtual string description => "Put text here to explain how to use the editor.";

    public void ShowHeader()
    {
        EditorGUILayout.Space();
        SetTitle();
        EditorGUILayout.Space();
        SetDescription();
    }

    public override void OnInspectorGUI()
    {
        ShowHeader();
    }

    protected virtual void SetTitle()
    {
        EditorGUILayout.LabelField(title, EditorUIUtil.guiTitleStyle);
    }

    protected virtual void SetDescription()
    {
        EditorGUILayout.LabelField(description, EditorUIUtil.guiMessageStyle);
    }
}
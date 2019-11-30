using UnityEngine;

public class EditorUIUtil
{
    public static GUIStyle guiTitleStyle =>
        new GUIStyle(GUI.skin.label)
        {
            normal = { textColor = Color.black },
            fontSize = 16,
            fixedHeight = 30
        };

    public static GUIStyle guiMessageStyle =>
        new GUIStyle(GUI.skin.label)
        {
            wordWrap = true
        };
}
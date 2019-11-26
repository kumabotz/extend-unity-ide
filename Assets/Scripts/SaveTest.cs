using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[Serializable]
public class SaveItem
{
    public string name;
    public Vector2 pos;

    public new string ToString()
    {
        return $"{name},{pos.x},{pos.y}";
    }
}

public class SaveTest : MonoBehaviour
{
    public List<SaveItem> items;

    public string Save()
    {
        var sb = new StringBuilder();
        var total = items.Count;
        for (var i = 0; i < total; i++)
        {
            sb.Append(items[i].ToString());
            if (i < total - 1)
            {
                sb.Append(";");
            }
        }
        return sb.ToString();
    }

    public void Load(string data)
    {
        var itemData = data.Split(';');
        items = new List<SaveItem>();
        var total = itemData.Length;
        for (var i = 0; i < total; i++)
        {
            var values = itemData[i].Split(',');
            var item = new SaveItem { name = values[0], pos = new Vector2(float.Parse(values[1]), float.Parse(values[2])) };
            items.Add(item);
        }
    }
}
using System.IO;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Project Viewにアセットサイズを表示させる。
/// https://gist.github.com/baba-s/86f124437037bd038fb2#file-exampleclass-cs
/// </summary>
public static class ProjectViewAssetSize
{
    private const string REMOVE_STR = "Assets";

    private static readonly int mRemoveCount = REMOVE_STR.Length;
    private static readonly Color mColor = Color.clear;

    [InitializeOnLoadMethod]
    private static void Example()
    {
        EditorApplication.projectWindowItemOnGUI += OnGUI;
    }

    private static void OnGUI(string guid, Rect selectionRect)
    {
        var dataPath = Application.dataPath;
        var startIndex = dataPath.LastIndexOf(REMOVE_STR);
        var dir = dataPath.Remove(startIndex, mRemoveCount);
        var path = dir + AssetDatabase.GUIDToAssetPath(guid);

        if (!File.Exists(path))
        {
            return;
        }

        var fileInfo = new FileInfo(path);
        var fileSize = fileInfo.Length;
        var text = GetFormatSizeString((int) fileSize);

        var label = EditorStyles.label;
        var content = new GUIContent(text);
        var width = label.CalcSize(content).x;

        var pos = selectionRect;
        pos.x = pos.xMax - width;
        pos.width = width;
        pos.yMin++;

        var color = GUI.color;
        GUI.color = mColor;
        GUI.DrawTexture(pos, EditorGUIUtility.whiteTexture);
        GUI.color = color;
        GUI.Label(pos, text);
    }

    private static string GetFormatSizeString(int size)
    {
        return GetFormatSizeString(size, 1024);
    }

    private static string GetFormatSizeString(int size, int p)
    {
        return GetFormatSizeString(size, p, "#,##0.##");
    }

    private static string GetFormatSizeString(int size, int p, string specifier)
    {
        var suffix = new []
        {
            "",
            "K",
            "M",
            "G",
            "T",
            "P",
            "E",
            "Z",
            "Y"
        };
        int index = 0;

        while (size >= p)
        {
            size /= p;
            index++;
        }

        return string.Format(
            "{0}{1}B",
            size.ToString(specifier),
            index < suffix.Length ? suffix[index] : "-"
        );
    }
}

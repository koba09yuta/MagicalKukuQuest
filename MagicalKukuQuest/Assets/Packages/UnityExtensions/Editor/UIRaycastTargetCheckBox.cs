using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// https://gist.github.com/baba-s/6610626bb3c1565d6c13c195f88a6fc6#file-exampleclass-cs
/// </summary>
public static class UIRaycastTargetCheckBox
{
    private const int WIDTH = 16;

    [InitializeOnLoadMethod]
    private static void Example()
    {
        EditorApplication.hierarchyWindowItemOnGUI += OnGUI;
    }

    private static void OnGUI(int instanceID, Rect selectionRect)
    {
        var go = EditorUtility.InstanceIDToObject(instanceID) as GameObject;

        if (go == null) return;

        var com = go.GetComponent<Graphic>();

        if (com == null) return;

        var pos = selectionRect;
        pos.x = pos.xMax - WIDTH;
        pos.width = WIDTH;

        var raycastTarget = GUI.Toggle(pos, com.raycastTarget, string.Empty);

        if (raycastTarget == com.raycastTarget) return;

        Undo.RecordObject(com, $"{com.name} : set raycastTarget {(raycastTarget ? "ON" : "OFF")} ");
        com.raycastTarget = raycastTarget;
        EditorUtility.SetDirty(com);
    }
}

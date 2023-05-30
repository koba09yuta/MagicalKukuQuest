﻿using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

/// <summary>
/// Prefab Editor
/// https://github.com/yukiarrr/PrefabEditor
/// </summary>
public class PrefabEditor : EditorWindow
{
    TreeViewState treeViewState;
    PrefabTreeView treeView;
    Transform preRoot;

    [MenuItem("Tools/Prefab Editor")]
    static void ShowWindow()
    {
        var window = GetWindow<PrefabEditor>();
        window.titleContent = new GUIContent("Prefab Editor");
        window.Show();
    }

    void OnSelectionChange()
    {
        if (treeView != null && IsPrefabObject())
        {
            var root = GetPrefabRoot();
            treeView.TargetPrefab = root;
            treeView.SetSelection(Selection.instanceIDs);
            var id = Selection.activeInstanceID;
            if (id != root.GetInstanceID())
            {
                treeView.FrameItem(id);
            }
        }

        Repaint();
    }

    void OnGUI()
    {
        DoToolbar();

        if ((treeView == null && !IsPrefabObject()) || (treeView != null && treeView.TargetPrefab == null))
        {
            GUILayout.Label("Please select prefab.");
        }
        else
        {
            if (treeViewState == null)
            {
                treeViewState = new TreeViewState();
            }

            if (IsPrefabObject())
            {
                var root = GetPrefabRoot();
                if (root != preRoot || treeView == null)
                {
                    treeView = new PrefabTreeView(treeViewState, root);
                }
                preRoot = root;
            }

            DoTreeView();
        }

    }

    bool IsPrefabObject()
    {
        return Selection.activeGameObject != null && PrefabUtility.GetPrefabType(Selection.activeGameObject) == PrefabType.Prefab;
    }

    Transform GetPrefabRoot()
    {
        return PrefabUtility.FindPrefabRoot(Selection.activeGameObject).transform;
    }

    void DoTreeView()
    {
        var rect = GUILayoutUtility.GetRect(0, 100000, 0, 100000);
        treeView.OnGUI(rect);
    }

    void DoToolbar()
    {
        GUILayout.BeginHorizontal(EditorStyles.toolbar);
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
    }
}

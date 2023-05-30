using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class ScreenshotTaker : EditorWindow
{
    string path = "";

    [MenuItem("Tools/Take Screenshot")]
    public static void ShowWindow()
    {
        EditorWindow editorWindow = EditorWindow.GetWindow(typeof(ScreenshotTaker));
        editorWindow.autoRepaintOnSceneChange = true;
        editorWindow.Show();
        editorWindow.titleContent = new GUIContent("ScreenshotTaker");
    }

    void OnGUI()
    {
        GUILayout.Label("Save Path", EditorStyles.boldLabel);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.TextField(path, GUILayout.ExpandWidth(false));
        if (GUILayout.Button("Browse", GUILayout.ExpandWidth(false)))
            path = EditorUtility.SaveFolderPanel("Path to Save Images", path, Application.dataPath);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();

        if (GUILayout.Button("Take Screenshot", GUILayout.MinHeight(60)))
        {
            if (path == "")
            {
                path = EditorUtility.SaveFolderPanel("Path to Save Images", path, Application.dataPath);
                Debug.Log("Path Set");
                TakeScreenShot();
            }
            else
            {
                TakeScreenShot();
            }
        }

        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Open Folder", GUILayout.MaxWidth(100), GUILayout.MinHeight(40)))
        {
            Application.OpenURL("file://" + path);
        }
    }

    string GetFileName()
    {
        string strPath = "";
        strPath = string.Format("{0}/screen_{1}.png",
            path,
            System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
        return strPath;
    }

    void TakeScreenShot()
    {
        var filename = GetFileName();
        ScreenCapture.CaptureScreenshot(filename);
        Debug.Log(string.Format("Took screenshot to: {0}", filename));
    }
}

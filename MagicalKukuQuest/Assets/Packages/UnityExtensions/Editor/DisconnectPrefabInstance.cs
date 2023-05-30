using UnityEditor;

/// <summary>
/// Prefabのコネクションを完全に断つ.
/// https://qiita.com/akihiro_0228/items/752009fd5088411f8547
/// </summary>
public class DisconnectPrefabInstance
{
    [MenuItem("GameObject/Disconnect Prefab Instance", true)]
    static bool IsEnabled()
    {
        return Selection.gameObjects.Length > 0;
    }

    [MenuItem("GameObject/Disconnect Prefab Instance", false, 100)]
    static void Disconnect()
    {
        var outputPath = "Assets/tmp.prefab";

        foreach (var gameObject in Selection.gameObjects)
        {
            PrefabUtility.CreatePrefab(outputPath, gameObject, ReplacePrefabOptions.ConnectToPrefab);
            PrefabUtility.DisconnectPrefabInstance(gameObject);
            AssetDatabase.DeleteAsset(outputPath);
        }
    }
}

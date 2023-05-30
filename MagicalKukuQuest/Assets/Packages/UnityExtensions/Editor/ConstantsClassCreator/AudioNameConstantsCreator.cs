using System;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// オーディオのファイル名を定数で管理するクラスを作成するスクリプト
/// https://gist.github.com/kankikuchi/f5471e8c3dde4765bc6b#file-audionamecreator-cs
/// </summary>
public static class AudioNameConstantsCreator
{
    const string NameSpace = "Benesse.Unity.Audio";
    // コマンド名
    const string COMMAND_NAME = "Tools/Create/AudioNameClass";
    const string AudioDir = "Assets/Audio";
    const string BgmDir = AudioDir + "/BGM";
    const string SeDir = AudioDir + "/SE";
    const string JingleDir = AudioDir + "/Jingle";
    const string VoiceDir = AudioDir + "/Voice";

    //オーディオファイルが編集または追加されたら自動でAUDIO.csを作成する
    private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {

        List<string[]> assetsList = new List<string[]>()
            {
                importedAssets, deletedAssets, movedAssets, movedFromAssetPaths
            };

        List<string> targetDirectoryNameList = new List<string>()
            {
                BgmDir,
                SeDir,
                JingleDir,
                VoiceDir
            };

        if (ExistsDirectoryInAssets(assetsList, targetDirectoryNameList))
        {
            Create();
        }
    }

    //スクリプトを作成します
    [MenuItem(COMMAND_NAME)]
    private static void Create()
    {
        CreateAudioNameClass("Bgm", BgmDir, "BGM名を定数で管理するクラス");
        CreateAudioNameClass("Se", SeDir, "SE名を定数で管理するクラス");
        CreateAudioNameClass("Jingle", JingleDir, "Jingle名を定数で管理するクラス");
        CreateAudioNameClass("Voice", VoiceDir, "Voice名を定数で管理するクラス");
    }

    static void CreateAudioNameClass(string className, string assetPath, string comment)
    {
        if (Directory.Exists(assetPath) && Directory.GetFiles(assetPath).Length > 0)
        {
            Dictionary<string, string> audioDic = NonResources.LoadAll<AudioClip>(assetPath)
                .Select(audioFile => ((AudioClip)audioFile).name)
                .ToDictionary(audioFile => audioFile);
            ConstantsClassCreator.Create(NameSpace, className, comment, audioDic);
        }
    }

    /// <summary>
    /// 入力されたassetsの中に、ディレクトリのパスがdirectoryNameの物はあるか
    /// </summary>
    static bool ExistsDirectoryInAssets(List<string[]> assetsList, List<string> targetDirectoryNameList)
    {

        return assetsList
            .Any(assets => assets                                       //入力されたassetsListに以下の条件を満たすか要素が含まれているか判定
             .Select(asset => System.IO.Path.GetDirectoryName(asset))   //assetsに含まれているファイルのディレクトリ名だけをリストにして取得
             .Intersect(targetDirectoryNameList)                         //上記のリストと入力されたディレクトリ名のリストの一致している物のリストを取得
             .Count() > 0);                                              //一致している物があるか
    }
}

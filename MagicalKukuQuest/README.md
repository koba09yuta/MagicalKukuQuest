# UnityTemplate

Unityプロジェクトのテンプレート構成です。

## バッチビルド

### バッチビルドで使用するソースコード・定義

sp000\_UnityTemplate/Assets/Editor  
  ┣ BatchBuild.cs (ビルドを実行する)  
  ┗ BuildPostProcessor.cs (ビルド実行後の後処理(ビルド条件の出力等)を実行)

### バッチビルドの実行手順

1. PlayerSettingsやGAの設定を変更
2. UnityのメニューからBuild→Androidを選択
3. Unityのコンソール見て、エラーがないかチェックする

### バッチビルドの出力先

sp000_UnityTemplate/apk  
  ┣ <パッケージ名の再後尾の文字列>.apk  
  ┗ usedBuildSettings.txt(ビルド条件)

### ビルド条件の保存（Androidの場合のみ）

Android版のバッチビルドでは、APKファイル内にビルド条件を保存しており、
後からAPKファイルを参照して、ビルド条件を確認することが可能です。

＊＊＊.apk
  ┗ /META-INF/BUILD.md          (ビルド条件の保存先)

BUILD.mdには、ビルド日時＋usedBuildSettings.txtの内容を保存しています。

ビルド条件を確認する場合は、以下のコマンドで確認することが可能です。
（または、ZIP解凍してエディタで確認しても良いです）
  unzip -c <APKファイル> META-INF/BUILD.md



## EditorConfig（命名規則解析ツール）

### EditorConfigで使用する定義
sp000\_UnityTemplate 
  ┗ .editorconfig (チェックする命名規則の定義)

### チェック可能項目
- クラス、インターフェース、構造体、Enum、デリゲート、イベント、メソッド、プロパティ宣言がパスカルケースか
- privateフィールド変数名が_で始まるパスカルケースか
- private以外のフィールド変数名がパスカルケースか
- ローカル変数名がキャメルケースか
- パラメーターがキャメルケースか

### EditorConfig利用時の注意点
- 名前空間がパスカルケースかは定義を書いているが正しく判定できないため、レビュー時に目視チェックが必要
- 定数が全て大文字で単語間が_になっているかは定義を書いているがprivateフィールド変数に対する定義が優先されて正しく判定できないため、レビュー時に目視チェックが必要

### EditorConfig利用方法
初回利用時のみ下記手順の実行が必要になります。
- EditorConfig  for VS Codeをインストール​

- VSCodeの設定を開き、Enable Editor Config Supportで検索しチェックを入れる​

- VSCodeの設定を開き、 Enable Roslyn Analyzersで検索しチェックを入れる

### Readme記載時のバージョン情報
- Editorconfig: https://github.com/editorconfig/editorconfig-vscode commit 6f85eb3 (2020/12/7)
- EditorConfig for VS Code: v0.15.1
- .editorconfigファイル作成日: 2021/2/18


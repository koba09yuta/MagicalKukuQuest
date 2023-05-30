#!/bin/bash -x
cd `dirname $0`
# ————————————————————————————————————————————————————
# 変数定義（引数指定可能としているが、アプリ毎に固定値に編集も可能）
# ————————————————————————————————————————————————————
# (引数1:必須)Unityで作成したAPKファイル名
APK_UNITY_MAKE=$1
# (引数2:必須)電子署名付与後のAPKファイル名
APK_SIGNED_FILE=$2
# (引数3:必須)電子署名のキーストアファイル名
SIGN_KEYSTORE_FILE=$3
# (引数4:必須)電子署名のストアパスワード
SIGN_STORE_PASSWD=$4
# (引数5:必須)電子署名のキーパスワード
SIGN_KEY_PASSWD=$5

if [ $# -ne 5 ]; then
  echo ‘引数が不足しています’
  exit 1
fi

# ————————————————————————————————————————————————————
# 一時ファイルが残っていれば削除
# ————————————————————————————————————————————————————
TODAY=$(date "+%Y%m%d")
APK_FILE_DEL_SIGN=${APK_UNITY_MAKE}_${TODAY}_delsign.apk
if [ -e $APK_FILE_DEL_SIGN ]; then
  rm $APK_FILE_DEL_SIGN
fi
APK_FILE_ADD_SIGN=${APK_UNITY_MAKE}_${TODAY}_addsign.apk
if [ -e $APK_FILE_ADD_SIGN ]; then
  rm $APK_FILE_ADD_SIGN
fi

# ————————————————————————————————————————————————————
# Unity作成APKから自動付与された電子署名を削除
# ————————————————————————————————————————————————————
cp $APK_UNITY_MAKE $APK_FILE_DEL_SIGN
zip -d $APK_FILE_DEL_SIGN "META-INF/MANIFEST.MF"
zip -d $APK_FILE_DEL_SIGN "META-INF/CERT.SF"
zip -d $APK_FILE_DEL_SIGN "META-INF/CERT.RSA"

# ————————————————————————————————————————————————————
# BC発行の電子署名を追加
# ————————————————————————————————————————————————————
export JAVA_HOME=$(/usr/libexec/java_home -v 1.6)
jarsigner -keystore $SIGN_KEYSTORE_FILE -storepass $SIGN_STORE_PASSWD -keypass $SIGN_KEY_PASSWD -signedjar $APK_FILE_ADD_SIGN $APK_FILE_DEL_SIGN 1

# ————————————————————————————————————————————————————
# BC発行の電子署名を追加
# ————————————————————————————————————————————————————
if [ -e $APK_SIGNED_FILE ]; then
  rm $APK_SIGNED_FILE
fi
zipalign -v -p 4 $APK_FILE_ADD_SIGN $APK_SIGNED_FILE

# ————————————————————————————————————————————————————
# 作成した一時ファイルを削除
# ————————————————————————————————————————————————————
rm $APK_FILE_DEL_SIGN
rm $APK_FILE_ADD_SIGN

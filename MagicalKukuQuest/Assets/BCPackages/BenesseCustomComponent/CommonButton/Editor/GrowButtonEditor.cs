using UnityEngine;
using UnityEditor;
using UnityEditor.UI;

namespace Benesse.Unity.CustomComponent
{
	[CustomEditor(typeof(GlowButton),true)]
	public class GrowButtonEditor : ButtonEditor {
		public override void OnInspectorGUI()
		{
			this.serializedObject.Update();
			base.OnInspectorGUI();	

			EditorGUILayout.LabelField(" タップ音設定");
			EditorGUI.indentLevel++;
			EditorGUILayout.PropertyField(this.serializedObject.FindProperty("tapAudioClip"), new GUIContent("Clip"), true);
			EditorGUI.indentLevel--;
			
			EditorGUILayout.LabelField(" 光彩設定");
			EditorGUI.indentLevel++;
			EditorGUILayout.PropertyField(this.serializedObject.FindProperty("speed"), new GUIContent("速さ"), true);
			EditorGUILayout.PropertyField(this.serializedObject.FindProperty("glowImage"),new GUIContent("光彩画像"), true);
			EditorGUI.indentLevel--;

			this.serializedObject.ApplyModifiedProperties();
		}
	}
}
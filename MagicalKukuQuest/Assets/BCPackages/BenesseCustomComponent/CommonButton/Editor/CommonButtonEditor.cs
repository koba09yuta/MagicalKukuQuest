using UnityEngine;
using UnityEditor;
using UnityEditor.UI;

namespace Benesse.Unity.CustomComponent
{
	[CustomEditor(typeof(CommonButton),true)]
	public class CommonButtonEditor : ButtonEditor {
		public override void OnInspectorGUI()
		{
			this.serializedObject.Update();
			base.OnInspectorGUI();	

			EditorGUILayout.LabelField(" タップ音設定");
			EditorGUI.indentLevel++;
			EditorGUILayout.PropertyField(this.serializedObject.FindProperty("tapAudioClip"), new GUIContent("Clip"), true);
			EditorGUI.indentLevel--;

			this.serializedObject.ApplyModifiedProperties();
		}
	}
}
// ---------------------------------------------------------
// Copyright (c) 2017 Benesse Corporation. All rights reserved.
//
// CommonModal.cs
// ---------------------------------------------------------
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Benesse.Unity.CustomComponent
{
	public class CommonModal {

		#region UnityEditor
		#if UNITY_EDITOR

		[MenuItem("GameObject/UI/CustomComponent/CommonModal", false, 0)]
		public static void CreateCommonButton()
		{	
			//追加するオブジェクトを作成
			GameObject commonModal = Resources.Load("CommonModalCanvas") as GameObject;
			GameObject modal = Object.Instantiate(commonModal, Vector3.zero, Quaternion.identity);
			modal.name = "CommonModalCanvas";
		}
		#endif
		#endregion
	}
}
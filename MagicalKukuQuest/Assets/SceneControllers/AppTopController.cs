// ---------------------------------------------------------
// Copyright (c) 2017 Benesse Corporation. All rights reserved.
//
// AppTopController.cs
// ---------------------------------------------------------
using UnityEngine;
using Benesse.Unity.CustomComponent;

namespace Benesse.Unity.spXXX
{
	/// <summary>
	/// アプリTOPのコントローラー
	/// </summary>
	public class AppTopController : MonoBehaviour {

		///<value>開始ボタン</value>
		[SerializeField]
		private GlowButton _StartButton;

		///<value>おうちのかたへモーダル</value>
		[SerializeField]
		private CommonModalView _NoticeModal;

		///<value>ライセンスモーダル</value>
		[SerializeField]
		private CommonModalView _LicenseModal;

		// Use this for initialization
		void Start () {
			
		}
		
		// Update is called once per frame
		void Update () {
			
		}

		/// <summary>
		/// スタートボタンクリック時の処理
		/// </summary>
		public void OnClickStartButton()
		{
			Debug.Log("スタートボタンをクリック。画面遷移とか");
			_StartButton.KeepPressedButton();
		}

		/// <summary>
		/// 終わるボタンクリック時の処理
		/// </summary>
		public void OnClickExitButton()
		{

		}

		/// <summary>
		/// お家の方へボタンクリック時の処理
		/// </summary>
		public void OnClickNoticeButton()
		{
			_NoticeModal.ShowModal();

		}

		/// <summary>
		/// お家の方へモーダルのクローズボタンの処理
		/// </summary>
		public void OnClickNoticeModalCloseButton()
		{
			_NoticeModal.HideModal();

		}

		/// <summary>
		/// ライセンス表示ボタンクリック時の処理
		/// </summary>
		public void OnClickLicenseShowButton()
		{
			_LicenseModal.ShowModal();

		}

		/// <summary>
		/// ライセンス非表示ボタンクリックの処理
		/// </summary>
		public void OnClickLicenseCloseButton()
		{
			_LicenseModal.HideModal();

		}
	}
}


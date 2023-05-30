// ---------------------------------------------------------
// Copyright (c) 2017 Benesse Corporation. All rights reserved.
//
// CommonButton.cs
// ---------------------------------------------------------
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Benesse.Unity.CustomComponent
{
	public class CommonButton : Button {

		#region  Private Filed

		///<value>タップ音</value>
		[SerializeField]
		public AudioClip tapAudioClip;

		///<value>通常設定のスプライト</value>
		private Sprite defaultTargetGraphicSprite;

		///<value>通常設定の色設定</value>
		private Color defaultTargetGraphicColor;

		///<value>押下時のスプライト</value>
		private Sprite preesdSprite;

		///<value>押下時のカラー</value>
		private Color pressedColor;

		///<value>オーディオソース</value>
		protected AudioSource audioSource;

		#endregion

		protected override void Awake()
		{
			base.Awake();

			//タップ音を設定
			audioSource = GetComponent<AudioSource>();
			
			//通常時のデフォルト設定を取得
			defaultTargetGraphicSprite = image.sprite;
			defaultTargetGraphicColor = image.color;

			//押下時のスプライト設定を取得
			preesdSprite = this.spriteState.pressedSprite;
			pressedColor = this.colors.pressedColor;

			//イベントを追加
			this.onClick.AddListener( () => OnClickAction() );
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();

			//ボタンのイベントを全て削除
			this.onClick.RemoveAllListeners();
		}

		/// <summary>
		/// ボタンのクリックアクション
		/// </summary>
		virtual protected void OnClickAction()
		{
			Debug.Log(gameObject.name + "が押されたよ");
			if(audioSource != null && tapAudioClip)
			{
				audioSource.clip = tapAudioClip;
				audioSource.Play(); 
			}
		}

		#region Public Method
		/// <summary>
		/// ボタンを非表示する。
		/// </summary>
		public void ShowButton()
		{
			this.enabled = true;
			image.enabled = true;
		}

		/// <summary>
		/// ボタンを非表示する。
		/// </summary>
		public void HideButton()
		{
			this.enabled = false;
			image.enabled = false;
		}

		/// <summary>
		/// ボタンを押下状態でキープする。
		/// ボタンの当たり判定はなくす。
		/// </summary>
		public void KeepPressedButton()
		{
			//グラフィックを入れ替え
			switch(this.transition)
			{
				case Selectable.Transition.SpriteSwap:
					image.sprite = preesdSprite;
					break;
				case Selectable.Transition.ColorTint:
					image.color = pressedColor;
					break;
			}

			//当たり判定を消す。
			image.raycastTarget = false;
		}

		/// <summary>
		/// ボタンの押下状態を解除する。
		/// ボタンの当たり判定はする。
		/// </summary>
		public void CancelPressedButton()
		{
			//グラフィックを通常設定に戻す
			switch(this.transition)
			{
				case Selectable.Transition.SpriteSwap:
					image.sprite = defaultTargetGraphicSprite;
					break;
				case Selectable.Transition.ColorTint:
					image.color = defaultTargetGraphicColor;
					break;
			}

			//当たり判定を復活させる。
			image.raycastTarget = true;
		}

		#endregion

		#region UnityEditor
		#if UNITY_EDITOR

		[MenuItem("GameObject/UI/CustomComponent/CommonButton", false, 0)]
		public static void CreateCommonButton()
		{
			//親オブジェクトを作成
			var activeGameObject = SelectedParent();
						
			//追加するオブジェクトを作成
			GameObject buttonObject = new GameObject();
			Image image = buttonObject.AddComponent<Image>();
			CommonButton button = buttonObject.AddComponent<CommonButton>();
			RectTransform rect = buttonObject.GetComponent<RectTransform>();
			AudioSource audio = buttonObject.AddComponent<AudioSource>();

			//パラメータを追加
			int width = 150;
			int height = 50;
			buttonObject.name = "CommonButton";
			button.targetGraphic = image;
			rect.sizeDelta = new Vector2(width, height);
			audio.playOnAwake = false;
			audio.loop = false;
			Navigation navi = button.navigation;
			navi.mode = Navigation.Mode.None;
			button.navigation = navi;
			buttonObject.transform.SetParent(activeGameObject.transform, false);

			//EventSystemを作成（ない場合）
			MakeEventSystem();
		}

		protected static GameObject SelectedParent()
		{
			var activeGameObject = Selection.activeGameObject;

			if (!activeGameObject)
			{
				activeGameObject = new GameObject();
				Canvas canvas = activeGameObject.AddComponent<Canvas>();
				activeGameObject.AddComponent<CanvasScaler>();
				activeGameObject.AddComponent<GraphicRaycaster>();

				activeGameObject.name = "Canvas";
				canvas.renderMode = RenderMode.ScreenSpaceOverlay;
			}

			return activeGameObject;
		}

		protected static void MakeEventSystem()
		{
			// EventSystem シングルトンインスタンスが存在しない場合、
			// EventSystem を動的に生成する
			if (!GameObject.Find("EventSystem"))
			{
				var instance = new GameObject ("EventSystem");
				instance.AddComponent<EventSystem>();
				instance.AddComponent<StandaloneInputModule>();
			}
		}
		#endif
		#endregion

	}
}
// ---------------------------------------------------------
// Copyright (c) 2017 Benesse Corporation. All rights reserved.
//
// GlowButton.cs
// ---------------------------------------------------------
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif


namespace Benesse.Unity.CustomComponent
{
	/// <summary>
	/// 光彩効果の制御
	/// </summary>
	[System.Serializable]
	public class GlowButton : CommonButton{


		///<value>アニメーション時間</value>
		[Range(1f, 10f)]
		[SerializeField]
		public float speed = 5f;

		///<value>光彩のイメージ</value>
		[SerializeField]
		public Image glowImage;

		///<value>現在のデーフォ</value>
		private Fade currentFade;

		/// <summary>
		/// 色彩のアルファ値にアクセス
		/// </summary>
		private float Alpha
		{
			get
			{
				if(glowImage != null)
				{
					return glowImage.color.a;
				}
				return 0;
			}
			set
			{
				if(glowImage != null)
				{
					glowImage.color = new Color(glowImage.color.r, glowImage.color.g, glowImage.color.b, value);
				}
			}
			
		}

		///<value>ボタンが使用可能か</value>
		private bool IsUsable
		{
			get
			{
				return (this.enabled && this.interactable && image.enabled && image.raycastTarget);
			}

		}

		///<value>押下状態</value>
		private bool isPressed
		{
			get;
			set;
		}

		///<value>選択状態</value>
		private bool isSelected
		{
			get;
			set;
		}

		///<value>フェード</value>
		private enum Fade
		{
			IN,
			OUT
		}

		// Use this for initialization
		protected override void Awake () {
			base.Awake();
			currentFade = Fade.IN;
			Alpha = 0;
		}

		void Update()
		{
			//選択状態でなく、ボタンが押せる状態であれば光彩
			if(!isPressed && IsUsable)
			{
				float increase = (1f / 30f) * speed * 0.1f;
				switch(currentFade)
				{
					case Fade.IN:
						Alpha += increase;
						if(Alpha >= 1)
						{
							currentFade = Fade.OUT;
						}
						break;
					case Fade.OUT:
						Alpha -= increase;
						if(Alpha <= 0)
						{
							currentFade = Fade.IN;
						}
						break;					
				}
			}
			//選択状態の場合は、光彩を止める
			else if(isPressed && IsUsable)
			{
				Alpha = 0;
				currentFade = Fade.IN;
			}
			//ボタンが押せない状態ならば光彩を止める
			else if (!IsUsable)
			{
				Alpha = 0;
				currentFade = Fade.IN;
			}
		}

		/// <summary>
		/// ボタン上で押下した時のイベント
		/// </summary>
		public override void OnPointerDown (PointerEventData eventData)
		{
			base.OnPointerDown(eventData);

			isPressed = true;
			isSelected = true;
		}

		/// <summary>
		/// ボタン上で押下をやめた時のイベント
		/// </summary>
		public override void OnPointerUp (PointerEventData eventData)
		{
			base.OnPointerUp(eventData);

			isPressed = false;
			isSelected = false;
		}

		/// <summary>
		/// ボタン上に入った時のイベント
		/// </summary>
		public override void OnPointerEnter (PointerEventData eventData)
		{
			base.OnPointerEnter(eventData);

			if(isSelected)
			{
				isPressed = true;
			}
		}

		/// <summary>
		/// ボタン上からでた時のイベント
		/// </summary>
		public override void OnPointerExit (PointerEventData eventData)
		{
			base.OnPointerExit(eventData);

			if(isSelected)
			{
				isPressed = false;
			}
		}

		#region UnityEditor
		#if UNITY_EDITOR

		[MenuItem("GameObject/UI/CustomComponent/GlowButton", false, 0)]
		public static void CreateGrowButton()
		{
			//親オブジェクトを作成
			var activeGameObject = SelectedParent();
						
			//追加するオブジェクトを作成
			GameObject buttonObject = new GameObject();
			Image image = buttonObject.AddComponent<Image>();
			GlowButton button = buttonObject.AddComponent<GlowButton>();
			RectTransform rect = buttonObject.GetComponent<RectTransform>();
			AudioSource audio = buttonObject.AddComponent<AudioSource>();

			//パラメータを追加
			buttonObject.name = "GlowButton";
			button.targetGraphic = image;
			Navigation navi = button.navigation;
			navi.mode = Navigation.Mode.None;
			button.navigation = navi;
			rect.sizeDelta = new Vector2(150, 50);
			audio.playOnAwake = false;
			audio.loop = false;
			buttonObject.transform.SetParent(activeGameObject.transform, false);

			//光彩用オブジェクト
			GameObject growObject = new GameObject();
			button.glowImage = growObject.AddComponent<Image>();
			RectTransform growRect = growObject.GetComponent<RectTransform>();

			//パラメータを追加
			growObject.name = "GlowImage";
			growObject.transform.SetParent(buttonObject.transform);
			growObject.transform.localPosition = Vector3.zero;
			growRect.sizeDelta = new Vector2(160, 60);
			button.glowImage .raycastTarget = false;
			

			//EventSystemを作成（ない場合）
			MakeEventSystem();
		}
		#endif
		#endregion
	}
}
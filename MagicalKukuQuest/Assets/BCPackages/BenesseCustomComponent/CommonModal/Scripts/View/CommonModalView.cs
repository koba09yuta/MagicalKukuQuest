// ---------------------------------------------------------
// Copyright (c) 2017 Benesse Corporation. All rights reserved.
//
// CommonModalView.cs
// ---------------------------------------------------------
using UnityEngine;
using UnityEngine.UI;

namespace Benesse.Unity.CustomComponent
{
	/// <summary>
	/// 共通モーダル制御
	/// </summary>
	public class CommonModalView : MonoBehaviour {

		///<value>モーダル</value>
		[SerializeField]
		protected GameObject _Modal;

		///<value>背景</value>
		[SerializeField]
		private Image _Background;

		///<value>モーダルの表示是非</value>
		public bool isShowModal
		{
			get
			{
				return _Background.enabled;
			}
		}

		[SerializeField]
		[Range(1, 10)]
		protected float _Speed = 5;

		[SerializeField]
		protected bool _Skip = false;


		[SerializeField]
		protected Vector3 SHOW_POSITION = new Vector3(0, 0, 0);

		[SerializeField]
		protected  Vector3 HIDE_POSITION = new Vector3(0, -800f, 0);

		private StatEnum currentState;

		private enum StatEnum
		{
			SHOW,
			HIDE,
			STOP
		}
		void Awake()
		{
			_Background.enabled = false;
			_Modal.transform.localPosition = HIDE_POSITION;
			currentState = StatEnum.STOP;
		}

		void Update()
		{
			if(!_Skip)
			{
				if(StatEnum.SHOW == currentState)
				{
					float step = _Speed * Time.deltaTime * 1000;

					_Modal.transform.localPosition = Vector3.MoveTowards (_Modal.transform.localPosition, SHOW_POSITION, step);

					if(SHOW_POSITION == _Modal.transform.localPosition)
					{
						currentState = StatEnum.STOP; 
					}
				}
				else if(StatEnum.HIDE == currentState)
				{
					float step = _Speed * Time.deltaTime * 1000;

					_Modal.transform.localPosition = Vector3.MoveTowards (_Modal.transform.localPosition, HIDE_POSITION, step);

					if(HIDE_POSITION == _Modal.transform.localPosition)
					{
						currentState = StatEnum.STOP; 

						//バックグランドを非表示
						_Background.enabled = false;
						_Background.raycastTarget = false;	
					}				
				}
			}
			else
			{
				if(StatEnum.SHOW == currentState)
				{
					_Modal.transform.localPosition = SHOW_POSITION;
					currentState = StatEnum.STOP;
				}
				else if(StatEnum.HIDE == currentState)
				{
					_Modal.transform.localPosition = HIDE_POSITION;
					currentState = StatEnum.STOP;
					//バックグランドを非表示
					_Background.enabled = false;
					_Background.raycastTarget = false;	
				}
			}

		}

		/// <summary>
		/// 実行用メソッド
		/// スクリーンを表示する。
		/// </summary>
		public void ShowModal()
		{
			currentState = StatEnum.SHOW;

			//バックグランドを表示
			_Background.enabled = true;
			_Background.raycastTarget = true;
			
		}

		/// <summary>
		/// 実行用メソッド
		/// スクリーンを隠す
		/// </summary>
		public void HideModal()
		{
			currentState = StatEnum.HIDE;
		}
	}
}
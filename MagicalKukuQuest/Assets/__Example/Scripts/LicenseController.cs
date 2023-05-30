// Copyright (c) 2017 Benesse Corporation. All rights reserved.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Benesse.Unity.Common
{
    /// <summary>
    /// ライセンス表記のパネルを制御するクラスです。
    /// </summary>
    public class LicenseController : MonoBehaviour
    {
        #region Fields
        #region Instance fields
        #region Private fields
        [SerializeField]
        private TextAsset LicenseTextAsset;
        [SerializeField]
        private GameObject _Panel;
        [SerializeField]
        private Scrollbar _LicenseScrollbar;
        [SerializeField]
        private Text _LicenseText;
        [SerializeField]
        private Button _ShowButton;
        [SerializeField]
        private Button _CloseButton;
        #endregion // Private fields
        #endregion // Instance fields
        #endregion // Fields

        #region Methods
        #region Public Methods
        /// <summary>
        /// ライセンスのダイアログを表示します。
        /// </summary>
        public void Show()
        {
            // パネルを表示
            _Panel.SetActive(true);
            // 閉じるボタンを有効化
            _CloseButton.interactable = true;
            // 表示ボタンを無効化
            _ShowButton.interactable = false;
            // スクロールバーの位置を最上部に戻す
            _LicenseScrollbar.value = 1.0f;
            // 最前面に表示
            _Panel.transform.SetAsLastSibling();
        }

        /// <summary>
        /// ライセンスのダイアログを非表示にします。
        /// </summary>
        public void Close()
        {
            // 閉じるボタンを無効化(2度押し防止)
            _CloseButton.interactable = false;
            // 表示ボタンを有効化
            _ShowButton.interactable = true;
            // パネルを非表示
            _Panel.SetActive(false);
        }
        #endregion // Public methods

        #region Private methods
        void Awake()
        {
            _LicenseText.text = LicenseTextAsset.text;

            // 表示ロジックをボタンに設定
            _ShowButton.onClick.AddListener(() => Show());

            // 非表示ロジックをボタンに設定
            _CloseButton.onClick.AddListener(() => Close());
        }

        void Start()
        {
            // あらかじめ非表示にしておく
            _Panel.SetActive(false);
        }

        void OnDestroy ()
        {
            _ShowButton.onClick.RemoveAllListeners();
            _CloseButton.onClick.RemoveAllListeners();
            LicenseTextAsset = null;
            _Panel = null;
            _LicenseText = null;
            _ShowButton = null;
            _CloseButton = null;
        }
        #endregion // Private methods
        #endregion // Methods
    }
}
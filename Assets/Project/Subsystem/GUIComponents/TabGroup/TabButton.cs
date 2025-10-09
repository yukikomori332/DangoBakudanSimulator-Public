using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Project.Subsystem.GUIComponents.TabGroup
{
    /// <summary>
    /// タブボタンのビューコンポーネント
    /// ホバー、クリック状態に応じて表示を切り替える機能を提供
    /// </summary>
    [RequireComponent(typeof(Button))]
    public sealed class TabButton : MonoBehaviour
    {
        [Header("Components")]
        public Image fill;              // ボタンの背景画像
        public Image icon;              // ボタンのアイコン画像
        public TextMeshProUGUI label;   //　ボタンのテキスト

        [Header("Color Settings")]
        public Color selectedFillColor = Color.white;
        public Color selectedIconColor = Color.white;
        public Color selectedLabelColor = Color.white;

        public Button Button { get; private set; }  // ボタンビュー

        private Color _initialFillColor;
        private Color _initialIconColor;
        private Color _initialLabelColor;

        private void OnDestroy()
        {

        }

        private void Awake()
        {
            Button = GetComponent<Button>();

            // ビューの初期状態を設定
            if (fill != null)
                _initialFillColor = fill.color;
            if (icon != null)
                _initialIconColor = icon.color;
            if (label != null)
                _initialLabelColor = label.color;
        }

        public void OnTabEnter()
        {
            if (fill != null)
                fill.color = selectedFillColor;

            if (icon != null)
                icon.color = selectedIconColor;

            if (label != null)
                label.color = selectedLabelColor;
        }

        public void OnTabExit()
        {
            if (fill != null)
                fill.color = _initialFillColor;

            if (icon != null)
                icon.color = _initialIconColor;

            if (label != null)
                label.color = _initialLabelColor;
        }
    }
}

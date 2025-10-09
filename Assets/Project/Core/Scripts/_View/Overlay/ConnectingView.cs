using UnityEngine;

namespace Project.Core.Scripts.View.Overlay
{
    /// <summary>
    /// 接続中のオーバーレイ表示を管理するクラス
    /// CanvasGroupを使用して表示/非表示を制御します
    /// </summary>
    [RequireComponent(typeof(CanvasGroup))]
    public sealed class ConnectingView : MonoBehaviour
    {
        /// <summary>
        /// UIの表示制御に使用するCanvasGroupコンポーネント
        /// </summary>
        private CanvasGroup _canvasGroup;

        /// <summary>
        /// コンポーネントの初期化時に実行
        /// CanvasGroupコンポーネントを取得します
        /// </summary>
        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        /// <summary>
        /// 初期表示時に実行
        /// オーバーレイを非表示状態に設定します
        /// </summary>
        private void Start()
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }

        /// <summary>
        /// オーバーレイを表示します
        /// 透明度を1に設定し、インタラクションを有効化します
        /// </summary>
        public void Show()
        {
            _canvasGroup.alpha = 1;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }

        /// <summary>
        /// オーバーレイを非表示にします
        /// 透明度を0に設定し、インタラクションを無効化します
        /// </summary>
        public void Hide()
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }
    }
}

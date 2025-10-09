using Coffee.UIEffects;
using UnityEngine;

namespace Project.Core.Scripts.View.Overlay
{
    /// <summary>
    /// ローディング画面を制御するビューコンポーネント
    /// CanvasGroupを使用して表示/非表示を切り替える
    /// </summary>
    [RequireComponent(typeof(CanvasGroup))]
    public sealed class LoadingView : MonoBehaviour
    {
        [SerializeField] private UIEffect uiEffect; // ローディング画面のアニメーション制御に使用するUIEffectコンポーネント

        private CanvasGroup _canvasGroup;           // ローディング画面の表示制御に使用するCanvasGroupコンポーネント

        private void Awake()
        {
            // コンポーネントの取得
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public void Show()
        {
            if (uiEffect != null)
                uiEffect.enabled = true;

            // ローディング画面を表示し、インタラクションを有効化
            _canvasGroup.alpha = 1;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }

        public void Hide()
        {
            // ローディング画面を非表示にし、インタラクションを無効化
            _canvasGroup.alpha = 0;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;

            if (uiEffect != null)
                uiEffect.enabled = false;
        }
    }
}

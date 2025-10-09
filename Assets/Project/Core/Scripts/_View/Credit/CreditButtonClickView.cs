using LitMotion;
using LitMotion.Extensions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

namespace Project.Core.Scripts.View.Credit
{
    /// <summary>
    /// クレジット画面のボタンのビューコンポーネント
    /// クリック状態に応じて表示を切り替える機能を提供
    /// </summary>
    [RequireComponent(typeof(CreditButtonView))]
    public sealed class CreditButtonClickView : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [Header("Components")]
        [SerializeField] private TextMeshProUGUI label;

        private Ease _ease = Ease.OutQuad;
        private float _duration = 0.07f;
        private Vector2 _animationSizeDelta = new(14f, 7f);
        private float _animationFontSizeDelta = 1f;

        private RectTransform _rectTransform;
        private Vector2 _initialSizeDelta;
        private float _initialFontSize;

        private CompositeMotionHandle _motionHandles = new(3);

        private void OnDestroy()
        {
            _motionHandles.Cancel();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _motionHandles.Cancel();

            if (_rectTransform != null)
                LMotion.Create(_rectTransform.sizeDelta, _initialSizeDelta - _animationSizeDelta, _duration)
                    .WithScheduler(MotionScheduler.UpdateIgnoreTimeScale) // 実行タイミングをSchedulerで指定
                    .WithEase(_ease)                                      // イージング関数を指定
                    .BindToSizeDelta(_rectTransform)                      // RectTransformにバインド
                    .AddTo(_motionHandles);                               // handleが破棄された際にモーションをキャンセルする

            if (label != null)
                LMotion.Create(label.fontSize, _initialFontSize - _animationFontSizeDelta, _duration)
                    .WithScheduler(MotionScheduler.UpdateIgnoreTimeScale) // 実行タイミングをSchedulerで指定
                    .WithEase(_ease)                                      // イージング関数を指定
                    .BindToFontSize(label)                                // FontSizeにバインド
                    .AddTo(_motionHandles);                               // handleが破棄された際にモーションをキャンセルする
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _motionHandles.Cancel();

            if (_rectTransform != null)
                LMotion.Create(_rectTransform.sizeDelta, _initialSizeDelta, _duration)
                    .WithScheduler(MotionScheduler.UpdateIgnoreTimeScale) // 実行タイミングをSchedulerで指定
                    .WithEase(_ease)                                      // イージング関数を指定
                    .BindToSizeDelta(_rectTransform)                      // RectTransformにバインド
                    .AddTo(_motionHandles);                               // handleが破棄された際にモーションをキャンセルする

            if (label != null)
                LMotion.Create(label.fontSize, _initialFontSize, _duration)
                    .WithScheduler(MotionScheduler.UpdateIgnoreTimeScale) // 実行タイミングをSchedulerで指定
                    .WithEase(_ease)                                      // イージング関数を指定
                    .BindToFontSize(label)                                // FontSizeにバインド
                    .AddTo(_motionHandles);                               // handleが破棄された際にモーションをキャンセルする
        }

        public void Initialize()
        {
            // コンポーネントの取得
            _rectTransform = GetComponent<RectTransform>();

            // ビューの初期状態を設定
            if (_rectTransform != null)
                _initialSizeDelta = _rectTransform.sizeDelta;
            if (label != null)
                _initialFontSize = label.fontSize;
        }
    }
}

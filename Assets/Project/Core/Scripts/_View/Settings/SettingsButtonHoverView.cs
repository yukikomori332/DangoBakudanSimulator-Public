using LitMotion;
using LitMotion.Extensions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

namespace Project.Core.Scripts.View.Setting
{
    /// <summary>
    /// 設定画面のボタンのビューコンポーネント
    /// ホバー状態に応じて表示を切り替える機能を提供
    /// </summary>
    [RequireComponent(typeof(SettingsButtonView))]
    public sealed class SettingsButtonHoverView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("Components")]
        [SerializeField] private Image fill;
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI label;

        [Header("Settings")]
        [SerializeField] private Color hoverFillColor = Color.white;
        [SerializeField] private Color hoverIconColor = Color.white;
        [SerializeField] private Color hoverLabelColor = Color.white;

        private Ease _ease = Ease.OutSine;
        private float _duration = 0.2f;

        private Color _initialFillColor;
        private Color _initialIconColor;
        private Color _initialLabelColor;

        private CompositeMotionHandle _motionHandles = new(2);

        private void OnDestroy()
        {
            _motionHandles.Cancel();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _motionHandles.Cancel();

            if (fill != null)
                LMotion.Create(fill.color, hoverFillColor, _duration)
                    .WithScheduler(MotionScheduler.UpdateIgnoreTimeScale) // 実行タイミングをSchedulerで指定
                    .WithEase(_ease)                                      // イージング関数を指定
                    .BindToColor(fill)                                    // Colorにバインド
                    .AddTo(_motionHandles);                               // handleが破棄された際にモーションをキャンセルする

            if (icon != null)
                LMotion.Create(icon.color, hoverIconColor, _duration)
                    .WithScheduler(MotionScheduler.UpdateIgnoreTimeScale) // 実行タイミングをSchedulerで指定
                    .WithEase(_ease)                                      // イージング関数を指定
                    .BindToColor(icon)                                    // Colorにバインド
                    .AddTo(_motionHandles);                               // handleが破棄された際にモーションをキャンセルする

            if (label != null)
                LMotion.Create(label.color, hoverLabelColor, _duration)
                    .WithScheduler(MotionScheduler.UpdateIgnoreTimeScale) // 実行タイミングをSchedulerで指定
                    .WithEase(_ease)                                      // イージング関数を指定
                    .BindToColor(label)                                   // Colorにバインド
                    .AddTo(_motionHandles);                               // handleが破棄された際にモーションをキャンセルする
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _motionHandles.Cancel();

            if (fill != null)
                LMotion.Create(fill.color, _initialFillColor, _duration)
                    .WithScheduler(MotionScheduler.UpdateIgnoreTimeScale) // 実行タイミングをSchedulerで指定
                    .WithEase(_ease)                                      // イージング関数を指定
                    .BindToColor(fill)                                    // Colorにバインド
                    .AddTo(_motionHandles);                               // handleが破棄された際にモーションをキャンセルする

            if (icon != null)
                LMotion.Create(icon.color, _initialIconColor, _duration)
                    .WithScheduler(MotionScheduler.UpdateIgnoreTimeScale) // 実行タイミングをSchedulerで指定
                    .WithEase(_ease)                                      // イージング関数を指定
                    .BindToColor(icon)                                    // Colorにバインド
                    .AddTo(_motionHandles);                               // handleが破棄された際にモーションをキャンセルする

            if (label != null)
                LMotion.Create(label.color, _initialLabelColor, _duration)
                    .WithScheduler(MotionScheduler.UpdateIgnoreTimeScale) // 実行タイミングをSchedulerで指定
                    .WithEase(_ease)                                      // イージング関数を指定
                    .BindToColor(label)                                   // Colorにバインド
                    .AddTo(_motionHandles);                               // handleが破棄された際にモーションをキャンセルする
        }

        public void Initialize()
        {
            // ビューの初期状態を設定
            if (fill != null)
                _initialFillColor = fill.color;
            if (icon != null)
                _initialIconColor = icon.color;
            if (label != null)
                _initialLabelColor = label.color;
        }
    }
}

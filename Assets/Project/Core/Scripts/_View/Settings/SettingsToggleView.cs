using LitMotion;
using LitMotion.Extensions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

namespace Project.Core.Scripts.View.Setting
{
    /// <summary>
    /// 設定画面のトグルのビューコンポーネント
    /// 値の状態に応じて表示を切り替える機能を提供
    /// </summary>
    [RequireComponent(typeof(Toggle))]
    public sealed class SettingsToggleView : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Image fill;
        // [SerializeField] private Image handle;
        [SerializeField] private Image checkmark;

        private Ease _ease = Ease.OutSine;
        private float _duration = 0.1f;
        private float _delay = 0.025f;
        // private float _slideOffset = 40f;

        private Toggle _toggle;
        // private Vector2 _initialHandlePosition;

        private CompositeMotionHandle _motionHandles = new(4);

        private void OnDestroy()
        {
            _motionHandles.Cancel();
        }

        public void Initialize()
        {
            // コンポーネントの取得
            _toggle = GetComponent<Toggle>();
            // トグルにイベントを登録
            _toggle.onValueChanged.AddListener(OnToggleChanged);

            // ビューの初期状態を設定
            // if (handle != null)
            //     _initialHandlePosition = handle.rectTransform.anchoredPosition;
        }

        private void OnToggleChanged(bool isOn)
        {
            _motionHandles.Cancel();

            // if (isOn)
            // {
            //     if (handle != null)
            //         LMotion.Create(handle.rectTransform.anchoredPosition.x, _initialHandlePosition.x + _slideOffset, _duration)
            //             .WithScheduler(MotionScheduler.UpdateIgnoreTimeScale) // 実行タイミングをSchedulerで指定
            //             .WithEase(_ease)                                      // イージング関数を指定
            //             .BindToAnchoredPositionX(handle.rectTransform)        // AnchoredPositionXにバインド
            //             .AddTo(_motionHandles);                               // handleが破棄された際にモーションをキャンセルする

            //     if (fill != null)
            //         LMotion.Create(fill.color.a, 1f, _duration)
            //             .WithScheduler(MotionScheduler.UpdateIgnoreTimeScale) // 実行タイミングをSchedulerで指定
            //             .WithEase(_ease)                                      // イージング関数を指定
            //             .BindToColorA(fill)                                   // ColorAlphaにバインド
            //             .AddTo(_motionHandles);                               // handleが破棄された際にモーションをキャンセルする
            // }
            // else if (!isOn)
            // {
            //     if (handle != null)
            //         LMotion.Create(handle.rectTransform.anchoredPosition.x, _initialHandlePosition.x, _duration)
            //             .WithScheduler(MotionScheduler.UpdateIgnoreTimeScale) // 実行タイミングをSchedulerで指定
            //             .WithEase(_ease)                                      // イージング関数を指定
            //             .BindToAnchoredPositionX(handle.rectTransform)        // AnchoredPositionXにバインド
            //             .AddTo(_motionHandles);                               // handleが破棄された際にモーションをキャンセルする

            //     if (fill != null)
            //         LMotion.Create(fill.color.a, 0f, _duration)
            //             .WithScheduler(MotionScheduler.UpdateIgnoreTimeScale) // 実行タイミングをSchedulerで指定
            //                 .WithEase(_ease)                                  // イージング関数を指定
            //                 .BindToColorA(fill)                               // ColorAlphaにバインド
            //                 .AddTo(_motionHandles);                           // handleが破棄された際にモーションをキャンセルする
            // }

            if (isOn)
            {
                if (checkmark != null)
                    LMotion.Create(checkmark.rectTransform.localScale, Vector3.one, _duration)
                        .WithEase(_ease)                                      // イージング関数を指定
                        .BindToLocalScale(checkmark.rectTransform)            // LocalScaleにバインド
                        .AddTo(_motionHandles);                               // handleが破棄された際にモーションをキャンセルする

                // if (fill != null)
                //     LMotion.Create(fill.color.a, 1f, _duration)
                //         .WithScheduler(MotionScheduler.UpdateIgnoreTimeScale) // 実行タイミングをSchedulerで指定
                //         .WithEase(_ease)                                      // イージング関数を指定
                //         .WithDelay(_delay)                                    // ディレイを指定
                //         .BindToColorA(fill)                                   // ColorAlphaにバインド
                //         .AddTo(_motionHandles);                               // handleが破棄された際にモーションをキャンセルする
            }
            else if (!isOn)
            {
                if (checkmark != null)
                    LMotion.Create(checkmark.rectTransform.localScale, Vector3.zero, _duration)
                        .WithEase(_ease)                                      // イージング関数を指定
                        .WithDelay(_delay)                                    // ディレイを指定
                        .BindToLocalScale(checkmark.rectTransform)            // LocalScaleにバインド
                        .AddTo(_motionHandles);                               // handleが破棄された際にモーションをキャンセルする

                // if (fill != null)
                //     LMotion.Create(fill.color.a, 0f, _duration)
                //         .WithScheduler(MotionScheduler.UpdateIgnoreTimeScale) // 実行タイミングをSchedulerで指定
                //         .WithEase(_ease)                                      // イージング関数を指定
                //         .BindToColorA(fill)                                   // ColorAlphaにバインド
                //         .AddTo(_motionHandles);                               // handleが破棄された際にモーションをキャンセルする
            }
        }
    }
}

using LitMotion;
using LitMotion.Extensions;
using UnityEngine;

namespace Project.Core.Scripts.View.Overlay
{
    /// <summary>
    /// ローディング中の回転アニメーションを表示するUIコンポーネント
    /// </summary>
    public sealed class LoadingSpinnerView : MonoBehaviour
    {
        private void Start()
        {
            // コンポーネントを取得
            var rect = GetComponent<RectTransform>();

            if (rect != null)
                LMotion.Create(0f, 360f, 0.6f)
                    .WithScheduler(MotionScheduler.UpdateIgnoreTimeScale) // 実行タイミングをSchedulerで指定
                    .WithEase(Ease.Linear)                                // イージング関数を指定
                    .WithLoops(-1, LoopType.Restart)                      // 再開始、無限ループ
                    .BindToEulerAnglesZ(rect)                             // Z回転にバインド
                    .AddTo(gameObject);                                   // ゲームオブジェクトが破棄された際にモーションをキャンセルする
        }
    }
}

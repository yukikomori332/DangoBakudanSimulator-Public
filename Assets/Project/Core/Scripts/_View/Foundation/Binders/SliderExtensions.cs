using System;
using UniRx;
using UnityEngine.UI;

namespace Project.Core.Scripts.View.Foundation.Binders
{
    /// <summary>
    /// UnityEngine.UI.Sliderの拡張メソッドを提供するクラス
    /// UniRxを使用して値の変更を監視する機能を実装
    /// </summary>
    public static class SliderExtensions
    {
        /// <summary>
        /// Sliderの値が変更されたときに実行されるコールバックを設定します
        /// </summary>
        /// <param name="self">対象のSliderコンポーネント</param>
        /// <param name="onValueChanged">値が変更されたときに実行されるコールバック</param>
        /// <returns>購読の解除に使用するIDisposable</returns>
        /// <remarks>
        /// このメソッドはUniRxを使用して値の変更を監視し、
        /// Sliderコンポーネントが破棄されたときに自動的に購読を解除します
        /// </remarks>
        public static IDisposable SetOnValueChangedDestination(this Slider self, Action<float> onValueChanged)
        {
            return self.onValueChanged
                .AsObservable()
                .Subscribe(onValueChanged.Invoke)
                .AddTo(self);
        }
    }
}

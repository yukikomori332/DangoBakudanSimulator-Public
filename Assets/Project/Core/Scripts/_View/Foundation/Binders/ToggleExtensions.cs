using System;
using UniRx;
using UnityEngine.UI;

namespace Project.Core.Scripts.View.Foundation.Binders
{
    /// <summary>
    /// UnityのToggleコンポーネントに対する拡張メソッドを提供するクラス
    /// UniRxを使用してリアクティブな実装を提供します
    /// </summary>
    public static class ToggleExtensions
    {
        /// <summary>
        /// Toggleの値が変更された時のイベントを購読し、指定されたアクションを実行します
        /// </summary>
        /// <param name="self">対象のToggleコンポーネント</param>
        /// <param name="onValueChanged">値が変更された時に実行するアクション</param>
        /// <returns>イベントの購読を解除するためのIDisposable</returns>
        /// <remarks>
        /// このメソッドはUniRxを使用して、Toggleの値変更イベントをObservableに変換し、
        /// 指定されたアクションをサブスクライブします。また、Toggleが破棄された時に
        /// 自動的にイベントの購読を解除するように設定されます。
        /// </remarks>
        public static IDisposable SetOnValueChangedDestination(this Toggle self, Action<bool> onValueChanged)
        {
            return self.onValueChanged
                .AsObservable()
                .Subscribe(onValueChanged.Invoke)
                .AddTo(self);
        }
    }
}

using System;
using UniRx;
using UnityEngine.UI;

namespace Project.Core.Scripts.View.Foundation.Binders
{
    /// <summary>
    /// UnityのButtonコンポーネントに対する拡張メソッドを提供するクラス
    /// UniRxを使用してボタンのクリックイベントをより簡潔に扱えるようにします
    /// </summary>
    public static class ButtonExtensions
    {
        /// <summary>
        /// ボタンのクリックイベントを設定し、指定されたアクションを実行します
        /// </summary>
        /// <param name="self">対象のButtonコンポーネント</param>
        /// <param name="onClick">クリック時に実行するアクション</param>
        /// <returns>イベントの購読を解除するためのIDisposable</returns>
        /// <remarks>
        /// このメソッドはUniRxを使用して、ボタンのクリックイベントをObservableとして扱います
        /// 戻り値のIDisposableを使用して、必要に応じてイベントの購読を解除できます
        /// </remarks>
        public static IDisposable SetOnClickDestination(this Button self, Action onClick)
        {
            return self.onClick
                .AsObservable()
                .Subscribe(x => onClick.Invoke())
                .AddTo(self);
        }
    }
}

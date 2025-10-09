using System;
using Project.Core.Scripts.Foundation.Common;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Core.Scripts.View.Foundation.Binders
{
    /// <summary>
    /// UnityEngine.UI.Imageの拡張メソッドを提供するクラス
    /// UniRxを使用して値の変更を監視する機能を実装
    /// </summary>
    public static class ImageExtensions
    {
        /// <summary>
        /// Spriteの値が変更されたときに実行されるコールバックを設定します
        /// </summary>
        /// <param name="self">対象のImageコンポーネント</param>
        /// <param name="source">バインドするSprite型のObservable</param>
        /// <returns>購読の解除に使用するIDisposable</returns>
        /// <remarks>
        /// このメソッドはUniRxを使用して値の変更を監視し、
        /// Imageコンポーネントが破棄されたときに自動的に購読を解除します
        /// </remarks>
        public static IDisposable SetSpriteSource(this Image self, IObservable<Sprite> source)
        {
            return source
                .Subscribe(x => { self.sprite = x; })
                .AddTo(self);
        }

        /// <summary>
        /// FillAmountの値が変更されたときに実行されるコールバックを設定します
        /// </summary>
        /// <param name="self">対象のImageコンポーネント</param>
        /// <param name="source">バインドするfloat型のObservable</param>
        /// <returns>購読の解除に使用するIDisposable</returns>
        /// <remarks>
        /// このメソッドはUniRxを使用して値の変更を監視し、
        /// Imageコンポーネントが破棄されたときに自動的に購読を解除します
        /// </remarks>
        public static IDisposable SetFillAmountSource(this Image self, IObservable<float> source)
        {
            return source
                .Subscribe(x => { self.fillAmount = x; })
                .AddTo(self);
        }
    }
}

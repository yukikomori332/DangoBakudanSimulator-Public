using System;
using UniRx;
using UnityEngine;

namespace Project.Core.Scripts.View.Foundation.Binders
{
    /// <summary>
    /// GameObjectの拡張メソッドを提供するユーティリティクラス
    /// </summary>
    public static class GamObjectExtensions
    {
        /// <summary>
        /// Observableの値に基づいてGameObjectのアクティブ状態を制御する拡張メソッド
        /// </summary>
        /// <param name="self">制御対象のGameObject</param>
        /// <param name="source">アクティブ状態を制御するObservable</param>
        /// <param name="invert">値を反転させるかどうか（trueの場合、Observableの値と逆の動作になります）</param>
        /// <returns>購読の解除に使用するIDisposable</returns>
        /// <remarks>
        /// このメソッドは、Observableの値が変更されるたびにGameObjectのアクティブ状態を更新します。
        /// メモリリークを防ぐため、GameObjectが破棄される際に自動的に購読が解除されます。
        /// </remarks>
        public static IDisposable SetActiveSelfSource(this GameObject self, IObservable<bool> source,
            bool invert = false)
        {
            return source
                .Subscribe(x =>
                {
                    x = invert ? !x : x;
                    self.SetActive(x);
                })
                .AddTo(self);
        }
    }
}

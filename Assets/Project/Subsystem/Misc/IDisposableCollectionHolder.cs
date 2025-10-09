using System;
using System.Collections.Generic;

namespace Project.Subsystem.Misc
{
    /// <summary>
    /// IDisposableオブジェクトのコレクションを保持するためのインターフェース
    /// リソース管理を一元化するために使用
    /// </summary>
    public interface IDisposableCollectionHolder
    {
        /// <summary>
        /// 保持しているIDisposableオブジェクトのコレクションを取得する
        /// </summary>
        /// <returns>IDisposableオブジェクトのコレクション</returns>
        ICollection<IDisposable> GetDisposableCollection();
    }

    /// <summary>
    /// IDisposableインターフェースの拡張メソッドを提供するクラス
    /// </summary>
    public static class DisposableExtensions
    {
        /// <summary>
        /// IDisposableオブジェクトを指定されたホルダーに追加する
        /// </summary>
        /// <param name="disposable">追加するIDisposableオブジェクト</param>
        /// <param name="holder">追加先のIDisposableCollectionHolder</param>
        /// <exception cref="ArgumentNullException">disposableまたはholderがnullの場合にスロー</exception>
        public static void AddTo(this IDisposable disposable, IDisposableCollectionHolder holder)
        {
            // nullチェック
            if (disposable == null) throw new ArgumentNullException(nameof(disposable));
            if (holder == null) throw new ArgumentNullException(nameof(holder));

            // ホルダーのコレクションに追加
            holder.GetDisposableCollection().Add(disposable);
        }
    }
}

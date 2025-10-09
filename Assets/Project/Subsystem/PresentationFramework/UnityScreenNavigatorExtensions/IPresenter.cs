using System;

namespace Project.Subsystem.PresentationFramework.UnityScreenNavigatorExtensions
{
    /// <summary>
    /// プレゼンターの基底インターフェース
    /// </summary>
    public interface IPresenter : IDisposable
    {
        // プレゼンターが破棄済みかどうかのフラグ
        bool IsDisposed { get; }

        // プレゼンターが初期化済みかどうかのフラグ
        bool IsInitialized { get; }

        /// <summary>
        /// プレゼンターを初期化する。
        /// 必要なリソースの読み込みや初期設定を行う。
        /// </summary>
        void Initialize();
    }
}

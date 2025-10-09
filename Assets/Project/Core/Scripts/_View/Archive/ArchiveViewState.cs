using System;
using Project.Core.Scripts.View.Foundation;
using Project.Subsystem.PresentationFramework;
using UniRx;

namespace Project.Core.Scripts.View.Archive
{
    public sealed class ArchiveViewState : AppViewState, IArchiveState
    {
        // 前の画面へ戻るボタンの状態管理
        public ArchiveButtonViewState BackButton { get; } = new ArchiveButtonViewState();

        // キャラクター表示の状態管理
        public ArchiveItemSetViewState Units { get; } = new ArchiveItemSetViewState();

        /// <summary>
        /// リソースの解放を行う
        /// </summary>
        protected override void DisposeInternal()
        {
            Units.Dispose();
        }
    }

    /// <summary>
    /// 図鑑画面の状態を制御するためのインターフェース
    /// </summary>
    internal interface IArchiveState
    {
    }
}

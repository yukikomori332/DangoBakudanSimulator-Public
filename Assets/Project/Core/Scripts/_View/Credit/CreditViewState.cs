using System;
using Project.Subsystem.PresentationFramework;
using UniRx;

namespace Project.Core.Scripts.View.Credit
{
    /// <summary>
    /// クレジット画面の状態を管理するクラス
    /// </summary>
    public sealed class CreditViewState : AppViewState, ICreditState
    {
        // 閉じるボタンの状態管理
        public CreditButtonViewState CloseButton { get; } = new CreditButtonViewState();

        /// <summary>
        /// リソースの解放を行う
        /// </summary>
        protected override void DisposeInternal()
        {
            CloseButton.Dispose();
        }
    }

    /// <summary>
    /// クレジット画面の状態を制御するためのインターフェース
    /// </summary>
    internal interface ICreditState
    {
    }
}

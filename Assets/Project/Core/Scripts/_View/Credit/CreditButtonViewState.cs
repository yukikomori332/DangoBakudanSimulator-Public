using System;
using Project.Subsystem.PresentationFramework;
using UniRx;

namespace Project.Core.Scripts.View.Credit
{
    public sealed class CreditButtonViewState : AppViewState, ICreditButtonState
    {
        // ボタンのロック状態を管理するリアクティブプロパティ
        private readonly ReactiveProperty<bool> _isLocked = new ReactiveProperty<bool>();
        // ボタンクリックイベントを発行するSubject
        private readonly Subject<Unit> _onClickedSubject = new Subject<Unit>();

        // ボタンのロック状態を外部に公開するプロパティ
        public IReactiveProperty<bool> IsLocked => _isLocked;
        // ボタンクリックイベントを購読可能なObservableとして公開
        public IObservable<Unit> OnClicked => _onClickedSubject;

        /// <summary>
        /// ボタンがクリックされた時の処理
        /// </summary>
        void ICreditButtonState.InvokeClicked()
        {
            _onClickedSubject.OnNext(Unit.Default);
        }

        /// <summary>
        /// リソースの解放を行う
        /// </summary>
        protected override void DisposeInternal()
        {
            _onClickedSubject.Dispose();
        }
    }

    /// <summary>
    /// クレジット画面のボタンの状態を制御するためのインターフェース
    /// </summary>
    internal interface ICreditButtonState
    {
        /// <summary>
        /// ボタンのクリックイベントを発火させる
        /// </summary>
        void InvokeClicked();
    }
}

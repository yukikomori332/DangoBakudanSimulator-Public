using System;
using Project.Subsystem.PresentationFramework;
using UniRx;

namespace Project.Core.Scripts.View.Dialogue
{
    /// <summary>
    /// ダイアログのボタンの状態を管理するViewStateクラス
    /// </summary>
    public sealed class DialogueButtonViewState : AppViewState, IDialogueButtonState
    {
        // ボタンのロック状態を管理するReactiveProperty
        private readonly ReactiveProperty<bool> _isLocked = new ReactiveProperty<bool>();

        // ボタンのクリックイベントを発行するSubject
        private readonly Subject<Unit> _onClickedSubject = new Subject<Unit>();

        // ボタンのロック状態を外部から監視・制御するためのプロパティ
        public IReactiveProperty<bool> IsLocked => _isLocked;

        // ボタンのクリックイベントを購読するためのObservable
        public IObservable<Unit> OnClicked => _onClickedSubject;

        /// <summary>
        /// ボタンがクリックされた時の処理
        /// </summary>
        void IDialogueButtonState.InvokeClicked()
        {
            _onClickedSubject.OnNext(Unit.Default);
        }

        /// <summary>
        /// リソースの解放を行う
        /// </summary>
        protected override void DisposeInternal()
        {
            _isLocked.Dispose();
            _onClickedSubject.Dispose();
        }
    }

    /// <summary>
    /// ボタンの状態を制御するための内部インターフェース
    /// </summary>
    internal interface IDialogueButtonState
    {
        /// <summary>
        /// ボタンのクリックイベントを発火させる
        /// </summary>
        void InvokeClicked();
    }
}

using Project.Subsystem.PresentationFramework;
using UniRx;

namespace Project.Core.Scripts.View.DialogueText
{
    /// <summary>
    /// ダイアログ文表示の状態を管理するクラス
    /// </summary>
    public sealed class DialogueTextViewState : AppViewState, IDialogueTextState
    {
        // ダイアログ文表示の状態を管理するReactiveProperty
        private readonly ReactiveProperty<string> _dialogueText = new ReactiveProperty<string>();

        // ダイアログ文表示の状態を外部に公開するプロパティ
        public IReactiveProperty<string> DialogueText => _dialogueText;

        /// <summary>
        /// リソースの解放を行う
        /// </summary>
        protected override void DisposeInternal()
        {
            _dialogueText.Dispose();
        }
    }

    /// <summary>
    /// ダイアログ文表示の状態を制御するためのインターフェース
    /// </summary>
    internal interface IDialogueTextState
    {
    }
}

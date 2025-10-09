using System;
using Project.Core.Scripts.View.CharName;
using Project.Core.Scripts.View.CharLabel;
using Project.Core.Scripts.View.DialogueText;
using Project.Subsystem.PresentationFramework;
using UniRx;
using UnityEngine;

namespace Project.Core.Scripts.View.Dialogue
{
    /// <summary>
    /// ダイアログ画面の状態を管理するクラス
    /// </summary>
    public sealed class DialogueViewState : AppViewState, IDialogueState
    {
        // キャラクター画像の状態を管理するReactiveProperty
        private readonly ReactiveProperty<Sprite> _sprite = new ReactiveProperty<Sprite>();

        // キャラクター画像の状態を外部から監視・制御するためのプロパティ
        public IReactiveProperty<Sprite> Sprite => _sprite;

        // キャラクター名表示の状態管理
        public CharNameViewState CharNameText { get; } = new CharNameViewState();
        // キャラクターラベル表示の状態管理
        public CharLabelViewState CharLabelText { get; } = new CharLabelViewState();
        // ダイアログ文表示の状態管理
        public DialogueTextViewState DialogueText { get; } = new DialogueTextViewState();

        // 次のダイアログへ遷移するボタンの状態管理
        public DialogueButtonViewState NextDialogueButton { get; } = new DialogueButtonViewState();

        /// <summary>
        /// リソースの解放を行う
        /// </summary>
        protected override void DisposeInternal()
        {
            _sprite.Dispose();

            CharNameText.Dispose();
            CharLabelText.Dispose();
            DialogueText.Dispose();

            NextDialogueButton.Dispose();
        }
    }

    /// <summary>
    /// ダイアログ画面の状態を制御するためのインターフェース
    /// </summary>
    internal interface IDialogueState
    {
    }
}

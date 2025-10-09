// using System;
using Cysharp.Threading.Tasks;
using Project.Core.Scripts.View.Foundation.Binders;
using Project.Subsystem.PresentationFramework;
using UniRx;
using UnityEngine;
using TMPro;

namespace Project.Core.Scripts.View.DialogueText
{
    /// <summary>
    /// ダイアログ文表示のビューを管理するクラス
    /// ダイアログ文表示などのUI要素を制御
    /// </summary>
    public sealed class DialogueTextView : AppView<DialogueTextViewState>
    {
        public TextMeshProUGUI dialogueText; // ダイアログ文表示用のテキスト

        protected override UniTask Initialize(DialogueTextViewState viewState)
        {
            // ダイアログ文表示用のテキストにイベントを設定
            dialogueText.SetTextSource(viewState.DialogueText).AddTo(this);

            return UniTask.CompletedTask;
        }
    }
}

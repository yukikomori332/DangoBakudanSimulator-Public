using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Project.Core.Scripts.View.Foundation.Binders;
using Project.Core.Scripts.View.CharName;
using Project.Core.Scripts.View.CharLabel;
using Project.Core.Scripts.View.DialogueText;
using Project.Subsystem.PresentationFramework;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Core.Scripts.View.Dialogue
{
    /// <summary>
    /// ダイアログ画面のビューを管理するクラス
    /// キャラクター名表示、設定ボタン、ガイド表示などのUI要素を制御
    /// </summary>
    public sealed class DialogueView : AppView<DialogueViewState>
    {
        public Image charImage;                       // キャラクター画像

        public CharNameView charNameView;             // キャラクター名表示用のビュー
        public CharLabelView charLabelView;           // キャラクターラベル表示用のビュー
        public DialogueTextView dialogueTextView;     // ダイアログ文表示用のビュー

        public DialogueButtonView nextDialogueButton; // 次のダイアログへ遷移するボタン

        /// <summary>
        /// ビューの初期化処理
        /// </summary>
        /// <param name="viewState">ビューの状態を管理するオブジェクト</param>
        protected override async UniTask Initialize(DialogueViewState viewState)
        {
            var internalState = (IDialogueState)viewState;

            // キャラクター画像にイベントを設定
            charImage.SetSpriteSource(viewState.Sprite).AddTo(this);

            // 初期化が必要なコンポーネントのタスクリストを作成
            var tasks = new List<UniTask>
            {
                charNameView.InitializeAsync(viewState.CharNameText),             // キャラクター名ビューの初期化
                charLabelView.InitializeAsync(viewState.CharLabelText),           // キャラクターラベルビューの初期化
                dialogueTextView.InitializeAsync(viewState.DialogueText),         // ダイアログ文ビューの初期化
                nextDialogueButton.InitializeAsync(viewState.NextDialogueButton), // 次のダイアログへ遷移するボタンのビューの初期化
            };

            await UniTask.WhenAll(tasks);
        }
    }
}

using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Cysharp.Threading.Tasks;
using Project.Core.Scripts.Domain.Dialogue.MasterRepository;
using Project.Core.Scripts.Presentation.Shared;
using Project.Core.Scripts.UseCase.Gameplay;
using Project.Core.Scripts.View.Dialogue;
using Project.Subsystem.Misc;
using UniRx;
using UnityEngine;

namespace Project.Core.Scripts.Presentation.Dialogue
{
    /// <summary>
    /// ダイアログ画面のプレゼンター
    /// ダイアログの状態管理とUIの更新を担当
    /// </summary>
    public sealed class DialoguePagePresenter : PagePresenterBase<DialoguePage, DialogueView, DialogueViewState>
    {
        private readonly IDialogueMasterRepository _dialogueMasterRepository; // ダイアログ管理
        private readonly GameplayUseCase _gameplayUseCase;                    // ゲームプレイのビジネスロジック
        private readonly IAudioPlayService _audioPlayService;                 // 音声再生サービス

        public DialoguePagePresenter(DialoguePage view, ITransitionService transitionService,
            GameplayUseCase gameplayUseCase, IDialogueMasterRepository dialogueMasterRepository, IAudioPlayService audioPlayService)
            : base(view, transitionService)
        {
            _gameplayUseCase = gameplayUseCase;
            _dialogueMasterRepository = dialogueMasterRepository;
            _audioPlayService = audioPlayService;
        }

        protected override async Task ViewDidLoad(DialoguePage view, DialogueViewState viewState)
        {
            // キャンセレーショントークンの生成
            var cts = new CancellationTokenSource();

            // モデルのデータを取得
            var dialogueMasterTable = await _dialogueMasterRepository.FetchTableAsync();

            // 各種モデルの参照を取得
            var gameData = _gameplayUseCase.GameData;   // ゲームデータ

            // パラメータの設定
            var dialogueIndex = 0;
            var id = $"dialogue_{dialogueIndex}";
            var master = dialogueMasterTable.FindById(id);
            var count = dialogueMasterTable.GetCount() - 1;

            // ビューステートの初期状態を設定
            SetCharImageDialogueViewState(viewState, master.Sprite);  // キャラクター名表示の設定
            SetCharNameDialogueViewState(viewState, master.CharName); // キャラクター名表示の設定
            SetCharLabelDialogueViewState(viewState, master.Label);   // キャラクターラベル表示の設定
            SetDialogueTextDialogueViewState(viewState, master.Text); // ダイアログ表示の設定

            // ダイアログ番号の増加
            dialogueIndex++;

            // ボタンのロック状態を設定
            viewState.NextDialogueButton.IsLocked.Value = false;

            // 設定ボタンのビューステートの変更を監視する
            if (!viewState.NextDialogueButton.IsLocked.Value)
                viewState.NextDialogueButton.OnClicked
                    .Subscribe(async _ =>
                    {
                        // ダイアログ番号が上限を超えたら
                        if (count < dialogueIndex)
                        {
                            // ダイアログの終了状態を有効にして保存
                            gameData.FinishedPrologue.Value = true;
                            await _gameplayUseCase.SaveFinishedPrologueAsync();

                            // 効果音を再生する
                            // await _audioPlayService.PlayButtonClickSound(cts);
                            // ゲームプレイ画面へ遷移する
                            TransitionService.GameplaySceneStarted();

                            return;
                        }

                        id = $"dialogue_{dialogueIndex}";
                        master = dialogueMasterTable.FindById(id);

                        SetCharImageDialogueViewState(viewState, master.Sprite);
                        SetCharNameDialogueViewState(viewState, master.CharName);
                        SetCharLabelDialogueViewState(viewState, master.Label);
                        SetDialogueTextDialogueViewState(viewState, master.Text);

                        dialogueIndex++;
                    })
                    .AddTo(this);

            await Task.CompletedTask;
        }

        /// <summary>
        /// キャラクター画像表示を更新
        /// </summary>
        private void SetCharImageDialogueViewState(DialogueViewState viewState, Sprite sprite)
        {
            viewState.Sprite.Value = sprite;
        }

        /// <summary>
        /// キャラクター名表示を更新
        /// </summary>
        private void SetCharNameDialogueViewState(DialogueViewState viewState, string charName)
        {
            viewState.CharNameText.CharName.Value = charName;
        }

        /// <summary>
        /// キャラクターラベル表示を更新
        /// </summary>
        private void SetCharLabelDialogueViewState(DialogueViewState viewState, string charLabel)
        {
            viewState.CharLabelText.CharLabel.Value = charLabel;
        }

        /// <summary>
        /// ダイアログ表示を更新
        /// </summary>
        private void SetDialogueTextDialogueViewState(DialogueViewState viewState, string text)
        {
            viewState.DialogueText.DialogueText.Value = text;
        }
    }
}

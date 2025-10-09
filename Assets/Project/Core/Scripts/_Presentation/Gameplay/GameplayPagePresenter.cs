using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Cysharp.Threading.Tasks;
using Project.Core.Scripts.Domain.FeatureFlag.MasterRepository;
using Project.Core.Scripts.Presentation.Shared;
using Project.Core.Scripts.UseCase.Gameplay;
using Project.Core.Scripts.View.Gameplay;
using Project.Core.Scripts.Gameplay.Domain.PlayerInput.Model;
using Project.Subsystem.Misc;
using UniRx;
using UnityEngine;

namespace Project.Core.Scripts.Presentation.Gameplay
{
    /// <summary>
    /// ゲームプレイ画面のプレゼンター
    /// ゲームプレイの状態管理とUIの更新を担当
    /// </summary>
    public sealed class GameplayPagePresenter : PagePresenterBase<GameplayPage, GameplayView, GameplayViewState>
    {
        private readonly IFeatureFlagMasterRepository _featureFlagMasterRepository; // 機能フラグ管理
        private readonly GameplayUseCase _gameplayUseCase;                          // ゲームプレイのビジネスロジック
        private readonly IAudioPlayService _audioPlayService;                       // 音声再生サービス

        public GameplayPagePresenter(GameplayPage view, ITransitionService transitionService,
            GameplayUseCase gameplayUseCase, IFeatureFlagMasterRepository featureFlagMasterRepository, IAudioPlayService audioPlayService)
            : base(view, transitionService)
        {
            _gameplayUseCase = gameplayUseCase;
            _featureFlagMasterRepository = featureFlagMasterRepository;
            _audioPlayService = audioPlayService;
        }

        protected override async Task ViewDidLoad(GameplayPage view, GameplayViewState viewState)
        {
            // キャンセレーショントークンの生成
            var cts = new CancellationTokenSource();

            // モデルのデータを取得
            var featureFlagMasterTable = await _featureFlagMasterRepository.FetchTableAsync();

            // 各種モデルの参照を取得
            var settings = _gameplayUseCase.Settings;   // ゲーム設定
            var gameData = _gameplayUseCase.GameData;   // ゲームデータ

            // プレイヤー入力のモデルを取得する
            var playerInputModel = _gameplayUseCase.PlayerInputModel;

            // 有効状態の確認
            var isSettingsEnabled = featureFlagMasterTable.FindById("settings")?.Enabled ?? false;
            var isCreditEnabled = featureFlagMasterTable.FindById("credit")?.Enabled ?? false;
            var isArchiveEnabled = featureFlagMasterTable.FindById("archive")?.Enabled ?? false;

            // ビューステートの初期状態を設定
            SetTitleGameplayViewState(viewState, "");                                            // タイトル表示の設定
            SetScoreGameplayViewState(viewState, gameData.Score.Value);                          // スコア表示の設定
            SetSpecialScoreGameplayViewState(viewState, gameData.SpecialScore.Value);            // 特別なスコア表示の設定
            SetWeaponCountGameplayViewState(viewState, _gameplayUseCase.GetPlacedWeaponCount()); // 武器数表示の設定

            // カウントダウン状態に入る
            _gameplayUseCase.SetCountingDown(true);

            // ボタンのロック状態を設定
            viewState.SettingsButton.IsLocked.Value = !isSettingsEnabled;
            viewState.CreditButton.IsLocked.Value = !isCreditEnabled;
            viewState.ArchiveButton.IsLocked.Value = !isArchiveEnabled;

            // 設定ボタンのビューステートの変更を監視
            if (!viewState.SettingsButton.IsLocked.Value)
                viewState.SettingsButton.OnClicked
                    .Subscribe(_ =>
                    {
                        // ゲームを一時停止
                        _gameplayUseCase.Settings.Pause.SetValue(true);
                        Time.timeScale = 0;

                        // 効果音を再生する
                        _audioPlayService.PlayButtonClickSound(cts);
                        // 設定画面へ遷移する
                        TransitionService.GameplayPageSettingsButtonClicked();
                    })
                    .AddTo(this);
            // クレジットボタンのビューステートの変更を監視
            if (!viewState.CreditButton.IsLocked.Value)
                viewState.CreditButton.OnClicked
                    .Subscribe(_ =>
                    {
                        // 効果音を再生する
                        _audioPlayService.PlayButtonClickSound(cts);
                        // クレジット画面へ遷移する
                        TransitionService.GameplayPageCreditButtonClicked();
                    })
                    .AddTo(this);
            // 図鑑ボタンのビューステートの変更を監視
            if (!viewState.ArchiveButton.IsLocked.Value)
                viewState.ArchiveButton.OnClicked
                    .Subscribe(_ =>
                    {
                        // 効果音を再生する
                        _audioPlayService.PlayButtonClickSound(cts);
                        // 図鑑画面へ遷移する
                        TransitionService.GameplayPageArchiveButtonClicked();
                    })
                    .AddTo(this);

            // カウントダウン状態の変更を監視
            gameData.CountingDown
                .Where(_ => gameData.CountingDown.Value)
                .Subscribe(async _ =>
                {
                    // コンポーネントを非表示にする
                    if (view != null || view.root != null)
                    {
                        view.root.scoreView.gameObject.SetActive(false);
                        view.root.specialScoreView.gameObject.SetActive(false);
                        view.root.weaponCountView.gameObject.SetActive(false);

                        view.root.settingsButton.gameObject.SetActive(false);
                        view.root.creditButton.gameObject.SetActive(false);
                        view.root.archiveButton.gameObject.SetActive(false);

                        view.root.buttons.SetActive(false);
                    }

                    // タイトルの表示
                    SetTitleGameplayViewState(viewState, "団子爆弾シュミレーター");
                    await UniTask.Delay(TimeSpan.FromSeconds(0.75f));

                    SetTitleGameplayViewState(viewState, "");
                    // カウントダウン状態を抜ける
                    _gameplayUseCase.SetCountingDown(false);

                    // コンポーネントを表示する
                    if (view != null || view.root != null)
                    {
                        view.root.scoreView.gameObject.SetActive(true);
                        view.root.specialScoreView.gameObject.SetActive(true);
                        view.root.weaponCountView.gameObject.SetActive(true);

                        view.root.settingsButton.gameObject.SetActive(true);
                        view.root.creditButton.gameObject.SetActive(true);
                        view.root.archiveButton.gameObject.SetActive(true);

                        view.root.buttons.SetActive(true);
                    }

                    // BGMの再生開始
                    _audioPlayService.PlayGameStartSound();

                    // タイマーを起動させる
                    _gameplayUseCase.SetRunningTimer(true);
                })
                .AddTo(this);

            // スコアの変更の監視
            gameData.Score
                .ObserveEveryValueChanged(_ => gameData.Score.Value)
                .Subscribe(_ =>
                {
                    // タイマーが作動していなければ、処理をスキップ
                    if (!gameData.RunningTimer.Value) return;

                    // スコア表示を更新
                    SetScoreGameplayViewState(viewState, gameData.Score.Value);
                })
                .AddTo(this);

            // 特別スコアの変更の監視
            gameData.SpecialScore
                .ObserveEveryValueChanged(_ => gameData.SpecialScore.Value)
                .Subscribe(_ =>
                {
                    // タイマーが作動していなければ、処理をスキップ
                    if (!gameData.RunningTimer.Value) return;

                    // スコア表示を更新
                    SetSpecialScoreGameplayViewState(viewState, gameData.SpecialScore.Value);
                })
                .AddTo(this);

            // プレイヤーの入力状態の変更を監視
            Observable.EveryUpdate()
                .Subscribe(_ =>
                {
                    // タイマーが作動していなければ、処理をスキップ
                    if (!gameData.RunningTimer.Value) return;

                    // 武器数表示の更新
                    SetWeaponCountGameplayViewState(viewState, _gameplayUseCase.GetPlacedWeaponCount());
                })
                .AddTo(this);

            await Task.CompletedTask;
        }

        /// <summary>
        /// タイトル表示を更新
        /// </summary>
        private void SetTitleGameplayViewState(GameplayViewState viewState, string text)
        {
            viewState.GameplayTitleText.GameplayTitle.Value = text;
        }

        /// <summary>
        /// スコア表示を更新
        /// </summary>
        private void SetScoreGameplayViewState(GameplayViewState viewState, float number)
        {
            viewState.ScoreText.Score.Value = number;
        }

        /// <summary>
        /// 特別スコア表示を更新
        /// </summary>
        private void SetSpecialScoreGameplayViewState(GameplayViewState viewState, float number)
        {
            viewState.SpecialScoreText.Score.Value = number;
        }

        /// <summary>
        /// 武器数表示を更新
        /// </summary>
        private void SetWeaponCountGameplayViewState(GameplayViewState viewState, int count)
        {
            viewState.WeaponCountText.WeaponCount.Value = count.ToString() + "/" + $"{_gameplayUseCase.GetMaxWeaponCount()}";
        }
    }
}

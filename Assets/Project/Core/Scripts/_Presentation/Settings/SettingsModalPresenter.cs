using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Project.Core.Scripts.Foundation.Common;
using Project.Core.Scripts.Presentation.Shared;
using Project.Core.Scripts.UseCase.Setting;
using Project.Core.Scripts.View.Overlay;
using Project.Core.Scripts.View.Setting;
using Project.Subsystem.Misc;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project.Core.Scripts.Presentation.Setting
{
    /// <summary>
    /// 設定画面のプレゼンター
    /// 音声設定（ボイス、BGM、SE）とガイド設定の管理を担当
    /// </summary>
    public sealed class SettingsModalPresenter : ModalPresenterBase<SettingsModal, SettingsView, SettingsViewState>
    {
        private readonly SettingsUseCase _settingsUseCase;    // 設定関連のユースケース
        private readonly IAudioPlayService _audioPlayService; // 音声再生サービス
        private readonly LoadingView _loadingView;            // ローディング画面のビュー

        // 設定が変更されたかどうかのフラグ
        private bool _dirty;

        public SettingsModalPresenter(SettingsModal view, ITransitionService transitionService,
            SettingsUseCase settingsUseCase, IAudioPlayService audioPlayService, LoadingView loadingView)
            : base(view, transitionService)
        {
            _settingsUseCase = settingsUseCase;
            _audioPlayService = audioPlayService;
            _loadingView = loadingView;
        }

        protected override async Task ViewDidLoad(SettingsModal view, SettingsViewState viewState)
        {
            // モデルのデータを取得
            await _settingsUseCase.FetchSoundSettingsAsync();
            var model = _settingsUseCase.Model;

            // キャンセレーショントークンの生成（非同期処理の制御用）
            var cts = new CancellationTokenSource();

            // ビューステートの初期状態を設定
            SetBgmSettingsViewState(viewState, model.Sounds.Bgm.Volume, model.Sounds.Bgm.Muted);
            SetSeSettingsViewState(viewState, model.Sounds.Se.Volume, model.Sounds.Se.Muted);

            // モデルの変更を監視し、ビューステートを更新
            model.Sounds.Bgm
                .ValueChanged
                .Subscribe(x => SetBgmSettingsViewState(viewState, x.Volume, x.Muted))
                .AddTo(this);
            model.Sounds.Se
                .ValueChanged
                .Subscribe(x => SetSeSettingsViewState(viewState, x.Volume, x.Muted))
                .AddTo(this);

            // ビューステートの変更を監視し、dirtyフラグを設定
            viewState.SoundSettings.IsBgmEnabled.Subscribe(_ => _dirty = true).AddTo(this);
            viewState.SoundSettings.IsSeEnabled.Subscribe(_ => _dirty = true).AddTo(this);
            viewState.SoundSettings.SeVolume.Subscribe(_ => _dirty = true).AddTo(this);
            viewState.SoundSettings.BgmVolume.Subscribe(_ => _dirty = true).AddTo(this);

            // ボタンをロック状態を設定
            viewState.CloseButton.IsLocked.Value = false;

            // ビューステートの変更を監視する
            if (!viewState.CloseButton.IsLocked.Value)
                viewState.CloseButton.OnClicked
                    .Subscribe(_ =>
                    {
                        // ゲームの一時停止を解除
                        model.Pause.SetValue(false);
                        Time.timeScale = 1;

                        _audioPlayService.PlayButtonClickSound(cts);
                        TransitionService.PopCommandExecuted();
                    })
                    .AddTo(this);
        }

        /// <summary>
        /// BGM設定のビューステートを更新
        /// </summary>
        private void SetBgmSettingsViewState(SettingsViewState viewState, float volume, bool isMuted)
        {
            viewState.SoundSettings.BgmVolume.Value = volume;
            viewState.SoundSettings.IsBgmEnabled.Value = !isMuted;
        }

        /// <summary>
        /// SE設定のビューステートを更新
        /// </summary>
        private void SetSeSettingsViewState(SettingsViewState viewState, float volume, bool isMuted)
        {
            viewState.SoundSettings.SeVolume.Value = volume;
            viewState.SoundSettings.IsSeEnabled.Value = !isMuted;
        }

        /// <summary>
        /// モーダルが閉じられる際の処理
        /// 設定が変更されている場合は保存を実行
        /// </summary>
        private async UniTask ViewWillExit(SettingsModal view, SettingsViewState viewState)
        {
            if (!_dirty)
                return;

            // 音声設定を保存
            await _settingsUseCase.SaveSoundSettingsAsync
            (
                new SettingsUseCase.SaveSoundSettingsRequest(
                    viewState.SoundSettings.BgmVolume.Value,
                    viewState.SoundSettings.SeVolume.Value,
                    !viewState.SoundSettings.IsBgmEnabled.Value,
                    !viewState.SoundSettings.IsSeEnabled.Value
                )
            );

            // 音量設定を更新
            await _audioPlayService.UpdateVolumeAsync();
        }

        protected override async Task ViewWillPopExit(SettingsModal view, SettingsViewState viewState)
        {
            await ViewWillExit(view, viewState);
        }

        protected override async Task ViewWillPushExit(SettingsModal view, SettingsViewState viewState)
        {
            await ViewWillExit(view, viewState);
        }
    }
}

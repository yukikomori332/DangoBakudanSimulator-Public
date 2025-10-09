using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Project.Core.Scripts.Presentation.Shared;
using Project.Core.Scripts.View.Credit;
using Project.Subsystem.Misc;
using UniRx;
using UnityEngine;

namespace Project.Core.Scripts.Presentation.Credit
{
    /// <summary>
    /// クレジット画面のプレゼンター
    /// クレジットの状態管理とUIの更新を担当
    /// </summary>
    public sealed class CreditModalPresenter : ModalPresenterBase<CreditModal, CreditView, CreditViewState>
    {
        private readonly IAudioPlayService _audioPlayService; // 音声再生サービス

        public CreditModalPresenter(CreditModal view, ITransitionService transitionService,
            IAudioPlayService audioPlayService)
            : base(view, transitionService)
        {
            _audioPlayService = audioPlayService;
        }

        protected override async Task ViewDidLoad(CreditModal view, CreditViewState viewState)
        {
            // キャンセレーショントークンの生成（非同期処理の制御用）
            var cts = new CancellationTokenSource();

            // ボタンのロック状態を設定
            viewState.CloseButton.IsLocked.Value = false;

            // ビューステートの変更を監視する
            if (!viewState.CloseButton.IsLocked.Value)
                viewState.CloseButton.OnClicked
                    .Subscribe(_ =>
                    {
                        // 効果音を再生する
                        _audioPlayService.PlayButtonClickSound(cts);
                        // 画面を遷移する
                        TransitionService.PopCommandExecuted();
                    })
                    .AddTo(this);

            await Task.CompletedTask;
        }
    }
}

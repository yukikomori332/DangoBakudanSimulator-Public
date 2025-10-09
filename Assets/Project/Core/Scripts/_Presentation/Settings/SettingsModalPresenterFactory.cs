using Project.Core.Scripts.Presentation.Shared;
using Project.Core.Scripts.UseCase.Setting;
using Project.Core.Scripts.View.Overlay;
using Project.Core.Scripts.View.Setting;

namespace Project.Core.Scripts.Presentation.Setting
{
    /// <summary>
    /// SettingsModalPresenterを生成するファクトリークラス
    /// 依存関係の注入とプレゼンターの生成を一元管理する
    /// </summary>
    public sealed class SettingsModalPresenterFactory
    {
        private readonly SettingsUseCase _settingsUseCase;    // 設定関連のビジネスロジックを管理するユースケース
        private readonly IAudioPlayService _audioPlayService; // オーディオ再生を管理するサービス
        private readonly LoadingView _loadingView;            // ローディング画面のビュー

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="settingsUseCase">設定関連のユースケース</param>
        /// <param name="audioPlayService">オーディオ再生サービス</param>
        /// <param name="loadingView">ローディング画面のビュー</param>
        public SettingsModalPresenterFactory(SettingsUseCase settingsUseCase, IAudioPlayService audioPlayService, LoadingView loadingView)
        {
            _settingsUseCase = settingsUseCase;
            _audioPlayService = audioPlayService;
            _loadingView = loadingView;
        }

        /// <summary>
        /// SettingsModalPresenterのインスタンスを生成する
        /// </summary>
        /// <param name="view">設定画面のビュー</param>
        /// <param name="transitionService">画面遷移サービス</param>
        /// <returns>生成されたSettingsModalPresenterのインスタンス</returns>
        public SettingsModalPresenter Create(SettingsModal view, ITransitionService transitionService)
        {
            return new SettingsModalPresenter(view, transitionService, _settingsUseCase, _audioPlayService, _loadingView);
        }
    }
}

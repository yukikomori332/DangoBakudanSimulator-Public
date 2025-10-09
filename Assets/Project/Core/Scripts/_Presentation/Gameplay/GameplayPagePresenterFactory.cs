using Project.Core.Scripts.Domain.FeatureFlag.MasterRepository;
using Project.Core.Scripts.Presentation.Shared;
using Project.Core.Scripts.UseCase.Gameplay;
using Project.Core.Scripts.View.Gameplay;

namespace Project.Core.Scripts.Presentation.Gameplay
{
    /// <summary>
    /// GameplayPagePresenterを生成するファクトリークラス
    /// 依存関係の注入とプレゼンターの生成を一元管理する
    /// </summary>
    public sealed class GameplayPagePresenterFactory
    {
        private readonly GameplayUseCase _gameplayUseCase;                          // ゲームプレイのビジネスロジックを管理するユースケース
        private readonly IFeatureFlagMasterRepository _featureFlagMasterRepository; // 機能フラグの管理を行うリポジトリ
        private readonly IAudioPlayService _audioPlayService;                       // オーディオ再生を管理するサービス

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="gameplayUseCase">ゲームプレイのユースケース</param>
        /// <param name="featureFlagMasterRepository">機能フラグリポジトリ</param>
        /// <param name="audioPlayService">オーディオ再生サービス</param>
        public GameplayPagePresenterFactory(GameplayUseCase gameplayUseCase, IFeatureFlagMasterRepository featureFlagMasterRepository, IAudioPlayService audioPlayService)
        {
            _gameplayUseCase = gameplayUseCase;
            _featureFlagMasterRepository = featureFlagMasterRepository;
            _audioPlayService = audioPlayService;
        }

        /// <summary>
        /// GameplayPagePresenterのインスタンスを生成する
        /// </summary>
        /// <param name="view">ゲームプレイ画面のビュー</param>
        /// <param name="transitionService">画面遷移サービス</param>
        /// <returns>生成されたGameplayPagePresenterのインスタンス</returns>
        public GameplayPagePresenter Create(GameplayPage view, ITransitionService transitionService)
        {
            return new GameplayPagePresenter(view, transitionService, _gameplayUseCase, _featureFlagMasterRepository, _audioPlayService);
        }
    }
}

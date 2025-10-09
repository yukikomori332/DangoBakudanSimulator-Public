using Project.Core.Scripts.Domain.Archive.MasterRepository;
using Project.Core.Scripts.Presentation.Shared;
using Project.Core.Scripts.UseCase.Gameplay;
using Project.Core.Scripts.View.Archive;

namespace Project.Core.Scripts.Presentation.Archive
{
    /// <summary>
    /// ArchivePagePresenterを生成するファクトリークラス
    /// 依存関係の注入とプレゼンターの生成を一元管理する
    /// </summary>
    public sealed class ArchivePagePresenterFactory
    {
        private readonly IArchiveMasterRepository _archiveMasterRepository; // 図鑑の管理を行うリポジトリ
        private readonly GameplayUseCase _gameplayUseCase;                  // ゲームプレイのビジネスロジックを管理するユースケース
        private readonly IAudioPlayService _audioPlayService;               // オーディオ再生を管理するサービス

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="archiveMasterRepository">図鑑リポジトリ</param>
        /// <param name="audioPlayService">オーディオ再生サービス</param>
        public ArchivePagePresenterFactory(GameplayUseCase gameplayUseCase, IArchiveMasterRepository archiveMasterRepository, IAudioPlayService audioPlayService)
        {
            _gameplayUseCase = gameplayUseCase;
            _archiveMasterRepository = archiveMasterRepository;
            _audioPlayService = audioPlayService;
        }

        /// <summary>
        /// ArchivePagePresenterのインスタンスを生成する
        /// </summary>
        /// <param name="view">図鑑画面のビュー</param>
        /// <param name="transitionService">画面遷移サービス</param>
        /// <returns>生成されたArchivePagePresenterのインスタンス</returns>
        public ArchivePagePresenter Create(ArchivePage view, ITransitionService transitionService)
        {
            return new ArchivePagePresenter(view, transitionService, _gameplayUseCase, _archiveMasterRepository, _audioPlayService);
        }
    }
}

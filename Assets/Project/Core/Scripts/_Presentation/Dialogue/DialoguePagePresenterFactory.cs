using Project.Core.Scripts.Domain.Dialogue.MasterRepository;
using Project.Core.Scripts.Presentation.Shared;
using Project.Core.Scripts.UseCase.Gameplay;
using Project.Core.Scripts.View.Dialogue;

namespace Project.Core.Scripts.Presentation.Dialogue
{
    /// <summary>
    /// DialoguePagePresenterを生成するファクトリークラス
    /// 依存関係の注入とプレゼンターの生成を一元管理する
    /// </summary>
    public sealed class DialoguePagePresenterFactory
    {
        private readonly IDialogueMasterRepository _dialogueMasterRepository; // ダイアログの管理を行うリポジトリ
        private readonly GameplayUseCase _gameplayUseCase;                    // ゲームプレイのビジネスロジックを管理するユースケース
        private readonly IAudioPlayService _audioPlayService;                 // オーディオ再生を管理するサービス

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dialogueMasterRepository">ダイアログリポジトリ</param>
        /// <param name="audioPlayService">オーディオ再生サービス</param>
        public DialoguePagePresenterFactory(GameplayUseCase gameplayUseCase, IDialogueMasterRepository dialogueMasterRepository, IAudioPlayService audioPlayService)
        {
            _gameplayUseCase = gameplayUseCase;
            _dialogueMasterRepository = dialogueMasterRepository;
            _audioPlayService = audioPlayService;
        }

        /// <summary>
        /// DialoguePagePresenterのインスタンスを生成する
        /// </summary>
        /// <param name="view">ダイアログ画面のビュー</param>
        /// <param name="transitionService">画面遷移サービス</param>
        /// <returns>生成されたDialoguePagePresenterのインスタンス</returns>
        public DialoguePagePresenter Create(DialoguePage view, ITransitionService transitionService)
        {
            return new DialoguePagePresenter(view, transitionService, _gameplayUseCase, _dialogueMasterRepository, _audioPlayService);
        }
    }
}

using Project.Core.Scripts.Presentation.Shared;
using Project.Core.Scripts.View.Credit;

namespace Project.Core.Scripts.Presentation.Credit
{
    /// <summary>
    /// CreditModalPresenterを生成するファクトリークラス
    /// 依存関係の注入とプレゼンターの生成を一元管理する
    /// </summary>
    public sealed class CreditModalPresenterFactory
    {
        private readonly IAudioPlayService _audioPlayService; // オーディオ再生を管理するサービス

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="audioPlayService">オーディオ再生サービス</param>
        public CreditModalPresenterFactory(IAudioPlayService audioPlayService)
        {
            _audioPlayService = audioPlayService;
        }

        /// <summary>
        /// CreditModalPresenterのインスタンスを生成する
        /// </summary>
        /// <param name="view">クレジット画面のビュー</param>
        /// <param name="transitionService">画面遷移サービス</param>
        /// <returns>生成されたCreditModalPresenterのインスタンス</returns>
        public CreditModalPresenter Create(CreditModal view, ITransitionService transitionService)
        {
            return new CreditModalPresenter(view, transitionService, _audioPlayService);
        }
    }
}

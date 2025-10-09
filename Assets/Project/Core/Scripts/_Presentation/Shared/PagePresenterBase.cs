using Project.Subsystem.PresentationFramework;

namespace Project.Core.Scripts.Presentation.Shared
{
    /// <summary>
    /// ページプレゼンターの基底クラス
    /// 画面遷移機能を持つプレゼンターの共通実装を提供する
    /// </summary>
    /// <typeparam name="TPage">ページの型</typeparam>
    /// <typeparam name="TRootView">ルートビューの型</typeparam>
    /// <typeparam name="TRootViewState">ルートビューの状態の型</typeparam>
    public abstract class PagePresenterBase<TPage, TRootView, TRootViewState>
        : PagePresenter<TPage, TRootView, TRootViewState>
        where TPage : Page<TRootView, TRootViewState>
        where TRootView : AppView<TRootViewState>
        where TRootViewState : AppViewState, new()
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="view">ページビュー</param>
        /// <param name="transitionService">画面遷移サービス</param>
        protected PagePresenterBase(TPage view, ITransitionService transitionService) : base(view)
        {
            TransitionService = transitionService;
        }

        /// <summary>
        /// 画面遷移サービス
        /// 画面遷移の表示・非表示の制御に使用される
        /// </summary>
        protected ITransitionService TransitionService { get; }
    }
}

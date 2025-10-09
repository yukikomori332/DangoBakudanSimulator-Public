using Project.Subsystem.PresentationFramework;

namespace Project.Core.Scripts.Presentation.Shared
{
    /// <summary>
    /// モーダルプレゼンターの基底クラス
    /// モーダル画面の遷移機能を持つプレゼンターの共通実装を提供する
    /// </summary>
    /// <typeparam name="TModal">モーダルの型</typeparam>
    /// <typeparam name="TRootView">ルートビューの型</typeparam>
    /// <typeparam name="TRootViewState">ルートビューの状態の型</typeparam>
    public abstract class ModalPresenterBase<TModal, TRootView, TRootViewState>
        : ModalPresenter<TModal, TRootView, TRootViewState>
        where TModal : Modal<TRootView, TRootViewState>
        where TRootView : AppView<TRootViewState>
        where TRootViewState : AppViewState, new()
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="view">モーダルビュー</param>
        /// <param name="transitionService">画面遷移サービス</param>
        protected ModalPresenterBase(TModal view, ITransitionService transitionService) : base(view)
        {
            TransitionService = transitionService;
        }

        /// <summary>
        /// 画面遷移サービス
        /// モーダル画面の表示・非表示の制御に使用される
        /// </summary>
        protected ITransitionService TransitionService { get; }
    }
}

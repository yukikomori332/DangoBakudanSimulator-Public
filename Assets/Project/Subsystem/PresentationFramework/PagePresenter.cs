using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Project.Subsystem.Misc;
using Project.Subsystem.PresentationFramework.UnityScreenNavigatorExtensions;

namespace Project.Subsystem.PresentationFramework
{
    /// <summary>
    /// ページのプレゼンター基底クラス
    /// </summary>
    /// <typeparam name="TPage">ページビューの型</typeparam>
    /// <typeparam name="TRootView">ルートビューの型</typeparam>
    /// <typeparam name="TRootViewState">ビューの状態を表す型</typeparam>
    public abstract class PagePresenter<TPage, TRootView, TRootViewState> : PagePresenter<TPage>,
        IDisposableCollectionHolder
        where TPage : Page<TRootView, TRootViewState>
        where TRootView : AppView<TRootViewState>
        where TRootViewState : AppViewState, new()
    {
        // 破棄可能なリソースを保持するリスト
        private readonly List<IDisposable> _disposables = new List<IDisposable>();

        // ビューの状態
        private TRootViewState _state;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="view">管理対象のページビュー</param>
        protected PagePresenter(TPage view) : base(view)
        {
        }

        // 破棄可能なリソースのコレクション
        ICollection<IDisposable> IDisposableCollectionHolder.GetDisposableCollection()
        {
            return _disposables;
        }

        /// <summary>
        /// ページの初期化時に呼び出される
        /// </summary>
        protected sealed override void Initialize(TPage view)
        {
            base.Initialize(view);
        }

        /// <summary>
        /// ページの初期化時に呼び出される
        /// </summary>
        protected sealed override async Task ViewDidLoad(TPage view)
        {
            await base.ViewDidLoad(view);
            var state = new TRootViewState();
            _state = state;
            _disposables.Add(state);
            view.Setup(state);
            await ViewDidLoad(view, _state);
        }

        /// <summary>
        /// ページが表示される直前に呼び出される
        /// </summary>
        protected sealed override async Task ViewWillPushEnter(TPage view)
        {
            await base.ViewWillPushEnter(view);
            await ViewWillPushEnter(view, _state);
        }

        /// <summary>
        /// ページが表示された直後に呼び出される
        /// </summary>
        protected sealed override void ViewDidPushEnter(TPage view)
        {
            base.ViewDidPushEnter(view);
            ViewDidPushEnter(view, _state);
        }

        /// <summary>
        /// ページが閉じられる直前に呼び出される
        /// </summary>
        protected sealed override async Task ViewWillPushExit(TPage view)
        {
            await base.ViewWillPushExit(view);
            await ViewWillPushExit(view, _state);
        }

        /// <summary>
        /// ページが閉じられた直後に呼び出される
        /// </summary>
        protected sealed override void ViewDidPushExit(TPage view)
        {
            base.ViewDidPushExit(view);
            ViewDidPushExit(view, _state);
        }

        //// <summary>
        /// ページがポップインされる直前に呼び出される
        /// </summary>
        protected sealed override async Task ViewWillPopEnter(TPage view)
        {
            await base.ViewWillPopEnter(view);
            await ViewWillPopEnter(view, _state);
        }

        /// <summary>
        /// ページがポップインされた直後に呼び出される
        /// </summary>
        protected sealed override void ViewDidPopEnter(TPage view)
        {
            base.ViewDidPopEnter(view);
            ViewDidPopEnter(view, _state);
        }

        /// <summary>
        /// ページがポップアウトされる直前に呼び出される
        /// </summary>
        protected sealed override async Task ViewWillPopExit(TPage view)
        {
            await base.ViewWillPopExit(view);
            await ViewWillPopExit(view, _state);
        }

        /// <summary>
        /// ページがポップアウトされた直後に呼び出される
        /// </summary>
        protected sealed override void ViewDidPopExit(TPage view)
        {
            base.ViewDidPopExit(view);
            ViewDidPopExit(view, _state);
        }

        /// <summary>
        /// ページが破棄される直前に呼び出される
        /// </summary>
        protected override async Task ViewWillDestroy(TPage view)
        {
            await base.ViewWillDestroy(view);
            await ViewWillDestroy(view, _state);
        }

        // 以下は各ライフサイクルメソッドの仮想メソッド
        // 継承先で必要に応じてオーバーライドして使用する

        /// <summary>
        /// ビューの初期化完了時に呼び出される
        /// </summary>
        protected virtual Task ViewDidLoad(TPage view, TRootViewState viewState)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// ページが表示される直前に呼び出される
        /// </summary>
        protected virtual Task ViewWillPushEnter(TPage view, TRootViewState viewState)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// ページが表示された直後に呼び出される
        /// </summary>
        protected virtual void ViewDidPushEnter(TPage view, TRootViewState viewState)
        {
        }

        /// <summary>
        /// ページが閉じられる直前に呼び出される
        /// </summary>
        protected virtual Task ViewWillPushExit(TPage view, TRootViewState viewState)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// ページが閉じられた直後に呼び出される
        /// </summary>
        protected virtual void ViewDidPushExit(TPage view, TRootViewState viewState)
        {
        }

        /// <summary>
        /// ページがポップインされる直前に呼び出される
        /// </summary>
        protected virtual Task ViewWillPopEnter(TPage view, TRootViewState viewState)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// ページがポップインされた直後に呼び出される
        /// </summary>
        protected virtual void ViewDidPopEnter(TPage view, TRootViewState viewState)
        {
        }

        /// <summary>
        /// ページがポップアウトされる直前に呼び出される
        /// </summary>
        protected virtual Task ViewWillPopExit(TPage view, TRootViewState viewState)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// ページがポップアウトされた直後に呼び出される
        /// </summary>
        protected virtual void ViewDidPopExit(TPage view, TRootViewState viewState)
        {
        }

        /// <summary>
        /// ビューが破棄される直前に呼び出される
        /// </summary>
        protected virtual Task ViewWillDestroy(TPage view, TRootViewState viewState)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// リソースの解放を行う
        /// 保持している全ての破棄可能なリソースを解放する
        /// </summary>
        protected sealed override void Dispose(TPage view)
        {
            base.Dispose(view);
            foreach (var disposable in _disposables)
                disposable.Dispose();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Project.Subsystem.Misc;
using Project.Subsystem.PresentationFramework.UnityScreenNavigatorExtensions;

namespace Project.Subsystem.PresentationFramework
{
    /// <summary>
    /// モーダルのプレゼンター基底クラス
    /// </summary>
    /// <typeparam name="TModal">モーダルビューの型</typeparam>
    /// <typeparam name="TRootView">ルートビューの型</typeparam>
    /// <typeparam name="TRootViewState">ビューの状態を表す型</typeparam>
    public abstract class ModalPresenter<TModal, TRootView, TRootViewState> : ModalPresenter<TModal>,
        IDisposableCollectionHolder
        where TModal : Modal<TRootView, TRootViewState>
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
        /// <param name="view">管理対象のモーダルビュー</param>
        protected ModalPresenter(TModal view) : base(view)
        {
        }

        // 破棄可能なリソースのコレクション
        ICollection<IDisposable> IDisposableCollectionHolder.GetDisposableCollection()
        {
            return _disposables;
        }

        /// <summary>
        /// モーダルの初期化時に呼び出される
        /// </summary>
        protected sealed override void Initialize(TModal view)
        {
            base.Initialize(view);
        }

        /// <summary>
        /// モーダルの初期化時に呼び出される
        /// </summary>
        protected sealed override async Task ViewDidLoad(TModal view)
        {
            await base.ViewDidLoad(view);
            var state = new TRootViewState();
            _state = state;
            _disposables.Add(state);
            view.Setup(state);
            await ViewDidLoad(view, _state);
        }

        /// <summary>
        /// モーダルが表示される直前に呼び出される
        /// </summary>
        protected sealed override async Task ViewWillPushEnter(TModal view)
        {
            await base.ViewWillPushEnter(view);
            await ViewWillPushEnter(view, _state);
        }

        /// <summary>
        /// モーダルが表示された直後に呼び出される
        /// </summary>
        protected sealed override void ViewDidPushEnter(TModal view)
        {
            base.ViewDidPushEnter(view);
            ViewDidPushEnter(view, _state);
        }

        /// <summary>
        /// モーダルが閉じられる直前に呼び出される
        /// </summary>
        protected sealed override async Task ViewWillPushExit(TModal view)
        {
            await base.ViewWillPushExit(view);
            await ViewWillPushExit(view, _state);
        }

        /// <summary>
        /// モーダルが閉じられた直後に呼び出される
        /// </summary>
        protected sealed override void ViewDidPushExit(TModal view)
        {
            base.ViewDidPushExit(view);
            ViewDidPushExit(view, _state);
        }

        /// <summary>
        /// モーダルがポップインされる直前に呼び出される
        /// </summary>
        protected sealed override async Task ViewWillPopEnter(TModal view)
        {
            await base.ViewWillPopEnter(view);
            await ViewWillPopEnter(view, _state);
        }

        /// <summary>
        /// モーダルがポップインされた直後に呼び出される
        /// </summary>
        protected sealed override void ViewDidPopEnter(TModal view)
        {
            base.ViewDidPopEnter(view);
            ViewDidPopEnter(view, _state);
        }

        /// <summary>
        /// モーダルがポップアウトされる直前に呼び出される
        /// </summary>
        protected sealed override async Task ViewWillPopExit(TModal view)
        {
            await base.ViewWillPopExit(view);
            await ViewWillPopExit(view, _state);
        }

        /// <summary>
        /// モーダルがポップアウトされた直後に呼び出される
        /// </summary>
        protected sealed override void ViewDidPopExit(TModal view)
        {
            base.ViewDidPopExit(view);
            ViewDidPopExit(view, _state);
        }

        /// <summary>
        /// モーダルが破棄される直前に呼び出される
        /// </summary>
        protected override async Task ViewWillDestroy(TModal view)
        {
            await base.ViewWillDestroy(view);
            await ViewWillDestroy(view, _state);
        }

        // 以下は各ライフサイクルメソッドの仮想メソッド
        // 継承先で必要に応じてオーバーライドして使用する

        /// <summary>
        /// ビューの初期化完了時に呼び出される
        /// </summary>
        protected virtual Task ViewDidLoad(TModal view, TRootViewState viewState)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// モーダルが表示される直前に呼び出される
        /// </summary>
        protected virtual Task ViewWillPushEnter(TModal view, TRootViewState viewState)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// モーダルが表示された直後に呼び出される
        /// </summary>
        protected virtual void ViewDidPushEnter(TModal view, TRootViewState viewState)
        {
        }

        /// <summary>
        /// モーダルが閉じられる直前に呼び出される
        /// </summary>
        protected virtual Task ViewWillPushExit(TModal view, TRootViewState viewState)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// モーダルが閉じられた直後に呼び出される
        /// </summary>
        protected virtual void ViewDidPushExit(TModal view, TRootViewState viewState)
        {
        }

        /// <summary>
        /// モーダルがポップインされる直前に呼び出される
        /// </summary>
        protected virtual Task ViewWillPopEnter(TModal view, TRootViewState viewState)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// モーダルがポップインされた直後に呼び出される
        /// </summary>
        protected virtual void ViewDidPopEnter(TModal view, TRootViewState viewState)
        {
        }

        /// <summary>
        /// モーダルがポップアウトされる直前に呼び出される
        /// </summary>
        protected virtual Task ViewWillPopExit(TModal view, TRootViewState viewState)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// モーダルがポップアウトされた直後に呼び出される
        /// </summary>
        protected virtual void ViewDidPopExit(TModal view, TRootViewState viewState)
        {
        }

        /// <summary>
        /// ビューが破棄される直前に呼び出される
        /// </summary>
        protected virtual Task ViewWillDestroy(TModal view, TRootViewState viewState)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// リソースの解放を行う
        /// 保持している全ての破棄可能なリソースを解放する
        /// </summary>
        protected sealed override void Dispose(TModal view)
        {
            base.Dispose(view);
            foreach (var disposable in _disposables)
                disposable.Dispose();
        }
    }
}

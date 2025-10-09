using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Project.Subsystem.Misc;
using Project.Subsystem.PresentationFramework.UnityScreenNavigatorExtensions;

namespace Project.Subsystem.PresentationFramework
{
    /// <summary>
    /// シートのプレゼンター基底クラス
    /// </summary>
    /// <typeparam name="TPage">シートビューの型</typeparam>
    /// <typeparam name="TRootView">ルートビューの型</typeparam>
    /// <typeparam name="TRootViewState">ビューの状態を表す型</typeparam>
    public abstract class SheetPresenter<TSheet, TRootView, TRootViewState> : SheetPresenter<TSheet>,
        IDisposableCollectionHolder
        where TSheet : Sheet<TRootView, TRootViewState>
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
        /// <param name="view">管理対象のシートビュー</param>
        protected SheetPresenter(TSheet view) : base(view)
        {
        }

        // 破棄可能なリソースのコレクション
        ICollection<IDisposable> IDisposableCollectionHolder.GetDisposableCollection()
        {
            return _disposables;
        }

        /// <summary>
        /// シートの初期化時に呼び出される
        /// </summary>
        protected sealed override void Initialize(TSheet view)
        {
            base.Initialize(view);
        }

        /// <summary>
        /// シートの初期化時に呼び出される
        /// </summary>
        protected sealed override async Task ViewDidLoad(TSheet view)
        {
            await base.ViewDidLoad(view);
            var state = new TRootViewState();
            _state = state;
            _disposables.Add(state);
            view.Setup(state);
            await ViewDidLoad(view, _state);
        }

        /// <summary>
        /// シートが表示される直前に呼び出される
        /// </summary>
        protected sealed override async Task ViewWillEnter(TSheet view)
        {
            await base.ViewWillEnter(view);
            await ViewWillEnter(view, _state);
        }

        /// <summary>
        /// シートが表示された直後に呼び出される
        /// </summary>
        protected sealed override void ViewDidEnter(TSheet view)
        {
            base.ViewDidEnter(view);
            ViewDidEnter(view, _state);
        }

        /// <summary>
        /// シートが閉じられる直前に呼び出される
        /// </summary>
        protected sealed override async Task ViewWillExit(TSheet view)
        {
            await base.ViewWillExit(view);
            await ViewWillExit(view, _state);
        }

        /// <summary>
        /// シートが閉じられた直後に呼び出される
        /// </summary>
        protected sealed override void ViewDidExit(TSheet view)
        {
            base.ViewDidExit(view);
            ViewDidExit(view, _state);
        }

        /// <summary>
        /// シートが破棄される直前に呼び出される
        /// </summary>
        protected override async Task ViewWillDestroy(TSheet view)
        {
            await base.ViewWillDestroy(view);
            await ViewWillDestroy(view, _state);
        }

        // 以下は各ライフサイクルメソッドの仮想メソッド
        // 継承先で必要に応じてオーバーライドして使用する

        /// <summary>
        /// ビューの初期化完了時に呼び出される
        /// </summary>
        protected virtual Task ViewDidLoad(TSheet view, TRootViewState viewState)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// シートが表示される直前に呼び出される
        /// </summary>
        protected virtual Task ViewWillEnter(TSheet view, TRootViewState viewState)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// シートが表示された直後に呼び出される
        /// </summary>
        protected virtual void ViewDidEnter(TSheet view, TRootViewState viewState)
        {
        }

        /// <summary>
        /// シートが閉じられる直前に呼び出される
        /// </summary>
        protected virtual Task ViewWillExit(TSheet view, TRootViewState viewState)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// シートが閉じられた直後に呼び出される
        /// </summary>
        protected virtual void ViewDidExit(TSheet view, TRootViewState viewState)
        {
        }

        /// <summary>
        /// ビューが破棄される直前に呼び出される
        /// </summary>
        protected virtual Task ViewWillDestroy(TSheet view, TRootViewState viewState)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// リソースの解放を行う
        /// 保持している全ての破棄可能なリソースを解放する
        /// </summary>
        protected sealed override void Dispose(TSheet view)
        {
            base.Dispose(view);
            foreach (var disposable in _disposables)
                disposable.Dispose();
        }
    }
}

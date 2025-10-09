using System.Threading.Tasks;
using UnityScreenNavigator.Runtime.Core.Page;

namespace Project.Subsystem.PresentationFramework.UnityScreenNavigatorExtensions
{
    /// <summary>
    /// UnityScreenNavigatorのページ遷移を管理するプレゼンターの基底クラス
    /// </summary>
    /// <typeparam name="TPage">管理対象のページの型</typeparam>
    public abstract class PagePresenter<TPage> : Presenter<TPage>, IPagePresenter where TPage : Page
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="view">管理対象のページビュー</param>
        protected PagePresenter(TPage view) : base(view)
        {
            View = view;
        }

        // Presenterが管理するViewのインスタンス
        private TPage View { get; }

        // ライフサイクルイベントの実装
        // 各メソッドは非同期（Task）またはコルーチン（IEnumerator）のいずれかで実装可能
        // USN_USE_ASYNC_METHODSの定義によって切り替わる

        /// <summary>
        /// ページの初期化時に呼ばれる
        /// </summary>
#if USN_USE_ASYNC_METHODS
        Task IPageLifecycleEvent.Initialize()
        {
            return ViewDidLoad(View);
        }
#else
        IEnumerator IPageLifecycleEvent.Initialize()
        {
            return ViewDidLoad(View);
        }
#endif

        /// <summary>
        /// ページがプッシュされて表示される直前に呼ばれる
        /// </summary>
#if USN_USE_ASYNC_METHODS
        Task IPageLifecycleEvent.WillPushEnter()
        {
            return ViewWillPushEnter(View);
        }
#else
        IEnumerator IPageLifecycleEvent.WillPushEnter()
        {
            return ViewWillPushEnter(View);
        }
#endif

        /// <summary>
        /// ページがプッシュされて表示された直後に呼ばれる
        /// </summary>
        void IPageLifecycleEvent.DidPushEnter()
        {
            ViewDidPushEnter(View);
        }

        /// <summary>
        /// ページがプッシュスタックから出る直前に呼ばれる
        /// </summary>
#if USN_USE_ASYNC_METHODS
        Task IPageLifecycleEvent.WillPushExit()
        {
            return ViewWillPushExit(View);
        }
#else
        IEnumerator IPageLifecycleEvent.WillPushExit()
        {
            return ViewWillPushExit(View);
        }
#endif

        /// <summary>
        /// ページがプッシュスタックから出た直後に呼ばれる
        /// </summary>
        void IPageLifecycleEvent.DidPushExit()
        {
            ViewDidPushExit(View);
        }

        /// <summary>
        /// ページがポップされて表示される直前に呼ばれる
        /// </summary>
#if USN_USE_ASYNC_METHODS
        Task IPageLifecycleEvent.WillPopEnter()
        {
            return ViewWillPopEnter(View);
        }
#else
        IEnumerator IPageLifecycleEvent.WillPopEnter()
        {
            return ViewWillPopEnter(View);
        }
#endif

        /// <summary>
        /// ページがポップされて表示された直後に呼ばれる
        /// </summary>
        void IPageLifecycleEvent.DidPopEnter()
        {
            ViewDidPopEnter(View);
        }

        /// <summary>
        /// ページがポップスタックから出る直前に呼ばれる
        /// </summary>
#if USN_USE_ASYNC_METHODS
        Task IPageLifecycleEvent.WillPopExit()
        {
            return ViewWillPopExit(View);
        }
#else
        IEnumerator IPageLifecycleEvent.WillPopExit()
        {
            return ViewWillPopExit(View);
        }
#endif

        /// <summary>
        /// ページがポップスタックから出た直後に呼ばれる
        /// </summary>
        void IPageLifecycleEvent.DidPopExit()
        {
            ViewDidPopExit(View);
        }

        /// <summary>
        /// ページの破棄時に呼ばれる
        /// </summary>
#if USN_USE_ASYNC_METHODS
        Task IPageLifecycleEvent.Cleanup()
        {
            return ViewWillDestroy(View);
        }
#else
        IEnumerator IPageLifecycleEvent.Cleanup()
        {
            return ViewWillDestroy(View);
        }
#endif

        // 以下は各ライフサイクルイベントのデフォルト実装
        // 継承先で必要に応じてオーバーライド可能

        /// <summary>
        /// ページの初期化時に呼び出される
        /// </summary>
#if USN_USE_ASYNC_METHODS
        protected virtual Task ViewDidLoad(TPage view)
        {
            return Task.CompletedTask;
        }
#else
        protected virtual IEnumerator ViewDidLoad(TPage view)
        {
            yield break;
        }
#endif

        /// <summary>
        /// ページが表示される直前に呼び出される
        /// </summary>
#if USN_USE_ASYNC_METHODS
        protected virtual Task ViewWillPushEnter(TPage view)
        {
            return Task.CompletedTask;
        }
#else
        protected virtual IEnumerator ViewWillPushEnter(TPage view)
        {
            yield break;
        }
#endif

        /// <summary>
        /// ページが表示された直後に呼び出される
        /// </summary>
        protected virtual void ViewDidPushEnter(TPage view)
        {
        }

        /// <summary>
        /// ページが閉じられる直前に呼び出される
        /// </summary>
#if USN_USE_ASYNC_METHODS
        protected virtual Task ViewWillPushExit(TPage view)
        {
            return Task.CompletedTask;
        }
#else
        protected virtual IEnumerator ViewWillPushExit(TPage view)
        {
            yield break;
        }
#endif

        /// <summary>
        /// ページが閉じられた直後に呼び出される
        /// </summary>
        protected virtual void ViewDidPushExit(TPage view)
        {
        }

        /// <summary>
        /// ページがポップインされる直前に呼び出される
        /// </summary>
#if USN_USE_ASYNC_METHODS
        protected virtual Task ViewWillPopEnter(TPage view)
        {
            return Task.CompletedTask;
        }
#else
        protected virtual IEnumerator ViewWillPopEnter(TPage view)
        {
            yield break;
        }
#endif

        /// <summary>
        /// ページがポップインされた直後に呼び出される
        /// </summary>
        protected virtual void ViewDidPopEnter(TPage view)
        {
        }

        /// <summary>
        /// モーダルがポップアウトされる直前に呼び出される
        /// </summary>
#if USN_USE_ASYNC_METHODS
        protected virtual Task ViewWillPopExit(TPage view)
        {
            return Task.CompletedTask;
        }
#else
        protected virtual IEnumerator ViewWillPopExit(TPage view)
        {
            yield break;
        }
#endif

        /// <summary>
        /// ページがポップアウトされた直後に呼び出される
        /// </summary>
        protected virtual void ViewDidPopExit(TPage view)
        {
        }

        /// <summary>
        /// ページが破棄される直前に呼び出される
        /// </summary>
#if USN_USE_ASYNC_METHODS
        protected virtual Task ViewWillDestroy(TPage view)
        {
            return Task.CompletedTask;
        }
#else
        protected virtual IEnumerator ViewWillDestroy(TPage view)
        {
            yield break;
        }
#endif

        /// <summary>
        /// プレゼンターの初期化処理
        /// ビューのライフサイクルイベントに優先度1で登録する
        /// </summary>
        protected override void Initialize(TPage view)
        {
            // The lifecycle event of the view will be added with priority 0.
            // Presenters should be processed after the view so set the priority to 1.
            // ビューのライフサイクルイベントは優先度0で登録される
            // プレゼンターはビューの後に処理されるべきなので、優先度を1に設定
            view.AddLifecycleEvent(this, 1);
        }

        /// <summary>
        /// プレゼンターの破棄処理
        /// ビューのライフサイクルイベントから登録を解除する
        /// </summary>
        protected override void Dispose(TPage view)
        {
            view.RemoveLifecycleEvent(this);
        }
    }
}

using System.Threading.Tasks;
using UnityScreenNavigator.Runtime.Core.Modal;

namespace Project.Subsystem.PresentationFramework.UnityScreenNavigatorExtensions
{
    /// <summary>
    /// UnityScreenNavigatorのモーダル遷移を管理するプレゼンターの基底クラス
    /// </summary>
    /// <typeparam name="TModal">管理対象のモーダルの型</typeparam>
    public abstract class ModalPresenter<TModal> : Presenter<TModal>, IModalPresenter where TModal : Modal
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="view">管理対象のモーダルビュー</param>
        protected ModalPresenter(TModal view) : base(view)
        {
            View = view;
        }

        // Presenterが管理するViewのインスタンス
        private TModal View { get; }

        // ライフサイクルイベントの実装
        // 各メソッドは非同期（Task）またはコルーチン（IEnumerator）のいずれかで実装可能
        // USN_USE_ASYNC_METHODSの定義によって切り替わる

        /// <summary>
        /// モーダルの初期化時に呼ばれる
        /// </summary>
#if USN_USE_ASYNC_METHODS
        Task IModalLifecycleEvent.Initialize()
        {
            return ViewDidLoad(View);
        }
#else
        IEnumerator IModalLifecycleEvent.Initialize()
        {
            return ViewDidLoad(View);
        }
#endif

        /// <summary>
        /// モーダルがプッシュされて表示される直前に呼ばれる
        /// </summary>
#if USN_USE_ASYNC_METHODS
        Task IModalLifecycleEvent.WillPushEnter()
        {
            return ViewWillPushEnter(View);
        }
#else
        IEnumerator IModalLifecycleEvent.WillPushEnter()
        {
            return ViewWillPushEnter(View);
        }
#endif

        /// <summary>
        /// モーダルがプッシュされて表示された直後に呼ばれる
        /// </summary>
        void IModalLifecycleEvent.DidPushEnter()
        {
            ViewDidPushEnter(View);
        }

        /// <summary>
        /// モーダルがプッシュスタックから出る直前に呼ばれる
        /// </summary>
#if USN_USE_ASYNC_METHODS
        Task IModalLifecycleEvent.WillPushExit()
        {
            return ViewWillPushExit(View);
        }
#else
        IEnumerator IModalLifecycleEvent.WillPushExit()
        {
            return ViewWillPushExit(View);
        }
#endif

        /// <summary>
        /// モーダルがプッシュスタックから出た直後に呼ばれる
        /// </summary>
        void IModalLifecycleEvent.DidPushExit()
        {
            ViewDidPushExit(View);
        }

        /// <summary>
        /// モーダルがポップされて表示される直前に呼ばれる
        /// </summary>
#if USN_USE_ASYNC_METHODS
        Task IModalLifecycleEvent.WillPopEnter()
        {
            return ViewWillPopEnter(View);
        }
#else
        IEnumerator IModalLifecycleEvent.WillPopEnter()
        {
            return ViewWillPopEnter(View);
        }
#endif

        /// <summary>
        /// モーダルがポップされて表示された直後に呼ばれる
        /// </summary>
        void IModalLifecycleEvent.DidPopEnter()
        {
            ViewDidPopEnter(View);
        }

        /// <summary>
        /// モーダルがポップスタックから出る直前に呼ばれる
        /// </summary>
#if USN_USE_ASYNC_METHODS
        Task IModalLifecycleEvent.WillPopExit()
        {
            return ViewWillPopExit(View);
        }
#else
        IEnumerator IModalLifecycleEvent.WillPopExit()
        {
            return ViewWillPopExit(View);
        }
#endif

        /// <summary>
        /// モーダルがポップスタックから出た直後に呼び出される
        /// </summary>
        void IModalLifecycleEvent.DidPopExit()
        {
            ViewDidPopExit(View);
        }

        /// <summary>
        /// モーダルの破棄時に呼び出される
        /// </summary>
#if USN_USE_ASYNC_METHODS
        Task IModalLifecycleEvent.Cleanup()
        {
            return ViewWillDestroy(View);
        }
#else
        IEnumerator IModalLifecycleEvent.Cleanup()
        {
            return ViewWillDestroy(View);
        }
#endif

        // 以下は各ライフサイクルイベントのデフォルト実装
        // 継承先で必要に応じてオーバーライド可能

        /// <summary>
        /// モーダルの初期化時に呼び出される
        /// </summary>
#if USN_USE_ASYNC_METHODS
        protected virtual Task ViewDidLoad(TModal view)
        {
            return Task.CompletedTask;
        }
#else
        protected virtual IEnumerator ViewDidLoad(TModal view)
        {
            yield break;
        }
#endif

        /// <summary>
        /// モーダルが表示される直前に呼び出される
        /// </summary>
#if USN_USE_ASYNC_METHODS
        protected virtual Task ViewWillPushEnter(TModal view)
        {
            return Task.CompletedTask;
        }
#else
        protected virtual IEnumerator ViewWillPushEnter(TModal view)
        {
            yield break;
        }
#endif

        /// <summary>
        /// モーダルが表示された直後に呼び出される
        /// </summary>
        protected virtual void ViewDidPushEnter(TModal view)
        {
        }

        /// <summary>
        /// モーダルが閉じられる直前に呼び出される
        /// </summary>
#if USN_USE_ASYNC_METHODS
        protected virtual Task ViewWillPushExit(TModal view)
        {
            return Task.CompletedTask;
        }
#else
        protected virtual IEnumerator ViewWillPushExit(TModal view)
        {
            yield break;
        }
#endif

        /// <summary>
        /// モーダルが閉じられた直後に呼び出される
        /// </summary>
        protected virtual void ViewDidPushExit(TModal view)
        {
        }

        /// <summary>
        /// モーダルがポップインされる直前に呼び出される
        /// </summary>
#if USN_USE_ASYNC_METHODS
        protected virtual Task ViewWillPopEnter(TModal view)
        {
            return Task.CompletedTask;
        }
#else
        protected virtual IEnumerator ViewWillPopEnter(TModal view)
        {
            yield break;
        }
#endif

        /// <summary>
        /// モーダルがポップインされた直後に呼び出される
        /// </summary>
        protected virtual void ViewDidPopEnter(TModal view)
        {
        }

        /// <summary>
        /// モーダルがポップアウトされる直前に呼び出される
        /// </summary>
#if USN_USE_ASYNC_METHODS
        protected virtual Task ViewWillPopExit(TModal view)
        {
            return Task.CompletedTask;
        }
#else
        protected virtual IEnumerator ViewWillPopExit(TModal view)
        {
            yield break;
        }
#endif

        /// <summary>
        /// モーダルがポップアウトされた直後に呼び出される
        /// </summary>
        protected virtual void ViewDidPopExit(TModal view)
        {
        }

        /// <summary>
        /// モーダルが破棄される直前に呼び出される
        /// </summary>
#if USN_USE_ASYNC_METHODS
        protected virtual Task ViewWillDestroy(TModal view)
        {
            return Task.CompletedTask;
        }
#else
        protected virtual IEnumerator ViewWillDestroy(TModal view)
        {
            yield break;
        }
#endif

        /// <summary>
        /// プレゼンターの初期化処理
        /// ビューのライフサイクルイベントに優先度1で登録する
        /// </summary>
        protected override void Initialize(TModal view)
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
        protected override void Dispose(TModal view)
        {
            view.RemoveLifecycleEvent(this);
        }
    }
}

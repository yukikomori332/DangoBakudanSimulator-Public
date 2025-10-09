using System.Threading.Tasks;
using UnityScreenNavigator.Runtime.Core.Sheet;

namespace Project.Subsystem.PresentationFramework.UnityScreenNavigatorExtensions
{
    /// <summary>
    /// UnityScreenNavigatorのシート遷移を管理するプレゼンターの基底クラス
    /// </summary>
    /// <typeparam name="TSheet">管理対象のシートの型</typeparam>
    public abstract class SheetPresenter<TSheet> : Presenter<TSheet>, ISheetPresenter
        where TSheet : Sheet
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="view">管理対象のシートビュー</param>
        protected SheetPresenter(TSheet view) : base(view)
        {
            View = view;
        }

        // Presenterが管理するViewのインスタンス
        private TSheet View { get; }

        // ライフサイクルイベントの実装
        // 各メソッドは非同期（Task）またはコルーチン（IEnumerator）のいずれかで実装可能
        // USN_USE_ASYNC_METHODSの定義によって切り替わる

        /// <summary>
        /// シートの初期化時に呼ばれる
        /// </summary>
#if USN_USE_ASYNC_METHODS
        Task ISheetLifecycleEvent.Initialize()
        {
            return ViewDidLoad(View);
        }
#else
        IEnumerator ISheetLifecycleEvent.Initialize()
        {
            return ViewDidLoad(View);
        }
#endif

        /// <summary>
        /// シートが表示される直前に呼ばれる
        /// </summary>
#if USN_USE_ASYNC_METHODS
        Task ISheetLifecycleEvent.WillEnter()
        {
            return ViewWillEnter(View);
        }
#else
        IEnumerator ISheetLifecycleEvent.WillEnter()
        {
            return ViewWillEnter(View);
        }
#endif

        /// <summary>
        /// シートが表示された直後に呼ばれる
        /// </summary>
        void ISheetLifecycleEvent.DidEnter()
        {
            ViewDidEnter(View);
        }

        /// <summary>
        /// シートが出る直前に呼ばれる
        /// </summary>
#if USN_USE_ASYNC_METHODS
        Task ISheetLifecycleEvent.WillExit()
        {
            return ViewWillExit(View);
        }
#else
        IEnumerator ISheetLifecycleEvent.WillExit()
        {
            return ViewWillExit(View);
        }
#endif

        /// <summary>
        /// シートが出た直後に呼ばれる
        /// </summary>
        void ISheetLifecycleEvent.DidExit()
        {
            ViewDidExit(View);
        }

         /// <summary>
        /// シートの破棄時に呼ばれる
        /// </summary>
#if USN_USE_ASYNC_METHODS
        Task ISheetLifecycleEvent.Cleanup()
        {
            return ViewWillDestroy(View);
        }
#else
        IEnumerator ISheetLifecycleEvent.Cleanup()
        {
            return ViewWillDestroy(View);
        }
#endif

        // 以下は各ライフサイクルイベントのデフォルト実装
        // 継承先で必要に応じてオーバーライド可能

        /// <summary>
        /// シートの初期化時に呼び出される
        /// </summary>
#if USN_USE_ASYNC_METHODS
        protected virtual Task ViewDidLoad(TSheet view)
        {
            return Task.CompletedTask;
        }
#else
        protected virtual IEnumerator ViewDidLoad(TSheet view)
        {
            yield break;
        }
#endif

        /// <summary>
        /// シートが表示される直前に呼び出される
        /// </summary>
#if USN_USE_ASYNC_METHODS
        protected virtual Task ViewWillEnter(TSheet view)
        {
            return Task.CompletedTask;
        }
#else
        protected virtual IEnumerator ViewWillEnter(TSheet view)
        {
            yield break;
        }
#endif

        /// <summary>
        /// シートが表示された直後に呼び出される
        /// </summary>
        protected virtual void ViewDidEnter(TSheet view)
        {
        }

        /// <summary>
        /// シートが閉じられる直前に呼び出される
        /// </summary>
#if USN_USE_ASYNC_METHODS
        protected virtual Task ViewWillExit(TSheet view)
        {
            return Task.CompletedTask;
        }
#else
        protected virtual IEnumerator ViewWillExit(TSheet view)
        {
            yield break;
        }
#endif

        /// <summary>
        /// シートが閉じられた直後に呼び出される
        /// </summary>
        protected virtual void ViewDidExit(TSheet view)
        {
        }

        /// <summary>
        /// シートが破棄される直前に呼び出される
        /// </summary>
#if USN_USE_ASYNC_METHODS
        protected virtual Task ViewWillDestroy(TSheet view)
        {
            return Task.CompletedTask;
        }
#else
        protected virtual IEnumerator ViewWillDestroy(TSheet view)
        {
            yield break;
        }
#endif

        /// <summary>
        /// プレゼンターの初期化処理
        /// ビューのライフサイクルイベントに優先度1で登録する
        /// </summary>
        protected override void Initialize(TSheet view)
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
        protected override void Dispose(TSheet view)
        {
            view.RemoveLifecycleEvent(this);
        }
    }
}

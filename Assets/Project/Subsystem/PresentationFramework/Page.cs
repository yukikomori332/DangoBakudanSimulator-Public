using System.Threading.Tasks;
using UnityEngine.Assertions;
using UnityScreenNavigator.Runtime.Core.Page;

namespace Project.Subsystem.PresentationFramework
{
    /// <summary>
    /// ページの基底クラス。
    /// </summary>
    /// <typeparam name="TRootView">ルートビューの型。</typeparam>
    /// <typeparam name="TViewState">ビューの状態を表す型。</typeparam>
    public abstract class Page<TRootView, TViewState> : Page
        where TRootView : AppView<TViewState>
        where TViewState : AppViewState
    {
        public TRootView root;       // ページのルートビュー
        private bool _isInitialized; // 初期化済みかどうかのフラグ
        private TViewState _state;   // ビューの状態

        /// <summary>
        /// ルートビューの初期化タイミングを指定する
        /// BeforeFirstEnter: 初回表示前に初期化
        /// Initialize: インスタンス生成時に初期化
        /// </summary>
        protected virtual ViewInitializationTiming RootInitializationTiming =>
            ViewInitializationTiming.BeforeFirstEnter;

        /// <summary>
        /// ページの初期設定を行う
        /// </summary>
        /// <param name="state">ビューの初期状態</param>
        public void Setup(TViewState state)
        {
            _state = state;
        }

#if USN_USE_ASYNC_METHODS
        /// <summary>
        /// ページの初期化処理
        /// </summary>
        public override async Task Initialize()
        {
            // ルートビューが設定されていることを確認
            Assert.IsNotNull(root);

            await base.Initialize();

            // 初期化タイミングがInitializeの場合、ここで初期化を実行
            if (RootInitializationTiming == ViewInitializationTiming.Initialize && !_isInitialized)
            {
                await root.InitializeAsync(_state);
                _isInitialized = true;
            }
        }
#else
        public override IEnumerator Initialize()
        {
            Assert.IsNotNull(root);

            yield return base.Initialize();

            if (RootInitializationTiming == ViewInitializationTiming.Initialize && !_isInitialized)
            {
                yield return root.InitializeAsync(_state).ToCoroutine();
                _isInitialized = true;
            }
        }
#endif

#if USN_USE_ASYNC_METHODS
        /// <summary>
        /// ページが表示される直前の処理
        /// </summary>
        public override async Task WillPushEnter()
        {
            // ルートビューが設定されていることを確認
            Assert.IsNotNull(root);

            await base.WillPushEnter();

            // 初期化タイミングがBeforeFirstEnterの場合、ここで初期化を実行
            if (RootInitializationTiming == ViewInitializationTiming.BeforeFirstEnter && !_isInitialized)
            {
                await root.InitializeAsync(_state);
                _isInitialized = true;
            }
        }
#else
        public override IEnumerator WillPushEnter()
        {
            Assert.IsNotNull(root);

            yield return base.WillPushEnter();

            if (RootInitializationTiming == ViewInitializationTiming.BeforeFirstEnter && !_isInitialized)
            {
                yield return root.InitializeAsync(_state).ToCoroutine();
                _isInitialized = true;
            }
        }
#endif
    }
}

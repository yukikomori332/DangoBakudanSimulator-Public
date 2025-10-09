using System;

namespace Project.Subsystem.PresentationFramework.UnityScreenNavigatorExtensions
{
    /// <summary>
    /// UnityScreenNavigatorにおけるPresenterの基底クラス
    /// </summary>
    /// <typeparam name="TView">管理対象のViewの型</typeparam>
    public abstract class Presenter<TView> : IDisposable
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="view">管理対象のViewのインスタンス</param>
        protected Presenter(TView view)
        {
            View = view;
        }

        // Presenterが破棄済みかどうかを示すフラグ
        public bool IsDisposed { get; private set; }

        // Presenterが初期化済みかどうかを示すフラグ
        public bool IsInitialized { get; private set; }

        // Presenterが管理するViewのインスタンス
        private TView View { get; }

        /// <summary>
        /// Presenterのリソースを解放する。
        /// 初期化されていない、または既に破棄済みの場合は何もしない。
        /// </summary>
        public virtual void Dispose()
        {
            if (!IsInitialized)
                return;

            if (IsDisposed)
                return;

            Dispose(View);
            IsDisposed = true;
        }

        /// <summary>
        /// Presenterを初期化する。
        /// 既に初期化済み、または破棄済みの場合は例外をスローする。
        /// </summary>
        /// <exception cref="InvalidOperationException">既に初期化済みの場合にスロー</exception>
        /// <exception cref="ObjectDisposedException">既に破棄済みの場合にスロー</exception>
        public void Initialize()
        {
            if (IsInitialized)
                throw new InvalidOperationException($"{GetType().Name} is already initialized.");

            if (IsDisposed)
                throw new ObjectDisposedException(nameof(Presenter<TView>));

            Initialize(View);
            IsInitialized = true;
        }

        /// <summary>
        /// Presenterを初期化する
        /// 派生クラスで実装する必要がある。
        /// </summary>
        /// <param name="view">初期化対象のView</param>
        protected abstract void Initialize(TView view);

        /// <summary>
        /// Presenterを破棄する
        /// 派生クラスで実装する必要がある。
        /// </summary>
        /// <param name="view">破棄対象のView</param>
        protected abstract void Dispose(TView view);
    }

    /// <summary>
    /// データソースを持つPresenterの基底クラス。
    /// View、Presenter、データソースの間のインタラクションを管理する。
    /// </summary>
    /// <typeparam name="TView">Presenterが管理するViewの型</typeparam>
    /// <typeparam name="TDataSource">Presenterが使用するデータソースの型</typeparam>
    public abstract class Presenter<TView, TDataSource> : IDisposable
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="view">Presenterが管理するViewのインスタンス</param>
        /// <param name="dataSource">Presenterが使用するデータソースのインスタンス</param>
        protected Presenter(TView view, TDataSource dataSource)
        {
            View = view;
            DataSource = dataSource;
        }

        // Presenterが破棄済みかどうかを示すフラグ
        public bool IsDisposed { get; private set; }

        // Presenterが初期化済みかどうかを示すフラグ
        public bool IsInitialized { get; private set; }

        // Presenterが管理するViewのインスタンス
        private TView View { get; }

        // Presenterが使用するデータソースのインスタンス
        private TDataSource DataSource { get; }

        /// <summary>
        /// Presenterのリソースを解放する
        /// 初期化されていない、または既に破棄済みの場合は何もしない。
        /// </summary>
        public virtual void Dispose()
        {
            if (!IsInitialized)
                return;

            if (IsDisposed)
                return;

            Dispose(View, DataSource);
            IsDisposed = true;
        }

        /// <summary>
        /// Presenterを初期化する
        /// 既に初期化済み、または破棄済みの場合は例外をスローする。
        /// </summary>
        /// <exception cref="InvalidOperationException">既に初期化済みの場合にスロー</exception>
        /// <exception cref="ObjectDisposedException">既に破棄済みの場合にスロー</exception>
        public void Initialize()
        {
            if (IsInitialized)
                throw new InvalidOperationException($"{GetType().Name} is already initialized.");

            if (IsDisposed)
                throw new ObjectDisposedException(nameof(Presenter<TView, TDataSource>));

            Initialize(View, DataSource);
            IsInitialized = true;
        }

        /// <summary>
        /// データソースを持つPresenterを初期化する
        /// 派生クラスで実装する必要がある。
        /// </summary>
        /// <param name="view">初期化対象のView</param>
        /// <param name="dataStore">初期化対象のデータソース</param>
        protected abstract void Initialize(TView view, TDataSource dataStore);

        /// <summary>
        /// データソースを持つPresenterを破棄する
        /// 派生クラスで実装する必要がある。
        /// </summary>
        /// <param name="view">破棄対象のView</param>
        /// <param name="dataSource">破棄対象のデータソース</param>
        protected abstract void Dispose(TView view, TDataSource dataSource);
    }
}

using System;

namespace Project.Subsystem.PresentationFramework
{
    /// <summary>
    /// アプリケーションのビュー状態を管理する基底クラス
    /// </summary>
    public abstract class AppViewState : IDisposable
    {
        /// <summary>
        /// オブジェクトが既に破棄されているかどうかを示すフラグ
        /// </summary>
        private bool _isDisposed;

        /// <summary>
        /// リソースを解放する
        /// 既に破棄済みの場合はObjectDisposedExceptionをスローする。
        /// </summary>
        /// <exception cref="ObjectDisposedException">オブジェクトが既に破棄されている場合にスローされる。</exception>
        public void Dispose()
        {
            if (_isDisposed)
                throw new ObjectDisposedException(nameof(AppViewState));

            DisposeInternal();

            _isDisposed = true;
        }

        /// <summary>
        /// リソースを解放する
        /// この関数はDispose関数から呼び出され、派生クラスで実装する必要がある。
        /// </summary>
        protected abstract void DisposeInternal();
    }
}

using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Project.Subsystem.PresentationFramework
{
    /// <summary>
    /// アプリケーションのビューを表す基底クラス
    /// </summary>
    /// <typeparam name="TState">ビューの状態を表す型。</typeparam>
    public abstract class AppView<TState> : MonoBehaviour
        where TState : AppViewState
    {
        // ビューが初期化済みかどうかを示すフラグ
        private bool _isInitialized;

        /// <summary>
        /// ビューを初期化する
        /// </summary>
        /// <param name="state">初期化に使用する状態オブジェクト</param>
        /// <returns>初期化完了を表すUniTask</returns>
        /// <remarks>
        /// 既に初期化済みの場合は何も行わない。
        /// </remarks>
        public async UniTask InitializeAsync(TState state)
        {
            if (_isInitialized)
                return;

            _isInitialized = true;

            await Initialize(state);
        }

        /// <summary>
        /// ビューを初期化する
        /// 派生クラスで実装する必要がある。
        /// </summary>
        /// <param name="state">初期化に使用する状態オブジェクト</param>
        /// <returns>初期化完了を表すUniTask</returns>
        protected abstract UniTask Initialize(TState state);
    }
}

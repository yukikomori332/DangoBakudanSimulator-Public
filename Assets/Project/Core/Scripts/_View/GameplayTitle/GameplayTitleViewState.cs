using Project.Subsystem.PresentationFramework;
using UniRx;

namespace Project.Core.Scripts.View.GameplayTitle
{
    /// <summary>
    /// ゲームタイトル表示の状態を管理するクラス
    /// </summary>
    public sealed class GameplayTitleViewState : AppViewState, IGameplayTitleState
    {
        // ゲームタイトル表示の状態を管理するReactiveProperty
        private readonly ReactiveProperty<string> _gameplayTitle = new ReactiveProperty<string>();

        // ゲームタイトル表示の状態を外部に公開するプロパティ
        public IReactiveProperty<string> GameplayTitle => _gameplayTitle;

        /// <summary>
        /// リソースの解放を行う
        /// </summary>
        protected override void DisposeInternal()
        {
            _gameplayTitle.Dispose();
        }
    }

    /// <summary>
    /// ゲームタイトル表示の状態を制御するためのインターフェース
    /// </summary>
    internal interface IGameplayTitleState
    {
    }
}

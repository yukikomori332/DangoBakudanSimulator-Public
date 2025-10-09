using Project.Subsystem.PresentationFramework;
using UniRx;

namespace Project.Core.Scripts.View.CharName
{
    /// <summary>
    /// キャラクター名表示の状態を管理するクラス
    /// </summary>
    public sealed class CharNameViewState : AppViewState, ICharNameState
    {
        // キャラクター名表示の状態を管理するReactiveProperty
        private readonly ReactiveProperty<string> _charName = new ReactiveProperty<string>();

        // キャラクター名表示の状態を外部に公開するプロパティ
        public IReactiveProperty<string> CharName => _charName;

        /// <summary>
        /// リソースの解放を行う
        /// </summary>
        protected override void DisposeInternal()
        {
            _charName.Dispose();
        }
    }

    /// <summary>
    /// キャラクター名表示の状態を制御するためのインターフェース
    /// </summary>
    internal interface ICharNameState
    {
    }
}



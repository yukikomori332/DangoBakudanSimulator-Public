using Project.Subsystem.PresentationFramework;
using UniRx;

namespace Project.Core.Scripts.View.CharLabel
{
    /// <summary>
    /// キャラクターラベル表示の状態を管理するクラス
    /// </summary>
    public sealed class CharLabelViewState : AppViewState, ICharLabelState
    {
        // キャラクターラベル表示の状態を管理するReactiveProperty
        private readonly ReactiveProperty<string> _charLabel = new ReactiveProperty<string>();

        // キャラクターラベル表示の状態を外部に公開するプロパティ
        public IReactiveProperty<string> CharLabel => _charLabel;

        /// <summary>
        /// リソースの解放を行う
        /// </summary>
        protected override void DisposeInternal()
        {
            _charLabel.Dispose();
        }
    }

    /// <summary>
    /// キャラクターラベル表示の状態を制御するためのインターフェース
    /// </summary>
    internal interface ICharLabelState
    {
    }
}



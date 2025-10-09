using Cysharp.Threading.Tasks;
using Project.Core.Scripts.View.Foundation.Binders;
using Project.Subsystem.PresentationFramework;
using UniRx;
using TMPro;

namespace Project.Core.Scripts.View.CharLabel
{
    /// <summary>
    /// キャラクターラベル表示のビューを管理するクラス
    /// キャラクターラベル表示などのUI要素を制御
    /// </summary>
    public sealed class CharLabelView : AppView<CharLabelViewState>
    {
        public TextMeshProUGUI charLabelText; // キャラクターラベル表示用のテキスト

        protected override UniTask Initialize(CharLabelViewState viewState)
        {
            // キャラクターラベル表示用のテキストにイベントを設定
            charLabelText.SetTextSource(viewState.CharLabel).AddTo(this);

            return UniTask.CompletedTask;
        }
    }
}

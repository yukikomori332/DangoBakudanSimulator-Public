using Cysharp.Threading.Tasks;
using Project.Core.Scripts.View.Foundation.Binders;
using Project.Subsystem.PresentationFramework;
using UniRx;
using TMPro;

namespace Project.Core.Scripts.View.CharName
{
    /// <summary>
    /// キャラクター名表示のビューを管理するクラス
    /// キャラクター名表示などのUI要素を制御
    /// </summary>
    public sealed class CharNameView : AppView<CharNameViewState>
    {
        public TextMeshProUGUI charNameText; // キャラクター名表示用のテキスト

        protected override UniTask Initialize(CharNameViewState viewState)
        {
            // キャラクター名表示用のテキストにイベントを設定
            charNameText.SetTextSource(viewState.CharName).AddTo(this);

            return UniTask.CompletedTask;
        }
    }
}

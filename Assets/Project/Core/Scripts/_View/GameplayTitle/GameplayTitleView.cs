using Cysharp.Threading.Tasks;
using Project.Core.Scripts.View.Foundation.Binders;
using Project.Subsystem.PresentationFramework;
using UniRx;
using TMPro;

namespace Project.Core.Scripts.View.GameplayTitle
{
    /// <summary>
    /// ゲームタイトル表示のビューを管理するクラス
    /// ゲームタイトル表示などのUI要素を制御
    /// </summary>
    public sealed class GameplayTitleView : AppView<GameplayTitleViewState>
    {
        public TextMeshProUGUI gameplayTitleText; // ゲームタイトル表示用のテキスト

        protected override UniTask Initialize(GameplayTitleViewState viewState)
        {
            // ゲームタイトル表示用のテキストにイベントを設定
            gameplayTitleText.SetTextSource(viewState.GameplayTitle).AddTo(this);

            return UniTask.CompletedTask;
        }
    }
}

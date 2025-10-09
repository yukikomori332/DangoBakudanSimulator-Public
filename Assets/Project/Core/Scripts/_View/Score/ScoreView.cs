using Cysharp.Threading.Tasks;
using Project.Core.Scripts.View.Foundation.Binders;
using Project.Subsystem.PresentationFramework;
using UniRx;
using TMPro;

namespace Project.Core.Scripts.View.Score
{
    /// <summary>
    /// スコア表示のビューを管理するクラス
    /// スコア表示などのUI要素を制御
    /// </summary>
    public sealed class ScoreView : AppView<ScoreViewState>
    {
        public TextMeshProUGUI scoreText; // スコア表示用のテキスト

        /// <summary>
        /// ビューの初期化処理
        /// </summary>
        /// <param name="viewState">ビューの状態を管理するオブジェクト</param>
        protected override UniTask Initialize(ScoreViewState viewState)
        {
            // スコア表示用のテキストにイベントを設定
            scoreText.SetTextSource(viewState.Score).AddTo(this);

            return UniTask.CompletedTask;
        }
    }
}

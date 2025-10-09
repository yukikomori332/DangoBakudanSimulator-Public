using Project.Subsystem.PresentationFramework;
using UniRx;

namespace Project.Core.Scripts.View.Score
{
    /// <summary>
    /// スコア表示の状態を管理するクラス
    /// </summary>
    public sealed class ScoreViewState : AppViewState, IScoreState
    {
        // スコア情報を管理するReactiveProperty
        private readonly ReactiveProperty<float> _score = new ReactiveProperty<float>();

        // スコア情報を外部に公開するプロパティ
        public IReactiveProperty<float> Score => _score;

        /// <summary>
        /// リソースの解放を行う
        /// </summary>
        protected override void DisposeInternal()
        {
            _score.Dispose();
        }
    }

    /// <summary>
    /// スコア表示の状態を制御するためのインターフェース
    /// </summary>
    internal interface IScoreState
    {
    }
}

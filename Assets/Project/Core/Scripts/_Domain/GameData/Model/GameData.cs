using UniRx;

namespace Project.Core.Scripts.Domain.GameData.Model
{
    /// <summary>
    /// ゲームの情報を管理するクラス
    /// スコア、最高ダメージなどの各種設定を管理
    /// </summary>
    public sealed class GameData
    {
        // Alien有効状態の有効/無効状態 （デフォルトは無効）
        public bool AlienEnabled { get; set; } = false;
        public bool Alien1Enabled { get; set; } = false;
        public bool Alien2Enabled { get; set; } = false;
        public bool Alien3Enabled { get; set; } = false;
        public bool Alien4Enabled { get; set; } = false;
        public bool Alien5Enabled { get; set; } = false;
        public bool Alien6Enabled { get; set; } = false;
        public bool Alien7Enabled { get; set; } = false;
        public bool Alien8Enabled { get; set; } = false;

        // スコアを管理するReactiveProperty
        private readonly ReactiveProperty<float> _score = new ReactiveProperty<float>();
        // 特別スコアを管理するReactiveProperty
        private readonly ReactiveProperty<float> _specialScore = new ReactiveProperty<float>();
        // プロローグの状態を管理するReactiveProperty
        private readonly ReactiveProperty<bool> _finishedPrologue = new ReactiveProperty<bool>();
        // カウントダウン状態を管理するReactiveProperty
        private readonly ReactiveProperty<bool> _countingDown = new ReactiveProperty<bool>();
        // タイマー中の状態を管理するReactiveProperty
        private readonly ReactiveProperty<bool> _runningTimer = new ReactiveProperty<bool>();

        // スコアを外部に公開するプロパティ
        public IReactiveProperty<float> Score => _score;
        // 特別スコアを外部に公開するプロパティ
        public IReactiveProperty<float> SpecialScore => _specialScore;
        // プロローグの状態を外部に公開するプロパティ
        public IReactiveProperty<bool> FinishedPrologue => _finishedPrologue;
        // カウントダウン状態を外部に公開するプロパティ
        public IReactiveProperty<bool> CountingDown => _countingDown;
        // タイマー中の状態を外部に公開するプロパティ
        public IReactiveProperty<bool> RunningTimer => _runningTimer;

        /// <summary>
        /// リソースの解放を行う
        /// </summary>
        public void Dispose()
        {
            _score.Dispose();
            _specialScore.Dispose();
            _finishedPrologue.Dispose();
            _countingDown.Dispose();
            _runningTimer.Dispose();
        }
    }
}

using System;
using Project.Core.Scripts.View.GameplayTitle;
using Project.Core.Scripts.View.Score;
using Project.Core.Scripts.View.WeaponCount;
using Project.Subsystem.PresentationFramework;
using UniRx;

namespace Project.Core.Scripts.View.Gameplay
{
    /// <summary>
    /// ゲームプレイ画面の状態を管理するクラス
    /// </summary>
    public sealed class GameplayViewState : AppViewState, IGameplayState
    {
        // ゲームタイトル表示の状態管理
        public GameplayTitleViewState GameplayTitleText { get; } = new GameplayTitleViewState();
        // スコア表示の状態管理
        public ScoreViewState ScoreText { get; } = new ScoreViewState();
        // スコア表示の状態管理
        public ScoreViewState SpecialScoreText { get; } = new ScoreViewState();
        // 武器数表示の状態管理
        public WeaponCountViewState WeaponCountText { get; } = new WeaponCountViewState();

        // 設定ボタンの状態管理
        public GameplayButtonViewState SettingsButton { get; } = new GameplayButtonViewState();
        // クレジットボタンの状態管理
        public GameplayButtonViewState CreditButton { get; } = new GameplayButtonViewState();
        // 図鑑ボタンの状態管理
        public GameplayButtonViewState ArchiveButton { get; } = new GameplayButtonViewState();

        /// <summary>
        /// リソースの解放を行う
        /// </summary>
        protected override void DisposeInternal()
        {
            GameplayTitleText.Dispose();
            ScoreText.Dispose();
            SpecialScoreText.Dispose();
            WeaponCountText.Dispose();

            SettingsButton.Dispose();
            CreditButton.Dispose();
            ArchiveButton.Dispose();
        }
    }

    /// <summary>
    /// ゲームプレイ画面の状態を制御するためのインターフェース
    /// </summary>
    internal interface IGameplayState
    {
    }
}

using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Project.Core.Scripts.View.Foundation.Binders;
using Project.Core.Scripts.View.GameplayTitle;
using Project.Core.Scripts.View.Score;
using Project.Core.Scripts.View.WeaponCount;
using Project.Subsystem.PresentationFramework;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Core.Scripts.View.Gameplay
{
    /// <summary>
    /// ゲームプレイ画面のビューを管理するクラス
    /// スコア表示、設定ボタン、ガイド表示などのUI要素を制御
    /// </summary>
    public sealed class GameplayView : AppView<GameplayViewState>
    {
        public GameplayTitleView gameplayTitleView; // ゲームタイトル表示用のビュー
        public ScoreView scoreView;                 // スコア表示用のビュー
        public ScoreView specialScoreView;          // 特別スコア表示用のビュー
        public WeaponCountView weaponCountView;     // 武器数表示用のビュー

        public GameplayButtonView settingsButton;   // 設定画面を開くボタン
        public GameplayButtonView creditButton;     // クレジット画面を開くボタン
        public GameplayButtonView archiveButton;    // 図鑑画面を開くボタン


        public GameObject buttons;                  // ボタン

        /// <summary>
        /// ビューの初期化処理
        /// </summary>
        /// <param name="viewState">ビューの状態を管理するオブジェクト</param>
        protected override async UniTask Initialize(GameplayViewState viewState)
        {
            var internalState = (IGameplayState)viewState;

            // 初期化が必要なコンポーネントのタスクリストを作成
            var tasks = new List<UniTask>
            {
                gameplayTitleView.InitializeAsync(viewState.GameplayTitleText), // ゲームタイトル表示用のビューの初期化
                scoreView.InitializeAsync(viewState.ScoreText),                 // スコアビューの初期化
                specialScoreView.InitializeAsync(viewState.SpecialScoreText),   // スコアビューの初期化
                weaponCountView.InitializeAsync(viewState.WeaponCountText),     // 武器数表示用のビューの初期化
                settingsButton.InitializeAsync(viewState.SettingsButton),       // 設定ボタンのビューの初期化
                creditButton.InitializeAsync(viewState.CreditButton),           // クレジットボタンのビューの初期化
                archiveButton.InitializeAsync(viewState.ArchiveButton),         // 設定ボタンのビューの初期化
            };
            await UniTask.WhenAll(tasks);
        }
    }
}

using Cysharp.Threading.Tasks;
using Project.Core.Scripts.View.Foundation.Binders;
using Project.Subsystem.PresentationFramework;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Core.Scripts.View.Gameplay
{
    /// <summary>
    /// ゲームプレイ中のボタン表示を管理するビュークラス
    /// ボタンのロック状態とクリックイベントを制御する
    /// </summary>
    public sealed class GameplayButtonView : AppView<GameplayButtonViewState>
    {
        // UIコンポーネントの参照
        public Button button;           // クリック可能なボタン
        public GameObject lockedRoot;   // ロック状態の表示用オブジェクト
        public GameObject unlockedRoot; // アンロック状態の表示用オブジェクト

        private GameplayButtonHoverView _hoverView; // ホバー時のビュー
        private GameplayButtonClickView _clickView; // クリック時のビュー

        /// <summary>
        /// ビューの初期化処理
        /// ボタンの状態に応じた表示切り替えとクリックイベントの設定を行う
        /// </summary>
        /// <param name="viewState">ボタンの状態を管理するビュー状態</param>
        protected override UniTask Initialize(GameplayButtonViewState viewState)
        {
            // コンポーネントの取得
            if (TryGetComponent<GameplayButtonHoverView>(out _hoverView))
                _hoverView.Initialize();
            if (TryGetComponent<GameplayButtonClickView>(out _clickView))
                _clickView.Initialize();

            var internalState = (IGameplayButtonState)viewState;

            // ロック状態に応じてlockedRootの表示/非表示を切り替え
            lockedRoot.SetActiveSelfSource(viewState.IsLocked).AddTo(this);
            // ロック状態に応じてunlockedRootの表示/非表示を切り替え
            unlockedRoot.SetActiveSelfSource(viewState.IsLocked, true).AddTo(this);

            // ボタンのクリック時のイベントを設定
            button.SetOnClickDestination(internalState.InvokeClicked).AddTo(this);

            return UniTask.CompletedTask;
        }
    }
}

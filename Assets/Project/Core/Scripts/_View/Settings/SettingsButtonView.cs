using Cysharp.Threading.Tasks;
using Project.Core.Scripts.View.Foundation.Binders;
using Project.Subsystem.PresentationFramework;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Core.Scripts.View.Setting
{
    /// <summary>
    /// 設定画面のボタンのビューコンポーネント
    /// ロック状態に応じて表示を切り替える機能を提供
    /// </summary>
    public sealed class SettingsButtonView : AppView<SettingsButtonViewState>
    {
        public Button button;           // ボタンのUIコンポーネント
        public GameObject lockedRoot;   // ロック状態時の表示オブジェクト
        public GameObject unlockedRoot; // アンロック状態時の表示オブジェクト

        private SettingsButtonHoverView _hoverView; // ホバー時のビュー
        private SettingsButtonClickView _clickView; // クリック時のビュー

        /// <summary>
        /// ビューの初期化処理
        /// ロック状態に応じた表示切り替えとボタンクリックイベントの設定を行う
        /// </summary>
        /// <param name="viewState">ビューの状態</param>
        protected override UniTask Initialize(SettingsButtonViewState viewState)
        {
            // コンポーネントの取得
            if (TryGetComponent<SettingsButtonHoverView>(out _hoverView))
                _hoverView.Initialize();
            if (TryGetComponent<SettingsButtonClickView>(out _clickView))
                _clickView.Initialize();

            var internalState = (ISettingsButtonState)viewState;

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

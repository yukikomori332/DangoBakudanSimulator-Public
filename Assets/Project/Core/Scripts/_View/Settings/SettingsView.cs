using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Project.Core.Scripts.View.Foundation.Binders;
using Project.Subsystem.PresentationFramework;
using UniRx;
using UnityEngine.UI;

namespace Project.Core.Scripts.View.Setting
{
    /// <summary>
    /// ゲーム内の設定画面を管理するビュークラス
    /// ゲームの再開などの機能を提供
    /// </summary>
    public sealed class SettingsView : AppView<SettingsViewState>
    {
        // 設定画面の各ボタン要素
        public SettingsButtonView closeButton;      // 設定画面を閉じるボタン
        public SoundSettingsView soundSettingsView; // サウンド設定のビュー

        /// <summary>
        /// 設定画面の初期化処理
        /// 各ボタンの初期化とイベントハンドラの設定を行う
        /// </summary>
        /// <param name="viewState">設定画面の状態を管理するステート</param>
        protected override async UniTask Initialize(SettingsViewState viewState)
        {
            var internalState = (ISettingsState)viewState;

            // 初期化が必要なコンポーネントのタスクリストを作成
            var tasks = new List<UniTask>
            {
                closeButton.InitializeAsync(viewState.CloseButton)
            };
            await UniTask.WhenAll(tasks);

            // サウンド設定の初期化
            await soundSettingsView.InitializeAsync(viewState.SoundSettings);
        }
    }
}

using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Project.Core.Scripts.View.Foundation.Binders;
using Project.Subsystem.PresentationFramework;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Project.Core.Scripts.View.Archive
{
    /// <summary>
    /// 図鑑アイテム表示のビューを管理するクラス
    /// 金額表示用の画像、ビューのロック状態/アンロック状態、購入ボタンなどのUI要素を制御
    /// </summary>
    public sealed class ArchiveItemView : AppView<ArchiveItemViewState>
    {
        public GameObject lockedRoot;           // ロック状態時の表示オブジェクト
        public GameObject unlockedRoot;         // アンロック状態時の表示オブジェクト

        public Image thumbnail;                 // アイテムのサムネイル画像
        public TextMeshProUGUI itemNameText;    // アイテム名表示用のテキスト
        public TextMeshProUGUI costText;        // 解放条件表示用のテキスト
        public TextMeshProUGUI specialCostText; // 特別な解放条件表示用のテキスト

        /// <summary>
        /// ビューの初期化処理
        /// アイテムのサムネイル表示用の画像、ビューのロック状態/アンロック状態などの表示切り替えとボタンのクリックイベントの設定を行う
        /// </summary>
        /// <param name="viewState">ビューの状態</param>
        protected override async UniTask Initialize(ArchiveItemViewState viewState)
        {
            // ロック状態に応じてlockedRootの表示/非表示を切り替え
            lockedRoot.SetActiveSelfSource(viewState.IsLocked).AddTo(this);
            // ロック状態に応じてunlockedRootの表示/非表示を切り替え
            unlockedRoot.SetActiveSelfSource(viewState.IsLocked, true).AddTo(this);

            // ビューステートがロック状態なら
            if (viewState.IsLocked.Value)
            {
                // 解放条件表示用のテキストにイベントを設定
                costText.SetTextSource(viewState.Cost).AddTo(this);
                // 特別な解放条件表示用のテキストにイベントを設定
                specialCostText.SetTextSource(viewState.SpecialCost).AddTo(this);
            }
            // ビューステートがロック状態でなければ
            else if (!viewState.IsLocked.Value)
            {
                // アイテムのサムネイル画像にイベントを設定
                thumbnail.SetSpriteSource(viewState.Thumbnail).AddTo(this);
                // アイテム名表示用のテキストにイベントを設定
                itemNameText.SetTextSource(viewState.ItemName).AddTo(this);
            }

            await UniTask.CompletedTask;
        }
    }
}

using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Project.Core.Scripts.View.Foundation.Binders;
using Project.Subsystem.PresentationFramework;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Core.Scripts.View.Archive
{
    /// <summary>
    /// 図鑑画面のビューを管理するクラス
    /// 前の画面へ戻るボタンなどのUI要素を制御
    /// </summary>
    public sealed class ArchiveView : AppView<ArchiveViewState>
    {
        public ArchiveButtonView backButton; // 前の画面へ戻るボタン

        public ArchiveItemSetView archiveItemSetView; // アイテム一覧ビュー

        protected override async UniTask Initialize(ArchiveViewState viewState)
        {
            var internalState = (IArchiveState)viewState;

            // 初期化が必要なコンポーネントのタスクリストを作成
            var tasks = new List<UniTask>
            {
                backButton.InitializeAsync(viewState.BackButton),    // 前の画面へ戻るボタンビューの初期化
                archiveItemSetView.InitializeAsync(viewState.Units), // アイテム一覧ビューの初期化
            };
            await UniTask.WhenAll(tasks);
        }
    }
}

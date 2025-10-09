using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Cysharp.Threading.Tasks;
using Project.Core.Scripts.Domain.Archive.MasterRepository;
using Project.Core.Scripts.Domain.Archive.Model;
using Project.Core.Scripts.Foundation.Common;
using Project.Core.Scripts.Presentation.Shared;
using Project.Core.Scripts.UseCase.Gameplay;
using Project.Core.Scripts.View.Archive;
using Project.Subsystem.Misc;
using UniRx;
using UnityEngine;

using Project.Core.Scripts.Domain.GameData.Model;

namespace Project.Core.Scripts.Presentation.Archive
{
    /// <summary>
    /// 図鑑画面のプレゼンター
    /// 図鑑の状態管理とUIの更新を担当
    /// </summary>
    public sealed class ArchivePagePresenter : PagePresenterBase<ArchivePage, ArchiveView, ArchiveViewState>
    {
        private readonly IArchiveMasterRepository _archiveMasterRepository; // 図鑑管理
        private readonly GameplayUseCase _gameplayUseCase;                  // ゲームプレイのビジネスロジック
        private readonly IAudioPlayService _audioPlayService;               // 音声再生サービス

        public ArchivePagePresenter(ArchivePage view, ITransitionService transitionService,
            GameplayUseCase gameplayUseCase, IArchiveMasterRepository archiveMasterRepository, IAudioPlayService audioPlayService)
            : base(view, transitionService)
        {
            _gameplayUseCase = gameplayUseCase;
            _archiveMasterRepository = archiveMasterRepository;
            _audioPlayService = audioPlayService;
        }

        protected override async Task ViewDidLoad(ArchivePage view, ArchiveViewState viewState)
        {
            // キャンセレーショントークンの生成
            var cts = new CancellationTokenSource();

            // マスターテーブルを取得
            IArchiveItemMasterTable masterTable = null;
            await UniTask.WhenAll(
                UniTask.Create(async () => masterTable = await _archiveMasterRepository.FetchTableAsync()),
                // ゲームデータのフラグを取得
                _gameplayUseCase.FetchAlienEnabledAsync(),
                _gameplayUseCase.FetchAlien1EnabledAsync(),
                _gameplayUseCase.FetchAlien2EnabledAsync(),
                _gameplayUseCase.FetchAlien3EnabledAsync(),
                _gameplayUseCase.FetchAlien4EnabledAsync(),
                _gameplayUseCase.FetchAlien5EnabledAsync(),
                _gameplayUseCase.FetchAlien6EnabledAsync(),
                _gameplayUseCase.FetchAlien7EnabledAsync(),
                _gameplayUseCase.FetchAlien8EnabledAsync()
            );

            // モデルを取得
            var model = new ArchiveItemSet();
            // 各種モデルの参照を取得
            var gameData = _gameplayUseCase.GameData;   // ゲームデータ

            //　モデルにデータを挿入
            var count = masterTable.GetCount();
            var items = new List<ArchiveItem>(count);
            for (int i = 0; i < count; i++)
            {
                var archiveItem = new ArchiveItem($"{i}", $"archive_item_{i}");
                items.Add(archiveItem);
            }
            model.SetItems(items);

            // ビューステートの初期状態を設定
            SetupArchiveItemSetView(viewState.Units, model.Units, masterTable, gameData);

            // ボタンのロック状態を設定
            viewState.BackButton.IsLocked.Value = false;

            // ビューステートの変更を監視する
            if (!viewState.BackButton.IsLocked.Value)
                viewState.BackButton.OnClicked
                    .Subscribe(_ =>
                    {
                        // 効果音を再生する
                        _audioPlayService.PlayButtonClickSound(cts);
                        // 画面を遷移する
                        TransitionService.ArchivePageCloseButtonClicked();
                    })
                    .AddTo(this);
        }

        /// <summary>
        /// 図鑑アイテム表示の状態を設定
        /// </summary>
        private void SetupArchiveItemSetView(ArchiveItemSetViewState viewState, IReadOnlyList<ArchiveItem> archiveItemModels, IArchiveItemMasterTable masterTable, GameData gameData)
        {
            // 図鑑アイテムの状態リストを作成
            var archiveItemViewStates = new List<ArchiveItemViewState>
            {
                viewState.Item1,
                viewState.Item2,
                viewState.Item3,
                viewState.Item4,
                viewState.Item5,
                viewState.Item6,
                viewState.Item7,
                viewState.Item8,
                viewState.Item9,
            };

            var count = masterTable.GetCount();
            for (var i = 0; i < count; i++)
            {
                // 図鑑アイテムの状態を取得
                var archiveItemViewState = archiveItemViewStates[i];

                // 図鑑アイテムのモデルを取得
                if (archiveItemModels[i] == null)
                {
                    Debug.Log($"インデックス番号 - {i}: モデルが存在しません。");
                    continue;
                }
                var archiveItemModel = archiveItemModels[i];

                // 図鑑アイテムのマスターデータを取得
                var archiveItemMaster = masterTable.FindById(archiveItemModel.MasterId);
                if (archiveItemMaster == null)
                {
                    Debug.Log($"マスターID - {archiveItemModel.MasterId}: マスターデータが存在しません。");
                    continue;
                }

                switch (i)
                {
                    case 0:
                        SetupArchiveItemView(archiveItemViewState, archiveItemMaster, gameData.AlienEnabled);
                        break;
                    case 1:
                        SetupArchiveItemView(archiveItemViewState, archiveItemMaster, gameData.Alien1Enabled);
                        break;
                    case 2:
                        SetupArchiveItemView(archiveItemViewState, archiveItemMaster, gameData.Alien2Enabled);
                        break;
                    case 3:
                        SetupArchiveItemView(archiveItemViewState, archiveItemMaster, gameData.Alien3Enabled);
                        break;
                    case 4:
                        SetupArchiveItemView(archiveItemViewState, archiveItemMaster, gameData.Alien4Enabled);
                        break;
                    case 5:
                        SetupArchiveItemView(archiveItemViewState, archiveItemMaster, gameData.Alien5Enabled);
                        break;
                    case 6:
                        SetupArchiveItemView(archiveItemViewState, archiveItemMaster, gameData.Alien6Enabled);
                        break;
                    case 7:
                        SetupArchiveItemView(archiveItemViewState, archiveItemMaster, gameData.Alien7Enabled);
                        break;
                    case 8:
                        SetupArchiveItemView(archiveItemViewState, archiveItemMaster, gameData.Alien8Enabled);
                        break;
                }
            }
        }

        /// <summary>
        /// 図鑑アイテム表示のロック/アンロック状態を設定
        /// </summary>
        private void SetupArchiveItemView(ArchiveItemViewState viewState, ArchiveItemMaster master, bool state)
        {
            if (state)
            {
                // 図鑑アイテムのアンロック状態を表示
                SetupUnlockedArchiveItemView(viewState, master);
            }
            else if (!state)
            {
                // 図鑑アイテムのロック状態を表示
                SetupLockedArchiveItemView(viewState, master);
            }
        }

        /// <summary>
        /// ロック状態の図鑑アイテム表示を設定
        /// </summary>
        private void SetupLockedArchiveItemView(ArchiveItemViewState viewState, ArchiveItemMaster master)
        {
            viewState.IsLocked.Value = true;                  // ロック状態を有効にする
            viewState.Cost.Value = master.Cost;               // 解放条件を設定
            viewState.SpecialCost.Value = master.SpecialCost; // 特別な解放条件を設定
        }

        /// <summary>
        /// アンロック状態の図鑑アイテム表示を設定
        /// </summary>
        private void SetupUnlockedArchiveItemView(ArchiveItemViewState viewState, ArchiveItemMaster master)
        {
            viewState.IsLocked.Value = false;           // ロック状態を無効にする
            viewState.Thumbnail.Value = master.Sprite;  // アイテム画像を設定
            viewState.ItemName.Value = master.ItemName; // アイテム名を設定
        }
    }
}

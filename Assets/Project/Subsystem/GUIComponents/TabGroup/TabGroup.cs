using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using UnityScreenNavigator.Runtime.Core.Sheet;

namespace Project.Subsystem.GUIComponents.TabGroup
{
    /// <summary>
    /// タブの遷移を管理するサービスクラス
    /// タブの表示・非表示を制御し、各タブのプレゼンターを生成・初期化する
    /// </summary>
    public sealed class TabGroup : MonoBehaviour
    {
        public SheetContainer sheetContainer;                   // 対象のシートコンテナ
        public List<TabSource> sources = new List<TabSource>(); // タブソースのリスト
        public int initialIndex;                                // 初期の番号

        private readonly Dictionary<int, string> _indexToSheetIdMap = new Dictionary<int, string>(); // シートIDを番号に変換したディクショナリ
        private readonly Subject<TabLoadedEvent> _onTabLoadedSubject = new Subject<TabLoadedEvent>(); // タブの読み込みを通知するSubject

        // 初期化中かどうかのフラグ
        public bool IsInitializing { get; private set; }
        // 初期化後かどうかのフラグ
        public bool IsInitialized { get; private set; }

        // タブを読み込んだかを監視するためのObservable
        public IObservable<TabLoadedEvent> OnTabLoaded => _onTabLoadedSubject;

        private void Awake()
        {
            _onTabLoadedSubject.AddTo(this);
            _onTabLoadedSubject.Subscribe(x => { x.Sheet.AddLifecycleEvent(); });
        }

        /// <summary>
        /// 初期化処理を非同期で行う
        /// </summary>
        /// <returns>完了したタスク</returns>
        /// <exception cref="InvalidOperationException"></exception> <summary>
        public async UniTask InitializeAsync()
        {
            if (IsInitialized)
                throw new InvalidOperationException($"{nameof(TabGroup)} is Already initialized.");

            if (IsInitializing)
                throw new InvalidOperationException($"{nameof(TabGroup)} is initializing.");

            IsInitializing = true;

            var registerTasks = new List<UniTask>();
            for (var i = 0; i < sources.Count; i++)
            {
                // Register sheets.
                var source = sources[i];
                var index = i;
                var registerTask = sheetContainer.Register(source.sheetResourceKey, y =>
                {
                    var sheetId = y.sheetId;
                    var sheet = y.sheet;
                    _indexToSheetIdMap.Add(index, sheetId);
                    _onTabLoadedSubject.OnNext(new TabLoadedEvent(index, sheetId, sheet));

                    if (sheet is ITabContent tabContent)
                        tabContent.SetTabIndex(index);
                }).ToUniTask();
                registerTasks.Add(registerTask);

                // Setup buttons.
                // source.button
                source.button.Button
                    .onClick
                    .AsObservable()
                    .Subscribe(_ =>
                    {
                        if (sheetContainer.IsInTransition)
                            return;

                        if (sheetContainer.ActiveSheetId == _indexToSheetIdMap[index])
                            return;

                        ResetAllTab();

                        source.button.OnTabEnter();

                        SetActiveTabAsync(index, true).Forget();
                    })
                    .AddTo(this);
            }

            await UniTask.WhenAll(registerTasks);

            // Set initial sheet.
            // 初期のシートを設定
            await SetActiveTabAsync(initialIndex, false);
            // 初期のタブボタンを設定
            sources[initialIndex].button.OnTabEnter();

            // 初期化後フラグを有効にする
            IsInitialized = true;
            // 初期化中フラグを無効にする
            IsInitializing = false;
        }

        /// <summary>
        /// 番号からシートIDを取得する
        /// </summary>
        /// <param name="index">番号</param>
        /// <returns>シートID</returns>
        public string GetSheetIdFromIndex(int index)
        {
            return _indexToSheetIdMap[index];
        }

        /// <summary>
        /// タブを非同期で有効にする
        /// </summary>
        /// <param name="index">番号</param>
        /// <param name="playAnimation">アニメーションさせるかどうかのフラグ</param>
        /// <returns>完了したタスク</returns>
        public async UniTask SetActiveTabAsync(int index, bool playAnimation)
        {
            var sheetId = GetSheetIdFromIndex(index);
            await sheetContainer.Show(sheetId, playAnimation).ToUniTask();
        }

        /// <summary>
        /// 全てタブを無効にする
        /// </summary>
        public void ResetAllTab()
        {
            for (var i = 0; i < sources.Count; i++)
            {
                var source = sources[i];
                source.button.OnTabExit();
            }
        }

        /// <summary>
        /// タブの読み込みイベントを表す構造体
        /// </summary>
        public readonly struct TabLoadedEvent
        {
            /// <summary>
            /// 新しい読み込みでイベントを初期化
            /// </summary>
            /// <param name="index">読込後の番号</param>
            /// <param name="sheetId">読込後のシートID</param>
            /// <param name="sheet">読込後のシート</param>
            public TabLoadedEvent(int index, string sheetId, Sheet sheet)
            {
                Index = index;
                SheetId = sheetId;
                Sheet = sheet;
            }

            /// <summary>
            /// 読込後の番号
            /// </summary>
            public int Index { get; }

            /// <summary>
            /// 読込後のシートID
            /// </summary>
            public string SheetId { get; }

            /// <summary>
            /// 読込後のシート
            /// </summary>
            public Sheet Sheet { get; }
        }

        [Serializable]
        public sealed class TabSource
        {
            // public Button button;           // ボタンビュー
            public TabButton button;        // ボタンビュー
            public string sheetResourceKey; // シートのリソースキー
        }
    }
}

using System;
using Cysharp.Threading.Tasks;
using Project.Core.Scripts.Foundation.Common;
using Project.Core.Scripts.Presentation.Archive;
using Project.Core.Scripts.Presentation.Credit;
using Project.Core.Scripts.Presentation.Dialogue;
using Project.Core.Scripts.Presentation.Gameplay;
using Project.Core.Scripts.Presentation.Setting;
using Project.Core.Scripts.Presentation.Shared;
using Project.Core.Scripts.View.Archive;
using Project.Core.Scripts.View.Credit;
using Project.Core.Scripts.View.Dialogue;
using Project.Core.Scripts.View.Gameplay;
using Project.Core.Scripts.View.Setting;
using Project.Subsystem.PresentationFramework.UnityScreenNavigatorExtensions;
using UniRx;
using UnityScreenNavigator.Runtime.Core.Modal;
using UnityScreenNavigator.Runtime.Core.Page;

namespace Project.Core.Scripts.Composition
{
    /// <summary>
    /// 画面遷移を管理するサービスクラス
    /// ページとモーダルの表示・非表示を制御し、各画面のプレゼンターを生成・初期化する
    /// </summary>
    public sealed class TransitionService : ITransitionService
    {
        // 各画面のプレゼンターファクトリー
        private readonly ArchivePagePresenterFactory _archivePagePresenterFactory;
        private readonly CreditModalPresenterFactory _creditModalPresenterFactory;
        private readonly DialoguePagePresenterFactory _dialoguePagePresenterFactory;
        private readonly GameplayPagePresenterFactory _gameplayPagePresenterFactory;
        private readonly SettingsModalPresenterFactory _settingsModalPresenterFactory;

        public TransitionService(
            DialoguePagePresenterFactory dialoguePagePresenterFactory,
            GameplayPagePresenterFactory gameplayPagePresenterFactory,
            ArchivePagePresenterFactory archivePagePresenterFactory,
            SettingsModalPresenterFactory settingsModalPresenterFactory,
            CreditModalPresenterFactory creditModalPresenterFactory
        )
        {
            _dialoguePagePresenterFactory = dialoguePagePresenterFactory;
            _gameplayPagePresenterFactory = gameplayPagePresenterFactory;
            _archivePagePresenterFactory = archivePagePresenterFactory;
            _settingsModalPresenterFactory = settingsModalPresenterFactory;
            _creditModalPresenterFactory = creditModalPresenterFactory;
        }

        /// <summary>
        /// メインページコンテナへの参照を取得
        /// </summary>
        private static PageContainer MainPageContainer => PageContainer.Find("MainPageContainer");

        /// <summary>
        /// メインモーダルコンテナへの参照を取得
        /// </summary>
        private static ModalContainer MainModalContainer => ModalContainer.Find("MainModalContainer");

        /// <summary>
        /// ゲームシーン開始時にゲームプレイ画面を表示
        /// </summary>
        public void GameplaySceneStarted()
        {
            MainPageContainer.Push<GameplayPage>(ResourceKey.Prefabs.GameplayPage, false,
                onLoad: x =>
                {
                    var page = x.page;
                    var presenter = _gameplayPagePresenterFactory.Create(page, this);
                    OnPagePresenterCreated(presenter, page);
                });
        }

        /// <summary>
        /// ダイアログ画面を表示
        /// </summary>
        public void DialogueStarted()
        {
            MainPageContainer.Push<DialoguePage>(ResourceKey.Prefabs.DialoguePage, false,
                onLoad: x =>
                {
                    var page = x.page;
                    var presenter = _dialoguePagePresenterFactory.Create(page, this);
                    OnPagePresenterCreated(presenter, page);
                });
        }

        /// <summary>
        /// ゲームプレイ画面の設定ボタンクリック時の処理
        /// 設定モーダルを表示
        /// </summary>
        public void GameplayPageSettingsButtonClicked()
        {
            MainModalContainer.Push<SettingsModal>(ResourceKey.Prefabs.SettingsModal, true,
                onLoad: x =>
                {
                    var modal = x.modal;
                    var presenter = _settingsModalPresenterFactory.Create(modal, this);
                    OnModalPresenterCreated(presenter, modal);
                });
        }

        /// <summary>
        /// ゲームプレイ画面のクレジットボタンクリック時の処理
        /// クレジットモーダルを表示
        /// </summary>
        public void GameplayPageCreditButtonClicked()
        {
            MainModalContainer.Push<CreditModal>(ResourceKey.Prefabs.CreditModal, true,
                onLoad: x =>
                {
                    var modal = x.modal;
                    var presenter = _creditModalPresenterFactory.Create(modal, this);
                    OnModalPresenterCreated(presenter, modal);
                });
        }

        /// <summary>
        /// ゲームプレイ画面の図鑑ボタンクリック時の処理
        /// 図鑑画面を表示
        /// </summary>
        public void GameplayPageArchiveButtonClicked()
        {
            MainPageContainer.Push<ArchivePage>(ResourceKey.Prefabs.ArchivePage, true,
                onLoad: x =>
                {
                    var page = x.page;
                    var presenter = _archivePagePresenterFactory.Create(page, this);
                    OnPagePresenterCreated(presenter, page);
                });
        }

        /// <summary>
        /// 図鑑画面の閉じるボタンクリック時の処理
        /// 指定の画面を表示
        /// </summary>
        public void ArchivePageCloseButtonClicked()
        {
            if (MainModalContainer.IsInTransition || MainPageContainer.IsInTransition)
                throw new InvalidOperationException("Cannot pop page or modal while in transition.");

            if (MainModalContainer.Modals.Count >= 1)
                MainModalContainer.Pop(false);
            else if (MainPageContainer.Pages.Count >= 1)
                MainPageContainer.Pop(false);
            else
                throw new InvalidOperationException("Cannot pop page or modal because there is no page or modal.");
        }

        /// <summary>
        /// 画面を一つ戻る処理
        /// モーダルが表示されている場合はモーダルを閉じ、そうでない場合はページを戻る
        /// 遷移中や画面が存在しない場合は例外をスロー
        /// </summary>
        public void PopCommandExecuted()
        {
            if (MainModalContainer.IsInTransition || MainPageContainer.IsInTransition)
                throw new InvalidOperationException("Cannot pop page or modal while in transition.");

            if (MainModalContainer.Modals.Count >= 1)
                MainModalContainer.Pop(true);
            else if (MainPageContainer.Pages.Count >= 1)
                MainPageContainer.Pop(true);
            else
                throw new InvalidOperationException("Cannot pop page or modal because there is no page or modal.");
        }

        /// <summary>
        /// ページプレゼンターの生成と初期化を行う
        /// </summary>
        /// <param name="presenter">生成されたプレゼンター</param>
        /// <param name="page">対象のページ</param>
        /// <param name="shouldInitialize">初期化を行うかどうか</param>
        /// <returns>初期化済みのプレゼンター</returns>
        private IPagePresenter OnPagePresenterCreated(IPagePresenter presenter, Page page, bool shouldInitialize = true)
        {
            if (shouldInitialize)
            {
                ((IPresenter)presenter).Initialize();
                presenter.AddTo(page.gameObject);
            }

            return presenter;
        }

        /// <summary>
        /// モーダルプレゼンターの生成と初期化を行う
        /// </summary>
        /// <param name="presenter">生成されたプレゼンター</param>
        /// <param name="modal">対象のモーダル</param>
        /// <param name="shouldInitialize">初期化を行うかどうか</param>
        /// <returns>初期化済みのプレゼンター</returns>
        private IModalPresenter OnModalPresenterCreated(IModalPresenter presenter, Modal modal,
            bool shouldInitialize = true)
        {
            if (shouldInitialize)
            {
                ((IPresenter)presenter).Initialize();
                presenter.AddTo(modal.gameObject);
            }

            return presenter;
        }
    }
}

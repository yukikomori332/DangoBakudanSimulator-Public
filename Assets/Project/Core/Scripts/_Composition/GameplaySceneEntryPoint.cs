using System.Collections.Generic;
using Project.Core.Scripts.APIGateway.GameData;
using Project.Core.Scripts.APIGateway.Setting;
using Project.Core.Scripts.APIGateway.Unityroom;
using Project.Core.Scripts.Domain.GameData.Model;
using Project.Core.Scripts.Domain.Setting.Model;
using Project.Core.Scripts.MasterRepository.Archive;
using Project.Core.Scripts.MasterRepository.Audio;
using Project.Core.Scripts.MasterRepository.Dialogue;
using Project.Core.Scripts.MasterRepository.FeatureFlag;
using Project.Core.Scripts.Presentation.Archive;
using Project.Core.Scripts.Presentation.Credit;
using Project.Core.Scripts.Presentation.Dialogue;
using Project.Core.Scripts.Presentation.Gameplay;
using Project.Core.Scripts.Presentation.Setting;
using Project.Core.Scripts.UseCase.Gameplay;
using Project.Core.Scripts.UseCase.Setting;
using Project.Core.Scripts.View.Overlay;
using Project.Core.Scripts.Gameplay.Domain.PlayerInput.Model;
using Project.Core.Scripts.Gameplay.MasterRepository.AI;
using Project.Core.Scripts.Gameplay.MasterRepository.Weapon;
using Project.Core.Scripts.Gameplay.Presentation.AI;
using Project.Core.Scripts.Gameplay.Presentation.Weapon;
using Project.Core.Scripts.Gameplay.Provider.PlayerInput;
using UnityEngine;

namespace Project.Core.Scripts.Composition
{
    /// <summary>
    /// ゲームプレイシーンの依存性注入と初期化を管理するクラス
    /// </summary>
    public sealed class GameplaySceneEntryPoint : MonoBehaviour
    {
        [Header("ゲームデータの削除")]
        public bool canDelete = false;

        private GameData _gameData;                                       // ゲームデータ
        private Settings _settings;                                       // アプリケーション全体で共有される設定情報

        private AudioMasterRepository _audioMasterRepository;             // オーディオのマスターデータリポジトリ
        private FeatureFlagMasterRepository _featureFlagMasterRepository; // 機能フラグのマスターデータリポジトリ
        private DialogueMasterRepository _dialogueMasterRepository;       // ダイアログのマスターデータリポジトリ
        private ArchiveMasterRepository _archiveMasterRepository;         // 図鑑のマスターデータリポジトリ

        private AIMasterRepository _aiMasterRepository;                   // AIのマスターデータリポジトリ
        private WeaponMasterRepository _weaponMasterRepository;           // 武器のマスターデータリポジトリ

        private PlayerInputModel _playerInputModel;                       // プレイヤーの入力情報
        private PlayerInputProvider _playerInputProvider;                 // プレイヤーの入力情報を提供するプロバイダー

        private GameplayUseCase _gameplayUseCase;                         // ゲームプレイのビジネスロジック

        // ローディング画面のビュー
        private LoadingView _loadingView
        {
            get => FindFirstObjectByType<LoadingView>(FindObjectsInactive.Exclude);
        }

        // 接続状態表示用のビュー
        private ConnectingView _connectingView
        {
            get => FindFirstObjectByType<ConnectingView>(FindObjectsInactive.Exclude);
        }

        /// <summary>
        /// シーンの初期化処理
        /// </summary>
        private async void Start()
        {
            // フレームレートを60FPSに固定
            Application.targetFrameRate = 60;

            // ローディング画面の表示
            _loadingView.Show();

            // モデルの初期化
            _gameData = new GameData();
            _settings = new Settings();

            // 入力モデルの初期化
            _playerInputModel = new PlayerInputModel();
            _playerInputProvider = new PlayerInputProvider(_playerInputModel);

            // APIゲートウェイの初期化
            var settingsAPIGateway = new SettingsAPIGateway();
            var gameDataAPIGateway = new GameDataAPIGateway();
            var unityroomAPIGateway = new UnityroomAPIGateway();

            // マスターデータリポジトリの初期化
            _featureFlagMasterRepository = new FeatureFlagMasterRepository();
            _audioMasterRepository = new AudioMasterRepository();
            _dialogueMasterRepository = new DialogueMasterRepository();
            _archiveMasterRepository = new ArchiveMasterRepository();

            // オーディオのマスターテーブルを読み込む
            var audioMasterTable = await _audioMasterRepository.FetchTableAsync();
            // オーディオ再生サービスの初期化
            var audioPlayService = new AudioPlayService(audioMasterTable, _settings);

            // マスターデータリポジトリの初期化
            _aiMasterRepository = new AIMasterRepository();
            _weaponMasterRepository = new WeaponMasterRepository();

            // ユースケースの初期化
            var settingsService = new SettingsUseCase(_settings, settingsAPIGateway);
            // 音量設定の取得
            await settingsService.FetchSoundSettingsAsync();

            // ユースケースの初期化
            _gameplayUseCase = new GameplayUseCase(
                _gameData, _settings, _playerInputModel,
                _aiMasterRepository, _weaponMasterRepository,
                gameDataAPIGateway, unityroomAPIGateway);

#if UNITY_EDITOR
            // ゲームデータの削除
            if (canDelete)
                await gameDataAPIGateway.DeleteAllGameData();
#endif

            // 現在のスコアの取得
            await _gameplayUseCase.FetchCurrentScoreAsync();

            // 現在の特別スコアの取得
            await _gameplayUseCase.FetchSpecialScoreAsync();

            // プロローグ状態の取得
            await _gameplayUseCase.FetchFinishedPrologueAsync();

#if UNITY_EDITOR
            if (canDelete)
            {
                // Alienの状態の取得
                await _gameplayUseCase.FetchAlienEnabledAsync();
                await _gameplayUseCase.FetchAlien1EnabledAsync();
                await _gameplayUseCase.FetchAlien2EnabledAsync();
                await _gameplayUseCase.FetchAlien3EnabledAsync();
                await _gameplayUseCase.FetchAlien4EnabledAsync();
                await _gameplayUseCase.FetchAlien5EnabledAsync();
                await _gameplayUseCase.FetchAlien6EnabledAsync();
                await _gameplayUseCase.FetchAlien7EnabledAsync();
                await _gameplayUseCase.FetchAlien8EnabledAsync();

                Debug.Log($"現在のスコア: {_gameData.Score.Value}");
                Debug.Log($"現在の特別スコア: {_gameData.SpecialScore.Value}");
                Debug.Log($"プロローグのフラグ: {_gameData.FinishedPrologue.Value}");
                Debug.Log($"Alien: {_gameData.AlienEnabled}");
                Debug.Log($"Alien1: {_gameData.Alien1Enabled}");
                Debug.Log($"Alien2: {_gameData.Alien2Enabled}");
                Debug.Log($"Alien3: {_gameData.Alien3Enabled}");
                Debug.Log($"Alien4: {_gameData.Alien4Enabled}");
                Debug.Log($"Alien5: {_gameData.Alien5Enabled}");
                Debug.Log($"Alien6: {_gameData.Alien6Enabled}");
                Debug.Log($"Alien7: {_gameData.Alien7Enabled}");
                Debug.Log($"Alien8: {_gameData.Alien8Enabled}");
            }
#endif

            // レポジトリからマスターテーブルを取得
            await _gameplayUseCase.Load();
            // モデルセットを生成・取得
            _gameplayUseCase.SetAIModels();
            _gameplayUseCase.SetWeaponModels();

            // プレゼンターファクトリーの初期化
            var aiPresenterFactory = new AIPresenterFactory(_gameplayUseCase, audioPlayService);
            var weaponPresenterFactory = new WeaponPresenterFactory(_gameplayUseCase, audioPlayService);

            // AIのセットアップ
            aiPresenterFactory.Setup();
            // 武器のセットアップ
            weaponPresenterFactory.Setup();

            // プレゼンターファクトリーの初期化
            var dialoguePagePresenterFactory = new DialoguePagePresenterFactory(_gameplayUseCase, _dialogueMasterRepository, audioPlayService);
            var gameplayPagePresenterFactory = new GameplayPagePresenterFactory(_gameplayUseCase, _featureFlagMasterRepository, audioPlayService);
            var archivePagePresenterFactory = new ArchivePagePresenterFactory(_gameplayUseCase, _archiveMasterRepository, audioPlayService);
            var settingsModalPresenterFactory = new SettingsModalPresenterFactory(settingsService, audioPlayService, _loadingView);
            var creditModalPresenterFactory = new CreditModalPresenterFactory(audioPlayService);

            // 画面遷移サービスの初期化
            var transitionService = new TransitionService(
                dialoguePagePresenterFactory,
                gameplayPagePresenterFactory,
                archivePagePresenterFactory,
                settingsModalPresenterFactory,
                creditModalPresenterFactory
            );

            // プロローグが終了していれば
            if (_gameData.FinishedPrologue.Value)
                // 初期画面の表示
                transitionService.GameplaySceneStarted();
            // プロローグが終了していなければ
            else if (!_gameData.FinishedPrologue.Value)
                // ダイアログ画面の表示
                transitionService.DialogueStarted();

            // ローディング画面の非表示
            _loadingView.Hide();
        }

        /// <summary>
        /// シーン終了時のリソース解放処理
        /// </summary>
        private void OnDestroy()
        {
            _gameData.Dispose();
            _settings.Dispose();

            _featureFlagMasterRepository.ClearCache();
            _featureFlagMasterRepository.ReleaseHandle();
            _audioMasterRepository.ClearCache();
            _audioMasterRepository.ReleaseHandle();
            _dialogueMasterRepository.ClearCache();
            _dialogueMasterRepository.ReleaseHandle();
            _archiveMasterRepository.ClearCache();
            _archiveMasterRepository.ReleaseHandle();

            _aiMasterRepository.ClearCache();
            _aiMasterRepository.ReleaseHandle();
            _weaponMasterRepository.ClearCache();
            _weaponMasterRepository.ReleaseHandle();

            _playerInputModel.Dispose();
            _playerInputProvider.Dispose();

            _gameplayUseCase.Dispose();
        }

        /// <summary>
        /// アプリケーションのポーズ状態変更時の処理
        /// ポーズ中はゲーム時間を停止、解除時は再開
        /// </summary>
        private void OnApplicationPause(bool pauseStatus)
        {
            // 設定が存在しなければ、処理をスキップ
            if (_settings == null) return;

            if (pauseStatus)
            {
                Time.timeScale = 0;
            }
            else
            {
                // ポーズ中なら、処理をスキップ
                if (_settings.Pause.Paused) return;

                Time.timeScale = 1;
            }
        }

        /// <summary>
        /// アプリケーションのフォーカス状態変更時の処理
        /// フォーカス喪失時はゲーム時間を停止、獲得時は再開
        /// </summary>
        private void OnApplicationFocus(bool hasFocus)
        {
            // 設定が存在しなければ、処理をスキップ
            if (_settings == null) return;

            if (hasFocus)
            {
                // ポーズ中なら、処理をスキップ
                if (_settings.Pause.Paused) return;

                Time.timeScale = 1;
            }
            else
            {
                Time.timeScale = 0;
            }
        }
    }
}

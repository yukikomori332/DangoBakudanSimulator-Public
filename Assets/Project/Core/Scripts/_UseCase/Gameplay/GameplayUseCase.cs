using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Project.Core.Scripts.APIGateway.GameData;
using Project.Core.Scripts.APIGateway.Unityroom;
using Project.Core.Scripts.Domain.Setting.Model;
using Project.Core.Scripts.Domain.GameData.Model;
using Project.Core.Scripts.Gameplay.Domain.AI.Model;
using Project.Core.Scripts.Gameplay.Domain.Weapon.Model;
using Project.Core.Scripts.Gameplay.Domain.PlayerInput.Model;
using Project.Core.Scripts.Gameplay.MasterRepository.AI;
using Project.Core.Scripts.Gameplay.MasterRepository.Weapon;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Core.Scripts.UseCase.Gameplay
{
    /// <summary>
    /// モデルの取得・設定、マスターテーブルの取得・設定、フラグの更新、ゲームデータの更新、Unityroomへのスコア送信などの機能を提供するクラス
    /// </summary>
    public sealed class GameplayUseCase
    {
        private readonly AIMasterRepository _aiMasterRepository;               // AIのマスターデータリポジトリ
        private readonly WeaponMasterRepository _weaponMasterRepository;       // 武器のマスターデータリポジトリ
        private readonly GameDataAPIGateway _gameDataApiGateway;               // ゲームデータのAPIゲートウェイ
        private readonly UnityroomAPIGateway _unityroomApiGateway;             // UnityroomのAPIゲートウェイ

        public GameplayUseCase(GameData gameData, Settings settings, PlayerInputModel playerInputModel,
            AIMasterRepository aiMasterRepository, WeaponMasterRepository weaponMasterRepository,
            GameDataAPIGateway gameDataApiGateway, UnityroomAPIGateway unityroomApiGateway)
        {
            GameData = gameData;
            Settings = settings;
            PlayerInputModel = playerInputModel;
            _aiMasterRepository = aiMasterRepository;
            _weaponMasterRepository = weaponMasterRepository;
            _gameDataApiGateway = gameDataApiGateway;
            _unityroomApiGateway = unityroomApiGateway;
        }

        // 設定モデル
        public Settings Settings { get; private set; }
        // ゲーム情報モデル
        public GameData GameData { get; private set; }

        // プレイヤーの入力情報
        public PlayerInputModel PlayerInputModel { get; private set; }

        // AIモデルセット
        public AIModelSet AIModelSet { get; private set; }
        // 武器モデルセット
        public WeaponModelSet WeaponModelSet { get; private set; }

        // AIのマスターテーブル
        public AIMasterTable AIMasterTable { get; private set; }
        // 武器のマスターテーブル
        public WeaponMasterTable WeaponMasterTable { get; private set; }

        // カウントダウンのフラグ
        public bool CountingDown => GameData.CountingDown.Value;
        // タイマーのフラグ
        public bool RunningTimer => GameData.RunningTimer.Value;

        // スコア
        public float Score => GameData.Score.Value;
        // 特別スコア
        public float SpecialScore => GameData.SpecialScore.Value;

        // Alienの有効状態
        public bool AlienEnabled => GameData.AlienEnabled;
        public bool Alien1Enabled => GameData.Alien1Enabled;
        public bool Alien2Enabled => GameData.Alien2Enabled;
        public bool Alien3Enabled => GameData.Alien3Enabled;
        public bool Alien4Enabled => GameData.Alien4Enabled;
        public bool Alien5Enabled => GameData.Alien5Enabled;
        public bool Alien6Enabled => GameData.Alien6Enabled;
        public bool Alien7Enabled => GameData.Alien7Enabled;
        public bool Alien8Enabled => GameData.Alien8Enabled;

        private int _maxAICount = 100;
        private int _maxWeaponCount = 100;

        private bool _isLoaded = false; // 読み込んだかどうか

        /// <summary>
        /// マスターリポジトリの読み込み
        /// </summary>
        public async UniTask Load()
        {
            if (_isLoaded)
            {
                Debug.Log($"{nameof(AIMasterTable)}、{nameof(WeaponMasterTable)}はすでに初期化されています。");
                return;
            }

            if (_aiMasterRepository != null)
                AIMasterTable = await _aiMasterRepository.FetchTableAsync();
            if (_weaponMasterRepository != null)
                WeaponMasterTable = await _weaponMasterRepository.FetchTableAsync();

            _isLoaded = true;
        }

        /// <summary>
        /// AIモデルの設定
        /// </summary>
        public void SetAIModels()
        {
            if (!_isLoaded)
            {
                Debug.Log($"{nameof(AIMasterTable)}は初期化されていません。先に{nameof(Load)}()を呼んでください。");
                return;
            }

            AIModelSet = new AIModelSet();

            var count = AIMasterTable.GetCount();
            var ais = new List<AIModel>(_maxAICount);
            var masterId = "ai_0";

            for (int i = 0; i < _maxAICount; i++)
            {
                // 【修正】
                // 【メモ】 0〜91までのマスターIDは"ai_0"、92以降のマスターIDは"ai_{i + 1}"ずつ増加
                if (i == 92)
                {
                    masterId = "ai_1";
                }
                else if (i == 93)
                {
                    masterId = "ai_2";
                }
                else if (i == 94)
                {
                    masterId = "ai_3";
                }
                else if (i == 95)
                {
                    masterId = "ai_4";
                }
                else if (i == 96)
                {
                    masterId = "ai_5";
                }
                else if (i == 97)
                {
                    masterId = "ai_6";
                }
                else if (i == 98)
                {
                    masterId = "ai_7";
                }
                else if (i == 99)
                {
                    masterId = "ai_8";
                }

                // モデルを作成
                var aiModel = new AIModel($"{i}", masterId);
                ais.Add(aiModel);
            }

            AIModelSet.SetAIs(ais);
        }

        /// <summary>
        /// 武器モデルの設定
        /// </summary>
        public void SetWeaponModels()
        {
            if (!_isLoaded)
            {
                Debug.Log($"{nameof(WeaponMasterTable)}は初期化されていません。先に{nameof(Load)}()を呼んでください。");
                return;
            }

            WeaponModelSet = new WeaponModelSet();

            var count = WeaponMasterTable.GetCount();
            var weapons = new List<WeaponModel>(_maxWeaponCount);

            for (int i = 0; i < _maxWeaponCount; i++)
            {
                var masterId = "weapon_0";
                var weaponModel = new WeaponModel($"{i}", masterId);
                weapons.Add(weaponModel);
            }

            WeaponModelSet.SetWeapons(weapons);
        }

        /// <summary>
        /// IDによるキャラクターモデルの取得
        /// </summary>
        /// <param name="id">ID</param>
        public AIModel GetAIModelById(string id)
        {
            for (int i = 0; i < AIModelSet.AIs.Count; i++)
            {
                if (AIModelSet.AIs[i].Id == id)
                    return AIModelSet.AIs[i];
            }
            return null;
        }

        /// <summary>
        /// IDによる武器モデルの取得
        /// </summary>
        /// <param name="id">ID</param>
        public WeaponModel GetWeaponModelById(string id)
        {
            for (int i = 0; i < WeaponModelSet.Weapons.Count; i++)
            {
                if (WeaponModelSet.Weapons[i].Id == id)
                    return WeaponModelSet.Weapons[i];
            }
            return null;
        }

        /// <summary>
        /// AIモデルが最大数に達したかどうか
        /// </summary>
        public bool HasReachedMaxAI()
        {
            var count = 0;
            for (int i = 0; i < AIModelSet.AIs.Count; i++)
            {
                if (AIModelSet.AIs[i] == null)
                {
                    Debug.Log($"{i}番目のモデルが存在しません。");
                    continue;
                }

                if (AIModelSet.AIs[i].IsActive.Value)
                    count++;
            }

            return _maxAICount == count;
        }

        /// <summary>
        /// 武器モデルが最大数に達したかどうか
        /// </summary>
        public bool HasReachedMaxWeapon()
        {
            var count = 0;
            for (int i = 0; i < WeaponModelSet.Weapons.Count; i++)
            {
                if (WeaponModelSet.Weapons[i] == null)
                {
                    Debug.Log($"{i}番目のモデルが存在しません。");
                    continue;
                }

                if (WeaponModelSet.Weapons[i].IsActive.Value)
                    count++;
            }

            return _maxWeaponCount == count;
        }

        /// <summary>
        /// 現在設置中の武器モデルの数
        /// </summary>
        public int GetPlacedWeaponCount()
        {
            var count = 0;

            for (int i = 0; i < WeaponModelSet.Weapons.Count; i++)
            {
                if (WeaponModelSet.Weapons[i] == null)
                {
                    Debug.Log($"{i}番目のモデルが存在しません。");
                    continue;
                }

                if (WeaponModelSet.Weapons[i].IsPlaced.Value)
                    count++;
            }

            return count;
        }

        /// <summary>
        /// 現在使用中の武器モデルの数
        /// </summary>
        public int GetMaxWeaponCount()
        {
            return _maxWeaponCount;
        }

        /// <summary>
        /// カウントダウンのフラグを指定した値に設定する
        /// </summary>
        /// <param name="state">設定するフラグの値</param>
        public void SetCountingDown(bool state)
        {
            GameData.CountingDown.Value = state;
        }

        /// <summary>
        /// タイマーのフラグを指定した値に設定する
        /// </summary>
        /// <param name="state">設定するフラグの値</param>
        public void SetRunningTimer(bool state)
        {
            GameData.RunningTimer.Value = state;
        }

        /// <summary>
        /// スコアを指定した値に設定する
        /// </summary>
        /// <param name="score">設定するスコア値</param>
        public void SetScore(float score)
        {
            // 値の制限
            var value = Mathf.Clamp(score, 0, int.MaxValue);
            GameData.Score.Value = value;
        }

        /// <summary>
        /// 特別スコアを指定した値に設定する
        /// </summary>
        /// <param name="score">設定するスコア値</param>
        public void SetSpecialScore(float score)
        {
            // 値の制限
            var value = Mathf.Clamp(score, 0, int.MaxValue);
            GameData.SpecialScore.Value = value;
        }

        /// <summary>
        /// フラグを指定した値に設定する
        /// </summary>
        /// <param name="state">設定するフラグ値</param>
        public void SetAlienEnabled(bool state)
        {
            GameData.AlienEnabled = state;
        }
        public void SetAlien1Enabled(bool state)
        {
            GameData.Alien1Enabled = state;
        }
        public void SetAlien2Enabled(bool state)
        {
            GameData.Alien2Enabled = state;
        }
        public void SetAlien3Enabled(bool state)
        {
            GameData.Alien3Enabled = state;
        }
        public void SetAlien4Enabled(bool state)
        {
            GameData.Alien4Enabled = state;
        }
        public void SetAlien5Enabled(bool state)
        {
            GameData.Alien5Enabled = state;
        }
        public void SetAlien6Enabled(bool state)
        {
            GameData.Alien6Enabled = state;
        }
        public void SetAlien7Enabled(bool state)
        {
            GameData.Alien7Enabled = state;
        }
        public void SetAlien8Enabled(bool state)
        {
            GameData.Alien8Enabled = state;
        }

        /// <summary>
        /// 現在のスコアを取得する
        /// </summary>
        public async UniTask FetchCurrentScoreAsync()
        {
            var response = await _gameDataApiGateway.FetchCurrentScoreAsync();
            GameData.Score.Value = response;
        }

        /// <summary>
        /// 現在のスコアを保存する
        /// </summary>
        public async UniTask SaveCurrentScoreAsync()
        {
            await _gameDataApiGateway.SaveCurrentScoreAsync((int)GameData.Score.Value);
        }

        /// <summary>
        /// 特別スコアを取得する
        /// </summary>
        public async UniTask FetchSpecialScoreAsync()
        {
            var response = await _gameDataApiGateway.FetchSpecialScoreAsync();
            GameData.SpecialScore.Value = response;
        }

        /// <summary>
        /// 特別スコアを保存する
        /// </summary>
        public async UniTask SaveSpecialScoreAsync()
        {
            await _gameDataApiGateway.SaveSpecialScoreAsync((int)GameData.SpecialScore.Value);
        }

        /// <summary>
        /// プロローグの状態を取得する
        /// </summary>
        public async UniTask FetchFinishedPrologueAsync()
        {
            var response = await _gameDataApiGateway.FetchFinishedPrologueAsync();
            GameData.FinishedPrologue.Value = response;
        }

        /// <summary>
        /// プロローグの状態を保存する
        /// </summary>
        public async UniTask SaveFinishedPrologueAsync()
        {
            await _gameDataApiGateway.SaveFinishedPrologueAsync(GameData.FinishedPrologue.Value);
        }

        /// <summary>
        /// Alienの有効状態を取得する
        /// </summary>
        public async UniTask FetchAlienEnabledAsync()
        {
            var response = await _gameDataApiGateway.FetchAlienEnabledAsync();
            GameData.AlienEnabled = response;
        }
        public async UniTask FetchAlien1EnabledAsync()
        {
            var response = await _gameDataApiGateway.FetchAlien1EnabledAsync();
            GameData.Alien1Enabled = response;
        }
        public async UniTask FetchAlien2EnabledAsync()
        {
            var response = await _gameDataApiGateway.FetchAlien2EnabledAsync();
            GameData.Alien2Enabled = response;
        }
        public async UniTask FetchAlien3EnabledAsync()
        {
            var response = await _gameDataApiGateway.FetchAlien3EnabledAsync();
            GameData.Alien3Enabled = response;
        }
        public async UniTask FetchAlien4EnabledAsync()
        {
            var response = await _gameDataApiGateway.FetchAlien4EnabledAsync();
            GameData.Alien4Enabled = response;
        }
        public async UniTask FetchAlien5EnabledAsync()
        {
            var response = await _gameDataApiGateway.FetchAlien5EnabledAsync();
            GameData.Alien5Enabled = response;
        }
        public async UniTask FetchAlien6EnabledAsync()
        {
            var response = await _gameDataApiGateway.FetchAlien6EnabledAsync();
            GameData.Alien6Enabled = response;
        }
        public async UniTask FetchAlien7EnabledAsync()
        {
            var response = await _gameDataApiGateway.FetchAlien7EnabledAsync();
            GameData.Alien7Enabled = response;
        }
        public async UniTask FetchAlien8EnabledAsync()
        {
            var response = await _gameDataApiGateway.FetchAlien8EnabledAsync();
            GameData.Alien8Enabled = response;
        }

        /// <summary>
        /// Alienの有効状態を保存する
        /// </summary>
        public async UniTask SaveAlienEnabledAsync()
        {
            await _gameDataApiGateway.SaveAlienEnabledAsync(GameData.AlienEnabled);
        }
        public async UniTask SaveAlien1EnabledAsync()
        {
            await _gameDataApiGateway.SaveAlien1EnabledAsync(GameData.Alien1Enabled);
        }
        public async UniTask SaveAlien2EnabledAsync()
        {
            await _gameDataApiGateway.SaveAlien2EnabledAsync(GameData.Alien2Enabled);
        }
        public async UniTask SaveAlien3EnabledAsync()
        {
            await _gameDataApiGateway.SaveAlien3EnabledAsync(GameData.Alien3Enabled);
        }
        public async UniTask SaveAlien4EnabledAsync()
        {
            await _gameDataApiGateway.SaveAlien4EnabledAsync(GameData.Alien4Enabled);
        }
        public async UniTask SaveAlien5EnabledAsync()
        {
            await _gameDataApiGateway.SaveAlien5EnabledAsync(GameData.Alien5Enabled);
        }
        public async UniTask SaveAlien6EnabledAsync()
        {
            await _gameDataApiGateway.SaveAlien6EnabledAsync(GameData.Alien6Enabled);
        }
        public async UniTask SaveAlien7EnabledAsync()
        {
            await _gameDataApiGateway.SaveAlien7EnabledAsync(GameData.Alien7Enabled);
        }
        public async UniTask SaveAlien8EnabledAsync()
        {
            await _gameDataApiGateway.SaveAlien8EnabledAsync(GameData.Alien8Enabled);
        }

        /// <summary>
        /// フラグの保存処理
        /// </summary>
        /// <param name="list"></param>
        /// <returns>完了したタスク</returns>
        public async UniTask SaveRarityFlagIndex(IReadOnlyList<int> list)
        {
            // 配列が存在しないまたは要素が0なら、処理をスキップ
            if (list == null || list.Count == 0 )
                await UniTask.CompletedTask;

            // 特定の番号が含まれ、かつフラグが無効状態なら、フラグを有効にしてデータを保存
            if (ContainsIndex(list, 0) && !AlienEnabled)
            {
                SetAlienEnabled(true);
                await SaveAlienEnabledAsync();
            }
            if (ContainsIndex(list, 1) && !Alien1Enabled)
            {
                SetAlien1Enabled(true);
                await SaveAlien1EnabledAsync();
            }
            if (ContainsIndex(list, 2) && !Alien2Enabled)
            {
                SetAlien2Enabled(true);
                await SaveAlien2EnabledAsync();
            }
            if (ContainsIndex(list, 3) && !Alien3Enabled)
            {
                SetAlien3Enabled(true);
                await SaveAlien3EnabledAsync();
            }
            if (ContainsIndex(list, 4) && !Alien4Enabled)
            {
                SetAlien4Enabled(true);
                await SaveAlien4EnabledAsync();
            }
            if (ContainsIndex(list, 5) && !Alien5Enabled)
            {
                SetAlien5Enabled(true);
                await SaveAlien5EnabledAsync();
            }
            if (ContainsIndex(list, 6) && !Alien6Enabled)
            {
                SetAlien6Enabled(true);
                await SaveAlien6EnabledAsync();
            }
            if (ContainsIndex(list, 7) && !Alien7Enabled)
            {
                SetAlien7Enabled(true);
                await SaveAlien7EnabledAsync();
            }
            if (ContainsIndex(list, 8) && !Alien8Enabled)
            {
                SetAlien8Enabled(true);
                await SaveAlien8EnabledAsync();
            }

            await UniTask.CompletedTask;
        }

        /// <summary>
        /// インデックスの検索
        /// </summary>
        /// <param name="list"></param>
        /// <param name="index"></param>
        /// <returns>特定の値が含まれているかどうか</returns>
        private bool ContainsIndex(IReadOnlyList<int> list, int index)
        {
            bool result = false;
            foreach (var item in list)
            {
                if (item == index)
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// スコアを降順でUnityroomに送信する
        /// </summary>
        public void SendScoreByDesc(int index, float score)
        {
            _unityroomApiGateway.SendScoreByDesc(index, score);
        }

        /// <summary>
        /// リソースを解放する
        /// </summary>
        public void Dispose()
        {
            for (int i = 0; i < AIModelSet.AIs.Count; i++)
                AIModelSet.AIs[i].Dispose();
        }
    }
}

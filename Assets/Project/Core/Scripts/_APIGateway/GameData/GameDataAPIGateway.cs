using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Project.Core.Scripts.APIGateway.GameData
{
    /// <summary>
    /// ゲームデータをPlayerPrefsから取得・保存するためのAPIゲートウェイ
    /// </summary>
    public sealed class GameDataAPIGateway
    {
        /// <summary>
        /// PlayerPrefsで使用するキー定義
        /// </summary>
        private const string CurrentScorePrefsKey = "Project_CurrentScore";
        private const string SpecialScorePrefsKey = "Project_SpecialScore";
        private const string FinishedProloguePrefsKey = "Project_FinishedPrologue";

        /// <summary>
        /// 現在のスコア（デフォルト： 0）
        /// </summary>
        private static int CurrentScore
        {
            get => PlayerPrefs.GetInt(CurrentScorePrefsKey, 0);
            set => PlayerPrefs.SetInt(CurrentScorePrefsKey, value);
        }

        /// <summary>
        /// 特別スコア（デフォルト： 0）
        /// </summary>
        private static int SpecialScore
        {
            get => PlayerPrefs.GetInt(SpecialScorePrefsKey, 0);
            set => PlayerPrefs.SetInt(SpecialScorePrefsKey, value);
        }

        /// <summary>
        /// プロローグの状態（デフォルト： 無効）
        /// </summary>
        private static bool FinishedPrologue
        {
            get => PlayerPrefs.GetInt(FinishedProloguePrefsKey, 0) == 1;
            set => PlayerPrefs.SetInt(FinishedProloguePrefsKey, value ? 1 : 0);
        }

        /// <summary>
        /// 現在のスコアを非同期で取得する
        /// </summary>
        /// <returns>現在のスコア</returns>
        public UniTask<int> FetchCurrentScoreAsync()
        {
            var response = CurrentScore;
            return UniTask.FromResult(response);
        }

        /// <summary>
        /// 現在のスコアを非同期で保存する
        /// </summary>
        /// <param name="request">現在のスコア</param>
        /// <returns>タスクの完了</returns>
        public UniTask SaveCurrentScoreAsync(int request)
        {
            CurrentScore = request;
            return UniTask.CompletedTask;
        }

        /// <summary>
        /// 特別スコアを非同期で取得する
        /// </summary>
        /// <returns>特別スコア</returns>
        public UniTask<int> FetchSpecialScoreAsync()
        {
            var response = SpecialScore;
            return UniTask.FromResult(response);
        }

        /// <summary>
        /// 特別スコアを非同期で保存する
        /// </summary>
        /// <param name="request">特別スコア</param>
        /// <returns>タスクの完了</returns>
        public UniTask SaveSpecialScoreAsync(int request)
        {
            SpecialScore = request;
            return UniTask.CompletedTask;
        }

        /// <summary>
        /// プロローグの状態を非同期で取得する
        /// </summary>
        /// <returns>プロローグの状態</returns>
        public UniTask<bool> FetchFinishedPrologueAsync()
        {
            var response = FinishedPrologue;
            return UniTask.FromResult(response);
        }

        /// <summary>
        /// プロローグの状態を非同期で保存する
        /// </summary>
        /// <param name="request">最高スコア</param>
        /// <returns>タスクの完了</returns>
        public UniTask SaveFinishedPrologueAsync(bool request)
        {
            FinishedPrologue = request;
            return UniTask.CompletedTask;
        }

        /// <summary>
        /// PlayerPrefsで使用するキー定義
        /// </summary>
        private const string IsAlienEnabledPrefsKey = "Project_AlienEnabled";
        private const string IsAlien1EnabledPrefsKey = "Project_Alien1Enabled";
        private const string IsAlien2EnabledPrefsKey = "Project_Alien2Enabled";
        private const string IsAlien3EnabledPrefsKey = "Project_Alien3Enabled";
        private const string IsAlien4EnabledPrefsKey = "Project_Alien4Enabled";
        private const string IsAlien5EnabledPrefsKey = "Project_Alien5Enabled";
        private const string IsAlien6EnabledPrefsKey = "Project_Alien6Enabled";
        private const string IsAlien7EnabledPrefsKey = "Project_Alien7Enabled";
        private const string IsAlien8EnabledPrefsKey = "Project_Alien8Enabled";

        /// <summary>
        /// Alienの有効状態（デフォルト： 有効）
        /// </summary>
        private static bool IsAlienEnabled
        {
            get => PlayerPrefs.GetInt(IsAlienEnabledPrefsKey, 1) == 1;
            set => PlayerPrefs.SetInt(IsAlienEnabledPrefsKey, value ? 1 : 0);
        }
        /// <summary>
        /// Alienの有効状態（デフォルト： 無効）
        /// </summary>
        private static bool IsAlien1Enabled
        {
            get => PlayerPrefs.GetInt(IsAlien1EnabledPrefsKey, 0) == 1;
            set => PlayerPrefs.SetInt(IsAlien1EnabledPrefsKey, value ? 1 : 0);
        }
        private static bool IsAlien2Enabled
        {
            get => PlayerPrefs.GetInt(IsAlien2EnabledPrefsKey, 0) == 1;
            set => PlayerPrefs.SetInt(IsAlien2EnabledPrefsKey, value ? 1 : 0);
        }
        private static bool IsAlien3Enabled
        {
            get => PlayerPrefs.GetInt(IsAlien3EnabledPrefsKey, 0) == 1;
            set => PlayerPrefs.SetInt(IsAlien3EnabledPrefsKey, value ? 1 : 0);
        }
        private static bool IsAlien4Enabled
        {
            get => PlayerPrefs.GetInt(IsAlien4EnabledPrefsKey, 0) == 1;
            set => PlayerPrefs.SetInt(IsAlien4EnabledPrefsKey, value ? 1 : 0);
        }
        private static bool IsAlien5Enabled
        {
            get => PlayerPrefs.GetInt(IsAlien5EnabledPrefsKey, 0) == 1;
            set => PlayerPrefs.SetInt(IsAlien5EnabledPrefsKey, value ? 1 : 0);
        }
        private static bool IsAlien6Enabled
        {
            get => PlayerPrefs.GetInt(IsAlien6EnabledPrefsKey, 0) == 1;
            set => PlayerPrefs.SetInt(IsAlien6EnabledPrefsKey, value ? 1 : 0);
        }
        private static bool IsAlien7Enabled
        {
            get => PlayerPrefs.GetInt(IsAlien7EnabledPrefsKey, 0) == 1;
            set => PlayerPrefs.SetInt(IsAlien7EnabledPrefsKey, value ? 1 : 0);
        }
        private static bool IsAlien8Enabled
        {
            get => PlayerPrefs.GetInt(IsAlien8EnabledPrefsKey, 0) == 1;
            set => PlayerPrefs.SetInt(IsAlien8EnabledPrefsKey, value ? 1 : 0);
        }

        /// <summary>
        /// Alienの有効状態を非同期で取得する
        /// </summary>
        /// <returns>Alienの表示が有効かどうかのレスポンス</returns>
        public UniTask<bool> FetchAlienEnabledAsync()
        {
            var response = IsAlienEnabled;
            return UniTask.FromResult(response);
        }
        public UniTask<bool> FetchAlien1EnabledAsync()
        {
            var response = IsAlien1Enabled;
            return UniTask.FromResult(response);
        }
        public UniTask<bool> FetchAlien2EnabledAsync()
        {
            var response = IsAlien2Enabled;
            return UniTask.FromResult(response);
        }
        public UniTask<bool> FetchAlien3EnabledAsync()
        {
            var response = IsAlien3Enabled;
            return UniTask.FromResult(response);
        }
        public UniTask<bool> FetchAlien4EnabledAsync()
        {
            var response = IsAlien4Enabled;
            return UniTask.FromResult(response);
        }
        public UniTask<bool> FetchAlien5EnabledAsync()
        {
            var response = IsAlien5Enabled;
            return UniTask.FromResult(response);
        }
        public UniTask<bool> FetchAlien6EnabledAsync()
        {
            var response = IsAlien6Enabled;
            return UniTask.FromResult(response);
        }
        public UniTask<bool> FetchAlien7EnabledAsync()
        {
            var response = IsAlien7Enabled;
            return UniTask.FromResult(response);
        }
        public UniTask<bool> FetchAlien8EnabledAsync()
        {
            var response = IsAlien8Enabled;
            return UniTask.FromResult(response);
        }

        /// <summary>
        /// Alienの有効状態を非同期で保存する
        /// </summary>
        /// <param name="request">Alienの有効状態の有効/無効状態</param>
        /// <returns>タスクの完了</returns>
        public UniTask SaveAlienEnabledAsync(bool request)
        {
            IsAlienEnabled = request;
            return UniTask.CompletedTask;
        }
        public UniTask SaveAlien1EnabledAsync(bool request)
        {
            IsAlien1Enabled = request;
            return UniTask.CompletedTask;
        }
        public UniTask SaveAlien2EnabledAsync(bool request)
        {
            IsAlien2Enabled = request;
            return UniTask.CompletedTask;
        }
        public UniTask SaveAlien3EnabledAsync(bool request)
        {
            IsAlien3Enabled = request;
            return UniTask.CompletedTask;
        }
        public UniTask SaveAlien4EnabledAsync(bool request)
        {
            IsAlien4Enabled = request;
            return UniTask.CompletedTask;
        }
        public UniTask SaveAlien5EnabledAsync(bool request)
        {
            IsAlien5Enabled = request;
            return UniTask.CompletedTask;
        }
        public UniTask SaveAlien6EnabledAsync(bool request)
        {
            IsAlien6Enabled = request;
            return UniTask.CompletedTask;
        }
        public UniTask SaveAlien7EnabledAsync(bool request)
        {
            IsAlien7Enabled = request;
            return UniTask.CompletedTask;
        }
        public UniTask SaveAlien8EnabledAsync(bool request)
        {
            IsAlien8Enabled = request;
            return UniTask.CompletedTask;
        }

#if UNITY_EDITOR
        /// <summary>
        /// PlayerPrefsのすべてのデータを削除する
        /// </summary>
        public UniTask DeleteAllGameData()
        {
            PlayerPrefs.DeleteAll();
            return UniTask.CompletedTask;
        }
#endif
    }
}

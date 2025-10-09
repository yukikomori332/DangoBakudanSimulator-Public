using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Project.Core.Scripts.APIGateway.Setting
{
    /// <summary>
    /// 設定関連のデータをPlayerPrefsから取得・保存するためのAPIゲートウェイ
    /// </summary>
    public sealed class SettingsAPIGateway
    {
        /// <summary>
        /// PlayerPrefsで使用するキー定義
        /// </summary>
        private const string BgmVolumePrefsKey = "Project_BgmVolume";
        private const string SeVolumePrefsKey = "Project_SeVolume";
        private const string IsBgmMutedPrefsKey = "Project_BgmMuted";
        private const string IsSeMutedPrefsKey = "Project_SeMuted";

        /// <summary>
        /// BGMボリュームの設定値 (デフォルト： 0.5f)
        /// </summary>
        private static float BgmVolume
        {
            get => PlayerPrefs.GetFloat(BgmVolumePrefsKey, 0.5f);
            set => PlayerPrefs.SetFloat(BgmVolumePrefsKey, value);
        }

        /// <summary>
        /// SE（効果音）ボリュームの設定値 (デフォルト： 0.5f)
        /// </summary>
        private static float SeVolume
        {
            get => PlayerPrefs.GetFloat(SeVolumePrefsKey, 0.5f);
            set => PlayerPrefs.SetFloat(SeVolumePrefsKey, value);
        }

        /// <summary>
        /// BGMのミュート状態
        /// </summary>
        private static bool IsBgmMuted
        {
            get => PlayerPrefs.GetInt(IsBgmMutedPrefsKey, 0) == 1;
            set => PlayerPrefs.SetInt(IsBgmMutedPrefsKey, value ? 1 : 0);
        }

        /// <summary>
        /// SE（効果音）のミュート状態
        /// </summary>
        private static bool IsSeMuted
        {
            get => PlayerPrefs.GetInt(IsSeMutedPrefsKey, 0) == 1;
            set => PlayerPrefs.SetInt(IsSeMutedPrefsKey, value ? 1 : 0);
        }

        /// <summary>
        /// サウンド設定を非同期で取得する
        /// </summary>
        /// <returns>サウンド設定情報を含むレスポンス</returns>
        public UniTask<FetchSoundSettingsResponse> FetchSoundSettingsAsync()
        {
            var soundSettings = new FetchSoundSettingsResponse
            (
                BgmVolume,
                SeVolume,
                IsBgmMuted,
                IsSeMuted
            );
            return UniTask.FromResult(soundSettings);
        }

        /// <summary>
        /// サウンド設定を非同期で保存する
        /// </summary>
        /// <param name="request">保存するサウンド設定情報</param>
        /// <returns>タスクの完了</returns>
        public UniTask SaveSoundSettingsAsync(SaveSoundSettingsRequest request)
        {
            IsBgmMuted = request.IsBgmMuted;
            IsSeMuted = request.IsSeMuted;
            BgmVolume = request.BgmVolume;
            SeVolume = request.SeVolume;
            return UniTask.CompletedTask;
        }

        #region Requests

        public readonly struct SaveSoundSettingsRequest
        {
            /// <summary>
            /// サウンド設定の保存リクエスト構造体
            /// </summary>
            public SaveSoundSettingsRequest(float bgmVolume, float seVolume,
                bool isBgmMuted, bool isSeMuted)
            {
                BgmVolume = bgmVolume;
                SeVolume = seVolume;
                IsBgmMuted = isBgmMuted;
                IsSeMuted = isSeMuted;
            }

            public float BgmVolume { get; }
            public float SeVolume { get; }
            public bool IsBgmMuted { get; }
            public bool IsSeMuted { get; }
        }

        #endregion

        #region Responses

        public readonly struct FetchSoundSettingsResponse
        {
            /// <summary>
            /// サウンド設定の取得レスポンス構造体
            /// </summary>
            public FetchSoundSettingsResponse(float bgmVolume, float seVolume,
                bool isBgmMuted, bool isSeMuted)
            {
                BgmVolume = bgmVolume;
                SeVolume = seVolume;
                IsBgmMuted = isBgmMuted;
                IsSeMuted = isSeMuted;
            }

            public float BgmVolume { get; }
            public float SeVolume { get; }
            public bool IsBgmMuted { get; }
            public bool IsSeMuted { get; }
        }

        #endregion
    }
}

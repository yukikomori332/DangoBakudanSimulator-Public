using Cysharp.Threading.Tasks;
using Project.Core.Scripts.APIGateway.Setting;
using Project.Core.Scripts.Domain.Setting.Model;

namespace Project.Core.Scripts.UseCase.Setting
{
    /// <summary>
    /// 設定に関するユースケースを管理するクラス
    /// サウンド設定とガイド設定の取得・保存を提供
    /// </summary>
    public sealed class SettingsUseCase
    {
        private readonly SettingsAPIGateway _apiGateway; // 設定APIゲートウェイ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="model">設定モデル</param>
        /// <param name="apiGateway">設定APIゲートウェイ</param>
        public SettingsUseCase(Settings model, SettingsAPIGateway apiGateway)
        {
            Model = model;
            _apiGateway = apiGateway;
        }

        // 設定モデル
        public Settings Model { get; }

        /// <summary>
        /// サウンド設定をAPIゲートウェイから取得し、モデルに反映する
        /// </summary>
        public async UniTask FetchSoundSettingsAsync()
        {
            var response = await _apiGateway.FetchSoundSettingsAsync();
            var sounds = Model.Sounds;
            sounds.Bgm.SetValues(response.BgmVolume, response.IsBgmMuted);
            sounds.Se.SetValues(response.SeVolume, response.IsSeMuted);
        }

        /// <summary>
        /// サウンド設定を保存する
        /// </summary>
        /// <param name="request">保存するサウンド設定のリクエスト</param>
        public async UniTask SaveSoundSettingsAsync(SaveSoundSettingsRequest request)
        {
            var sounds = Model.Sounds;
            sounds.Bgm.SetValues(request.BgmVolume, request.IsBgmMuted);
            sounds.Se.SetValues(request.SeVolume, request.IsSeMuted);
            var apiRequest = new SettingsAPIGateway.SaveSoundSettingsRequest(request.BgmVolume,
                request.SeVolume, request.IsBgmMuted, request.IsSeMuted);
            await _apiGateway.SaveSoundSettingsAsync(apiRequest);
        }

        #region Requests

        /// <summary>
        /// サウンド設定の保存リクエストを表す構造体
        /// </summary>
        public readonly struct SaveSoundSettingsRequest
        {
            /// <summary>
            /// サウンド設定の保存リクエストを初期化
            /// </summary>
            /// <param name="bgmVolume">BGM音量</param>
            /// <param name="seVolume">効果音量</param>
            /// <param name="isBgmMuted">BGMミュート状態</param>
            /// <param name="isSeMuted">効果音ミュート状態</param>
            public SaveSoundSettingsRequest(float bgmVolume, float seVolume,
                bool isBgmMuted, bool isSeMuted)
            {
                BgmVolume = bgmVolume;
                SeVolume = seVolume;
                IsBgmMuted = isBgmMuted;
                IsSeMuted = isSeMuted;
            }

            /// <summary>
            /// BGM音量
            /// </summary>
            public float BgmVolume { get; }

            /// <summary>
            /// 効果音量
            /// </summary>
            public float SeVolume { get; }

            /// <summary>
            /// BGMミュート状態
            /// </summary>
            public bool IsBgmMuted { get; }

            /// <summary>
            /// 効果音ミュート状態
            /// </summary>
            public bool IsSeMuted { get; }
        }

        #endregion
    }
}

using System;

namespace Project.Core.Scripts.Domain.Setting.Model
{
    /// <summary>
    /// ゲーム全体の設定を管理するクラス
    /// サウンド、ガイド、ポーズなどの各種設定を統合的に管理
    /// </summary>
    public sealed class Settings
    {
        // ゲーム内のサウンド関連の設定を管理するプロパティ （BGMやSEの音量設定などを含む）
        public SoundSettingsSet Sounds { get; } = new SoundSettingsSet();

        // ゲームの一時停止機能に関する設定を管理するプロパティ
        public PauseSettings Pause { get; } = new PauseSettings();

        /// <summary>
        /// 設定クラスのリソースの解放を行う
        /// サウンド設定とガイド設定、ポーズ設定のリソースを解放する
        /// </summary>
        public void Dispose()
        {
            Sounds.Dispose();
            Pause.Dispose();
        }
    }
}

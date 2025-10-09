using System;
using Project.Subsystem.PresentationFramework;
using UniRx;

namespace Project.Core.Scripts.View.Setting
{
    /// <summary>
    /// 設定画面の状態を管理するクラス
    /// </summary>
    public sealed class SettingsViewState : AppViewState, ISettingsState
    {
        // サウンド設定の状態管理
        public SoundSettingsViewState SoundSettings { get; } = new SoundSettingsViewState();

        // 閉じるボタンの状態管理
        public SettingsButtonViewState CloseButton { get; } = new SettingsButtonViewState();

        /// <summary>
        /// リソースの解放を行う
        /// </summary>
        protected override void DisposeInternal()
        {
            SoundSettings.Dispose();

            CloseButton.Dispose();
        }
    }

    /// <summary>
    /// 設定画面の状態を制御するためのインターフェース
    /// </summary>
    internal interface ISettingsState
    {
    }
}

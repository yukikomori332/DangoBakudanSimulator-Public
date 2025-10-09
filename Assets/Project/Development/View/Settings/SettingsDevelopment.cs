using Project.Core.Scripts.View.Setting;
using Project.Development.View.Shared;
using UniRx;
using UnityEngine;

namespace Project.Development.View.Settings
{
    public sealed class SettingsDevelopment : AppViewDevelopment<SettingsView, SettingsViewState>
    {
        protected override SettingsViewState CreateState()
        {
            var state = new SettingsViewState();
            state.SoundSettings.BgmVolume.Value = 0.5f;
            state.SoundSettings.SeVolume.Value = 1.0f;
            state.SoundSettings.IsBgmEnabled.Value = true;
            state.SoundSettings.IsSeEnabled.Value = false;
            state.SoundSettings.BgmVolume
                .Subscribe(volume => Debug.Log($"BGM volume: {volume}"))
                .AddTo(this);
            state.SoundSettings.SeVolume
                .Subscribe(volume => Debug.Log($"SE volume: {volume}"))
                .AddTo(this);

            state.CloseButton.IsLocked.Value = false;
            state.CloseButton.OnClicked
                .Subscribe(_ => Debug.Log("Close Button Clicked"))
                .AddTo(this);
            return state;
        }
    }
}

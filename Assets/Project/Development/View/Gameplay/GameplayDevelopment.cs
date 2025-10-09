using Project.Core.Scripts.View.Gameplay;
using Project.Development.View.Shared;
using UniRx;
using UnityEngine;

namespace Project.Development.View.Gameplay
{
    public sealed class GameplayDevelopment : AppViewDevelopment<GameplayView, GameplayViewState>
    {
        protected override GameplayViewState CreateState()
        {
            var state = new GameplayViewState();
            state.SettingsButton.OnClicked.Subscribe(_ => Debug.Log("Settings Button Clicked")).AddTo(this);
            state.CreditButton.OnClicked.Subscribe(_ => Debug.Log("Credit Button Clicked")).AddTo(this);
            state.ArchiveButton.OnClicked.Subscribe(_ => Debug.Log("Archive Button Clicked")).AddTo(this);
            return state;
        }
    }
}

using Project.Core.Scripts.View.Dialogue;
using Project.Development.View.Shared;
using UniRx;
using UnityEngine;

namespace Project.Development.View.Dialogue
{
    public sealed class DialogueDevelopment : AppViewDevelopment<DialogueView, DialogueViewState>
    {
        protected override DialogueViewState CreateState()
        {
            var state = new DialogueViewState();
            state.NextDialogueButton.OnClicked.Subscribe(_ => Debug.Log("Next Dialogue Button Clicked")).AddTo(this);
            return state;
        }
    }
}

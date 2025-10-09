using Project.Core.Scripts.View.Credit;
using Project.Development.View.Shared;
using UniRx;
using UnityEngine;

namespace Project.Development.View.Credit
{
    public sealed class CreditDevelopment : AppViewDevelopment<CreditView, CreditViewState>
    {
        protected override CreditViewState CreateState()
        {
            var state = new CreditViewState();
            state.CloseButton.OnClicked.Subscribe(_ => Debug.Log("Close Button Clicked")).AddTo(this);
            return state;
        }
    }
}

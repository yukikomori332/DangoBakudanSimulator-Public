using Project.Core.Scripts.Foundation.Common;
using Project.Core.Scripts.View.Archive;
using Project.Development.View.Shared;
using UniRx;
using UnityEngine;

namespace Project.Development.View.Archive
{
    public sealed class ArchiveDevelopment : AppViewDevelopment<ArchiveView, ArchiveViewState>
    {
        protected override ArchiveViewState CreateState()
        {
            var state = new ArchiveViewState();

            var itemNames = new string[]
            {
                "Alien",
                "Gold Alien",
            };
            var costs = new int[]
            {
                0,
                100,
            };
            var specialCosts = new int[]
            {
                0,
                0,
            };
            SetupItemSet(state.Units, itemNames, costs, specialCosts);

            state.BackButton.IsLocked.Value = false;
            state.BackButton.OnClicked.Subscribe(_ => Debug.Log("Back Button Clicked")).AddTo(this);

            return state;
        }

        private void SetupItemSet(ArchiveItemSetViewState viewState, string[] itemNames, int[] costs, int[] specialCosts)
        {
            viewState.Item1.IsLocked.Value = false;
            viewState.Item1.ItemName.Value = itemNames[0];
            viewState.Item1.Cost.Value = costs[0];
            viewState.Item1.SpecialCost.Value = specialCosts[0];
            viewState.Item2.IsLocked.Value = true;
            viewState.Item2.ItemName.Value = itemNames[1];
            viewState.Item2.Cost.Value = costs[1];
            viewState.Item2.SpecialCost.Value = specialCosts[1];
            viewState.Item3.IsLocked.Value = true;
            viewState.Item4.IsLocked.Value = true;
            viewState.Item5.IsLocked.Value = true;
            viewState.Item6.IsLocked.Value = true;
            viewState.Item7.IsLocked.Value = true;
            viewState.Item8.IsLocked.Value = true;
            viewState.Item9.IsLocked.Value = true;
        }
    }
}

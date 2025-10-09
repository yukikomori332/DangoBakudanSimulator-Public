using Project.Core.Scripts.Foundation.Common;
using Project.Core.Scripts.View.Archive;
using Project.Development.View.Shared;
using UniRx;
using UnityEngine;

namespace Project.Development.View.Archive
{
    public sealed class ArchiveItemSetDevelopment : AppViewDevelopment<ArchiveItemSetView, ArchiveItemSetViewState>
    {
        protected override ArchiveItemSetViewState CreateState()
        {
            var state = new ArchiveItemSetViewState();

            state.Item1.IsLocked.Value = false;
            state.Item1.ItemName.Value = "Alien";
            state.Item1.Cost.Value = 0;
            state.Item1.SpecialCost.Value = 0;

            state.Item2.IsLocked.Value = true;
            state.Item2.ItemName.Value = "Gold Alien";
            state.Item2.Cost.Value = 100;
            state.Item2.SpecialCost.Value = 0;

            state.Item3.IsLocked.Value = true;
            state.Item4.IsLocked.Value = true;
            state.Item5.IsLocked.Value = true;
            state.Item6.IsLocked.Value = true;
            state.Item7.IsLocked.Value = true;
            state.Item8.IsLocked.Value = true;
            state.Item9.IsLocked.Value = true;
            return state;
        }
    }
}

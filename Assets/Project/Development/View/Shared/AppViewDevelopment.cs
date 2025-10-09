using Project.Subsystem.PresentationFramework;
using UnityEngine;

namespace Project.Development.View.Shared
{
    public abstract class AppViewDevelopment<TView, TState> : MonoBehaviour
        where TView : AppView<TState>
        where TState : AppViewState
    {
        public TView view;
        public GameObject loadingCover;

        private async void Start()
        {
            if (loadingCover != null)
                loadingCover.SetActive(true);
            var state = CreateState();
            await view.InitializeAsync(state);
            if (loadingCover != null)
                loadingCover.SetActive(false);
        }

        protected abstract TState CreateState();
    }
}

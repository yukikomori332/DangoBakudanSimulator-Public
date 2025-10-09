using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Project.Core.Scripts.View.Foundation;
using Project.Subsystem.PresentationFramework;

namespace Project.Core.Scripts.View.Archive
{
    /// <summary>
    /// 図鑑アイテムのビューコンポーネント
    /// 図鑑アイテムを制御する機能を提供
    /// </summary>
    public sealed class ArchiveItemSetView : AppView<ArchiveItemSetViewState>
    {
        public ArchiveItemView item1;
        public ArchiveItemView item2;
        public ArchiveItemView item3;
        public ArchiveItemView item4;
        public ArchiveItemView item5;
        public ArchiveItemView item6;
        public ArchiveItemView item7;
        public ArchiveItemView item8;
        public ArchiveItemView item9;

        /// <summary>
        /// ビューの初期化処理
        /// </summary>
        /// <param name="viewState">ビューの状態</param>
        protected override async UniTask Initialize(ArchiveItemSetViewState viewState)
        {
            var tasks = new List<UniTask>
            {
                item1.InitializeAsync(viewState.Item1),
                item2.InitializeAsync(viewState.Item2),
                item3.InitializeAsync(viewState.Item3),
                item4.InitializeAsync(viewState.Item4),
                item5.InitializeAsync(viewState.Item5),
                item6.InitializeAsync(viewState.Item6),
                item7.InitializeAsync(viewState.Item7),
                item8.InitializeAsync(viewState.Item8),
                item9.InitializeAsync(viewState.Item9)
            };
            await UniTask.WhenAll(tasks);
        }
    }
}

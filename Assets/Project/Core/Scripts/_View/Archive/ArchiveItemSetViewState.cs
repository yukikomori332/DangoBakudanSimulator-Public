using Project.Core.Scripts.View.Foundation;
using Project.Subsystem.PresentationFramework;

namespace Project.Core.Scripts.View.Archive
{
    public sealed class ArchiveItemSetViewState : AppViewState, IArchiveItemSetState
    {
        public ArchiveItemViewState Item1 { get; } = new ArchiveItemViewState();
        public ArchiveItemViewState Item2 { get; } = new ArchiveItemViewState();
        public ArchiveItemViewState Item3 { get; } = new ArchiveItemViewState();
        public ArchiveItemViewState Item4 { get; } = new ArchiveItemViewState();
        public ArchiveItemViewState Item5 { get; } = new ArchiveItemViewState();
        public ArchiveItemViewState Item6 { get; } = new ArchiveItemViewState();
        public ArchiveItemViewState Item7 { get; } = new ArchiveItemViewState();
        public ArchiveItemViewState Item8 { get; } = new ArchiveItemViewState();
        public ArchiveItemViewState Item9 { get; } = new ArchiveItemViewState();

        /// <summary>
        /// リソースの解放を行う
        /// </summary>
        protected override void DisposeInternal()
        {
            Item1.Dispose();
            Item2.Dispose();
            Item3.Dispose();
            Item4.Dispose();
            Item5.Dispose();
            Item6.Dispose();
            Item7.Dispose();
            Item8.Dispose();
            Item9.Dispose();
        }
    }

    /// <summary>
    /// 図鑑アイテムのビューの状態を制御するためのインターフェース
    /// </summary>
    internal interface IArchiveItemSetState
    {
    }
}

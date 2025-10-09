using Project.Subsystem.PresentationFramework;

namespace Project.Core.Scripts.View.Archive
{
    /// <summary>
    /// 図鑑画面のページを管理するクラス
    /// </summary>
    /// <remarks>
    /// 図鑑画面の表示・非表示の制御と状態管理を行う。
    /// ArchiveViewとArchiveViewStateを継承して、図鑑画面のビューと状態を紐付ける。
    /// </remarks>
    public sealed class ArchivePage : Page<ArchiveView, ArchiveViewState>
    {
    }
}

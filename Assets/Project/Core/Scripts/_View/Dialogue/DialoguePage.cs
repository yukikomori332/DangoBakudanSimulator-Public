using Project.Subsystem.PresentationFramework;

namespace Project.Core.Scripts.View.Dialogue
{
    /// <summary>
    /// ダイアログ画面のページを管理するクラス
    /// </summary>
    /// <remarks>
    /// ダイアログ画面の表示・非表示の制御と状態管理を行う。
    /// DialogueViewとDialogueViewStateを継承して、ダイアログ画面のビューと状態を紐付ける。
    /// </remarks>
    public sealed class DialoguePage : Page<DialogueView, DialogueViewState>
    {
    }
}

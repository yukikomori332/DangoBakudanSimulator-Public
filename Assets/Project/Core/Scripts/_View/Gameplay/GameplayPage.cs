using Project.Subsystem.PresentationFramework;

namespace Project.Core.Scripts.View.Gameplay
{
    /// <summary>
    /// ゲームプレイ画面のページを管理するクラス
    /// </summary>
    /// <remarks>
    /// ゲームプレイ画面の表示・非表示の制御と状態管理を行う。
    /// GameplayViewとGameplayViewStateを継承して、ゲームプレイ画面のビューと状態を紐付ける。
    /// </remarks>
    public sealed class GameplayPage : Page<GameplayView, GameplayViewState>
    {
    }
}

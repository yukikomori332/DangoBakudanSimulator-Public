using Project.Subsystem.PresentationFramework;

namespace Project.Core.Scripts.View.Setting
{
    /// <summary>
    /// 設定画面のモーダルを管理するクラス
    /// </summary>
    /// <remarks>
    /// 設定画面の表示・非表示の制御と状態管理を行う。
    /// SettingsViewとSettingsViewStateを継承して、設定画面のビューと状態を紐付ける。
    /// </remarks>
    public sealed class SettingsModal : Modal<SettingsView, SettingsViewState>
    {
    }
}

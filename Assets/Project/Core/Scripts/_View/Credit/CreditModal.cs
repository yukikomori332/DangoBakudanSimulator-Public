using Project.Subsystem.PresentationFramework;

namespace Project.Core.Scripts.View.Credit
{
    /// <summary>
    /// クレジット画面のモーダルを管理するクラス
    /// </summary>
    /// <remarks>
    /// クレジット画面の表示・非表示の制御と状態管理を行う。
    /// CreditViewとCreditViewStateを継承して、クレジット画面のビューと状態を紐付ける。
    /// </remarks>
    public sealed class CreditModal : Modal<CreditView, CreditViewState>
    {
    }
}

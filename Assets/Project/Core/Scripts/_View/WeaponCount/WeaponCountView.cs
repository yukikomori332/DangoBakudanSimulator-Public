using Cysharp.Threading.Tasks;
using Project.Core.Scripts.View.Foundation.Binders;
using Project.Subsystem.PresentationFramework;
using UniRx;
using TMPro;

namespace Project.Core.Scripts.View.WeaponCount
{
    /// <summary>
    /// 武器数表示のビューを管理するクラス
    /// 武器数表示などのUI要素を制御
    /// </summary>
    public sealed class WeaponCountView : AppView<WeaponCountViewState>
    {
        public TextMeshProUGUI weaponCountText; // 武器数表示用のテキスト

        protected override UniTask Initialize(WeaponCountViewState viewState)
        {
            // 武器数表示用のテキストにイベントを設定
            weaponCountText.SetTextSource(viewState.WeaponCount).AddTo(this);

            return UniTask.CompletedTask;
        }
    }
}

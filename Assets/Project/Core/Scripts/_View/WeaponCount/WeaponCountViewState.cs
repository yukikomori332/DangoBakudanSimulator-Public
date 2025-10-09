using Project.Subsystem.PresentationFramework;
using UniRx;

namespace Project.Core.Scripts.View.WeaponCount
{
    /// <summary>
    /// 武器数表示の状態を管理するクラス
    /// </summary>
    public sealed class WeaponCountViewState : AppViewState, IWeaponCountState
    {
        // 武器数表示の状態を管理するReactiveProperty
        private readonly ReactiveProperty<string> _weaponCount = new ReactiveProperty<string>();

        // 武器数表示の状態を外部に公開するプロパティ
        public IReactiveProperty<string> WeaponCount => _weaponCount;

        /// <summary>
        /// リソースの解放を行う
        /// </summary>
        protected override void DisposeInternal()
        {
            _weaponCount.Dispose();
        }
    }

    /// <summary>
    /// 武器数表示の状態を制御するためのインターフェース
    /// </summary>
    internal interface IWeaponCountState
    {
    }
}


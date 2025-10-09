using System;
using System.Collections.Generic;
using UniRx;

namespace Project.Core.Scripts.Gameplay.Domain.Weapon.Model
{
    /// <summary>
    /// 武器の情報を一括で管理するクラス
    /// </summary>
    public sealed class WeaponModelSet
    {
        // 武器の情報リスト
        private readonly List<WeaponModel> _weapons = new List<WeaponModel>();

        // 武器の情報リストを外部に公開するプロパティ
        public IReadOnlyList<WeaponModel> Weapons => _weapons;

        /// <summary>
        /// 武器の情報リストをセットする
        /// </summary>
        public void SetWeapons(IReadOnlyList<WeaponModel> Weapons)
        {
            _weapons.Clear();
            _weapons.AddRange(Weapons);
        }
    }
}

using System;
using UnityEngine;

namespace Project.Core.Scripts.Gameplay.Domain.Weapon.Model
{
    /// <summary>
    /// 武器のマスターデータを管理するクラス
    /// </summary>
    [Serializable]
    public sealed class WeaponMaster
    {
        [SerializeField] private string id;         // 武器の一意識別子
        [SerializeField] private GameObject prefab; // 武器のプレハブ

        // 武器の一意識別子
        public string Id => id;

        // 武器のプレハブ
        public GameObject Prefab => prefab;
    }
}

using UnityEngine;

namespace Project.Core.Scripts.Gameplay.MasterRepository.Weapon
{
    /// <summary>
    /// 武器のマスターデータを管理するScriptableObject
    /// </summary>
    [CreateAssetMenu(menuName = "Project/Master Data/Weapon")]
    public sealed class WeaponMasterTableAsset : ScriptableObject
    {
        // 武器のマスターテーブルデータ
        [SerializeField] private WeaponMasterTable masterTable = new WeaponMasterTable();

        // マスターテーブルデータへの読み取り専用アクセスを提供
        public WeaponMasterTable MasterTable => masterTable;
    }
}

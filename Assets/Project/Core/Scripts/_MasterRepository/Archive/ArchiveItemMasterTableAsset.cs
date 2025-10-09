using UnityEngine;

namespace Project.Core.Scripts.MasterRepository.Archive
{
    /// <summary>
    /// 図鑑アイテムのマスターデータを管理するScriptableObject
    /// </summary>
    [CreateAssetMenu(menuName = "Project/Master Data/Archive Item")]
    public sealed class ArchiveItemMasterTableAsset : ScriptableObject
    {
        [SerializeField] private ArchiveItemMasterTable masterTable = new ArchiveItemMasterTable(); // 図鑑アイテムのマスターテーブルデータ

        // マスターテーブルデータへの読み取り専用アクセスを提供
        public ArchiveItemMasterTable MasterTable => masterTable;
    }
}

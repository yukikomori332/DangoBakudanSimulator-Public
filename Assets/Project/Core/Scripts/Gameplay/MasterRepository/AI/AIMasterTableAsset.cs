using UnityEngine;

namespace Project.Core.Scripts.Gameplay.MasterRepository.AI
{
    /// <summary>
    /// AIのマスターデータを管理するScriptableObject
    /// </summary>
    [CreateAssetMenu(menuName = "Project/Master Data/AI")]
    public sealed class AIMasterTableAsset : ScriptableObject
    {
        // AIのマスターテーブルデータ
        [SerializeField] private AIMasterTable masterTable = new AIMasterTable();

        // マスターテーブルデータへの読み取り専用アクセスを提供
        public AIMasterTable MasterTable => masterTable;
    }
}

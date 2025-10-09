using UnityEngine;

namespace Project.Core.Scripts.MasterRepository.FeatureFlag
{
    /// <summary>
    /// 機能フラグのマスターデータを管理するScriptableObject
    /// </summary>
    [CreateAssetMenu(menuName = "Project/Master Data/Feature Flag")]
    public sealed class FeatureFlagMasterTableAsset : ScriptableObject
    {
        // 機能フラグのマスターテーブルデータ
        [SerializeField] private FeatureFlagMasterTable masterTable = new FeatureFlagMasterTable();

        // マスターテーブルデータへの読み取り専用アクセスを提供
        public FeatureFlagMasterTable MasterTable => masterTable;
    }
}

using UnityEngine;

namespace Project.Core.Scripts.MasterRepository.Audio
{
    /// <summary>
    /// オーディオのマスターデータを管理するScriptableObject
    /// </summary>
    [CreateAssetMenu(menuName = "Project/Master Data/Audio")]
    public sealed class AudioMasterTableAsset : ScriptableObject
    {
        // オーディオのマスターテーブルデータ
        [SerializeField] AudioMasterTable masterTable = new AudioMasterTable();

        // マスターテーブルデータへの読み取り専用アクセスを提供
        public AudioMasterTable MasterTable => masterTable;
    }
}

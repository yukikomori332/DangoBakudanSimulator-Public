using UnityEngine;

namespace Project.Core.Scripts.MasterRepository.Dialogue
{
    /// <summary>
    /// ダイアログのマスターデータを管理するScriptableObject
    /// </summary>
    [CreateAssetMenu(menuName = "Project/Master Data/Dialogue")]
    public sealed class DialogueMasterTableAsset : ScriptableObject
    {
        // ダイアログのマスターテーブルデータ
        [SerializeField] DialogueMasterTable masterTable = new DialogueMasterTable();

        // マスターテーブルデータへの読み取り専用アクセスを提供
        public DialogueMasterTable MasterTable => masterTable;
    }
}

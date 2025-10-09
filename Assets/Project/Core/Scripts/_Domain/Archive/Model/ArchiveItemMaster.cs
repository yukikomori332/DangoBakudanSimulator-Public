using System;
using UnityEngine;

namespace Project.Core.Scripts.Domain.Archive.Model
{
    /// <summary>
    /// 図鑑のマスターデータを管理するクラス
    /// </summary>
    [Serializable]
    public sealed class ArchiveItemMaster
    {
        [SerializeField] private string id;                    // 図鑑の一意識別子
        [SerializeField] private string itemName;              // アイテム名
        [SerializeField] private Sprite sprite;                // アイテム画像
        [SerializeField] private int cost;                     // コスト
        [SerializeField] private int specialCost;              // 特別なコスト

        // 図鑑の一意識別子
        public string Id => id;

        // アイテム名
        public string ItemName => itemName;

        // アイテム画像
        public Sprite Sprite => sprite;

        // コスト
        public int Cost => cost;

        // 特別なコスト
        public int SpecialCost => specialCost;
    }
}

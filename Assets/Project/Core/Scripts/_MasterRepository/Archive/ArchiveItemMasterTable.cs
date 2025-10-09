using System;
using System.Collections.Generic;
using System.Linq;
using Project.Core.Scripts.Domain.Archive.MasterRepository;
using Project.Core.Scripts.Domain.Archive.Model;
using UnityEngine;

namespace Project.Core.Scripts.MasterRepository.Archive
{
    /// <summary>
    /// 図鑑アイテムのマスターデータを管理するテーブルクラス
    /// </summary>
    [Serializable]
    public sealed class ArchiveItemMasterTable : IArchiveItemMasterTable
    {
        [SerializeField] private List<ArchiveItemMaster> items = new List<ArchiveItemMaster>(); // 図鑑アイテムのマスターデータのリスト

        [NonSerialized] private bool _isInitialized;                                            // 初期化済みかどうかのフラグ
        private Dictionary<string, ArchiveItemMaster> _items;                                   // 図鑑アイテムのマスターデータをIDで検索するためのディクショナリ

        /// <summary>
        /// 指定されたIDの図鑑アイテムのマスターデータを検索する
        /// </summary>
        /// <param name="id">検索する図鑑アイテムのID</param>
        /// <returns>見つかった図鑑アイテムのマスターデータ。見つからない場合はnullを返す</returns>
        public ArchiveItemMaster FindById(string id)
        {
            if (!_isInitialized)
                throw new InvalidOperationException($"{nameof(ArchiveItemMasterTable)} is not initialized. Call {nameof(Initialize)}() first.");

            return !_items.TryGetValue(id, out var item) ? null : item;
        }

        /// <summary>
        /// テーブルを初期化する
        /// </summary>
        public void Initialize()
        {
            if (_isInitialized)
                return;

            _items = items.ToDictionary(x => x.Id);

            _isInitialized = true;
        }

        /// <summary>
        /// テーブルのアイテム数を取得する
        /// </summary>
        public int GetCount()
        {
            if (items != null && items.Count > 0)
                return items.Count;
            else
                return 0;
        }
    }
}

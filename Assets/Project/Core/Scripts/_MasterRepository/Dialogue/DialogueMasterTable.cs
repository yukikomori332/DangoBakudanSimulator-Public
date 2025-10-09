using System;
using System.Collections.Generic;
using System.Linq;
using Project.Core.Scripts.Domain.Dialogue.MasterRepository;
using Project.Core.Scripts.Domain.Dialogue.Model;
using UnityEngine;

namespace Project.Core.Scripts.MasterRepository.Dialogue
{
    /// <summary>
    /// ダイアログのマスターデータを管理するテーブルクラス
    /// </summary>
    [Serializable]
    public sealed class DialogueMasterTable : IDialogueMasterTable
    {
        // ダイアログのマスターデータのリスト
        [SerializeField] private List<DialogueMaster> items = new List<DialogueMaster>();

        // 初期化済みかどうかのフラグ
        [NonSerialized] private bool _isInitialized;

        // ダイアログのマスターデータをIDで検索するためのディクショナリ
        private Dictionary<string, DialogueMaster> _items;

        /// <summary>
        /// 指定されたIDに対応するダイアログのマスターデータを検索する
        /// </summary>
        /// <param name="id">検索対象のダイアログのID</param>
        /// <returns>見つかったダイアログのマスターデータ。見つからない場合はnullを返す</returns>
        public DialogueMaster FindById(string id)
        {
            if (!_isInitialized)
                throw new InvalidOperationException(
                    $"{nameof(DialogueMasterTable)} is not initialized. Call {nameof(Initialize)}() first.");

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

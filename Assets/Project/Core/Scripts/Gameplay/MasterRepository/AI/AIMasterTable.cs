using System;
using System.Collections.Generic;
using System.Linq;
using Project.Core.Scripts.Gameplay.Domain.AI.Model;
using UnityEngine;

namespace Project.Core.Scripts.Gameplay.MasterRepository.AI
{
    /// <summary>
    /// AIのマスターデータを管理するテーブルクラス
    /// </summary>
    [Serializable]
    public sealed class AIMasterTable
    {
        // AIのマスターデータのリスト
        [SerializeField] private List<AIMaster> items = new List<AIMaster>();

        // 初期化済みかどうかのフラグ
        [NonSerialized] private bool _isInitialized;

        // AIのマスターデータをIDで検索するためのディクショナリ
        private Dictionary<string, AIMaster> _items;

        /// <summary>
        /// 指定されたIDに対応するAIのマスターデータを検索する
        /// </summary>
        /// <param name="id">検索対象のAIのID</param>
        /// <returns>見つかったAIのマスターデータ。見つからない場合はnullを返す</returns>
        public AIMaster FindById(string id)
        {
            if (!_isInitialized)
                throw new InvalidOperationException($"{nameof(AIMasterTable)}は初期化されていません。先に{nameof(Initialize)}()を呼んでください。");

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

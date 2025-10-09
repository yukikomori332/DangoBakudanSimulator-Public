using System;
using System.Collections.Generic;
using System.Linq;
using Project.Core.Scripts.Gameplay.Domain.Weapon.Model;
using UnityEngine;

namespace Project.Core.Scripts.Gameplay.MasterRepository.Weapon
{
    /// <summary>
    /// 武器のマスターデータを管理するテーブルクラス
    /// </summary>
    [Serializable]
    public sealed class WeaponMasterTable
    {
        // 武器のマスターデータのリスト
        [SerializeField] private List<WeaponMaster> items = new List<WeaponMaster>();

        // 初期化済みかどうかのフラグ
        [NonSerialized] private bool _isInitialized;

        // 武器のマスターデータをIDで検索するためのディクショナリ
        private Dictionary<string, WeaponMaster> _items;

        /// <summary>
        /// 指定されたIDに対応する武器のマスターデータを検索する
        /// </summary>
        /// <param name="id">検索対象の武器のID</param>
        /// <returns>見つかった武器のマスターデータ。見つからない場合はnullを返す</returns>
        public WeaponMaster FindById(string id)
        {
            if (!_isInitialized)
                throw new InvalidOperationException($"{nameof(WeaponMasterTable)}は初期化されていません。先に{nameof(Initialize)}()を呼んでください。");

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

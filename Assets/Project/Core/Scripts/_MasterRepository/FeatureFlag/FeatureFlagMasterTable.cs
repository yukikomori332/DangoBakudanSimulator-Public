using System;
using System.Collections.Generic;
using System.Linq;
using Project.Core.Scripts.Domain.FeatureFlag.MasterRepository;
using Project.Core.Scripts.Domain.FeatureFlag.Model;
using UnityEngine;

namespace Project.Core.Scripts.MasterRepository.FeatureFlag
{
    /// <summary>
    /// 機能フラグのマスターデータを管理するテーブルクラス
    /// </summary>
    [Serializable]
    public sealed class FeatureFlagMasterTable : IFeatureFlagMasterTable
    {
        // 機能フラグのマスターデータのリスト
        [SerializeField] private List<FeatureFlagMaster> items = new List<FeatureFlagMaster>();

        // 初期化済みかどうかのフラグ
        [NonSerialized] private bool _isInitialized;

        // 機能フラグのマスターデータをIDで検索するためのディクショナリ
        private Dictionary<string, FeatureFlagMaster> _items;

        /// <summary>
        /// 指定されたIDの機能フラグのマスターデータを検索する
        /// </summary>
        /// <param name="id">検索する機能フラグのID</param>
        /// <returns>見つかった機能フラグのマスターデータ。見つからない場合はnullを返す</returns>
        public FeatureFlagMaster FindById(string id)
        {
            if (!_isInitialized)
                throw new InvalidOperationException($"{nameof(FeatureFlagMasterTable)} is not initialized. Call {nameof(Initialize)}() first.");

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
    }
}

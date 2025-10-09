using System;
using System.Collections.Generic;
using System.Linq;
using Project.Core.Scripts.Domain.Audio.Model;
using UnityEngine;

namespace Project.Core.Scripts.MasterRepository.Audio
{
    /// <summary>
    /// オーディオのマスターデータを管理するテーブルクラス
    /// </summary>
    [Serializable]
    public sealed class AudioMasterTable
    {
        // オーディオのマスターデータのリスト
        [SerializeField] private List<AudioMaster> items = new List<AudioMaster>();

        // 初期化済みかどうかのフラグ
        [NonSerialized] private bool _isInitialized;

        // オーディオのマスターデータをIDで検索するためのディクショナリ
        private Dictionary<string, AudioMaster> _items;

        /// <summary>
        /// 指定されたIDに対応するオーディオのマスターデータを検索する
        /// </summary>
        /// <param name="id">検索対象のオーディオのID</param>
        /// <returns>見つかったオーディオのマスターデータ。見つからない場合はnullを返す</returns>
        public AudioMaster FindById(string id)
        {
            if (!_isInitialized)
                throw new InvalidOperationException(
                    $"{nameof(AudioMasterTable)} is not initialized. Call {nameof(Initialize)}() first.");

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

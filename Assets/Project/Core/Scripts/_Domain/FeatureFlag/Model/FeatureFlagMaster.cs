using System;
using UnityEngine;

namespace Project.Core.Scripts.Domain.FeatureFlag.Model
{
    /// <summary>
    /// 機能フラグのマスターデータを管理するクラス
    /// </summary>
    [Serializable]
    public sealed class FeatureFlagMaster
    {
        [SerializeField] private string id;    // 機能フラグの一意識別子
        [SerializeField] private bool enabled; // 機能フラグの有効/無効状態 （true: 機能が有効、 false: 機能が無効）

        // 機能フラグの一意識別子
        public string Id => id;

        // 機能フラグの有効/無効状態
        public bool Enabled => enabled;
    }
}

using System;
using UniRx;
using UnityEngine;

namespace Project.Core.Scripts.Gameplay.Domain.AI.Model
{
    /// <summary>
    /// ID、マスターデータID、オブジェクトの有効状態を管理するクラス
    /// </summary>
    public sealed class AIModel
    {
        public AIModel(string id, string masterId)
        {
            Id = id;
            MasterId = masterId;
            _isActive.Value = true;
        }

        // ID
        public string Id { get; }

        // マスターデータID
        public string MasterId { get; }

        // オブジェクトの有効状態を管理するReactiveProperty
        private readonly ReactiveProperty<bool> _isActive = new ReactiveProperty<bool>();

        // オブジェクトの有効状態を外部に公開するプロパティ
        public IReactiveProperty<bool> IsActive => _isActive;

        /// <summary>
        /// リソースの解放を行う
        /// </summary>
        public void Dispose()
        {
            _isActive.Dispose();
        }
    }
}

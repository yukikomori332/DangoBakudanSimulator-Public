using System;
using UniRx;
using UnityEngine;

namespace Project.Core.Scripts.Gameplay.Domain.Weapon.Model
{
    /// <summary>
    /// ID、マスターデータID、オブジェクトの有効状態を管理するクラス
    /// </summary>
    public sealed class WeaponModel
    {
        public WeaponModel(string id, string masterId)
        {
            Id = id;
            MasterId = masterId;
        }

        // ID
        public string Id { get; }

        // マスターデータID
        public string MasterId { get; }

        // オブジェクトの有効状態を管理するReactiveProperty
        private readonly ReactiveProperty<bool> _isActive = new ReactiveProperty<bool>();
        // 設置状態を管理するReactiveProperty
        private readonly ReactiveProperty<bool> _isPlaced = new ReactiveProperty<bool>();

        // オブジェクトの有効状態を外部に公開するプロパティ
        public IReactiveProperty<bool> IsActive => _isActive;
        // プレビューの状態を外部に公開するプロパティ
        public IReactiveProperty<bool> IsPlaced => _isPlaced;

        /// <summary>
        /// リソースの解放を行う
        /// </summary>
        public void Dispose()
        {
            _isActive.Dispose();
            _isPlaced.Dispose();
        }
    }
}

using System;
using UniRx;
using UnityEngine;

namespace Project.Core.Scripts.Gameplay.Domain.PlayerInput.Model
{
    /// <summary>
    /// プレイヤーの入力状態を管理するクラス
    /// </summary>
    public sealed class PlayerInputModel
    {
        public PlayerInputModel()
        {
        }

        // 移動方向の状態を管理するReactiveProperty
        private readonly ReactiveProperty<Vector2> _moveDirection = new ReactiveProperty<Vector2>();

        // 移動方向の状態を外部に公開するプロパティ
        public IReactiveProperty<Vector2> MoveDirection => _moveDirection;


        // 武器の位置情報の状態を管理するReactiveProperty
        private readonly ReactiveProperty<Vector3> _weaponPosition = new ReactiveProperty<Vector3>();

        // プレビューの状態を管理するReactiveProperty
        private readonly ReactiveProperty<bool> _isPreview = new ReactiveProperty<bool>();

        // 設置状態を管理するReactiveProperty
        private readonly ReactiveProperty<bool> _isPlaced = new ReactiveProperty<bool>();


        // 武器の位置情報の状態を外部に公開するプロパティ
        public IReactiveProperty<Vector3> WeaponPosition => _weaponPosition;

        // プレビューの状態を外部に公開するプロパティ
        public IReactiveProperty<bool> IsPreview => _isPreview;

        // プレビューの状態を外部に公開するプロパティ
        public IReactiveProperty<bool> IsPlaced => _isPlaced;

        /// <summary>
        /// リソースの解放を行う
        /// </summary>
        public void Dispose()
        {
            _moveDirection.Dispose();

            _weaponPosition.Dispose();
            _isPreview.Dispose();
            _isPlaced.Dispose();
        }
    }
}

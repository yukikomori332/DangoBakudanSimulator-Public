using System;
using Project.Core.Scripts.Foundation.Common;
using Project.Core.Scripts.Gameplay.Domain.PlayerInput.Model;
using Project.Core.Scripts.Gameplay.View.AI;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Project.Core.Scripts.Gameplay.Provider.PlayerInput
{
    /// <summary>
    /// プレイヤーの入力状態を管理するクラス
    /// </summary>
    public sealed class PlayerInputProvider
    {
        private readonly PlayerInputModel _playerInputModel;         // プレイヤー入力のモデル

        public PlayerInputProvider(PlayerInputModel playerInputModel)
        {
            _playerInputModel = playerInputModel;

            Initialize();
        }

        private CompositeDisposable _disposables = new CompositeDisposable(9999);

        private PlayerInputAction _playerInputAction;                // プレイヤーの入力アクション
        private GameObject _collidedObject;                          // 衝突したオブジェクト

        /// <summary>
        /// 入力プロバイダーを初期化する
        /// </summary>
        private void Initialize()
        {
            // CompositeDisposableを作成
            _disposables = new CompositeDisposable();

            // プレイヤーの入力アクションの初期化
            _playerInputAction = new PlayerInputAction();

            // プレイヤーの入力アクションのイベント登録・有効化
            if (_playerInputAction != null)
            {
                _playerInputAction.Enable();
            }

            // パラメータの設定
            var camera = Camera.main;
            var clickableLayerMask = 1 << 6;

            // 入力状態の変更を監視
            Observable.EveryUpdate()
                .Subscribe(_ =>
                {
                    // プレイヤーの入力アクションが作成できていない
                    // UIの上にカーソルがあれば入力を受け付けない
                    if (_playerInputAction == null ||
                        EventSystem.current.IsPointerOverGameObject())
                        return;

                    // プレイヤーが発射ボタンを押したとき
                    // プレビュー状態なら
                    if (_playerInputAction.Player.Place.WasPressedThisFrame()
                        && _playerInputModel.IsPreview.Value)
                    {
                        // レイキャストとレイを作成
                        RaycastHit hit;
                        Ray ray = camera.ScreenPointToRay(Mouse.current.position.ReadValue());

                        // レイキャストでオブジェクトとの衝突を検出
                        if(Physics.Raycast(ray, out hit, Mathf.Infinity, clickableLayerMask))
                        {
                            // 武器の位置情報を更新する
                            _playerInputModel.WeaponPosition.Value = hit.point;

                            // プレビュー状態を無効にする
                            _playerInputModel.IsPreview.Value = false;

                            // 設置状態を有効にする
                            _playerInputModel.IsPlaced.Value = true;
                        }
                    }
                    // プレイヤーが発射ボタンを押していないとき
                    // 設置状態でないなら
                    else if (!_playerInputAction.Player.Place.WasPressedThisFrame()
                        && !_playerInputModel.IsPlaced.Value)
                    {
                        // レイキャストとレイを作成
                        RaycastHit hit;
                        Ray ray = camera.ScreenPointToRay(Mouse.current.position.ReadValue());

                        // レイキャストでオブジェクトとの衝突を検出
                        if(Physics.Raycast(ray, out hit, Mathf.Infinity, clickableLayerMask))
                        {
                            // 武器の位置情報を更新する
                            _playerInputModel.WeaponPosition.Value = hit.point;

                            // プレビュー状態を有効にする
                            _playerInputModel.IsPreview.Value = true;
                        }
                    }
                })
                .AddTo(_disposables);
        }

        /// <summary>
        /// リソースの解放を行う
        /// </summary>
        public void Dispose()
        {
            // プレイヤーの入力アクションのイベント解除・無効化
            if (_playerInputAction != null)
            {
                _playerInputAction.Disable();
                _playerInputAction.Dispose();
            }

            _playerInputAction = null;
            _disposables.Dispose();
        }
    }
}

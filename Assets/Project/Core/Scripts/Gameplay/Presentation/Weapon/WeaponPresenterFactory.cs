using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Project.Core.Scripts.Foundation.Common;
using Project.Core.Scripts.Presentation.Shared;
using Project.Core.Scripts.UseCase.Gameplay;
using Project.Core.Scripts.Gameplay.Components;
using Project.Core.Scripts.Gameplay.Domain.Weapon.Model;
using Project.Core.Scripts.Gameplay.MasterRepository.Weapon;
using Project.Core.Scripts.Gameplay.View.Weapon;
using UniRx;
using UnityEngine;

namespace Project.Core.Scripts.Gameplay.Presentation.Weapon
{
    /// <summary>
    /// WeaponPresenterを生成するファクトリークラス
    /// </summary>
    public sealed class WeaponPresenterFactory
    {
        private readonly GameplayUseCase _gameplayUseCase;                          // ゲームプレイのビジネスロジック
        private readonly IAudioPlayService _audioPlayService;                       // 音声再生サービス

        public WeaponPresenterFactory(GameplayUseCase gameplayUseCase, IAudioPlayService audioPlayService)
        {
            _gameplayUseCase = gameplayUseCase;
            _audioPlayService = audioPlayService;
        }

        private GameObject _parent; // プールの親オブジェクト

        private CompositeDisposable _disposables = new CompositeDisposable(9999);

        private List<GameObject> _pool; // 武器のプール
        private GameObject _currentWeapon; // 現在の武器

        private Planet _planet
        {
            get => UnityEngine.Object.FindFirstObjectByType<Planet>(FindObjectsInactive.Exclude);
        }

        /// <summary>
        /// 武器のセットアップ
        /// </summary>
        public /*async UniTask*/ void Setup()
        {
            // プールの親となるオブジェクトを生成
            _parent = new GameObject("Weapons Root");

            // プレイヤー入力のモデルを取得する
            var playerInputModel = _gameplayUseCase.PlayerInputModel;

            // 武器のマスターテーブルを取得
            var masterTable = _gameplayUseCase.WeaponMasterTable;

            // 武器情報のリストを取得
            // var weapons = new List<WeaponModel>(_gameplayUseCase.WeaponModelSet.Weapons);
            var weapons = _gameplayUseCase.WeaponModelSet.Weapons;

            // プールを作成
            _pool = new List<GameObject>(weapons.Count);

            for (int i = 0; i < weapons.Count; i++)
            {
                // IDで武器のデータを検索
                var weaponMaster = masterTable.FindById(weapons[i].MasterId);
                // 新しい武器の生成
                var newWeapon = UnityEngine.Object.Instantiate(weaponMaster.Prefab, weaponMaster.Prefab.transform.position, Quaternion.identity);

                // 名前を設定
                newWeapon.name = $"Weapon ({i})";
                // 親となるオブジェクトを設定
                newWeapon.transform.SetParent(_parent.transform);

                // WeaponViewを取得
                if (newWeapon.TryGetComponent<WeaponView>(out WeaponView weaponView))
                {
                    // WeaponPresenterを取得
                    if (weaponView.TryGetComponent<WeaponPresenter>(out WeaponPresenter weaponPresenter))
                    {
                        // プレゼンターの初期化を行う
                        weaponPresenter.Initialize($"{i}", weaponView, _gameplayUseCase, _audioPlayService);
                    }

                    // 設置時の見た目を無効にする
                    weaponView.TogglePlacedVisual(false);
                }

                // プールに追加
                _pool.Add(newWeapon);
            }

            // 全ての武器を無効状態にする
            for (int i = 0; i < _pool.Count; i++)
            {
                if (_pool[i] != null)
                    _pool[i].SetActive(false);
            }

            // プレイヤー入力の状態の変更を監視
            playerInputModel.IsPreview
                .ObserveEveryValueChanged(_ => playerInputModel.IsPreview.Value)
                .Subscribe(_ =>
                {
                    // タイマーが作動していなければ、処理をスキップ
                    if (!_gameplayUseCase.RunningTimer) return;

                    // プレビュー状態なら
                    if (playerInputModel.IsPreview.Value)
                    {
                        // 現在の武器が存在しなければ
                        if (_currentWeapon == null)
                        {
                            // プールのインデックス番号を取得
                            var index = 0;
                            // 現在の武器を取得する
                            _currentWeapon = GetPool(index);
                        }

                        // 武器を保有していれば
                        if (_currentWeapon != null)
                        {
                            // 武器を有効状態にする
                            _currentWeapon.SetActive(true);

                            // WeaponViewを取得
                            if (_currentWeapon.TryGetComponent<WeaponView>(out WeaponView weaponView))
                            {
                                // 設置時の見た目を無効にする
                                weaponView.TogglePlacedVisual(false);

                                // 武器が最大個数に達していなければ
                                if (!_gameplayUseCase.HasReachedMaxWeapon())
                                {
                                    // プレビュー時のマテリアルを更新する
                                    weaponView.TogglePreviewMaterialColor(true);
                                    // プレビュー時の見た目を有効にする
                                    weaponView.TogglePreviewVisual(true);
                                }
                                // 武器が最大個数に達していれば
                                else if (_gameplayUseCase.HasReachedMaxWeapon())
                                {
                                    // プレビュー時のマテリアルを更新する
                                    weaponView.TogglePreviewMaterialColor(false);
                                    // プレビュー時の見た目を有効にする
                                    weaponView.TogglePreviewVisual(true);

                                    playerInputModel.IsPreview.Value = false;
                                }
                            }
                        }
                    }
                })
                .AddTo(_disposables);

            // プレイヤー入力の状態の変更を監視
            playerInputModel.IsPlaced
                .ObserveEveryValueChanged(_ => playerInputModel.IsPlaced.Value)
                .Subscribe(_ =>
                {
                    // 設置状態なら
                    if (playerInputModel.IsPlaced.Value)
                    {
                        if (_currentWeapon != null)
                        {
                            // WeaponViewを取得
                            if (_currentWeapon.TryGetComponent<WeaponView>(out WeaponView weaponView))
                            {
                                // プレビュー時の見た目を無効にする
                                weaponView.TogglePreviewVisual(false);
                                // 設置時の見た目を有効にする
                                weaponView.TogglePlacedVisual(true);
                            }
                        }

                        // 現在の武器を破棄する
                        _currentWeapon = null;

                        // 配置状態を無効にする
                        playerInputModel.IsPlaced.Value = false;
                    }
                    // 設置状態なら
                    else if (playerInputModel.IsPlaced.Value)
                    {
                        // 配置状態を無効にする
                        playerInputModel.IsPlaced.Value = false;
                    }
                })
                .AddTo(_disposables);

            Observable.EveryUpdate()
                .Subscribe(_ =>
                {
                    // 武器を保有していれば
                    if (_currentWeapon != null)
                    {
                        //　武器の位置を更新
                        _currentWeapon.transform.position = ProjectOntoSphere(playerInputModel.WeaponPosition.Value);

                        // 球体表面に対して正しい向きに設定
                        var up = (_currentWeapon.transform.position - _planet.transform.position).normalized;
                        _currentWeapon.transform.up = up;
                    }
                })
                .AddTo(_disposables);
        }

        /// <summary>
        /// 指定されたインデックスに対応するゲームオブジェクトをプールから取得
        /// </summary>
        /// <param name="index">指定されたインデックス</param>
        /// <returns>ゲームオブジェクト</returns>
        private GameObject GetPool(int index)
        {
            GameObject select = null;

            // プール内の無効のオブジェクトを探す
            foreach (GameObject pool in _pool)
            {
                if (pool == null) continue;;

                if (!pool.activeSelf)
                {
                    select = pool;
                    break;
                }
            }

            return select;
        }

        // <summary>
        /// 位置を球体表面に投影
        /// </summary>
        private Vector3 ProjectOntoSphere(Vector3 position)
        {
            var direction = (position - _planet.transform.position).normalized;
            return _planet.transform.position + direction * 15f/*sphereRadius*/;
        }
    }
}

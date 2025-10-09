using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Project.Core.Scripts.Foundation.Common;
using Project.Core.Scripts.Presentation.Shared;
using Project.Core.Scripts.UseCase.Gameplay;
using Project.Core.Scripts.Gameplay.Components;
using Project.Core.Scripts.Gameplay.Domain.Weapon.Model;
using Project.Core.Scripts.Gameplay.View.Weapon;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Project.Core.Scripts.Gameplay.Presentation.Weapon
{
    /// <summary>
    /// 武器の状態管理とビューの更新を管理するクラス
    /// </summary>
    public sealed class WeaponPresenter : MonoBehaviour
    {
        private WeaponView _weaponView;                                    // 武器のビュー
        private GameplayUseCase _gameplayUseCase;                          // ゲームプレイのビジネスロジック
        private IAudioPlayService _audioPlayService;                       // 音声再生サービス

        /// <summary>
        /// 武器のプレゼンターを初期化する
        /// </summary>
        public void Initialize(string weaponId, WeaponView weaponView, GameplayUseCase gameplayUseCase, IAudioPlayService audioPlayService)
        {
            _weaponView = weaponView;
            _gameplayUseCase = gameplayUseCase;
            _audioPlayService = audioPlayService;

            // キャンセレーショントークンの生成
            var cts = new CancellationTokenSource();

            // IDの取得
            var id = weaponId;
            // モデルの取得
            var weaponModel = _gameplayUseCase.GetWeaponModelById(id);

            // パラメータの設定
            var hittableLayerMask = 1 << 8; // レイヤーマスク

            var maxColliderCount = 50;
            var detectionRadius = 2f;
            var detectedColliders = new Collider[maxColliderCount];

            var scoreToAdd = 0;
            var specialScoreToAdd = 0;
            var flags = new List<int>(1000);

            // パラメータの初期化
            _weaponView.PS.gameObject.SetActive(false); // パーティクルを無効状態にする

            // オブジェクトの有効状態の変更を監視
            this.gameObject
                .ObserveEveryValueChanged(_ => this.gameObject.activeSelf)
                .Subscribe(_ => {
                    if (gameObject.activeSelf)
                    {
                        weaponModel.IsActive.Value = true;
                    }
                    else if (!gameObject.activeSelf)
                    {
                        weaponModel.IsActive.Value = false;
                    }
                })
                .AddTo(this);

            // 設置状態オブジェクトの有効状態の変更を監視
            _weaponView.PlacedVisual
                .ObserveEveryValueChanged(_ => _weaponView.PlacedVisual.gameObject.activeSelf)
                .Subscribe(_ => {
                    if (_weaponView.PlacedVisual.gameObject.activeSelf)
                    {
                        // 設置状態を有効にする
                        weaponModel.IsPlaced.Value = true;
                    }
                    else if (!_weaponView.PlacedVisual.gameObject.activeSelf)
                    {
                        // 設置状態を無効にする
                        weaponModel.IsPlaced.Value = false;
                    }
                })
                .AddTo(this);

            // 設置状態オブジェクトの有効状態の変更を監視
            _weaponView.PS
                .ObserveEveryValueChanged(_ => _weaponView.PS.gameObject.activeSelf)
                .Subscribe(async _ => {
                    if (_weaponView.PS.gameObject.activeSelf)
                    {
                        //　設置状態の見た目を更新する
                        _weaponView.TogglePlacedVisual(false);

                        // 効果音を再生する
                        _audioPlayService.PlayBombExploadSound();

                        // パーティクルを再生する
                        await _weaponView.PlayExplosionAnimation(cts);

                        // パーティクルを無効状態にする
                        _weaponView.PS.gameObject.SetActive(false);

                        // オブジェクトを無効状態にする
                        gameObject.SetActive(false);

                        // 【メモ】 以降、ゲームシステムに関するの処理
                        // スコアを増加
                        var totalScore = (int)_gameplayUseCase.Score + scoreToAdd;
                        _gameplayUseCase.SetScore(totalScore);
                        // スコアを保存する
                        await _gameplayUseCase.SaveCurrentScoreAsync();

                        // 特別スコアを増加
                        var totalSpecialScore = (int)_gameplayUseCase.SpecialScore + specialScoreToAdd;
                        _gameplayUseCase.SetSpecialScore(totalSpecialScore);
                        // 特別スコアを保存する
                        await _gameplayUseCase.SaveSpecialScoreAsync();

                        // フラグを保存する
                        await _gameplayUseCase.SaveRarityFlagIndex(flags);

                        // スコアの送信（降順）
                        _gameplayUseCase.SendScoreByDesc(1, _gameplayUseCase.Score);
                        _gameplayUseCase.SendScoreByDesc(2, _gameplayUseCase.SpecialScore);
                    }
                })
                .AddTo(this);

            // ビューと状態の変更を監視
            this.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    // 設置状態が有効なら
                    if (weaponModel.IsPlaced.Value)
                    {
                        var numFound = Physics.OverlapSphereNonAlloc(transform.position, detectionRadius, detectedColliders, hittableLayerMask);
                        var result = new Collider[numFound];
                        Array.Copy(detectedColliders, result, numFound);

                        var detected = result;

                        // 衝突していなければ、処理をスキップ
                        if (detected == null || detected.Length == 0 ) return;

                        // 衝突したコライダを取得し、処理を行う
                        var score = 0;
                        var specialScore = 0;
                        var values = new List<int>(detected.Length);
                        // 複数のコライダを取得
                        for (int i = 0; i < detected.Length; i++)
                        {
                            // 処理をスキップ
                            if (detected[i] == null) continue;

                            // 衝突したコライダを無効にする
                            detected[i].enabled = false;
                            // スコアの計算
                            score++;

                            // レア判定
                            if (detected[i].gameObject.TryGetComponent<AIRarityFlag>(out AIRarityFlag flag))
                            {
                                // 特別スコアの計算
                                specialScore++;
                                // 特別スコアの計算
                                values.Add(flag.Index);
                            }
                        }
                        scoreToAdd = score;
                        specialScoreToAdd = specialScore;
                        flags.Clear();
                        flags.AddRange(values);

                        // パーティクルを有効状態にする
                        _weaponView.PS.gameObject.SetActive(true);
                    }
                })
                .AddTo(this);
        }
    }
}

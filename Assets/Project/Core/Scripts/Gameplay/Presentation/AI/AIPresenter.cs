using Project.Core.Scripts.Foundation.Common;
using Project.Core.Scripts.Presentation.Shared;
using Project.Core.Scripts.UseCase.Gameplay;
using Project.Core.Scripts.Gameplay.Components;
using Project.Core.Scripts.Gameplay.Domain.AI.Model;
using Project.Core.Scripts.Gameplay.View.AI;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Project.Core.Scripts.Gameplay.Presentation.AI
{
    /// <summary>
    /// AIの状態管理とビューの更新を管理するクラス
    /// </summary>
    public sealed class AIPresenter : MonoBehaviour
    {
        private AIView _aiView;                                            // AIのビュー
        private GameplayUseCase _gameplayUseCase;                          // ゲームプレイのビジネスロジック
        private IAudioPlayService _audioPlayService;                       // 音声再生サービス

        private Planet _planet
        {
            get => UnityEngine.Object.FindFirstObjectByType<Planet>(FindObjectsInactive.Exclude);
        }

        private Vector3 _targetPosition;
        private bool _isMoving = false;

        /// <summary>
        /// AIのプレゼンターを初期化する
        /// </summary>
        public void Initialize(string aiId, AIView aiView, GameplayUseCase gameplayUseCase, IAudioPlayService audioPlayService)
        {
            _aiView = aiView;
            _gameplayUseCase = gameplayUseCase;
            _audioPlayService = audioPlayService;

            // IDの取得
            var id = aiId;
            // モデルの取得
            var aiModel = _gameplayUseCase.GetAIModelById(id);

            // コンポーネントの取得
            _aiView.GetColliderComponent();

            // パラメータの設定
            var arrivalThreshold = 0.5f; // 目的地までの距離
            var coll = _aiView.GetCollider();
            var canMove = true;

            // ランダムな目標位置を設定
            SetRandomTarget();

            // オブジェクトの有効状態の変更を監視
            this.gameObject
                .ObserveEveryValueChanged(_ => this.gameObject.activeSelf)
                .Subscribe(_ => {
                    if (gameObject.activeSelf)
                    {
                        // コライダを有効にする
                        _aiView.EnableCollider(true);

                        aiModel.IsActive.Value = true;
                    }
                    else if (!gameObject.activeSelf)
                    {
                        // コライダを無効にする
                        _aiView.EnableCollider(false);

                        aiModel.IsActive.Value = false;
                    }
                })
                .AddTo(this);

            // コライダの状態の変更を監視
            coll
                .ObserveEveryValueChanged(_ => coll.enabled)
                .Subscribe(_ =>
                {
                    if (coll.enabled)
                    {
                        canMove = true;
                    }
                    else if (!coll.enabled)
                    {
                        canMove = false;

                        // オブジェクトを無効状態にする
                        gameObject.SetActive(false);
                    }
                })
                .AddTo(this);

            // ビューと状態の変更を監視
            this.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    // 【メモ】　ObserveEveryValueChangedが1フレーム毎にしか動作しない
                    // 【メモ】　同じフレーム内でコライダの状態の変更が複数回起こった時に、コライダが無効状態のままオブジェクトが有効になってしまう挙動を防ぐ
                    // オブジェクトの有効状態に応じて
                    if (gameObject.activeSelf)
                    {
                        // コライダを有効にする
                        _aiView.EnableCollider(true);

                        aiModel.IsActive.Value = true;
                    }
                    else if (!gameObject.activeSelf)
                    {
                        // コライダを無効にする
                        _aiView.EnableCollider(false);

                        aiModel.IsActive.Value = false;
                    }

                    // 移動可能でなければ
                    if (!canMove) return;

                    // 目標との距離をチェック
                    var distanceToTarget = Vector3.Distance(transform.position, _targetPosition);
                    if (distanceToTarget <= arrivalThreshold)
                    {
                        // 移動を中止
                        _isMoving = false;
                        // ランダムな目標位置を再設定
                        SetRandomTarget();
                        return;
                    }

                    // 球体表面での移動方向を計算
                    var moveDirection = CalculateSphereMovementDirection();

                    // 移動を実行
                    var newPosition = transform.position + moveDirection * 1f/*3f*//*moveSpeed*/ * Time.deltaTime;
                    // 新しい位置を球体表面に投影
                    var projectedPosition = ProjectOntoSphere(newPosition);
                    transform.position = projectedPosition;

                    // 球体表面に対して正しい向きに設定
                    var up = (transform.position - _planet.transform.position).normalized;
                    transform.up = up;

                    // 移動方向を向く
                    if (moveDirection != Vector3.zero)
                    {
                        var targetRotation = Quaternion.LookRotation(moveDirection, up);
                        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 180f/*rotationSpeed*/ * Time.deltaTime);
                    }
                })
                .AddTo(this);
        }

        /// <summary>
        /// ランダムな目標位置を設定
        /// </summary>
        private void SetRandomTarget()
        {
            // 現在位置周辺のランダムな方向を生成
            var randomDirection = UnityEngine.Random.onUnitSphere;
            var currentDirection = (transform.position - _planet.transform.position).normalized;

            // 現在位置から一定範囲内のランダムな位置を計算
            var targetDirection = Vector3.Slerp(currentDirection, randomDirection, 8f/*徘徊範囲*//*movementRadius*/ / 15f/*sphereRadius*/).normalized;
            // 新しいターゲットの位置を計算
            var newTarget = _planet.transform.position + targetDirection * 15f/*sphereRadius*/;

            SetTarget(newTarget);
        }

        // <summary>
        /// 特定の位置を目標として設定
        /// </summary>
        private void SetTarget(Vector3 newTarget)
        {
            // 目標位置を球体表面に投影
            var directionFromCenter = (newTarget - _planet.transform.position).normalized;
            _targetPosition = _planet.transform.position + directionFromCenter * 15f/*sphereRadius*/;
            _isMoving = true;
        }

        /// <summary>
        /// 球体表面での移動方向を計算
        /// </summary>
        private Vector3 CalculateSphereMovementDirection()
        {
            // 現在位置から目標位置への球面上の最短経路を計算
            var currentPos = transform.position;
            var targetPos = _targetPosition;

            // 球体中心から見た現在位置と目標位置のベクトル
            var currentDir = (currentPos - _planet.transform.position).normalized;
            var targetDir = (targetPos - _planet.transform.position).normalized;

            // 球面上での移動方向（接線方向）
            var cross = Vector3.Cross(currentDir, targetDir);
            var tangentDirection = Vector3.Cross(cross, currentDir).normalized;

            return tangentDirection;
        }

        // <summary>
        /// 位置を球体表面に投影
        /// </summary>
        private Vector3 ProjectOntoSphere(Vector3 position)
        {
            var direction = (position - _planet.transform.position).normalized;
            return _planet.transform.position + direction * 15f/*sphereRadius*/;
        }

        // デバッグ用のギズモ表示
        private void OnDrawGizmosSelected()
        {
            // 目標位置
            if (_isMoving)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(_targetPosition, 0.15f);

                // 移動経路
                Gizmos.color = Color.yellow;
                Gizmos.DrawLine(transform.position, _targetPosition);
            }
        }
    }
}

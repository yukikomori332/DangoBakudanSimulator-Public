using System.Collections.Generic;
using Project.Core.Scripts.Foundation.Common;
using Project.Core.Scripts.Presentation.Shared;
using Project.Core.Scripts.UseCase.Gameplay;
using Project.Core.Scripts.Gameplay.Components;
using Project.Core.Scripts.Gameplay.Domain.AI.Model;
using Project.Core.Scripts.Gameplay.MasterRepository.AI;
using Project.Core.Scripts.Gameplay.View.AI;
using UniRx;
using UnityEngine;

namespace Project.Core.Scripts.Gameplay.Presentation.AI
{
    /// <summary>
    /// AIPresenterを生成するファクトリークラス
    /// </summary>
    public sealed class AIPresenterFactory
    {
        private readonly GameplayUseCase _gameplayUseCase;                          // ゲームプレイのビジネスロジック
        private readonly IAudioPlayService _audioPlayService;                       // 音声再生サービス

        public AIPresenterFactory(GameplayUseCase gameplayUseCase, IAudioPlayService audioPlayService)
        {
            _gameplayUseCase = gameplayUseCase;
            _audioPlayService = audioPlayService;
        }

        private CompositeDisposable _disposables = new CompositeDisposable(9999);

        private GameObject _parent; // プールの親オブジェクト

        private List<GameObject>[] _pool; // AIのプール

        private Planet _planet
        {
            get => UnityEngine.Object.FindFirstObjectByType<Planet>(FindObjectsInactive.Exclude);
        }

        /// <summary>
        /// AIのセットアップ
        /// </summary>
        public void Setup()
        {
            // プールの親となるオブジェクトを生成
            _parent = new GameObject("AIs Root");

            // AIのマスターテーブルを取得
            var masterTable = _gameplayUseCase.AIMasterTable;

            // AI情報のリストを取得
            var ais = _gameplayUseCase.AIModelSet.AIs;

            // プールを作成
            var count = masterTable.GetCount();
            _pool = new List<GameObject>[count];
            for (int i = 0; i < _pool.Length; i++)
            {
                _pool[i] = new List<GameObject>(ais.Count);
            }

            for (int i = 0; i < ais.Count; i++)
            {
                // IDでAIのデータを検索
                var aiMaster = masterTable.FindById(ais[i].MasterId);
                // 新しいAIの生成
                var newAI = UnityEngine.Object.Instantiate(aiMaster.Prefab, aiMaster.Prefab.transform.position, Quaternion.identity);

                // 名前を設定
                newAI.name = $"AI ({i})";
                // 親となるオブジェクトを設定
                newAI.transform.SetParent(_parent.transform);

                // AIViewを取得
                if (newAI.TryGetComponent<AIView>(out AIView aiView))
                {
                    // AIPresenterを取得
                    if (aiView.TryGetComponent<AIPresenter>(out AIPresenter aiPresenter))
                    {
                        // プレゼンターの初期化を行う
                        aiPresenter.Initialize($"{i}", aiView, _gameplayUseCase, _audioPlayService);
                    }
                }

                // プールのインデックス番号を取得
                var index = 0;

                if (i == 92)
                {
                    index = 1;
                }
                else if (i == 93)
                {
                    index = 2;
                }
                else if (i == 94)
                {
                    index = 3;
                }
                else if (i == 95)
                {
                    index = 4;
                }
                else if (i == 96)
                {
                    index = 5;
                }
                else if (i == 97)
                {
                    index = 6;
                }
                else if (i == 98)
                {
                    index = 7;
                }
                else if (i == 99)
                {
                    index = 8;
                }

                // プールに追加
                _pool[index].Add(newAI);

                // AIを無効状態にする
                newAI.SetActive(false);
            }

            // パラメーターの設定
            var timeSpan = 0.5f;  // 生成間隔
            var elapsedTime = 0f; // 経過時間

            Observable.EveryUpdate()
                .Subscribe(_ =>
                {
                    // タイマーが作動していなければ、処理をスキップ
                    if (!_gameplayUseCase.RunningTimer) return;

                    elapsedTime += Time.deltaTime;

                    if (elapsedTime > timeSpan)
                    {
                        // 経過時間をリセット
                        elapsedTime = 0;

                        // AIが最大個数に達していれば、処理をスキップ
                        if (_gameplayUseCase.HasReachedMaxAI()) return;

                        // プールのインデックス番号を取得
                        var index = 0;

                        // 【修正】 GameplayUseCaseからArchiveMasterTableのデータを参照できるようにする必要がある
                        if(_gameplayUseCase.Score >= 80 && _gameplayUseCase.SpecialScore >= 20)
                            index = UnityEngine.Random.Range(0, 8 + 1); // 0-8
                        else if(_gameplayUseCase.Score >= 70 && _gameplayUseCase.SpecialScore >= 10)
                            index = UnityEngine.Random.Range(0, 7 + 1); // 0-7
                        else if(_gameplayUseCase.Score >= 60)
                            index = UnityEngine.Random.Range(0, 6 + 1); // 0-6
                        else if(_gameplayUseCase.Score >= 50)
                            index = UnityEngine.Random.Range(0, 5 + 1); // 0-5
                        else if(_gameplayUseCase.Score >= 40)
                            index = UnityEngine.Random.Range(0, 4 + 1); // 0-4
                        else if(_gameplayUseCase.Score >= 30)
                            index = UnityEngine.Random.Range(0, 3 + 1); // 0-3
                        else if(_gameplayUseCase.Score >= 20)
                            index = UnityEngine.Random.Range(0, 2 + 1); // 0-2
                        else if(_gameplayUseCase.Score >= 10)
                            index = UnityEngine.Random.Range(0, 1 + 1); // 0-1

                        // 既存のAIの取得/新しいAIの生成
                        var pool = GetPool(index);

                        // AIを保有していれば
                        if (pool != null)
                        {
                            // ランダムな目標位置を設定
                            var randomDirection = UnityEngine.Random.onUnitSphere;
                            var targetDirection = (randomDirection - _planet.transform.position).normalized;
                            var targetPosition = _planet.transform.position + targetDirection * 15f/*sphereRadius*/;

                            //　AIの位置を更新
                            pool.transform.position = targetPosition;

                            // 球体表面に対して正しい向きに設定
                            var up = (pool.transform.position - _planet.transform.position).normalized;
                            pool.transform.up = up;

                            // プールを有効にする
                            pool.SetActive(true);
                        }
                    }
                })
                .AddTo(_disposables);
        }

        /// <summary>
        /// 球面座標（緯度・経度）をワールド座標に変換
        /// </summary>
        /// <param name="center">球体の中心座標</param>
        /// <param name="radius">球体の半径</param>
        /// <param name="lat">緯度（度）</param>
        /// <param name="lon">経度（度）</param>
        /// <returns>ワールド座標</returns>
        private Vector3 GetSpawnPoint(Vector3 center, float radius, float lat, float lon)
        {
            // 度をラジアンに変換
            var latRad = lat * Mathf.Deg2Rad;
            var lonRad = lon * Mathf.Deg2Rad;

            // 球面座標からカルテシアン座標に変換
            var x = radius * Mathf.Cos(latRad) * Mathf.Cos(lonRad);
            var y = radius * Mathf.Sin(latRad);
            var z = radius * Mathf.Cos(latRad) * Mathf.Sin(lonRad);

            // 中心位置を基準とした座標に変換
            return center + new Vector3(x, y, z);
        }

        /// <summary>
        /// 指定されたインデックスに対応するゲームオブジェクトをプールから取得
        /// </summary>
        /// <param name="index">指定されたインデックス</param>
        /// <returns>ゲームオブジェクト</returns>
        private GameObject GetPool(int index)
        {
            GameObject select = null;

            foreach (GameObject pool in _pool[index])
            {
                if (!pool.activeSelf) {
                    select = pool;
                    break;
                }
            }

            return select;
        }
    }
}

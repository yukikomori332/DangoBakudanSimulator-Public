using System.Threading;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using Project.Core.Scripts.Foundation.Common;
using Project.Core.Scripts.MasterRepository.Audio;
using Project.Core.Scripts.Presentation.Shared;
using UnityEngine;

namespace Project.Core.Scripts.Composition
{
    /// <summary>
    /// オーディオソースのプールとオーディオ再生を管理するサービスクラス
    /// </summary>
    public class AudioPlayService : IAudioPlayService
    {
        private readonly AudioMasterTable _audioMasterTable;      // オーディオのマスターテーブル
        private readonly Domain.Setting.Model.Settings _settings; // 設定情報

        private List<AudioSource>[] _pool;                        // オーディオソースのプール（各音源タイプごとにリストで管理）
        private GameObject _parent;                               //　プールの親となるオブジェクト
        private CancellationTokenSource _cts;                     // キャンセレーショントークン

        public AudioPlayService(AudioMasterTable audioMasterTable, Domain.Setting.Model.Settings settings)
        {
            _audioMasterTable = audioMasterTable;
            _settings = settings;

            Initialize();
        }

        /// <summary>
        /// オーディオソースのプールを初期化
        /// </summary>
        private void Initialize()
        {
            var count = _audioMasterTable.GetCount();
            // プールを作成
            _pool = new List<AudioSource>[count];
            for (int i = 0; i < _pool.Length; i++) {
                _pool[i] = new List<AudioSource>(count);
            }

            // 生成したプールの親となるオブジェクトを生成
            _parent = new GameObject("Audio Parent");
        }

        /// <summary>
        /// 指定されたIDに対応するオーディオソースをプールから取得
        /// </summary>
        private AudioSource GetPool(int index)
        {
            AudioSource select = null;

            // プール内の再生中でないオブジェクトを探す
            foreach (AudioSource source in _pool[index])
            {
                if (!source.isPlaying) {
                    select = source;
                    break;
                }
            }

            // 再生中でないオブジェクトが見つからない場合
            if (!select)
            {
                // IDで検索
                var audioMaster = _audioMasterTable.FindById($"audio_{index}");

                // オブジェクトの親が存在していなければ
                if (_parent == null) return select;

                // 新しいオブジェクトを生成
                select = UnityEngine.Object.Instantiate(audioMaster.Source, _parent.transform);
                // 音源を設定
                select.clip = audioMaster.Clip;
                // プールに追加
                _pool[index].Add(select);
            }
            return select;
        }

        /// <summary>
        /// ゲームシーン開始時の音楽を再生
        /// </summary>
        public void PlayGameStartSound()
        {
            // プールからオーディオを取得
            AudioSource source = GetPool(0);
            if (source == null)
            {
                Debug.LogWarning($"AudioSourceを取得できませんでした。");
                return;
            }

            if (source != null)
            {
                // BGM音量設定を適用
                source.volume = _settings.Sounds.Bgm.Volume;
                // オーディオを再生
                source.Play();
            }
        }

        /// <summary>
        /// ボタンのクリック音を再生
        /// </summary>
        public async UniTask PlayButtonClickSound(CancellationTokenSource cts)
        {
            // プールからオーディオを取得
            AudioSource source = GetPool(1);
            if (source == null)
            {
                Debug.LogWarning($"AudioSourceを取得できませんでした。");
                await UniTask.CompletedTask;
            }

            if (source != null)
            {
                // SE音量設定を適用
                source.volume = _settings.Sounds.Se.Volume;
                // オーディオを再生
                source.Play();

                // オーディオが再生完了するまで待機
                await UniTask.WaitUntil(() => !source.isPlaying || source == null, cancellationToken: cts.Token);
            }
        }

        /// <summary>
        /// 爆弾の爆発音を再生
        /// </summary>
        public void PlayBombExploadSound()
        {
            // プールからオーディオを取得
            AudioSource source = GetPool(2);
            if (source == null)
            {
                Debug.LogWarning($"AudioSourceを取得できませんでした。");
                return;
            }

            if (source != null)
            {
                // SE音量設定を適用
                source.volume = _settings.Sounds.Se.Volume;
                // オーディオを再生
                source.Play();
            }
        }

        /// <summary>
        /// プール内のオーディオを音源タイプ（BGM/SE）に応じて適切な音量を設定
        /// </summary>
        public UniTask UpdateVolumeAsync()
        {
            for (int i = 0; i < _pool.Length; i++)
            {
                foreach (AudioSource source in _pool[i])
                {
                    switch(i)
                    {
                        case 0:
                            source.volume = _settings.Sounds.Bgm.Volume;
                            break;
                        case 1:
                            source.volume = _settings.Sounds.Se.Volume;
                            break;
                        case 2:
                            source.volume = _settings.Sounds.Se.Volume;
                            break;
                    }
                }
            }
            return UniTask.CompletedTask;
        }
    }
}

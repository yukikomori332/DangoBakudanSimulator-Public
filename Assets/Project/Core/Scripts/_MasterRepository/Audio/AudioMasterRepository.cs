using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Project.Core.Scripts.MasterRepository.Audio
{
    /// <summary>
    /// オーディオのマスターデータを管理するリポジトリクラス
    /// </summary>
    public sealed class AudioMasterRepository
    {
        // キャッシュされたマスターテーブル
        private AudioMasterTable _table;

        // ロードしたアセットを格納する非同期操作ハンドル
        private AsyncOperationHandle<AudioMasterTableAsset> _handle;

        // アセットがロード済みかどうか
        private bool _isLoaded = false;

        /// <summary>
        /// キャッシュされたマスターテーブルをクリアする
        /// </summary>
        public void ClearCache()
        {
            _table = null;
        }

        /// <summary>
        /// 非同期操作ハンドルのリソースを解放する
        /// メモリリークを防ぐために、使用終了時に必ず呼び出す必要がある
        /// </summary>
        public void ReleaseHandle()
        {
            if (_isLoaded)
                Addressables.Release(_handle);
        }

        /// <summary>
        /// オーディオのマスターテーブルを非同期で取得する
        /// キャッシュがある場合はキャッシュを返し、ない場合はAddressablesからロードする
        /// </summary>
        /// <returns>オーディオのマスターテーブル</returns>
        public async UniTask<AudioMasterTable> FetchTableAsync()
        {
            // キャッシュがある場合はそれを返す
            if (_table != null)
                return _table;

            // キャンセレーショントークンの作成
            var cancellationTokenSource = new CancellationTokenSource();

            // Addressablesを使用してアセットを非同期ロード
            _handle = Addressables.LoadAssetAsync<AudioMasterTableAsset>("AudioMasterTableAsset");
            await _handle.ToUniTask(cancellationToken: cancellationTokenSource.Token);

            // ロードの成功確認
            if (_handle.Status != AsyncOperationStatus.Succeeded)
            {
                cancellationTokenSource.Cancel();
                throw new Exception($"Failed to load asset. Name: {nameof(AudioMasterTableAsset)}");
            }

            var asset = _handle.Result;
            _table = asset.MasterTable;
            // テーブルの初期化処理を実行
            _table.Initialize();
            _isLoaded = true;

            return _table;
        }
    }
}

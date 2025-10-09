using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Project.Core.Scripts.Gameplay.MasterRepository.AI
{
    /// <summary>
    /// AIのマスターデータを管理するリポジトリクラス
    /// </summary>
    public sealed class AIMasterRepository
    {
        // キャッシュされたマスターテーブル
        private AIMasterTable _itemTable;

        // ロードしたアセットを格納する非同期操作ハンドル
        private AsyncOperationHandle<AIMasterTableAsset> _handle;

        // アセットがロード済みかどうか
        private bool _isLoaded = false;

        /// <summary>
        /// キャッシュされたマスターテーブルをクリアする
        /// </summary>
        public void ClearCache()
        {
            _itemTable = null;
        }

        /// <summary>
        /// 非同期操作ハンドルのリソースを解放する
        /// </summary>
        public void ReleaseHandle()
        {
            if (_isLoaded)
                Addressables.Release(_handle);
        }

        /// <summary>
        /// AIのマスターテーブルを非同期で取得する
        /// キャッシュがある場合はキャッシュを返し、ない場合はAddressablesからロードする
        /// </summary>
        /// <returns>AIのマスターテーブル</returns>
        public async UniTask<AIMasterTable> FetchTableAsync()
        {
            // キャッシュがある場合はそれを返す
            if (_itemTable != null)
                return _itemTable;

            // キャンセレーショントークンの作成
            var cancellationTokenSource = new CancellationTokenSource();

            // Addressablesを使用してアセットを非同期ロード
            _handle = Addressables.LoadAssetAsync<AIMasterTableAsset>("AIMasterTableAsset");
            await _handle.ToUniTask(cancellationToken: cancellationTokenSource.Token);

            // ロードの成功確認
            if (_handle.Status != AsyncOperationStatus.Succeeded)
            {
                cancellationTokenSource.Cancel();
                throw new Exception($"アセットの読み込みに失敗しました。アセット名: {nameof(AIMasterTableAsset)}");
            }

            var asset = _handle.Result;
            _itemTable = asset.MasterTable;
            // テーブルの初期化処理を実行
            _itemTable.Initialize();
            _isLoaded = true;

            return _itemTable;
        }
    }
}

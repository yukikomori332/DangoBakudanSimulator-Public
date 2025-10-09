namespace Project.Core.Scripts.Domain.Shared.MasterRepository
{
    /// <summary>
    /// マスターデータを管理するリポジトリのインターフェース
    /// キャッシュの管理やデータの永続化などの基本的な操作を定義
    /// </summary>
    public interface IMasterRepository
    {
        /// <summary>
        /// リポジトリのキャッシュをクリアする
        /// メモリ使用量の最適化や、データの再読み込みが必要な場合に使用
        /// </summary>
        void ClearCache();
    }
}

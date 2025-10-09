namespace Project.Core.Scripts.Domain.Shared.MasterRepository
{
    /// <summary>
    /// マスターデータテーブルの基底インターフェース
    /// 各マスターデータテーブルはこのインターフェースを実装することで、データの初期化や管理の共通インターフェースを提供
    /// </summary>
    public interface IMasterTable
    {
        /// <summary>
        /// マスターデータテーブルを初期化する
        /// データの読み込みや初期設定を行う際に呼び出す。
        /// </summary>
        void Initialize();
    }
}

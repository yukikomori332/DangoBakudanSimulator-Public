using Cysharp.Threading.Tasks;
using Project.Core.Scripts.Domain.Shared.MasterRepository;

namespace Project.Core.Scripts.Domain.Archive.MasterRepository
{
    /// <summary>
    /// 図鑑のマスターデータを管理するリポジトリインターフェース
    /// </summary>
    /// <remarks>
    /// 図鑑の設定を取得・管理するためのインターフェース
    /// IMasterRepositoryを継承し、マスターデータの基本的な操作機能を提供
    /// </remarks>
    public interface IArchiveMasterRepository : IMasterRepository
    {
        /// <summary>
        /// 図鑑のマスターテーブルを非同期で取得する
        /// </summary>
        /// <returns>図鑑のマスターテーブル</returns>
        UniTask<IArchiveItemMasterTable> FetchTableAsync();
    }
}

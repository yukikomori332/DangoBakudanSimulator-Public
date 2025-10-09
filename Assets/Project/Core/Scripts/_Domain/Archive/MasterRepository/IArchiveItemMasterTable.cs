using Project.Core.Scripts.Domain.Shared.MasterRepository;
using Project.Core.Scripts.Domain.Archive.Model;

namespace Project.Core.Scripts.Domain.Archive.MasterRepository
{
    /// <summary>
    /// 図鑑のマスターデータを管理するテーブルインターフェース
    /// </summary>
    /// <remarks>
    /// 図鑑の状態を管理するためのマスターデータへのアクセスを提供
    /// IMasterTableを継承し、マスターデータの基本的な操作機能を継承
    /// </remarks>
    public interface IArchiveItemMasterTable : IMasterTable
    {
        /// <summary>
        /// 指定されたIDの図鑑のマスターデータを取得する
        /// </summary>
        /// <param name="id">図鑑の一意識別子</param>
        /// <returns>指定されたIDに対応する図鑑のマスターデータ。存在しない場合はnullを返す</returns>
        ArchiveItemMaster FindById(string id);

        /// <summary>
        /// テーブルのアイテム数を取得する
        /// </summary>
        /// <returns>テーブルのアイテム数</returns>
        int GetCount();
    }
}

using Project.Core.Scripts.Domain.FeatureFlag.Model;
using Project.Core.Scripts.Domain.Shared.MasterRepository;

namespace Project.Core.Scripts.Domain.FeatureFlag.MasterRepository
{
    /// <summary>
    /// 機能フラグのマスターデータを管理するテーブルインターフェース
    /// </summary>
    /// <remarks>
    /// 機能フラグの有効/無効状態を管理するためのマスターデータへのアクセスを提供
    /// IMasterTableを継承し、マスターデータの基本的な操作機能を継承
    /// </remarks>
    public interface IFeatureFlagMasterTable : IMasterTable
    {
        /// <summary>
        /// 指定されたIDの機能フラグのマスターデータを取得する
        /// </summary>
        /// <param name="id">機能フラグの一意識別子</param>
        /// <returns>指定されたIDに対応する機能フラグのマスターデータ。存在しない場合はnullを返す</returns>
        FeatureFlagMaster FindById(string id);
    }
}

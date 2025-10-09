using Cysharp.Threading.Tasks;
using Project.Core.Scripts.Domain.Shared.MasterRepository;

namespace Project.Core.Scripts.Domain.FeatureFlag.MasterRepository
{
    /// <summary>
    /// 機能フラグのマスターデータを管理するリポジトリインターフェース
    /// </summary>
    /// <remarks>
    /// 機能フラグの設定を取得・管理するためのインターフェース
    /// IMasterRepositoryを継承し、マスターデータの基本的な操作機能を提供
    /// </remarks>
    public interface IFeatureFlagMasterRepository : IMasterRepository
    {
        /// <summary>
        /// 機能フラグのマスターテーブルを非同期で取得する
        /// </summary>
        /// <returns>機能フラグのマスターテーブル</returns>
        UniTask<IFeatureFlagMasterTable> FetchTableAsync();
    }
}

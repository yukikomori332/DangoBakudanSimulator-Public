using Cysharp.Threading.Tasks;
using Project.Core.Scripts.Domain.Shared.MasterRepository;

namespace Project.Core.Scripts.Domain.Dialogue.MasterRepository
{
    /// <summary>
    /// ダイアログのマスターデータを管理するリポジトリインターフェース
    /// </summary>
    /// <remarks>
    /// ダイアログの設定を取得・管理するためのインターフェース
    /// IMasterRepositoryを継承し、マスターデータの基本的な操作機能を提供
    /// </remarks>
    public interface IDialogueMasterRepository : IMasterRepository
    {
        /// <summary>
        /// ダイアログのマスターテーブルを非同期で取得する
        /// </summary>
        /// <returns>ダイアログのマスターテーブル</returns>
        UniTask<IDialogueMasterTable> FetchTableAsync();
    }
}

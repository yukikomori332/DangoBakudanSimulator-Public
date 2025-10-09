using Project.Core.Scripts.Domain.Shared.MasterRepository;
using Project.Core.Scripts.Domain.Dialogue.Model;

namespace Project.Core.Scripts.Domain.Dialogue.MasterRepository
{
    /// <summary>
    /// ダイアログのマスターデータを管理するテーブルインターフェース
    /// </summary>
    /// <remarks>
    /// ダイアログの状態を管理するためのマスターデータへのアクセスを提供
    /// IMasterTableを継承し、マスターデータの基本的な操作機能を継承
    /// </remarks>
    public interface IDialogueMasterTable : IMasterTable
    {
        /// <summary>
        /// 指定されたIDのダイアログのマスターデータを取得する
        /// </summary>
        /// <param name="id">ダイアログの一意識別子</param>
        /// <returns>指定されたIDに対応するダイアログのマスターデータ。存在しない場合はnullを返す</returns>
        DialogueMaster FindById(string id);

        /// <summary>
        /// テーブルのアイテム数を取得する
        /// </summary>
        /// <returns>テーブルのアイテム数</returns>
        int GetCount();
    }
}

using System;
using UniRx;

namespace Project.Core.Scripts.Domain.Archive.Model
{
    /// <summary>
    /// 図鑑アイテムを管理するクラス
    /// IDとマスターIDを保持する機能を提供
    /// </summary>
    public sealed class ArchiveItem
    {
        public ArchiveItem(string id, string masterId)
        {
            Id = id;
            MasterId = masterId;
        }

        // 図鑑アイテムの一意識別子
        public string Id { get; }

        // 図鑑アイテムの一意識別子
        public string MasterId { get; }
    }
}

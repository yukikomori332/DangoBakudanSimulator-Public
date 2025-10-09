using System;
using System.Collections.Generic;
using UniRx;

namespace Project.Core.Scripts.Domain.Archive.Model
{
    /// <summary>
    /// 図鑑アイテムを管理するクラス
    /// キャラクターなどの各アイテムを保持し、一括で管理
    /// </summary>
    public sealed class ArchiveItemSet
    {
        // アイテムの有効状態の変更を通知するSubject
        private readonly Subject<ItemsChangedEvent> _itemsChangedSubject = new Subject<ItemsChangedEvent>();

        private readonly List<ArchiveItem> _units = new List<ArchiveItem>();     // キャラクター

        public IReadOnlyList<ArchiveItem> Units => _units;

        // アイテムの有効状態の変更を監視するためのObservable
        public IObservable<ItemsChangedEvent> ItemsChanged => _itemsChangedSubject;

        internal void SetItems(IReadOnlyList<ArchiveItem> units)
        {
            _units.Clear();
            _units.AddRange(units);

            _itemsChangedSubject.OnNext(new ItemsChangedEvent(units));
        }

        /// <summary>
        /// アイテムの有効状態の変更イベントを表す構造体
        /// </summary>
        public readonly struct ItemsChangedEvent
        {
            /// <summary>
            /// 新しいアイテムの有効状態でイベントを初期化
            /// </summary>
            /// <param name="units"></param>
            public ItemsChangedEvent(IReadOnlyList<ArchiveItem> units)
            {
                Units = units;
            }

            /// <summary>
            /// 変更後のキャラクター
            /// </summary>
            public IReadOnlyList<ArchiveItem> Units { get; }
        }
    }
}

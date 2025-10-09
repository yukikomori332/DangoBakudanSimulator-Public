using System;
using Project.Subsystem.PresentationFramework;
using UniRx;
using UnityEngine;

namespace Project.Core.Scripts.View.Archive
{
    public sealed class ArchiveItemViewState : AppViewState, IArchiveItemState
    {
        //　アイテム名の状態を管理するReactiveProperty
        private readonly ReactiveProperty<string> _itemName = new ReactiveProperty<string>();
        // 解放条件の状態を管理するReactiveProperty
        private readonly ReactiveProperty<int> _cost = new ReactiveProperty<int>();
        // 特別な解放条件の状態を管理するReactiveProperty
        private readonly ReactiveProperty<int> _specialCost = new ReactiveProperty<int>();
        // ボタンのロック状態を管理するReactiveProperty
        private readonly ReactiveProperty<bool> _isLocked = new ReactiveProperty<bool>();
        // サムネイル画像の状態を管理するReactiveProperty
        private readonly ReactiveProperty<Sprite> _thumbnail = new ReactiveProperty<Sprite>();

        // アイテム名の状態を外部から監視・制御するためのプロパティ
        public IReactiveProperty<string> ItemName => _itemName;

        // 解放条件の状態を外部から監視・制御するためのプロパティ
        public IReactiveProperty<int> Cost => _cost;

        // 特別な解放条件の状態を外部から監視・制御するためのプロパティ
        public IReactiveProperty<int> SpecialCost => _specialCost;

        // ボタンのロック状態を外部から監視・制御するためのプロパティ
        public IReactiveProperty<bool> IsLocked => _isLocked;

        // サムネイル画像の状態を外部から監視・制御するためのプロパティ
        public IReactiveProperty<Sprite> Thumbnail => _thumbnail;

        /// <summary>
        /// リソースの解放を行う
        /// </summary>
        protected override void DisposeInternal()
        {
            _itemName.Dispose();
            _cost.Dispose();
            _specialCost.Dispose();
            _isLocked.Dispose();
            _thumbnail.Dispose();
        }
    }

    /// <summary>
    /// 図鑑アイテムのビューの状態を制御するためのインターフェース
    /// </summary>
    internal interface IArchiveItemState
    {
    }
}

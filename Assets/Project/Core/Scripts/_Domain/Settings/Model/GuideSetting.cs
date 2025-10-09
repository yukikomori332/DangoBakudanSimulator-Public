using System;
using UniRx;

namespace Project.Core.Scripts.Domain.Setting.Model
{
    /// <summary>
    /// ゲーム内のガイド機能の設定を管理するクラス
    /// ガイドの有効/無効状態を保持し、状態変更を通知する機能を提供
    /// </summary>
    public sealed class GuideSettings
    {
        // ガイド設定の変更を通知するSubject
        private readonly Subject<ValueChangedEvent> _valueChangedSubject = new Subject<ValueChangedEvent>();

        // ガイド機能の有効/無効状態 （デフォルトは有効（true））
        public bool GuideEnabled { get; private set; } = true;

        // ガイド設定の変更を監視するためのObservable
        public IObservable<ValueChangedEvent> ValueChanged => _valueChangedSubject;

        /// <summary>
        /// リソースの解放を行う
        /// </summary>
        public void Dispose()
        {
            _valueChangedSubject.Dispose();
        }

        /// <summary>
        /// ガイド設定の値を更新し、変更を通知する
        /// </summary>
        /// <param name="enabled">新しいガイド設定の状態</param>
        internal void SetValue(bool enabled)
        {
            GuideEnabled = enabled;
            _valueChangedSubject.OnNext(new ValueChangedEvent(enabled));
        }

        /// <summary>
        /// ガイド設定の変更イベントを表す構造体
        /// </summary>
        public readonly struct ValueChangedEvent
        {
            /// <summary>
            /// 新しいガイド設定の状態でイベントを初期化
            /// </summary>
            /// <param name="enabled">変更後のガイド設定の状態</param>
            public ValueChangedEvent(bool enabled)
            {
                GuideEnabled = enabled;
            }

            /// <summary>
            /// 変更後のガイド設定の状態
            /// </summary>
            public bool GuideEnabled { get; }
        }
    }
}

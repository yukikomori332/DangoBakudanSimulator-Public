using System;
using UniRx;

namespace Project.Core.Scripts.Domain.Setting.Model
{
    /// <summary>
    /// サウンド設定を管理するクラス
    /// 音量とミュート状態を保持し、設定変更を通知する機能を提供
    /// </summary>
    public sealed class SoundSettings
    {
        // サウンド設定の変更を通知するSubject
        private readonly Subject<ValueChangedEvent> _valueChangedSubject = new Subject<ValueChangedEvent>();

        // 現在の音量値（0.0f ～ 1.0f）
        public float Volume { get; private set; }

        // ミュート状態
        public bool Muted { get; private set; }

        // サウンド設定の変更を監視するためのObservable
        public IObservable<ValueChangedEvent> ValueChanged => _valueChangedSubject;

        /// <summary>
        /// リソースの解放を行う
        /// </summary>
        public void Dispose()
        {
            _valueChangedSubject.Dispose();
        }

        /// <summary>
        /// 音量とミュート状態を設定し、変更を通知する
        /// </summary>
        /// <param name="volume">設定する音量値</param>
        /// <param name="muted">設定するミュート状態</param>
        internal void SetValues(float volume, bool muted)
        {
            Volume = volume;
            Muted = muted;
            _valueChangedSubject.OnNext(new ValueChangedEvent(volume, muted));
        }

        /// <summary>
        /// サウンド設定の変更イベントを表す構造体
        /// </summary>
        public readonly struct ValueChangedEvent
        {
            /// <summary>
            /// 新しいサウンド設定でイベントを初期化
            /// </summary>
            /// <param name="volume">変更後の音量値</param>
            /// <param name="muted">変更後のミュート状態</param>
            public ValueChangedEvent(float volume, bool muted)
            {
                Volume = volume;
                Muted = muted;
            }

            /// <summary>
            /// 変更後の音量値
            /// </summary>
            public float Volume { get; }

            /// <summary>
            /// 変更後のミュート状態
            /// </summary>
            public bool Muted { get; }
        }
    }
}

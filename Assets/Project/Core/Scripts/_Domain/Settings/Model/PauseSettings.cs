using System;
using UniRx;

namespace Project.Core.Scripts.Domain.Setting.Model
{
    /// <summary>
    /// ゲームの一時停止状態を管理するクラス
    /// 一時停止状態の変更を監視可能なイベントとして提供
    /// </summary>
    public sealed class PauseSettings
    {
        // 一時停止状態の変更を通知するSubject
        private readonly Subject<ValueChangedEvent> _valueChangedSubject = new Subject<ValueChangedEvent>();

        // 一時停止状態の変更を監視するためのObservable
        public IObservable<ValueChangedEvent> ValueChanged => _valueChangedSubject;

        // 現在の一時停止状態 （true: 一時停止中、false: 動作中）
        public bool Paused { get; private set; } = false;

        /// <summary>
        /// リソースの解放を行う
        /// </summary>
        public void Dispose()
        {
            _valueChangedSubject.Dispose();
        }

        /// <summary>
        /// 一時停止状態を更新する
        /// </summary>
        /// <param name="paused">新しい一時停止状態</param>
        internal void SetValue(bool paused)
        {
            Paused = paused;
            _valueChangedSubject.OnNext(new ValueChangedEvent(paused));
        }

        /// <summary>
        /// 一時停止状態の変更イベントを表す構造体
        /// </summary>
        public readonly struct ValueChangedEvent
        {
            /// <summary>
            /// 新しい一時停止状態でイベントを初期化
            /// </summary>
            /// <param name="paused">更新後の一時停止状態</param>
            public ValueChangedEvent(bool paused)
            {
                Paused = paused;
            }

            /// <summary>
            /// 更新後の一時停止状態
            /// </summary>
            public bool Paused { get; }
        }
    }
}

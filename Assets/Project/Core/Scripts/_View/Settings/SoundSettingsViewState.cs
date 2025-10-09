using Project.Subsystem.PresentationFramework;
using UniRx;

namespace Project.Core.Scripts.View.Setting
{
    /// <summary>
    /// サウンド設定画面の状態を管理するクラス
    /// BGM、SE、ボイスの音量と有効/無効状態を保持する
    /// </summary>
    public sealed class SoundSettingsViewState : AppViewState, ISoundSettingsState
    {
        // 各サウンド設定の状態を保持するReactiveProperty
        private readonly ReactiveProperty<float> _bgmVolume = new ReactiveProperty<float>();      // BGMの音量 (0.0f ~ 1.0f)
        private readonly ReactiveProperty<float> _seVolume = new ReactiveProperty<float>();       // 効果音の音量 (0.0f ~ 1.0f)
        private readonly ReactiveProperty<bool> _isBgmEnabled = new ReactiveProperty<bool>();     // BGMの有効/無効状態
        private readonly ReactiveProperty<bool> _isSeEnabled = new ReactiveProperty<bool>();      // 効果音の有効/無効状態

        // 外部からアクセス可能なプロパティ
        public IReactiveProperty<float> BgmVolume => _bgmVolume;        // BGM音量の取得/設定用
        public IReactiveProperty<float> SeVolume => _seVolume;          // 効果音音量の取得/設定用
        public IReactiveProperty<bool> IsBgmEnabled => _isBgmEnabled;      // BGM有効/無効状態の取得/設定用
        public IReactiveProperty<bool> IsSeEnabled => _isSeEnabled;        // 効果音有効/無効状態の取得/設定用

        /// <summary>
        /// リソースの解放を行う
        /// </summary>
        protected override void DisposeInternal()
        {
            _bgmVolume.Dispose();
            _seVolume.Dispose();
            _isBgmEnabled.Dispose();
            _isSeEnabled.Dispose();
        }
    }

    /// <summary>
    /// サウンド設定の状態を制御するためのインターフェース
    /// 現在は空だが、将来的な拡張性のために用意
    /// </summary>
    internal interface ISoundSettingsState
    {
    }
}

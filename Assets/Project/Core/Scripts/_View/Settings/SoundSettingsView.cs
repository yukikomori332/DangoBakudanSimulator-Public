using Cysharp.Threading.Tasks;
using Project.Core.Scripts.View.Foundation.Binders;
using Project.Subsystem.PresentationFramework;
using UniRx;
using UnityEngine.UI;
using TMPro;

namespace Project.Core.Scripts.View.Setting
{
    /// <summary>
    /// サウンド設定画面のビュー
    /// 音量調整スライダーとミュート切り替えトグルを管理
    /// </summary>
    public sealed class SoundSettingsView : AppView<SoundSettingsViewState>
    {
        // UIコンポーネントの参照
        // 音量調整用スライダー
        public Slider bgmSlider;    // BGM音量
        public Slider seSlider;     // 効果音音量

        // ミュート切り替え用トグル
        public Toggle bgmToggle;    // BGMミュート
        public Toggle seToggle;     // 効果音ミュート

        // 音量表示用テキスト
        public TextMeshProUGUI bgmVolumeText;    // BGM音量表示
        public TextMeshProUGUI seVolumeText;     // 効果音音量表示

        /// <summary>
        /// ビューの初期化処理
        /// ViewStateの値とUIコンポーネントの双方向バインディングを設定
        /// </summary>
        protected override UniTask Initialize(SoundSettingsViewState viewState)
        {
            // ViewStateからUIへの一方向バインディング
            // 音量値の反映
            viewState.BgmVolume.Subscribe(x => bgmSlider.value = x).AddTo(this);
            viewState.SeVolume.Subscribe(x => seSlider.value = x).AddTo(this);

            // ミュート状態の反映
            viewState.IsBgmEnabled.Subscribe(x => bgmToggle.isOn = x).AddTo(this);
            viewState.IsSeEnabled.Subscribe(x => seToggle.isOn = x).AddTo(this);

            // ミュート状態に応じたスライダーの有効/無効切り替え
            viewState.IsBgmEnabled.Subscribe(x => bgmSlider.interactable = x).AddTo(this);
            viewState.IsSeEnabled.Subscribe(x => seSlider.interactable = x).AddTo(this);

            // UIからViewStateへの一方向バインディング
            // スライダー値の変更をViewStateに反映
            bgmSlider.SetOnValueChangedDestination(x => viewState.BgmVolume.Value = x).AddTo(this);
            seSlider.SetOnValueChangedDestination(x => viewState.SeVolume.Value = x).AddTo(this);

            // トグル状態の変更をViewStateに反映
            bgmToggle.SetOnValueChangedDestination(x => viewState.IsBgmEnabled.Value = x).AddTo(this);
            seToggle.SetOnValueChangedDestination(x => viewState.IsSeEnabled.Value = x).AddTo(this);

            // 音量テキストの更新（パーセンテージ表示）
            seVolumeText.SetPercentageTextSource(viewState.SeVolume).AddTo(this);
            bgmVolumeText.SetPercentageTextSource(viewState.BgmVolume).AddTo(this);

            return UniTask.CompletedTask;
        }
    }
}

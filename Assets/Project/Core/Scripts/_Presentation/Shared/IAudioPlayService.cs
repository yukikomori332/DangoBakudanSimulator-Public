using System.Threading;
using Cysharp.Threading.Tasks;

namespace Project.Core.Scripts.Presentation.Shared
{
    /// <summary>
    /// ゲーム内のオーディオ再生を管理するサービスのインターフェース
    /// </summary>
    public interface IAudioPlayService
    {
        /// <summary>
        /// ゲームシーン開始時の音楽を再生する
        /// </summary>
        void PlayGameStartSound();

        /// <summary>
        /// ボタンクリック時の効果音を再生する
        /// </summary>
        /// <param name="tokenSource">キャンセレーショントークンソース</param>
        /// <returns>非同期操作を表すUniTask</returns>
        UniTask PlayButtonClickSound(CancellationTokenSource tokenSource);

        /// <summary>
        /// 爆弾の爆発音を再生
        /// </summary>
        void PlayBombExploadSound();

        /// <summary>
        /// 音量設定を非同期で更新する
        /// </summary>
        /// <returns>非同期操作を表すUniTask</returns>
        UniTask UpdateVolumeAsync();
    }
}

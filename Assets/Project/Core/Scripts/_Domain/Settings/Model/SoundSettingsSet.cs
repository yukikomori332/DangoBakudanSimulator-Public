namespace Project.Core.Scripts.Domain.Setting.Model
{
    /// <summary>
    /// ゲーム内の音声設定を管理するクラス
    /// ボイス、BGM、効果音の各設定を保持し、一括で管理
    /// </summary>
    public sealed class SoundSettingsSet
    {
        // バックグラウンドミュージックの設定を管理するプロパティ
        public SoundSettings Bgm { get; } = new SoundSettings();

        // 効果音の設定を管理するプロパティ
        public SoundSettings Se { get; } = new SoundSettings();

        /// <summary>
        /// 各音声設定のリソースの解放を行う
        /// </summary>
        public void Dispose()
        {
            Bgm.Dispose();
            Se.Dispose();
        }
    }
}

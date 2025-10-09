namespace Project.Core.Scripts.Presentation.Shared
{
    /// <summary>
    /// アプリケーション内の画面遷移を管理するサービスのインターフェース
    /// </summary>
    public interface ITransitionService
    {
        /// <summary>
        /// ゲームプレイシーン開始時の画面遷移を処理する
        /// </summary>
        void GameplaySceneStarted();

        /// <summary>
        /// ダイアログ画面への画面遷移を処理する
        /// </summary>
        void DialogueStarted();

        /// <summary>
        /// ゲームプレイ画面の設定ボタンがクリックされた時の画面遷移を処理する
        /// </summary>
        void GameplayPageSettingsButtonClicked();

        /// <summary>
        /// ゲームプレイ画面のクレジットボタンがクリックされた時の画面遷移を処理する
        /// </summary>
        void GameplayPageCreditButtonClicked();

        /// <summary>
        /// ゲームプレイ画面の図鑑ボタンがクリックされた時の画面遷移を処理する
        /// </summary>
        void GameplayPageArchiveButtonClicked();

        /// <summary>
        /// 図鑑画面の閉じるボタンがクリックされた時の画面遷移を処理する
        /// </summary>
        void ArchivePageCloseButtonClicked();

        /// <summary>
        /// 前の画面に戻る（ポップ）コマンドが実行された時の画面遷移を処理する
        /// </summary>
        void PopCommandExecuted();
    }
}

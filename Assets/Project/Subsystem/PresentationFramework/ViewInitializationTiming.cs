namespace Project.Subsystem.PresentationFramework
{
    /// <summary>
    /// ビューの初期化タイミングを定義する列挙型
    /// ビューのライフサイクルにおける初期化処理の実行タイミングを制御する
    /// </summary>
    public enum ViewInitializationTiming
    {
        /// <summary>
        /// ビューのインスタンス生成時に初期化を実行
        /// ビューが生成された直後に初期化処理が必要な場合に使用する
        /// </summary>
        Initialize,

        /// <summary>
        /// ビューが最初に表示される直前（BeforeFirstEnter）に初期化を実行
        /// 表示直前まで初期化を遅延させたい場合に使用する
        /// </summary>
        BeforeFirstEnter
    }
}

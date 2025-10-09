namespace Project.Subsystem.GUIComponents.TabGroup
{
    /// <summary>
    /// タブの遷移を管理するサービスのインターフェース
    /// </summary>
    public interface ITabContent
    {
        /// <summary>
        /// タブの番号を設定する
        /// </summary>
        /// <param name="index"></param>
        void SetTabIndex(int index);
    }
}

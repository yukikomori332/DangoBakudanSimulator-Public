using UnityScreenNavigator.Runtime.Core.Sheet;

namespace Project.Subsystem.PresentationFramework.UnityScreenNavigatorExtensions
{
    /// <summary>
    /// シート画面のプレゼンターを定義するインターフェース
    /// </summary>
    /// <remarks>
    /// このインターフェースは以下の機能を提供する：
    /// - IPresenter: プレゼンテーション層の基本機能
    /// - ISheetLifecycleEvent: シートのライフサイクルイベント（表示、非表示、アニメーション等）の処理
    /// </remarks>
    public interface ISheetPresenter : IPresenter, ISheetLifecycleEvent
    {
    }
}

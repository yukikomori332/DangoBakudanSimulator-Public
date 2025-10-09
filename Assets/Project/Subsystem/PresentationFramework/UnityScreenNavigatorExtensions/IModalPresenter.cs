using UnityScreenNavigator.Runtime.Core.Modal;

namespace Project.Subsystem.PresentationFramework.UnityScreenNavigatorExtensions
{
    /// <summary>
    /// モーダル画面のプレゼンターを定義するインターフェース
    /// </summary>
    /// <remarks>
    /// このインターフェースは以下の機能を提供する：
    /// - IPresenter: プレゼンテーション層の基本機能
    /// - IPageLifecycleEvent: ページのライフサイクルイベント（表示、非表示、アニメーション等）の処理
    /// </remarks>
    public interface IModalPresenter : IPresenter, IModalLifecycleEvent
    {
    }
}

using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Project.Core.Scripts.View.Foundation.Binders;
using Project.Subsystem.PresentationFramework;
using UniRx;
using UnityEngine.UI;

namespace Project.Core.Scripts.View.Credit
{
    /// <summary>
    /// クレジット画面を管理するビュークラス
    /// </summary>
    public sealed class CreditView : AppView<CreditViewState>
    {
        public CreditButtonView closeButton; // クレジット画面を閉じるボタン

        /// <summary>
        /// クレジット画面の初期化処理
        /// </summary>
        /// <param name="viewState">クレジット画面の状態を管理するステート</param>
        protected override async UniTask Initialize(CreditViewState viewState)
        {
            var internalState = (ICreditState)viewState;

            // 初期化が必要なコンポーネントのタスクリストを作成
            var tasks = new List<UniTask>
            {
                closeButton.InitializeAsync(viewState.CloseButton)
            };
            await UniTask.WhenAll(tasks);

            await UniTask.CompletedTask;
        }
    }
}

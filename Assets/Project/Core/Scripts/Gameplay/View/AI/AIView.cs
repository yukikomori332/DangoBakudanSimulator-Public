using System.Threading;
using Cysharp.Threading.Tasks;
// using LitMotion;
// using LitMotion.Extensions;
using UnityEngine;

namespace Project.Core.Scripts.Gameplay.View.AI
{
    /// <summary>
    /// 物理演算、位置情報、アニメーション、エフェクトを管理するクラス
    /// </summary>
    public sealed class AIView : MonoBehaviour
    {
        private Collider _collider;

        /// <summary>
        /// コライダーコンポーネントの取得
        /// </summary>
        public void GetColliderComponent()
        {
            _collider = GetComponent<Collider>();
        }

        /// <summary>
        /// コライダーの取得
        /// </summary>
        public Collider GetCollider()
        {
            return _collider;
        }

        /// <summary>
        /// コライダーの有効/無効を設定(true: 有効, false: 無効)
        /// </summary>
        /// <param name="state">コライダーの状態</param>
        public void EnableCollider(bool state)
        {
            if (_collider == null)
            {
                Debug.Log($"コライダが見つかりません。");
                return;
            }

            _collider.enabled = state;
        }
    }
}

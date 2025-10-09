using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Project.Core.Scripts.Gameplay.View.Weapon
{
    /// <summary>
    /// 物理演算、位置情報、カメラ制御、アニメーション、エフェクトを管理するクラス
    /// </summary>
    public sealed class WeaponView : MonoBehaviour
    {
        [Header("Visual Settings")]
        [SerializeField] private GameObject placedVisual;
        [SerializeField] private GameObject previewVisual;
        [SerializeField] private Material previewMaterial;
        [SerializeField] private Color validColor;
        [SerializeField] private Color invalidColor;

        [Header("Particle Settings")]
        [SerializeField] private ParticleSystem ps;

        public GameObject PlacedVisual { get => placedVisual; set => placedVisual = value; }
        public ParticleSystem PS { get => ps; set => ps = value; }

        /// <summary>
        /// 設置オブジェクトのビジュアルを更新
        /// </summary>
        /// <param name="state">ビジュアルの状態</param>
        public void TogglePlacedVisual(bool state)
        {
            if (placedVisual == null)
            {
                Debug.Log($"オブジェクトが見つかりません。");
                return;
            }

            placedVisual.SetActive(state);
        }

        /// <summary>
        /// プレビューオブジェクトのビジュアルを更新
        /// </summary>
        /// <param name="state">ビジュアルの状態</param>
        public void TogglePreviewVisual(bool state)
        {
            if (previewVisual == null)
            {
                Debug.Log($"オブジェクトが見つかりません。");
                return;
            }

            previewVisual.SetActive(state);
        }

        /// <summary>
        /// プレビューオブジェクトのマテリアルカラーを更新
        /// </summary>
        /// <param name="state">ビジュアルの状態</param>
        public void TogglePreviewMaterialColor(bool state)
        {
            if (previewMaterial == null)
            {
                Debug.Log($"マテリアルが見つかりません。");
                return;
            }

            if (state)
                previewMaterial.color = validColor;
            else if (!state)
                previewMaterial.color = invalidColor;
        }

        /// <summary>
        /// 待機アニメーションを再生・停止
        /// </summary>
        public async UniTask PlayExplosionAnimation(CancellationTokenSource cts)
        {
            if (ps == null)
            {
                Debug.Log($"パーティクルが見つかりません。");
                return;
            }

            // パーティクルを再生する
            ps.Play();

            // パーティクルの再生完了するまで待機
            await UniTask.WaitUntil(() => !ps.isPlaying || ps == null, cancellationToken: cts.Token);
        }
    }
}

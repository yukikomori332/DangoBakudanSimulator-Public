using DG.Tweening;
// using LitMotion;
// using LitMotion.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Core.Scripts.View.Overlay
{
    /// <summary>
    /// ローディング中にアニメーションを表示するUIコンポーネント
    /// </summary>
    public sealed class LoadingAnimationView : MonoBehaviour
    {
        [SerializeField] private Image[] circles;
        [SerializeField] private Transform parent;
        [SerializeField] private Image[] images;

        private void Start()
        {
            // 画像が登録されていなければ処理をスキップ
            if (circles == null || circles.Length < 0)
                return;

            // 画像の親が登録されていなければ処理をスキップ
            if (parent == null)
                return;

            // 画像が登録されていなければ処理をスキップ
            if (images == null || images.Length < 0)
                return;

            var duration = 1f;

            // 順番に跳ねる
            for (var i = 0; i < circles.Length; i++)
            {
                // circles[i].rectTransform.anchoredPosition = new Vector2((i - circles.Length / 2) * 50f, 0);
                Sequence sequence = DOTween.Sequence()
                    .SetLoops(-1, LoopType.Restart)
                    .SetDelay((duration / 2) * ((float)i / circles.Length))
                    .Append(circles[i].rectTransform.DOAnchorPosY(7.5f/*15f*//*30f*/, duration / 4))
                    .Append(circles[i].rectTransform.DOAnchorPosY(0f, duration / 4))
                    .AppendInterval((duration / 2) * ((float)(1 - i) / circles.Length))
                    .SetLink(gameObject);                                                // ゲームオブジェクトが破棄された際にモーションをキャンセルする

                sequence.Play();
            }

            // 順番に拡大縮小
            // for (var i = 0; i < circles.Length; i++)
            // {
            //     // circles[i].rectTransform.anchoredPosition = new Vector2((i - circles.Length / 2) * 50f, 0);
            //     Sequence sequence = DOTween.Sequence()
            //         .SetLoops(-1, LoopType.Restart)
            //         .SetDelay((duration / 2) * ((float)i / circles.Length))
            //         .Append(circles[i].rectTransform.DOScale(1.5f, duration / 4))
            //         .Append(circles[i].rectTransform.DOScale(1f, duration / 4))
            //         .AppendInterval((duration / 2) * ((float)(1 - i) / circles.Length))
            //         .SetLink(gameObject);                                               // ゲームオブジェクトが破棄された際にモーションをキャンセルする

            //     sequence.Play();
            // }

            // 順番にフェード
            // for (int i = 0; i < circles.Length; i++)
            // {
            //     Sequence sequence = DOTween.Sequence()
            //         .SetLoops(-1, LoopType.Restart)
            //         .SetDelay((duration / 2) * ((float)i / circles.Length))
            //         .Append(circles[i].DOFade(0f, duration / 4))
            //         .Append(circles[i].DOFade(1f, duration / 4))
            //         .AppendInterval((duration / 2) * ((float)(1 - i) / circles.Length))
            //         .SetLink(gameObject);                                              // ゲームオブジェクトが破棄された際にモーションをキャンセルする

            //     sequence.Play();
            // }

            // 回転させる
            // for (var i = 0; i < images.Length; i++)
            // {
            //     // var angle = 2 * Mathf.PI * i / images.Length;
            //     // images[i].rectTransform.anchoredPosition = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * 50f;
            //     Sequence sequence = DOTween.Sequence()
            //         .SetLoops(-1, LoopType.Yoyo)
            //         .AppendInterval(duration / 4)
            //         // .Append(images[i].DOFade(0f, duration / 2))
            //         // .AppendInterval(duration / 4)
            //         .SetLink(gameObject);                       // ゲームオブジェクトが破棄された際にモーションをキャンセルする

            //     sequence.Play();
            // }

            // Sequence sequenceParent = DOTween.Sequence()
            //     .SetLoops(-1, LoopType.Incremental)
            //     .Append(parent.DOLocalRotate(Vector3.forward * (360f / images.Length), duration / 4))
            //     .AppendInterval(duration / 2)
            //     .Append(parent.DOLocalRotate(Vector3.forward * (360f / images.Length), duration / 4))
            //     .SetLink(gameObject);                                                                 // ゲームオブジェクトが破棄された際にモーションをキャンセルする

            // sequenceParent.Play();

            // 回転させる
            var initialPositions = new Vector2[images.Length];
            for (var i = 0; i < images.Length; i++)
            {
                initialPositions[i] = images[i].rectTransform.anchoredPosition;
            }

            // for (var i = 0; i < images.Length; i++)
            // {

            // }

            Sequence sequence1 = DOTween.Sequence()
                    .SetLoops(-1, LoopType.Restart)
                    .AppendInterval(duration / 4)
                    .Append(images[0].rectTransform.DOAnchorPos(initialPositions[1], duration / 2))
                    .AppendInterval(duration / 4)
                    .Append(images[0].rectTransform.DOAnchorPos(initialPositions[2], duration / 2))
                    .AppendInterval(duration / 4)
                    .Append(images[0].rectTransform.DOAnchorPos(initialPositions[0], duration / 2))
                    .SetLink(gameObject);                                                           // ゲームオブジェクトが破棄された際にモーションをキャンセルする

            sequence1.Play();

            Sequence sequence2 = DOTween.Sequence()
                    .SetLoops(-1, LoopType.Restart)
                    .AppendInterval(duration / 4)
                    .Append(images[1].rectTransform.DOAnchorPos(initialPositions[2], duration / 2))
                    .AppendInterval(duration / 4)
                    .Append(images[1].rectTransform.DOAnchorPos(initialPositions[0], duration / 2))
                    .AppendInterval(duration / 4)
                    .Append(images[1].rectTransform.DOAnchorPos(initialPositions[1], duration / 2))
                    .SetLink(gameObject);                                                           // ゲームオブジェクトが破棄された際にモーションをキャンセルする

            sequence2.Play();

            Sequence sequence3 = DOTween.Sequence()
                    .SetLoops(-1, LoopType.Restart)
                    .AppendInterval(duration / 4)
                    .Append(images[2].rectTransform.DOAnchorPos(initialPositions[0], duration / 2))
                    .AppendInterval(duration / 4)
                    .Append(images[2].rectTransform.DOAnchorPos(initialPositions[1], duration / 2))
                    .AppendInterval(duration / 4)
                    .Append(images[2].rectTransform.DOAnchorPos(initialPositions[2], duration / 2))
                    .SetLink(gameObject);                                                           // ゲームオブジェクトが破棄された際にモーションをキャンセルする

            sequence3.Play();
        }
    }
}

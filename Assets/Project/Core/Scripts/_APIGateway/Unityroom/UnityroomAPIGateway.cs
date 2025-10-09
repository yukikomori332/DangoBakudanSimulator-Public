using unityroom.Api;

namespace Project.Core.Scripts.APIGateway.Unityroom
{
    /// <summary>
    /// スコアのデータをUnityroomAPIへ送信するためのAPIゲートウェイ
    /// </summary>
    public sealed class UnityroomAPIGateway
    {
        /// <summary>
        /// スコアを降順で送信する
        /// </summary>
        /// <param name="index">送信するボードNo情報</param>
        /// <param name="score">送信するスコア情報</param>
        public void SendScoreByDesc(int index, float score)
        {
            UnityroomApiClient.Instance.SendScore(index, score, ScoreboardWriteMode.HighScoreDesc);
        }
    }
}

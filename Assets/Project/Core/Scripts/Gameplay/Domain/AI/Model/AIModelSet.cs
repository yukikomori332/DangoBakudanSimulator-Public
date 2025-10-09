using System;
using System.Collections.Generic;
using UniRx;

namespace Project.Core.Scripts.Gameplay.Domain.AI.Model
{
    /// <summary>
    /// AIの情報を一括で管理するクラス
    /// </summary>
    public sealed class AIModelSet
    {
        // AIの情報リスト
        private readonly List<AIModel> _ais = new List<AIModel>();
        // AIの情報リストを外部に公開するプロパティ
        public IReadOnlyList<AIModel> AIs => _ais;

        /// <summary>
        /// AIの情報リストをセットする
        /// </summary>
        public void SetAIs(IReadOnlyList<AIModel> ais)
        {
            _ais.Clear();
            _ais.AddRange(ais);
        }
    }
}

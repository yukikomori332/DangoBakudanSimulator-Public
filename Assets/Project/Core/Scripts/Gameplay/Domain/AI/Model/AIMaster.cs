using System;
using UnityEngine;

namespace Project.Core.Scripts.Gameplay.Domain.AI.Model
{
    /// <summary>
    /// AIのマスターデータを管理するクラス
    /// </summary>
    [Serializable]
    public sealed class AIMaster
    {
        [SerializeField] private string id;         // AIの一意識別子
        [SerializeField] private GameObject prefab; // AIのプレハブ

        // AIの一意識別子
        public string Id => id;

        // AIのプレハブ
        public GameObject Prefab => prefab;
    }
}

using System;
using UnityEngine;

namespace Project.Core.Scripts.Domain.Audio.Model
{
    /// <summary>
    /// オーディオのマスターデータを管理するクラス
    /// </summary>
    [Serializable]
    public sealed class AudioMaster
    {
        [SerializeField] private string id;          // オーディオの一意識別子
        [SerializeField] private AudioClip clip;     // オーディオクリップ
        [SerializeField] private AudioSource source; // オーディオソース

        // オーディオの一意識別子
        public string Id => id;

        // オーディオクリップ
        public AudioClip Clip => clip;

        // オーディオソース
        public AudioSource Source => source;
    }
}

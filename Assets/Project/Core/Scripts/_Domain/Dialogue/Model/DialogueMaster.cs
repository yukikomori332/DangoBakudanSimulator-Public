using System;
using UnityEngine;

namespace Project.Core.Scripts.Domain.Dialogue.Model
{
    /// <summary>
    /// ダイアログのマスターデータを管理するクラス
    /// </summary>
    [Serializable]
    public sealed class DialogueMaster
    {
        [SerializeField] private string id;               // ダイアログの一意識別子
        [SerializeField] private string charName;         // キャラクター名
        [SerializeField] private string label;            // キャラクターラベル
        [TextArea, SerializeField] private string text;   // ダイアログ文
        [SerializeField] private Sprite sprite;           // キャラクター画像

        // ダイアログの一意識別子
        public string Id => id;

        // キャラクター名
        public string CharName => charName;

        // キャラクターラベル
        public string Label => label;

        // ダイアログ文
        public string Text => text;

        // キャラクター画像
        public Sprite Sprite => sprite;
    }
}

namespace Project.Core.Scripts.Foundation.Common
{
    public static class ResourceKey
    {
        public static class Scenes
        {
            // Scene Name
            public const string GameplayScene = "Gameplay";
        }

        public static class Prefabs
        {
            private const string Prefix = "prefab_";

            // UI
            private const string ArchiveFolder = "Archive";
            private const string DialogueFolder = "Dialogue";
            private const string GameplayFolder = "Gameplay";
            private const string SettingsFolder = "Settings";
            private const string CreditFolder = "Credit";
            public const string ArchivePage = ArchiveFolder + "/" + Prefix + "archive_page.prefab";
            public const string DialoguePage = DialogueFolder + "/" + Prefix + "dialogue_page.prefab";
            public const string GameplayPage = GameplayFolder + "/" + Prefix + "gameplay_page.prefab";
            public const string SettingsModal = SettingsFolder + "/" + Prefix + "settings_modal.prefab";
            public const string CreditModal = CreditFolder + "/" + Prefix + "credit_modal.prefab";
        }

        public static class Tags
        {
            public const string Player = "Player";
            public const string AI = "AI";
        }
    }
}

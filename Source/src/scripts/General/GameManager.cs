using Godot;
using GodotGame.PlayerBehaviour.InventorySystem;
using GodotGame.Serialization;
using System.Collections.Generic;

namespace GodotGame.General
{
    public class GameManager : Node
    {
        const string ItemsFileName = "Items.json";
        const string PreferencesFileName = "Preferences.json";
        const string PathToSaves = @"saves\save.json";

        public static readonly string[] AvalibleLanguages = { "ru", "en" };

        public delegate void OnLanguageChange(string lang);
        public static OnLanguageChange LanguageChanged;

        static Preferences preferences = new Preferences();
        public static Preferences Preferences
        {
            get => preferences;

            set
            {
                SerializationSystem.ReplaceDataGeneric(
                    value,
                    PreferencesFileName);

                preferences = value;
            }
        }

        public static Item[] items;

        static GameSave currentSaveFile;
        public static GameSave СurrentSaveFile
        {
            get => currentSaveFile;

            set
            {
                currentSaveFile = value;

                GameEvents = new List<string>();
                GameEvents.AddRange(value.currentEvents);

                GD.Print($"- Current game events:");
                foreach (string @event in value.currentEvents)
                {
                    GD.Print($"- \"{@event}\"");
                }
                
            }
        }

        public static List<string> GameEvents = new List<string>();

        public override void _EnterTree()
        {
            /*            SerializationSystem.SaveDataGeneric<Preferences>(
                            new Preferences(string.Empty, Vector2.Zero, false, false),
                            PreferencesFileName);
                        SerializationSystem.SaveLocalizationDataGeneric<ItemsStruct>(
                            new ItemsStruct(new Item[] { new Item(), new Item() }),
                            $@"ru\{ItemsFileName}");
            */
            
            //SerializationSystem.ReplaceDataGeneric(new GameSave { currentEvents = new string[] { "da", "puk" } }, $@"{SerializationSystem.PathToSaves}Save.json");
            
            preferences = SerializationSystem.LoadDataGeneric<Preferences>
                (PreferencesFileName);
            items = SerializationSystem.LoadLocalizationDataGeneric<ItemsStruct>
                ($@"{preferences.language}\{ItemsFileName}").items;
            СurrentSaveFile = SerializationSystem.LoadDataGeneric<GameSave>
                ($@"{SerializationSystem.PathToSaves}Save.json");
        }

        public static Item GetItem(int id)
        {
            if (id >= items.Length) { GD.PrintErr("Item id beyond availible items!"); return null; }

            return items[id];
        }

        public static void ChangeLanguage (string lang)
        {
            if (lang == Preferences.language) return;

            Preferences = new Preferences(lang, preferences.resolution, preferences.fullscreen, preferences.vsync);
            LanguageChanged?.Invoke(lang);
        }

        public static void LoadSave()
        {
            СurrentSaveFile = SerializationSystem.LoadDataGeneric<GameSave>(PathToSaves);
        }
    }
}

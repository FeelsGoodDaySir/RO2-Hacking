using System.Collections.Generic;
using Valkyrie.Core;

namespace Valkyrie.Loader.Utils
{
    public static class Utilities
    {
        private static List<Map> maps = new List<Map>();
        private static Storage storage = new Storage();
        private static Settings settings = new Settings();

        public static void GenerateDataFile()
        {
            maps = storage.RestoreObject<List<Map>>("Resources/template");
            storage.StoreObject(maps, "Resources/data");
        }

        public static void GenerateConfigFile()
        {
            MetroStyle metroStyle = new MetroStyle
            {
                Theme = 2,
                Color = 10
            };
            settings.MetroStyle = metroStyle;

            List<KeyBinding> keyBindings = new List<KeyBinding>();

            float[] vector3 = { 0.0f, 0.0f, 0.0f };
            for (int i = 0; i < 8; i++)
            {
                KeyBinding keyBinding = new KeyBinding
                {
                    Id = i + 1,
                    Key = 0,
                    Alias = "None",
                    Coordinates = vector3
                };
                keyBindings.Add(keyBinding);
            }

            settings.KeyBinding = keyBindings;

            storage.StoreObject(settings, "Resources/config");
        }
    }
}

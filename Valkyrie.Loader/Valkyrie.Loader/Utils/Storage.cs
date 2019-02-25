using Newtonsoft.Json;
using System.IO;

namespace Valkyrie.Loader
{
    public class Storage
    {
        public T RestoreObject<T>(string key)
        {
            var json = File.ReadAllText($"{key}.json");
            return JsonConvert.DeserializeObject<T>(json);
        }

        public void StoreObject(object obj, string key)
        {
            var file = $"{key}.json";
            var json = JsonConvert.SerializeObject(obj, Formatting.Indented);
            File.WriteAllText(file, json);
        }
    }
}

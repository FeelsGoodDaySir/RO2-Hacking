using System.Collections.Generic;
using System.Windows.Forms;

namespace Valkyrie.Loader
{
    public class MetroStyle
    {
        public int Theme { get; set; }
        public int Color { get; set; }
    }

    public class KeyBinding
    {
        public int Id { get; set; }
        public Keys Key { get; set; }
        public string Alias { get; set; }
        public float[] Coordinates { get; set; }
    }

    public class Settings
    {
        public MetroStyle MetroStyle { get; set; }
        public List<KeyBinding> KeyBinding { get; set; }
    }
}

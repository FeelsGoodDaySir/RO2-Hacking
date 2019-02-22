using System.Collections.Generic;

namespace Valkyrie.Core
{
    public class Place
    {
        public string Name { get; set; }
        public float[] Coordinates { get; set; }
    }

    public class Map
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Zone { get; set; }
        public string Category { get; set; }
        public List<Place> Places { get; set; }
    }
}

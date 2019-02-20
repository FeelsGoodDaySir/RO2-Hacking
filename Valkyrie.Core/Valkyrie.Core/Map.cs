using System.Collections.Generic;

namespace Valkyrie.Core
{
    public struct Map
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Zone { get; set; }
        public string Category { get; set; }
        public List<Place> Places { get; set; }
    }
}

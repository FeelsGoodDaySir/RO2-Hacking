namespace Valkyrie.Core
{
    public static class Offsets
    {
        public static readonly string TARGET_GAMEVERSION = "1.1.7.2.37.26390";
        public static readonly string CORE_VERSION = "Version 1.1.0 [PUBLIC]";

        public static readonly string movSpeed = "0x15B177C,0xA8,0x50";
        public static readonly string hp = "0x15B177C,0xA8,0xB4";
        public static readonly string maxHp = "0x15B177C,0xA8,0xB8";
        public static readonly string[] position = new string[]
        {
            "0x15B177C,0x14C,0x58,0x11C",   // coordinate X 
            "0x15B177C,0x14C,0x58,0x10C",   // coordinate Y
            "0x15B177C,0x14C,0x58,0x114"    // coordinate Z
        };

        public static readonly string mapId = "0x15AFE20,0x11C,0x8";   // Need to update this address, wrong one....
        public static readonly string wallFriction = "0x15B177C,0x14C,0x58,0x160";

        public static readonly string version = "0x15C90A8,0x634,0x10";
    }
}

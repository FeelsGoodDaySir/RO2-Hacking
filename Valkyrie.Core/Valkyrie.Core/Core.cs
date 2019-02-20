using Memory;

namespace Valkyrie.Core
{
    public class Core
    {
        // Memory.dll
        private static Mem m = new Mem();

        private readonly string _CORE_VERSION_ = "1.0.0_RELEASE_STABLE";
        private readonly string _COMPATIBLE_GAME_VERSION_ = "1.1.7.2.37.26390";

        // Game address
        private readonly string versionAddr = "0x15C90A8,0x634,0x10";

        // Memory address character
        private readonly string movSpeedAddr = "0x15B177C,0xA8,0x50";
        private readonly string hpAdrr = "0x15B177C,0xA8,0xB4";
        private readonly string maxHpAddr = "0x15B177C,0xA8,0xB8";
        private readonly string[] positionAddr = new string[]
        {
            "0x15B177C,0x14C,0x58,0x11C",   // X 
            "0x15B177C,0x14C,0x58,0x10C",   // Y
            "0x15B177C,0x14C,0x58,0x114"    // Z
        };

        // Memory address world
        private readonly string mapIdAddr = "0x15AFE20,0x11C,0x8"; // Need to update this address, wrong one....
        private readonly string wallFrictionAddr = "0x15B177C,0x14C,0x58,0x160";

        private Player player = new Player();
        private Map map = new Map();

        public bool Inject(int processId)
        {
            if (!m.OpenProcess(processId))
                return false;

            return true;
        }

        public void Close()
        {
            m.closeProcess();
        }

        public void Update()
        {
            // Player
            player.MovementSpeed = m.readFloat(movSpeedAddr);
            player.Hp = m.readInt(hpAdrr);
            player.MaxHp = m.readInt(maxHpAddr);
            player.PosX = m.readFloat(positionAddr[0]);
            player.PosY = m.readFloat(positionAddr[1]);
            player.PosZ = m.readFloat(positionAddr[2]);
            player.WallFriction = m.readInt(wallFrictionAddr);

            // World
            map.Id = m.readInt(mapIdAddr);
        }

        #region cheats

        public void Speedhack(float value)
        {
            m.writeMemory(movSpeedAddr, "float", value.ToString());
        }

        public void WallfrictionHack(int value)
        {
            m.writeMemory(wallFrictionAddr, "int", value.ToString());
        }

        public void TeleportHack(float corX, float corY, float corZ)
        {
            m.writeMemory(positionAddr[0], "float", corX.ToString());
            m.writeMemory(positionAddr[1], "float", corY.ToString());
            m.writeMemory(positionAddr[2], "float", corZ.ToString());
        }

        #endregion

        #region getters

        public Player GetPlayerInfo()
        {
            return player;
        }

        public Map GetMapInfo()
        {
            return map;
        }

        public string GetCoreVersion()
        {
            return _CORE_VERSION_;
        }

        public string GetCompatibleVersion()
        {
            return _COMPATIBLE_GAME_VERSION_;
        }

        public string GetGameVersion()
        {
            try
            {
                string version = m.sanitizeString(System.Text.Encoding.UTF8.GetString(m.readBytes(versionAddr, 32)));
                return version;
            }
            catch
            {
                string version = "";
                return version;
            }

        }

        #endregion
    }
}

using Memory;

namespace Valkyrie.Core
{
    public class Engine
    {
        private static Mem m = new Mem();
        private Player player = new Player();
        private Map map = new Map();

        public void Inject(int processId)
        {
            m.OpenProcess(processId);
        }

        public void Close()
        {
            m.closeProcess();
        }

        public void Update()
        {
            player.MovementSpeed = m.readFloat(Offsets.movSpeed);
            player.Hp = m.readInt(Offsets.hp);
            player.MaxHp = m.readInt(Offsets.maxHp);
            player.PosX = m.readFloat(Offsets.position[0]);
            player.PosY = m.readFloat(Offsets.position[1]);
            player.PosZ = m.readFloat(Offsets.position[2]);
            map.Id = m.readInt(Offsets.mapId);
        }

        public void Speedhack(float value)
        {
            m.writeMemory(Offsets.movSpeed, "float", value.ToString());
        }

        public void WallfrictionHack(int value)
        {
            m.writeMemory(Offsets.wallFriction, "int", value.ToString());
        }

        public void TeleportHack(float corX, float corY, float corZ)
        {
            m.writeMemory(Offsets.position[0], "float", corX.ToString());
            m.writeMemory(Offsets.position[1], "float", corY.ToString());
            m.writeMemory(Offsets.position[2], "float", corZ.ToString());
        }

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
            return Offsets.CORE_VERSION;
        }

        public string GetCompatibleVersion()
        {
            return Offsets.TARGET_GAMEVERSION;
        }

        public string GetGameVersion()
        {
            string version;

            try
            {
                version = m.sanitizeString(System.Text.Encoding.UTF8.GetString(m.readBytes(Offsets.version, 32)));
            }
            catch
            {
                version = "";
            }

            return version;
        }
    }
}

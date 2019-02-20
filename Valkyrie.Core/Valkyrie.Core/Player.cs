namespace Valkyrie.Core
{
    public struct Player
    {
        public string Name { get; set; }
        public float MovementSpeed { get; set; }
        public int Hp { get; set; }
        public int MaxHp { get; set; }
        public float PosX { get; set; }
        public float PosY { get; set; }
        public float PosZ { get; set; }
        public int WallFriction { get; set; }
    }
}

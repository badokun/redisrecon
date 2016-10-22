namespace RedisRecon.Shared
{
    public class Bullet
    {
        public string Key { get; set; }
        public string Payload { get; set; }
    }

    public class DoubleBullet
    {
        public Bullet Left { get; set; }
        public Bullet Right { get; set; }
    }
}
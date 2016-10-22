namespace RedisRecon.Attacker
{
    public class MachineGun : IGun
    {
        private readonly Ammo _ammo;

        public MachineGun(Ammo ammo)
        {
            _ammo = ammo;
        }

        public class Ammo
        {
            public string FilePath { get; set; }
        }
    }
}
using RedisRecon.Shared;

namespace RedisRecon.Attacker
{
    public interface IAttacker
    {
        void FireAway(Battle battle, IGun leftGun, IGun rightGun);
    }
}
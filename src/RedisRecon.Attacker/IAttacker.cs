using RedisRecon.Shared;

namespace RedisRecon.Attacker
{
    public interface IAttacker
    {
        void AnnouceBattle(Battle battle);
        void FireAway();
    }
}
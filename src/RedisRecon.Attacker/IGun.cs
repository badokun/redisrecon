using System;
using RedisRecon.Shared;

namespace RedisRecon.Attacker
{
    public interface IGun
    {
        IObservable<Bullet> Fire();
    }
}
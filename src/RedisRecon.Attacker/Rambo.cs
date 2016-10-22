using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using log4net;
using RedisRecon.Shared;
using ServiceStack.Redis;

namespace RedisRecon.Attacker
{
    public enum Side
    {
        Left,
        Right
    }
    public class Rambo : IAttacker
    {
        private readonly IRedisClientsManager _clientsManager;
        private readonly ILog _log;

        public Rambo(IRedisClientsManager clientsManager, ILog log)
        {
            _clientsManager = clientsManager;
            _log = log;
        }


        public void FireAway(Battle battle, IGun leftGun, IGun rightGun)
        {
            var sw = Stopwatch.StartNew();

            var battles = _clientsManager.GetClient().As<Battle>();
            battles.Store(new Battle
            {
                Id = battles.GetNextSequence(),
                StartDate = DateTime.Now,
            });

            
            var leftSignal = FireAGun(leftGun, Side.Left, battle);
            var rightSignal = FireAGun(rightGun, Side.Right, battle);


            WaitHandle.WaitAll(new WaitHandle[] {leftSignal, rightSignal});
            sw.Stop();
            Console.WriteLine(string.Format("Total onslaught took '{0}' seconds", sw.Elapsed.TotalSeconds));
        }

        private ManualResetEvent FireAGun(IGun gun, Side side, Battle battle)
        {
            var signal = new ManualResetEvent(false);
            
            ThreadPool.QueueUserWorkItem(state =>
            {
                var sw = Stopwatch.StartNew();
                var dynamiteBuilder = new DynamiteBuilder(side, battle);
                gun.Fire().Subscribe(bullet =>
                {
                    dynamiteBuilder.AddGunpowder(bullet);
                }, () =>
                {
                    sw.Stop();
                    Console.WriteLine($"Gun took '{sw.Elapsed.TotalSeconds}' seconds to fire all");
                    OutOfAmmo(signal, gun, dynamiteBuilder);
                    
                });
            });

            return signal;
        }

        private void OutOfAmmo(EventWaitHandle signal, IGun gun, DynamiteBuilder dynamiteBuilder)
        {
            Console.WriteLine("I'm out of ammo! {0}", gun);
            _log.DebugFormat("I'm out of ammo! {0}", gun);
            dynamiteBuilder.Detonate();
            Console.WriteLine("denotation done!");
            signal.Set();
        }
    }
}

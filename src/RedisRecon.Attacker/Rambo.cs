using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using log4net;
using RedisRecon.Shared;
using ServiceStack.Redis;

namespace RedisRecon.Attacker
{
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
            var battles = _clientsManager.GetClient().As<Battle>();
            battles.Store(new Battle
            {
                Id = battles.GetNextSequence(),
                StartDate = DateTime.Now,
            });

            
            var leftSignal = FireAGun(leftGun);
            var rightSignal = FireAGun(rightGun);


            WaitHandle.WaitAll(new WaitHandle[] {leftSignal, rightSignal});
        }

        private ManualResetEvent FireAGun(IGun gun)
        {
            var signal = new ManualResetEvent(false);
            gun.Fire().Subscribe(bullet =>
            {
                
            }, () => { OutOfAmmo(signal, gun); });
            return signal;
        }

        private void OutOfAmmo(EventWaitHandle signal, IGun gun)
        {
            _log.DebugFormat("I'm out of ammo! {0}", gun);
            signal.Set();
        }
    }


}

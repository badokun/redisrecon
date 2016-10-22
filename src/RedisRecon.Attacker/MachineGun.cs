using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using RedisRecon.Shared;

namespace RedisRecon.Attacker
{
    public class MachineGun : IGun
    {
        private readonly Ammo _ammo;

        public MachineGun(Ammo ammo)
        {
            _ammo = ammo;
        }
      
        public IObservable<Bullet> Fire()
        {
            return Read().ToObservable();
        }

        private IEnumerable<Bullet> Read()
        {
            using (var file = new StreamReader(_ammo.FilePath))
            {
                while (!file.EndOfStream)
                {
                    var fragments = file.ReadLine().Split(',');
                    yield return new Bullet() { Key = fragments[0], Payload = string.Join(",", fragments.Skip(1)) };
                }
            }
        }

        public class Ammo
        {
            public string FilePath { get; set; }
        }
    }
}
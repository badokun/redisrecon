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
            int lineCount = 0;
            using (var file = new StreamReader(_ammo.FilePath))
            {
                while (!file.EndOfStream)
                {
                    lineCount++;
                    var fragments = file.ReadLine().Split(',');
                    yield return new Bullet() { Key = fragments[0], Payload = string.Join(",", fragments.Skip(1)) };
                }
            }
        }

        public override string ToString()
        {
            return _ammo.FilePath;
        }

        public class Ammo
        {
            public string FilePath { get; set; }
        }
    }
}
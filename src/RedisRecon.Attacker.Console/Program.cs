using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RedisRecon.Shared;

namespace RedisRecon.Attacker.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            IGun leftGun = new MachineGun(new MachineGun.Ammo() { FilePath = "left.csv"});
            IGun rightGun = new MachineGun(new MachineGun.Ammo() { FilePath = "right.csv" });
            IAttacker rambo = new Rambo(leftGun, rightGun);

            var battle = new Battle();
            rambo.AnnouceBattle(battle);
            rambo.FireAway();
        }
    }
}

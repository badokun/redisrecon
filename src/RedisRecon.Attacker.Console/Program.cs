using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using RedisRecon.Shared;

namespace RedisRecon.Attacker.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var kernel = new NinjectRegistration().Build();

            var root = "..\\..\\..\\..\\SampleData";

            IGun leftGun = new MachineGun(new MachineGun.Ammo() { FilePath = string.Format("{0}\\sample 20161022 083244 Left.csv", root) });
            IGun rightGun = new MachineGun(new MachineGun.Ammo() { FilePath = string.Format("{0}\\sample 20161022 083244 Right.csv", root) });
            var rambo = kernel.Get<IAttacker>();

            var battle = new Battle();
            rambo.FireAway(battle, leftGun, rightGun);
        }


    }
}

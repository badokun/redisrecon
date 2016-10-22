using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RedisRecon.Shared;

namespace RedisRecon.Attacker
{
    public class Rambo : IAttacker
    {
        public Rambo(IGun leftGun, IGun rightGun)
        {


        }

        public void AnnouceBattle(Battle battle)
        {
            throw new NotImplementedException();
        }

        public void FireAway()
        {
            throw new NotImplementedException();
        }
    }


}

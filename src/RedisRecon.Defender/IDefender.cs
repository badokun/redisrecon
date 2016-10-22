using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RedisRecon.Shared;

namespace RedisRecon.Defender
{
    public interface IDefender
    {
        void DodgeBullets();
        List<Battle> GetBattles();
        Scorecard GetScorecard(Battle battle);
    }
}

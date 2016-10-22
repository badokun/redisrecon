using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RedisRecon.Shared;

namespace RedisRecon.Defender.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            IDefender neo = new Neo();
            neo.DodgeBullets();
            
            while (true)
            {
                System.Console.WriteLine("Hit enter to get scorecard for the first battle");
                System.Console.ReadLine();
                var battles = neo.GetBattles();
                var firstBattle = battles.First();

                var scorecard = neo.GetScorecard(firstBattle);
                var left = scorecard.MissOnLeft();

                var right = scorecard.MissOnRight();
                var hits = scorecard.Hits();
                System.Console.WriteLine("Scorecard for Battle '{0}'\nLeft: {1}\nRight: {2}\nHits: {3}", firstBattle, left.Count, right.Count, hits.Count);
            }
        }
    }
}

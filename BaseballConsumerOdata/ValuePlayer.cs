using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseballConsumerOdata.AgileWays.BaseballService;

namespace BaseballConsumerOdata
{
    public class ValuePlayer
    {
        static void Main(string[] args)
        {
            BaseballEntities be = new BaseballEntities(new Uri("http://www.agilewaysdev.com/BaseballService/Services/AgileWays.Baseball.Service.svc"));

            var valuePlayers = be.BatterDetails.Where(
                      b =>
                            ((((Int32)(b.yearID) > 1980) &&
                                     ((((Int32?)(b.HR) > (Int32?)20) || ((Int32?)(b.Triples) > (Int32?)10)) ||
                                        ((Int32?)(b.Doubles) > (Int32?)30)
                                     )
                               ) &&
                               (b.salary < (Double?)2000000)
                            )
                   )
                   .OrderBy(b => b.salary)
                   .ThenBy(b => b.yearID)
                   .Select(
                      b =>
                         new
                         {
                             nameFirst = b.nameFirst,
                             nameLast = b.nameLast,
                             TeamName = b.TeamName,
                             salary = b.salary,
                             AVG = ((Double)(((Int32?)(b.H) * (Int32?)1000) / (Int32?)(b.AB)) / 1000),
                             HR = b.HR,
                             Triples = b.Triples,
                             Doubles = b.Doubles,
                             yearID = b.yearID
                         }
                   );
            foreach (var player in valuePlayers)
            {
                Console.WriteLine("{0} {1} in {3} made Salary = {2}", player.nameFirst, player.nameLast, player.salary.Value.ToString(), player.yearID.ToString());
            }

        }
    }
}

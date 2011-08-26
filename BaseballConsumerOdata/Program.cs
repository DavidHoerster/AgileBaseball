using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseballConsumerOdata.AgileWays.BaseballService;

namespace BaseballConsumerOdata
{
    class Program
    {
        static void Main(string[] args)
        {
            BaseballEntities be = 
                new BaseballEntities(new Uri("http://www.agilewaysdev.com/BaseballService/Services/AgileWays.Baseball.Service.svc"));

            bool isNext = true;
            Console.WriteLine("Enter query:");

            while (isNext)
            {
                string action = Console.ReadLine();
                if (action.ToLower()=="exit")
                {
                    isNext = false;
                    continue;
                }

                string[] parts = action.Split(new char[] { ' ' });

                if (parts.Length != 2)
                {
                    Console.WriteLine("Incorrect format.  Use [obj] [criteria] [max]");
                }
                else
                {
                    switch (parts[0].ToLower())
                    {
                        case "team":
                            string[] criteria = parts[1].Split(new char[] { ';' });

                            if (criteria.Length == 1)
                            {
                                //just year specified
                                var items = from t in be.Teams
                                        where t.yearID == Convert.ToInt16(criteria[0])
                                        orderby t.name ascending
                                        select new
                                        {
                                            t.name,
                                            t.yearID,
                                            t.W,
                                            t.L,
                                            t.attendance
                                        };

                                foreach (var item in items)
                                {
                                    Console.WriteLine("{3}\t{0}\t{1}\t{2}", item.yearID, item.W, item.L, item.name);
                                }
                            }
                            else
                            {
                                var items = from t in be.Teams
                                        where t.name == criteria[1] &&
                                            t.yearID == Convert.ToInt16(criteria[0])
                                        orderby t.yearID ascending
                                        select new
                                        {
                                            t.name,
                                            t.yearID,
                                            t.W,
                                            t.L,
                                            t.attendance
                                        };

                                foreach (var item in items)
                                {
                                    Console.WriteLine("{0} {1} {2}", item.yearID, item.W, item.L);
                                }
                            }
                            break;
                        //case "birthday":
                        //    string[] data = parts[1].Split(new char[] { ';' }); //format is first;last;year;month;day
                        //    if (data.Length != 5)
                        //    {
                        //        Console.WriteLine("Incorrect format.  Should be first;last;year;month;day");
                        //        break;
                        //    }
                        //    Birthday_Buddy bb = Birthday_Buddy.CreateBirthday_Buddy(
                        //        Convert.ToInt16(data[2]),
                        //        Convert.ToInt16(data[3]),
                        //        Convert.ToInt16(data[4]),
                        //        data[0],
                        //        data[1]);
                        //    be.AddToBirthday_Buddies(bb);
                        //    be.SaveChanges();
                        //    break;
                        case "batter":
                        case "pitcher":
                        default:
                            Console.WriteLine("Invalid object.  Use Team, Batter or Pitcher");
                            break;
                    }
                }
                Console.WriteLine("\n\nEnter Query:");
            }

        }
    }
}

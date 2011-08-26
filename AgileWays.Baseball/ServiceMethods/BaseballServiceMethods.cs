using System.Data.Services.Common;
using System.Linq;
using System.ServiceModel.Web;
using System.Web;
using AgileWays.Baseball.Models;
using System.Collections.Generic;
using System;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace AgileWays.Baseball.Services
{
    [DataContract]
    public class GridRowData
    {
        [DataMember(Name="playerID")]
        public String PlayerID { get; set; }
        [DataMember(Name="name")]
        public String Name { get; set; }
        [DataMember(Name="bats")]
        public String Bats { get; set; }
    }

    [DataContract]
    public class GridRow
    {
        [DataMember(Name="id")]
        public String ID { get; set; }
        [DataMember(Name="cell")]
        public String[] Cells { get; set; }
    }

    [DataContract]
    public class GridData
    {
        [DataMember(Name="total")]
        public Int32 Total { get; set; }
        [DataMember(Name="page")]
        public Int32 Page { get; set; }
        [DataMember(Name="records")]
        public Int32 Records { get; set; }
        [DataMember(Name="rows")]
        public GridRow[] Rows { get; set; }
    }

    public partial class AgileWays
    {
        [WebGet()]
        public IQueryable<PlayerInfo> DoPlayerSearch(string name)
        {
            var players = from p in CurrentDataSource.PlayerData
                          where p.nameFirst.StartsWith(name) || p.nameLast.StartsWith(name)
                          select p;

            return players;
        }


        [WebGet()]
        public IQueryable<HallOfFame> GetHOFInfo(string hofID)
        {
            var hofData = from h in CurrentDataSource.HallOfFames
                          where h.hofID == hofID
                          select h;

            return hofData;
        }

        [WebGet()]
        public List<string> GetTeamYears(Int32 startingYear)
        {
            var years = from t in CurrentDataSource.Teams
                        where t.yearID >= startingYear
                        select t.yearID;

            return years.Distinct().ToList().ConvertAll<string>(x => x.ToString());
        }

        [WebGet()]
        public IQueryable<Team> GetTeamsForYearAndSearchString(string year, string name)
        {
            int theYear;

            if (Int32.TryParse(year, out theYear))
            {
                if (String.IsNullOrEmpty(name))
                {
                    var teams = from t in CurrentDataSource.Teams
                                where t.yearID == theYear
                                select t;
                    return teams;
                }
                else
                {
                    var teams = from t in CurrentDataSource.Teams
                                where t.yearID == theYear && t.name.Contains(name)
                                select t;
                    return teams;
                }

            }
            else
            {
                return null;
            }
        }

        [WebGet()]
        public IQueryable<All_Time_HR_Leader> GetAllTimeHomerLeaders(string count)
        {
            int theCount;

            var homers = from h in CurrentDataSource.All_Time_HR_Leaders
                         orderby h.SumOfHR descending
                         select h;

            //if we passed in a count, take it
            if (Int32.TryParse(count, out theCount))
            {
                return homers.Take(theCount);
            }

            //otherwise, just return everything
            return homers;
        }

        [WebGet()]
        public IQueryable<BatterDetail> GetBatterDetails(string playerID)
        {
            var batterInfo = from b in CurrentDataSource.BatterDetails
                             where b.playerID == playerID
                             select b;

            return batterInfo;
        }


        [WebGet()]
        public IQueryable<PitcherDetail> GetPitcherDetails(string playerID)
        {
            var pitcherInfo = from p in CurrentDataSource.PitcherDetails
                              where p.playerID == playerID
                              select p;

            return pitcherInfo;
        }

        [WebGet()]
        public String GetPlayers2(string sidx, string sord, int page, int rows, bool _search, string searchField, string searchOper, string searchString)
        {
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            var players = from p in CurrentDataSource.PlayerData select p;
            int totalRecords = players.Count();
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)pageSize);
            var players2 = (from p2 in CurrentDataSource.PlayerData orderby p2.playerID select p2).Skip(pageIndex * pageSize).Take(pageSize);

            int ii = 0;
            var rowsObj = new List<GridRow>(pageSize > totalRecords ? totalRecords : pageSize);
            foreach (PlayerInfo q in players2)
            {
                rowsObj.Add(new GridRow()
                {
                    ID = q.playerID,
                    Cells = new String[] { q.playerID, q.nameFirst + " " + q.nameLast, q.bats }
                });

                //rowsObj[ii] = new { id = q.playerID, cell = new object[] { q.playerID, q.nameFirst + " " + q.nameLast, q.bats } };
                //ii++;
            }
            
            var result = new GridData()
            {
                Total = totalPages,
                Page = page,
                Records = totalRecords,
                Rows = rowsObj.ToArray()
            };
            string resultJson = JsonConvert.SerializeObject(result, Formatting.None);

            return resultJson;
        }

    }
}
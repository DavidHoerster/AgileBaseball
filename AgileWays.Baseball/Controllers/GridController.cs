using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Services;
using System.Data.Services.Common;
using AgileWays.Baseball.Models;

namespace AgileWays.Baseball.Controllers
{
    public class GridController : Controller
    {
        public ActionResult GetPlayers(string sidx, string sord, int page, int rows, bool _search, string searchField, string searchOper, string searchString)
        {
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            AgileWays.Baseball.Models.BaseballEntities ctx = new AgileWays.Baseball.Models.BaseballEntities();
            var players = from p in ctx.BatterDetails select p; 
            int totalRecords = players.Count();
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)pageSize);
            IQueryable<AgileWays.Baseball.Models.BatterDetail> players2;
            Int32 salary = Convert.ToInt32(searchString);
            if (_search)
            {
                players2 = (from p2 in ctx.BatterDetails where p2.salary <= salary orderby p2.playerID select p2).Skip(pageIndex * pageSize).Take(pageSize);
            }
            else
            {
                players2 = (from p2 in ctx.BatterDetails orderby p2.playerID select p2).Skip(pageIndex * pageSize).Take(pageSize);
            }
            
 
            int ii = 0;
            var rowsObj = new object[pageSize > totalRecords ? totalRecords : pageSize];
            foreach (BatterDetail q in players2)
            {
                rowsObj[ii] = new { id = q.playerID, cell = new object[] { q.playerID, q.yearID, q.nameFirst + " " + q.nameLast, q.H, q.Doubles, q.Triples, q.HR, q.salary } };
                ii++;
            }
            var result = new JsonResult();
            result.Data = new
            {
                total = totalPages,
                page = page,
                records = totalRecords,
                rows = rowsObj.ToArray()
            };
       
             return Json(result.Data, JsonRequestBehavior.AllowGet);
        }
    }
}

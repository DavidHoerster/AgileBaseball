using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Services;
using System.Linq.Expressions;

namespace AgileWays.Baseball.Services
{
    public partial class AgileWays
    {
        ////this won't get hit by service operation
        //[QueryInterceptor("Teams")]
        //public Expression<Func<Baseball.Models.Team, bool>> OnQueryTeams()
        //{
        //    return t => t.yearID == 2008;
        //}

        //[QueryInterceptor("AllstarWithNames")]
        //public Expression<Func<Baseball.Models.AllstarWithName, bool>> OnQueryAllStar()
        //{
        //    return p => p.yearID == 2008;
        //}

        //[QueryInterceptor("Birthday_Buddies")]
        //public Expression<Func<Baseball.Models.Birthday_Buddy, bool>> OnQueryBirthdaysGreaterThan1970()
        //{
        //    //filter out those requests that were only created in 2010...
        //    return p => p.birthYear > 1969;
        //}

        //[ChangeInterceptor("Allstars")]
        //public void OnChangeAllStar(Baseball.Models.Allstar allstar, UpdateOperations op)
        //{
        //    if (op == UpdateOperations.Change)
        //    {
        //        throw new DataServiceException(403, "You cannot delete AllStars -- it's anti-American!");
        //    }
        //}
    }
}
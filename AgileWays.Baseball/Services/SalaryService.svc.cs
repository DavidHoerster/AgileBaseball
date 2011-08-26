using System;
using System.Collections.Generic;
using System.Data.Services;
using System.Data.Services.Common;
using System.Linq;
using System.ServiceModel.Web;
using System.Web;
using AgileWays.Baseball.Models;

namespace AgileWays.Baseball.Services
{
    public class SalaryService : DataService<BaseballEntities>
    {
        public static void InitializeService(DataServiceConfiguration config)
        {
            config.SetEntitySetAccessRule("*", EntitySetRights.AllRead);
            config.SetEntitySetAccessRule("Salaries", EntitySetRights.All);

            config.SetEntitySetPageSize("*", 25);

            config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V2;
        }
    }
}

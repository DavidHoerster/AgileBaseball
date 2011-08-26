using System;
using System.Collections.Generic;
using System.Data.Services;
using System.Data.Services.Common;
using System.Linq;
using System.ServiceModel.Web;
using System.Web;
using AgileWays.Baseball.Models;
using System.Security;

using AgileWays.Baseball.Attributes;

namespace AgileWays.Baseball.Services
{
    [JSONPSupportBehaviorAttribute]
    public partial class AgileWays : DataService<BaseballEntities>
    {
        // This method is called only once to initialize service-wide policies.
        public static void InitializeService(DataServiceConfiguration config)
        {
            config.SetEntitySetAccessRule("*", EntitySetRights.All);
            //config.SetEntitySetAccessRule("Allstars", EntitySetRights.All);
            //config.SetEntitySetAccessRule("Salaries", EntitySetRights.AllWrite);

            //config.SetEntitySetAccessRule("Birthday_Buddies", EntitySetRights.AllRead | EntitySetRights.WriteAppend);
            //config.SetEntitySetAccessRule("Salaries", EntitySetRights.AllRead | EntitySetRights.WriteReplace | EntitySetRights.WriteMerge);

            config.SetEntitySetPageSize("*", 25);
            config.UseVerboseErrors = true;

            config.SetServiceOperationAccessRule("DoPlayerSearch", ServiceOperationRights.AllRead);
            config.SetServiceOperationAccessRule("GetHOFInfo", ServiceOperationRights.AllRead);
            config.SetServiceOperationAccessRule("GetTeamYears", ServiceOperationRights.AllRead);
            config.SetServiceOperationAccessRule("GetTeamsForYearAndSearchString", ServiceOperationRights.AllRead);
            config.SetServiceOperationAccessRule("GetAllTimeHomerLeaders", ServiceOperationRights.AllRead);
            config.SetServiceOperationAccessRule("GetBatterDetails", ServiceOperationRights.AllRead);
            config.SetServiceOperationAccessRule("GetPitcherDetails", ServiceOperationRights.AllRead);
            //config.SetServiceOperationAccessRule("GetPlayers2", ServiceOperationRights.AllRead);

            config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V2;
        }


        //for service methods, see ServiceMethods/BaseballServiceMethods.cs

        protected override void OnStartProcessingRequest(ProcessRequestArgs args)
        {
            base.OnStartProcessingRequest(args);

            HttpContext theContext = HttpContext.Current;

            ////a very rudimentary way of checking for an authorization header
            //if ((args.RequestUri.Segments.Last().Replace("/", String.Empty) != "$metadata") && (theContext.Request.Headers["token"] != "let me in"))
            //{
            //    throw new SecurityException("Invalid credentials -- What were you thinking!?!?!");
            //}

            //args contains the following:
            //  RequestUri -- the requested uri string
            //  OperationContext -- the context of the request, where you have access to
            //      request and response headers
            //  IsBatchOperation -- bool whether or not this request is part of a batch

        }
    }
}

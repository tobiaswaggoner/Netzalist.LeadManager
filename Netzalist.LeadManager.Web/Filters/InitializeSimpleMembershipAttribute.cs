// *********************************************************************
// (c) 2013 Rope Development
// *********************************************************************

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using Netzalist.LeadManager.Web.Models;
using WebMatrix.WebData;

namespace Netzalist.LeadManager.Web.Filters
{
    //[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    //public sealed class InitializeSimpleMembershipAttribute : ActionFilterAttribute
    //{
    //    private static SimpleMembershipInitializer _initializer;
    //    private static object _initializerLock = new object();
    //    private static bool _isInitialized;

    //    public override void OnActionExecuting(ActionExecutingContext filterContext)
    //    {
    //        // Ensure ASP.NET Simple Membership is initialized only once per app start
    //        LazyInitializer.EnsureInitialized(ref _initializer, ref _isInitialized, ref _initializerLock);
    //    }

    //    // ReSharper disable once ClassNeverInstantiated.Local
    //    private class SimpleMembershipInitializer
    //    {
    //        public  SimpleMembershipInitializer()
    //        {
    //            Database.SetInitializer<NetzalistDb>(null);

    //            try
    //            {
    //                using (var context = NetzalistDb.Instance)
    //                {
    //                    if (!context.Database.Exists())
    //                    {
    //                        // Create the SimpleMembership database without Entity Framework migration schema
    //                        ((IObjectContextAdapter) context).ObjectContext.CreateDatabase();
    //                    }
    //                }

    //                WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserId", "UserName",
    //                    true);
    //            }
    //            catch (Exception ex)
    //            {
    //                throw new InvalidOperationException(
    //                    "The ASP.NET Simple Membership database could not be initialized. For more information, please see http://go.microsoft.com/fwlink/?LinkId=256588",
    //                    ex);
    //            }
    //        }
    //    }
    //}
}
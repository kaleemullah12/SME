using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace SMEForm.Context
{
    internal class DbContext
    {
        internal static SMECoreEntities Current()
        {
            if (!HttpContext.Current.Items.Contains("_dbContext"))
            {
                HttpContext.Current.Items.Add("_dbContext", new SMECoreEntities());
            }
            return HttpContext.Current.Items["_dbContext"] as SMECoreEntities;
        }
    }
}
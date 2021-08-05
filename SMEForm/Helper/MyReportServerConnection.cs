using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Web;
using Microsoft.Reporting.WebForms;

namespace SMEForm.Helper
{
    public sealed class MyReportServerConnection : IReportServerConnection2
    {
        public WindowsIdentity ImpersonationUser
        {
            get
            {
                return null;
            }
        }

        public ICredentials NetworkCredentials
        {
            get
            {
                string userName = Util.RSUserName;
                string password = Util.RSPassword;
                string domain = Util.RSDomain;
                return new NetworkCredential(userName, password, domain);
            }
        }

        public bool GetFormsCredentials(out Cookie authCookie, out string userName, out string password, out string authority)
        {
            authCookie = null;
            userName = null;
            password = null;
            authority = null;
            return false;
        }

        public Uri ReportServerUrl
        {
            get
            {
                return new Uri(Util.ReportServerURL);
            }
        }

        public int Timeout
        {
            get
            {
                return 60000; // 60 seconds
            }
        }

        public IEnumerable<Cookie> Cookies
        {
            get
            {
                // No custom cookies
                return null;
            }
        }

        public IEnumerable<string> Headers
        {
            get
            {
                // No custom headers
                return null;
            }
        }
    }
}
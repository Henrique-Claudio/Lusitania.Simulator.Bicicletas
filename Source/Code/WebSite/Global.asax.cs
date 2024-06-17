using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace Lusitania.Simuladores.WebSite
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            //Lusitania.PerformanceMonitor.Modules.PerformanceMonitor.Main.Start();
            //Lusitania.PerformanceMonitor.Modules.PerformanceMonitor.LogShipper.Start();
        }
        
        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {
            //Lusitania.PerformanceMonitor.Modules.PerformanceMonitor.Main.Stop();
            //Lusitania.PerformanceMonitor.Modules.PerformanceMonitor.LogShipper.Stop();
        }
    }
}
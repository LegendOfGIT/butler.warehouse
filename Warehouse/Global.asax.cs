using System;
using System.Web;

using DataWarehouse.Queing;

namespace Warehouse
{
  public class Global : HttpApplication
  {

    protected void Application_Start(object sender, EventArgs e)
    {
      var manager = new QueueManager();
      manager.Subscribe("Store Information", QueueManager.Store);
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

    }
  }
}
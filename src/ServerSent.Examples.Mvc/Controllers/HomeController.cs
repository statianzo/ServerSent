using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace ServerSent.Examples.Mvc.Controllers
{
    public class HomeController : Controller
    {
        static readonly EventStreamManager Manager = new EventStreamManager();

        public ActionResult Index()
        {
            Manager.WriteToAll("New visitor");
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            return View();
        }

        public ActionResult Events()
        {
            Response.BufferOutput = false;
            Response.ContentType = "text/event-stream";
            var id = Guid.NewGuid();

            Manager.Register(id.ToString(), Response.OutputStream);

            Thread.Sleep(-1);

            return new EmptyResult();
        }
    }
}

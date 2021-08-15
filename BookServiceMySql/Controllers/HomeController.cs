using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NLog;


namespace BookServiceMySql.Controllers
{

    public class HomeController : Controller { 


    private static Logger logger = LogManager.GetCurrentClassLogger();

    public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            logger.Info("Hello You have visited the Index view.");
            return View();
        }
    }
}

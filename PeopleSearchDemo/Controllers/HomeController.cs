using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PeopleSearchDemo.Models;
using PeopleSearchDemo.ViewModel;
using PeopleSearchDemo.Domain;

namespace PeopleSearchDemo.Controllers
{
    public class HomeController : Controller
    {
        PeopleManager _peopleManager;

        public HomeController()
        {
            _peopleManager = new PeopleManager();
        }
        public ActionResult Index()
        {
            ViewBag.Message = "Home";

            return RedirectToAction("Search", "People");
        }

        #region Seed Data
        [HttpGet]
        public ActionResult DataLoadResult()
        {

            _peopleManager = new PeopleManager();
            string successOrErrorMsg = _peopleManager.SeedData();

            return Json(successOrErrorMsg);
        }
        #endregion

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PeopleSearchDemo;
using PeopleSearchDemo.Controllers;

namespace PeopleSearchDemo.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        HomeController controller;
        [TestInitialize]
        public void Initialize()
        {
            controller = new HomeController();
        }

        //[TestMethod]
        //public void Index()
        //{
        //    ViewResult result = controller.Index() as ViewResult;
        //    Assert.IsNotNull(result);
        //}


    }
}

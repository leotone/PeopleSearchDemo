using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeopleSearchDemo.Models;
using PeopleSearchDemo.Domain;
using PeopleSearchDemo.DataAccess;
using System.Data.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PeopleSearchDemo.Controllers;
using System.Web.Mvc;

namespace PeopleSearchDemo.Tests.Controllers
{
    [TestClass]
    public class PeopleControllerTest
    {
        PeopleController controller;
        [TestInitialize]
        public void Initialize()
        {
            controller = new PeopleController();
        }


        [TestMethod]
        public void TestAction_New()
        {
            var result = controller.New() as ViewResult;
            Assert.AreEqual("New", result.ViewName);

        }

        [TestMethod]
        public void TestAction_Search()
        {
            var result = controller.Search() as ViewResult;
            Assert.AreEqual("Search", result.ViewName);

        }
    }
}

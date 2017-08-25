using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PeopleSearchDemo.Helper;
using System.IO;
using PeopleSearchDemo.Models;
using PeopleSearchDemo.ViewModel;
using PeopleSearchDemo.Domain;
using System.Threading.Tasks;

namespace PeopleSearchDemo.Controllers
{

    public class PeopleController : Controller
    {


        public static byte[] _imageBytes;
        PeopleManager _peopleManager;
        public PeopleController()
        {
            _peopleManager = new PeopleManager();
        }

        public ActionResult Search()
        {
            return View("Search");
        }

        public ActionResult New()
        {
            return View("New");
        }

        #region Search People and Return Results
        [HttpGet]
        public async Task<ActionResult> Result(string name)
        {
            List<PeopleViewModel> resultPeoples = await _peopleManager.Search(name);

            return View(resultPeoples);
        }

        #endregion

        #region Upload Image

        [HttpPost]
        public ActionResult UploadImage()
        {
            Stream stream = FileHelper.GetAttachmentStramFromHttpFiles(Request.Files);
            _imageBytes = FileHelper.ConvertStramToByeArray(stream);

            return Json("File uploaded successfully");
        }
        #endregion

        #region Add People in DB
        [HttpPost]
        public ActionResult New(PeopleViewModel peopleViewModel)
        {
            int id = _peopleManager.Create(peopleViewModel, _imageBytes);

            return Json(new People());
        }
        #endregion
    }
}

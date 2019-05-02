using DocumentManagement;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UpravljanjeDokumentacijomWebApp.Models;

namespace UpravljanjeDokumentacijomWebApp.Controllers
{
    
    public class BinderController : Controller
    {
        public ActivityService _activityService;

        public BinderController(ActivityService activityService)
        {
            _activityService = activityService;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("CreateBinder")]
        [Consumes("multipart/form-data")]
        public IActionResult CreateBinder(BinderViewModel model)
        {

            return View();
        }

        [HttpGet]
        [Route("AddBinded")]
        public IActionResult AddBinded(int index)
        {

            return PartialView("_BindedDocumentPartial", new BindedDocumentVM { Id = index });
        }
    }
}

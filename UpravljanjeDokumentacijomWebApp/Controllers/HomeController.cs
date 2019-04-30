using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UpravljanjeDokumentacijomWebApp.Models;
using DTO.Models;
using AutoMapper;
using DocumentManagement;

namespace UpravljanjeDokumentacijomWebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActivityService _activityService;

        public HomeController(ActivityService activityService)
        {
            _activityService = activityService;
        }

        public IActionResult SaveOperation()
        {
            OperationDTO operationDTO = _activityService.LoadOperation();
            OperationViewModel operation = Mapper.Map<OperationViewModel>(operationDTO);
       
            return View(operation);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public IActionResult SaveOperation(OperationViewModel operation)
        {
            operation.InputReceive = operation.InputReceive.Where(x => x.Selected).ToList();
            operation.InputByRequest = operation.InputByRequest.Where(x => x.Selected).ToList();
            _activityService.SaveOperation(Mapper.Map<OperationDTO>(operation));
            return View();
        }


        //cuva aktivnost, odnosno kreira servis
        [HttpPost]
        [Consumes("multipart/form-data")]
        public IActionResult SaveActivity(ActivityViewModel activity)
        {
            
            ActivityDTO dto = Mapper.Map<ActivityDTO>(activity);
            _activityService.SaveActivity(dto);

            return View();

        }
        //za download dokumenta
        [HttpGet]
        [Route("Document/{id}")]
        public IActionResult GetDocument(int id = -1)
        {
            try
            {
                DocumentDTO dto = _activityService.GetDocumentById(id);
                return File(dto.File.File, "application/octet-stream", dto.File.Name);
            }
            catch (Exception)
            {
                return Error();
            }

        }
        //ajax za dodavanje input dokumenata
        [HttpGet]
        [Route("AddInput")]
        public IActionResult AddInput(int index)
        {
            return PartialView("_InputPartial", new DocumentViewModel { Id = index });
        }
        //ajax za dodavanje output dokumenata
        [HttpGet]
        [Route("AddOutput")]
        public IActionResult AddOutput(int index)
        {
            return PartialView("_OutputPartial", new DocumentViewModel { Id = index });
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}

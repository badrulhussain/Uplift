﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Uplift.DataAccess.Data.Repository.IRepository;
using Uplift.Models;
using Uplift.Models.ViewModels;

namespace Uplift.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ServiceController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        [BindProperty]
        public ServiceVM ServVM {get; set;}

        public ServiceController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            ServVM = new ServiceVM()
            {
                CategoryList = _unitOfWork.Category.GetCategoryListForDropDown(),
                FrequencyList = _unitOfWork.Frequency.GetFrequencyListForDropDown(),
                Service = new Service()
            };

            if(id != null)
            {
                ServVM.Service = _unitOfWork.Service.Get(id.GetValueOrDefault());
            }

            return View(ServVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert()
        {
            if (ModelState.IsValid)
            {
                string webRootPath = _hostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                
                // New service
                if(ServVM.Service.Id == 0)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"images\services");
                    var extention = Path.GetExtension(files[0].FileName);

                    using (var filesStreams = new FileStream(Path.Combine(uploads + fileName + extention), FileMode.Create))
                    {
                        files[0].CopyTo(filesStreams);
                    }
                    ServVM.Service.ImageUrl = @"\images\services\" + fileName + extention;

                    _unitOfWork.Service.Add(ServVM.Service);
                }
                else
                {
                    var serviceFromDB = _unitOfWork.Service.Get(ServVM.Service.Id);
                    if (serviceFromDB.Id > 0)
                    {
                        string fileName = Guid.NewGuid().ToString();
                        var uploads = Path.Combine(webRootPath, @"images\services");
                        var extention_new = Path.GetExtension(files[0].FileName);

                        var imagePath = Path.Combine(webRootPath, serviceFromDB.ImageUrl.Trim('\\'));
                        if(System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }

                        using (var fileStream = new FileStream(Path.Combine(uploads + fileName + extention_new), FileMode.Create))
                        {
                            files[0].CopyTo(fileStream);
                        }
                        ServVM.Service.ImageUrl = @"\images\services\" + fileName + extention_new;
                    }
                    else
                    {
                        ServVM.Service.ImageUrl = serviceFromDB.ImageUrl;
                    }

                    _unitOfWork.Service.UpdateService(ServVM.Service);
                }

                _unitOfWork.Save();

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(ServVM);
            }
        }

        #region API
        public IActionResult GetAll()
        {
            return Json(new { data = _unitOfWork.Service.GetAll(includeProperties: "Category,Frequency")});
        }
        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Uplift.DataAccess.Data.Repository.IRepository;
using Uplift.Models;
using Uplift.Models.ViewModels;

namespace Uplift.Areas.Admin.Controllers
{
    public class ServiceController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

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
            var ServVM = new ServiceVM()
            {
                CategoryList = _unitOfWork.Category.GetCategoryListForDropDown(),
                FrequencyList = _unitOfWork.FrequencyRepository.GetFrequencyListForDropDown(),
                Service = new Service()
            };

            if(id != null)
            {
                ServVM.Service = _unitOfWork.ServiceRepository.Get(id.GetValueOrDefault());
            }

            return View(ServVM);
        }

        #region API
        public IActionResult GetAll()
        {
            return Json(new { data = _unitOfWork.ServiceRepository.GetAll(includeProperties: "Category,Frequency")});
        }
        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Uplift.DataAccess.Data.Repository.IRepository;

namespace Uplift.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FrequencyController : Controller
    {
        private readonly IUnitOfWork _unitOfWord;

        public FrequencyController(IUnitOfWork unitOfWord)
        {
            _unitOfWord = unitOfWord;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region API

        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _unitOfWord.FrequencyRepository.GetAll() });
        }
        #endregion
    }
}
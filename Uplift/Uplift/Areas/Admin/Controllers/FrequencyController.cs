using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Uplift.DataAccess.Data.Repository.IRepository;
using Uplift.Models;

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

        public IActionResult Upsert(int? id)
        {
            var Frequency = new Frequency();
            if(id == null)
            {
                return View(Frequency);
            }

            Frequency = _unitOfWord.FrequencyRepository.Get(id.GetValueOrDefault());
            if(Frequency == null)
            {
                return NotFound();
            }

            return View(Frequency);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Upsert(Frequency frequency)
        {
            if(ModelState.IsValid)
            {
                if (frequency.Id == 0)
                {
                    _unitOfWord.FrequencyRepository.Add(frequency);
                }
                else
                {
                    _unitOfWord.FrequencyRepository.UpdateFrequency(frequency);
                }

                _unitOfWord.Save();

                return View(nameof(Index));
            }

            return View(frequency);
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
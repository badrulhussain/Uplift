using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Uplift.DataAccess.Data;
using Uplift.DataAccess.Data.Repository.IRepository;
using Uplift.Models;
using Uplift.Utility;

namespace Uplift.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class WebImageController : Controller
    {
        private readonly ApplicationDbContext _db;

        public WebImageController(ApplicationDbContext _db)
        {
            _db = db;
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}

        //public IActionResult Upsert(int? id)
        //{
        //    var category = new Category();
        //    if(id == null)
        //    {
        //        return View(category);
        //    }

        //    category = _unitOfWork.Category.Get(id.GetValueOrDefault());
        //    if(category == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(category);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Upsert(Category category)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (category.Id == 0)
        //        {
        //            _unitOfWork.Category.Add(category);
        //        }
        //        else
        //        {
        //            _unitOfWork.Category.Update(category);
        //        }

        //        _unitOfWork.Save();

        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(category);
        //}

        #region API

        [HttpGet]
        public IActionResult GetAll()
        {
            //return Json(new { data = _unitOfWork.Category.GetAll() });

            // Using the storeprocedure call to get the values 
            return Json(new { data = _db.WebImages.ToList()});
        }

        [HttpDelete]
        public IActionResult Delete(int Id)
        {
            var objFromDb = _db.WebImages.Find(Id);

            if(objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting."});
            }

            _db.WebImages.Remove(objFromDb);
            _db.SaveChanges();

            return Json(new { success = true, message = "Delete successful."});
        }

        #endregion
    }
}
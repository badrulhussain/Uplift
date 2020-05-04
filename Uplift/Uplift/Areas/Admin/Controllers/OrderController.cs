using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Uplift.DataAccess.Data.Repository.IRepository;
using Uplift.Models.ViewModels;
using Uplift.Utility;

namespace Uplift.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int Id)
        {
            var OrderVM = new OrderViewModel()
            {
                OrderHeader = _unitOfWork.OrderHeader.Get(Id),
                OrderDetails = _unitOfWork.OrderDetails.GetAll(o => o.OrderHeaderId == Id)
            };

            return View(OrderVM);
        }

        public IActionResult Approve(int Id)
        {
            var orderFromDb = _unitOfWork.OrderHeader.Get(Id);
            if(orderFromDb == null)
            {
                return NotFound();
            }
            _unitOfWork.OrderHeader.ChangeOrderStatus(Id,SD.StatusApproved);

            return View(nameof(Index));
        }

        public IActionResult Reject(int Id)
        {
            var orderFromDb = _unitOfWork.OrderHeader.Get(Id);
            if (orderFromDb == null)
            {
                return NotFound();
            }
            _unitOfWork.OrderHeader.ChangeOrderStatus(Id, SD.StatusRejcted);

            return View(nameof(Index));
        }

        #region API Calls
        public IActionResult GetAllOrders()
        {
            return Json( new { data = _unitOfWork.OrderHeader.GetAll() });
        }
        public IActionResult GetAllPendingOrders()
        {
            return Json(new { data = _unitOfWork.OrderHeader.GetAll(filter: o => o.Status == SD.StatusSubmitted) });
        }
        public IActionResult GetAllApprovedOrders()
        {
            return Json(new { data = _unitOfWork.OrderHeader.GetAll(filter: o => o.Status == SD.StatusApproved) });
        }
        #endregion
    }
}
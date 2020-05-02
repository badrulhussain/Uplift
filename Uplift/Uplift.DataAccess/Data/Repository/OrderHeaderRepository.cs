using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using Uplift.DataAccess.Data.Repository.IRepository;
using Uplift.Models;

namespace Uplift.DataAccess.Data.Repository
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private readonly ApplicationDbContext _db;

        public OrderHeaderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void ChangeOrderStatus(int oderHeaderId, string status)
        {
            var orderHeaderFromDb = _db.OrderHeader.FirstOrDefault(o => o.Id == oderHeaderId);
                orderHeaderFromDb.Status = status;
                _db.SaveChanges();
        }

    }
}

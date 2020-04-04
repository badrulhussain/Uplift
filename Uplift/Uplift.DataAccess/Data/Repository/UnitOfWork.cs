using System;
using System.Collections.Generic;
using System.Text;
using Uplift.DataAccess.Data.Repository.IRepository;

namespace Uplift.DataAccess.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private protected ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(db);
            FrequencyRepository = new FrequencyRepository(db);
            ServiceRepository = new ServiceRepository(db);
        }
        public ICategoryRepository Category { get; private set; }
        public IFrequencyRepository FrequencyRepository { get; private set; }
        public IServiceRepository ServiceRepository { get; private set; }
        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}

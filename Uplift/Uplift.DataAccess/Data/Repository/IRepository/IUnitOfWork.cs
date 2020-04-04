using System;
using System.Collections.Generic;
using System.Text;

namespace Uplift.DataAccess.Data.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository Category { get; }
        IFrequencyRepository FrequencyRepository { get; }
        IServiceRepository ServiceRepository { get; }
        void Save();
    }
}

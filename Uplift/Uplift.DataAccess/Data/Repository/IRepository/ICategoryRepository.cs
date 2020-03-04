﻿using System;
using System.Collections.Generic;
using System.Text;
using Uplift.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Uplift.DataAccess.Data.Repository.IRepository
{
    interface ICategoryRepository : IRepository<Category>
    {
        IEnumerable<SelectListItem> GetCategoryListForDropDown();

        void UpdateCategory(Category category);
    }
}

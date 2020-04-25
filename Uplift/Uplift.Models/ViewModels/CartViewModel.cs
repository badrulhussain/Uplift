using System;
using System.Collections.Generic;
using System.Text;

namespace Uplift.Models.ViewModels
{
    public class CartViewModel
    {
        public IList<Service> Service { get; set; }
        public OrderHeader OrderHeader { get; set; }
    }
}

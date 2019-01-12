using System;
using System.Collections.Generic;
using System.Text;

namespace AMVACChemical.ViewModels.TrackAbout.RequestParam.Customers
{
   public class CustomerAllVisibleList
    {
        public int totalRows { get; set; }
        public List<CustomerVM> rows { get; set; }
    }
}

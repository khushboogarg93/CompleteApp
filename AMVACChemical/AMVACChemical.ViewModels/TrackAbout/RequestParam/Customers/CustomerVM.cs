using System;
using System.Collections.Generic;
using System.Text;

namespace AMVACChemical.ViewModels.TrackAbout.RequestParam.Customers
{
  public  class CustomerVM
    {
        public int tId { get; set; }
        public string mId { get; set; }
        public string name { get; set; }
        public string followOnMId { get; set; }
        public string followOnName { get; set; }
    }
}

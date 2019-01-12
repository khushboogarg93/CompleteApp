using System;
using System.Collections.Generic;
using System.Text;

namespace AMVACChemical.ViewModels.TrackAbout.RequestParam.AssetTransferLocation
{
  public  class ListofallLocation
    {
        public int totalRows { get; set; }

        public List<AllListLocationVM> rows { get; set; }
    }
}

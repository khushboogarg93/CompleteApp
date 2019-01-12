using System;
using System.Collections.Generic;
using System.Text;

namespace AMVACChemical.ViewModels.TrackAbout.RequestParam.Fill
{
    public class Tagged
    {
        public Tag tag { get; set; }
        public string timeEntered { get; set; }
        public ProductCode productCode { get; set; }
    }
}

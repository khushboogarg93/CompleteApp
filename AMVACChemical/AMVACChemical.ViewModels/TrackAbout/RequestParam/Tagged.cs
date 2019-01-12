using System;
using System.Collections.Generic;
using System.Text;

namespace AMVACChemical.ViewModels.TrackAbout.RequestParam
{
    public class Tagged
    {
        public Tag tag { get; set; }
        public string serialNumber { get; set; }
        public string timeEntered { get; set; }
        public string direction { get; set; }
        public ProductCode productCode { get; set; }
    }
}

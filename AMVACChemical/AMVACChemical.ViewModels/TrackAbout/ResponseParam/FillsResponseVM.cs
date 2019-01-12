using System;
using System.Collections.Generic;
using System.Text;

namespace AMVACChemical.ViewModels.TrackAbout.ResponseParam
{
    public class FillsResponseVM
    {
        public List<Links> links { get; set; } = new List<Links>();
        public string message { get; set; }
        public List<Records> records { get; set; } = new List<Records>();
        public FillResponseStatus responseStatus { get; set; } = new FillResponseStatus();

    }
}

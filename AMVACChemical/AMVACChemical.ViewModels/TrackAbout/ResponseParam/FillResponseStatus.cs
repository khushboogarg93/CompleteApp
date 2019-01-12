using System;
using System.Collections.Generic;
using System.Text;

namespace AMVACChemical.ViewModels.TrackAbout.ResponseParam
{
    public class FillResponseStatus
    {
        public string errorCode { get; set; }
        public string message { get; set; }
        public string stackTrace { get; set; }
        public List<Errors> errors { get; set; }
    }
}

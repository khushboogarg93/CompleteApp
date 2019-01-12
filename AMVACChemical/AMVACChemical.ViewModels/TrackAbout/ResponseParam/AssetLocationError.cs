using System;
using System.Collections.Generic;
using System.Text;

namespace AMVACChemical.ViewModels.TrackAbout.ResponseParam
{
    public class AssetLocationError
    {
        public string errorCode { get; set; }
        public string message { get; set; }
        public List<AssetErrorsList> errors { get; set; } = new List<AssetErrorsList>();
    }
}

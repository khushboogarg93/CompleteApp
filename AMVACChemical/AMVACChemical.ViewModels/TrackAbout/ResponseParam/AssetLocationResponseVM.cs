using System;
using System.Collections.Generic;
using System.Text;

namespace AMVACChemical.ViewModels.TrackAbout.ResponseParam
{
    public class AssetLocationResponseVM
    {
        public bool isSuccess { get; set; }
        public AssetLocationError responseStatus { get; set; } = new AssetLocationError();
    }
}

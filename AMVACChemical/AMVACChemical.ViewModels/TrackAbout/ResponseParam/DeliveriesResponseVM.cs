using System.Collections.Generic;

namespace AMVACChemical.ViewModels.TrackAbout.ResponseParam
{
    public class DeliveriesResponseVM
    {
        public string message { get; set; }
        public List<Links> links { get; set; } = new List<Links>();    
        public AssetLocationError responseStatus { get; set; } = new AssetLocationError();
    }
}

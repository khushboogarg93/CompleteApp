using System.Collections.Generic;

namespace AMVACChemical.ViewModels.TrackAbout.RequestParam.AssetTransferLocation
{
    public class AssetTransferVM
    {
        public string locationMId { get; set; }
        public string UserEntryStartDate { get; set; }
        public string UserEntryEndDate { get; set; }
        public string useState { get; set; }
        public Assets assets { get; set; } = new Assets();
        public List<DynamicFormEntries> dynamicFormEntries { get; set; } = new List<DynamicFormEntries>();
    }
}

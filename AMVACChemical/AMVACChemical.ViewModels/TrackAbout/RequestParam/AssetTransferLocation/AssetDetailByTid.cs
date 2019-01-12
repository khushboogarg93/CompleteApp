using AMVACChemical.ViewModels.TrackAbout.Assets;
using System.Collections.Generic;

namespace AMVACChemical.ViewModels.TrackAbout.AssetTransferLocation
{
    public class AssetDetailByTid
    {
        public int totalRows { get; set; }
        public List<AssetDetailsVM> rows { get; set; }
    }
}

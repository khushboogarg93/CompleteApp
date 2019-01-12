using System;
using System.Collections.Generic;
using System.Text;

namespace AMVACChemical.ViewModels.TrackAbout.RequestParam.AssetTransferLocation
{
    public class FieldInfo
    {
        public string fieldName { get; set; }
        public string suffixLabel { get; set; }
        public int? customAssetInfoTypeTId { get; set; }
        public bool isVersionedCustomAssetInfo { get; set; }

    }
}

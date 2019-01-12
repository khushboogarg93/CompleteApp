using System;
using System.Collections.Generic;
using System.Text;

namespace AMVACChemical.ViewModels.TrackAbout.RequestParam.AssetTransferLocation
{
    public class DynamicFormEntries
    {
        public int? fieldTId { get; set; }
        public Value value { get; set; } = new Value();
        public FieldInfo fieldInfo { get; set; } = new FieldInfo();
    }
}

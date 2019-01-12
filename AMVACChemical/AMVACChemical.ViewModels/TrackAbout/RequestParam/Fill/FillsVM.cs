using AMVACChemical.ViewModels.TrackAbout.RequestParam.Fill;

namespace AMVACChemical.ViewModels.TrackAbout.RequestParam.Fill
{
    public class FillsVM
    {
        public string userEntryStartDate { get; set; }
        public string userEntryEndDate { get; set; }
        public Location location { get; set; } 
        public Assets assets { get; set; } 
    }
}

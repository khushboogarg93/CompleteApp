namespace AMVACChemical.ViewModels.TrackAbout.RequestParam
{
    public class DeliveriesVM
    {
        public Assets assets { get; set; }
        public Customer customer { get; set; }
        public OriginLocation originLocation { get; set; }
        public string userEntryStartDate { get; set; }
        public string userEntryEndDate { get; set; }
    }
}

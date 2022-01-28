namespace PaidAdsFeedFunctions.Configuration
{
    public class FeedCountryDetailsOptions
    {
        public const string SectionKey = "FeedCountryDetails";

        public string Address { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string Currency { get; set; }
        public string City { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string MileageUnit { get; set; }
    }
}

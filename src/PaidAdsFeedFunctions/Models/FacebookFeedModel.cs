using CsvHelper.Configuration.Attributes;

namespace PaidAdsFeedFunctions.Models
{
    public class FacebookFeedModel
    {
        [Name("vehicle_id")]
        public string VehicleId { get; set; }
        [Name("description")]
        public string Description { get; set; }
        [Name("url")]
        public string Url { get; set; }
        [Name("title")]
        public string Title { get; set; }
        [Name("body_style")]
        public string BodyStyle { get; set; }
        [Name("mileage.unit")]
        public string MileageUnit { get; set; }
        [Name("mileage.value")]
        public string MileageValue { get; set; }

        [Name("price")]
        public string Price { get; set; }

        [Name("sale_price")]
        public string SalePrice { get; set; }

        [Name("address.addr1")]
        public string Address1 { get; set; }
        [Name("address.addr2")]
        public string Address2 { get; set; }
        [Name("address.addr3")]
        public string Address3 { get; set; }
        [Name("address.city")]
        public string AddressCity { get; set; }
        [Name("address.city_id")]
        public string AddressCityId { get; set; }
        [Name("address.region")]
        public string AddressRegion { get; set; }
        [Name("address.postal_code")]
        public string AddressPostalCode { get; set; }
        [Name("address.country")]
        public string AddressCountry { get; set; }
        [Name("address.unit_number")]
        public string AddressUnitNumber { get; set; }

        [Name("latitude")]
        public string Latitude { get; set; }
        [Name("longitude")]
        public string Longitude { get; set; }

        [Name("neighborhood[0]")]
        public string Neighborhood { get; set; }

        [Name("state_of_vehicle")]
        public string StateOfVehicle { get; set; }

        [Name("make")]
        public string Make { get; set; }
        [Name("model")]
        public string Model { get; set; }
        [Name("year")]
        public string Year { get; set; }
        [Name("image[0].url")]
        public string ImageUrl { get; set; }
        [Name("image[0].tag[0]")]
        public string ImageTag { get; set; }
    }
}

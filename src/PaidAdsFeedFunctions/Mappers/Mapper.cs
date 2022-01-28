using System;
using PaidAdsFeedFunctions.Configuration;
using PaidAdsFeedFunctions.Models;

namespace PaidAdsFeedFunctions.Mappers
{
    public static class Mapper
    {
        public static FacebookFeedModel ToFacebookFeed(this VehicleFullDetailsEntity vehicle, FrontendOptions frontendOptions, FeedCountryDetailsOptions countryInfoOptions)
        {
            var feed = new FacebookFeedModel();

            feed.VehicleId = vehicle.Id;
            feed.Description = $"{vehicle.Year} MODEL {vehicle.Make} {vehicle.Model} {vehicle.FuelType} {vehicle.Transmission}";
            feed.Title = feed.Description;
            feed.BodyStyle = GetBodyStyle(vehicle.BodyType);
            feed.MileageUnit = countryInfoOptions.MileageUnit;
            feed.MileageValue = vehicle.Mileage.ToString();

            if (vehicle.PriceBeforeDiscount > 0)
            {
                feed.Price = $"{vehicle.PriceBeforeDiscount} {countryInfoOptions.Currency}";
                feed.SalePrice = $"{vehicle.Price} {countryInfoOptions.Currency}";
            }
            else
            {
                feed.Price = $"{vehicle.Price} {countryInfoOptions.Currency}";
            }

            feed.StateOfVehicle = "Used";

            feed.Address1 = countryInfoOptions.Address;
            feed.AddressCity = countryInfoOptions.City;
            feed.AddressRegion = countryInfoOptions.Country;
            feed.AddressCountry = countryInfoOptions.Country;
            feed.AddressPostalCode = countryInfoOptions.ZipCode;
            feed.Latitude = countryInfoOptions.Latitude;
            feed.Longitude = countryInfoOptions.Longitude;

            feed.Make = vehicle.Make;
            feed.Model = vehicle.Model;
            feed.Year = vehicle.Year.ToString();
            feed.ImageTag = vehicle.Model;

            feed.Url = GenerateFeedUrl(vehicle, frontendOptions);
            feed.ImageUrl = GetImageUrl(vehicle.MainImageUrl);

            return feed;
        }

        public static InstagramFeedModel ToInstagramFeed(this VehicleFullDetailsEntity vehicle, FrontendOptions frontendOptions, FeedCountryDetailsOptions countryInfoOptions)
        {
            var feed = new InstagramFeedModel();

            feed.VehicleId = vehicle.Id;
            feed.Description = $"{vehicle.Year} MODEL {vehicle.Make} {vehicle.Model} {vehicle.FuelType} {vehicle.Transmission}";
            feed.Title = feed.Description;
            feed.BodyStyle = GetBodyStyle(vehicle.BodyType);
            feed.MileageUnit = countryInfoOptions.MileageUnit;
            feed.MileageValue = vehicle.Mileage.ToString();

            if (vehicle.PriceBeforeDiscount > 0)
            {
                feed.Price = $"{vehicle.PriceBeforeDiscount} {countryInfoOptions.Currency}";
                feed.SalePrice = $"{vehicle.Price} {countryInfoOptions.Currency}";
            }
            else
            {
                feed.Price = $"{vehicle.Price} {countryInfoOptions.Currency}";
            }

            feed.StateOfVehicle = "Used";

            feed.Address1 = countryInfoOptions.Address;
            feed.AddressCity = countryInfoOptions.City;
            feed.AddressRegion = countryInfoOptions.Country;
            feed.AddressCountry = countryInfoOptions.Country;
            feed.AddressPostalCode = countryInfoOptions.ZipCode;
            feed.Latitude = countryInfoOptions.Latitude;
            feed.Longitude = countryInfoOptions.Longitude;

            feed.Make = vehicle.Make;
            feed.Model = vehicle.Model;
            feed.Year = vehicle.Year.ToString();

            feed.Url = GenerateFeedUrl(vehicle, frontendOptions);
            feed.ImageUrl = GetImageUrl(vehicle.MainImageUrl);

            feed.Availability = "in stock";
            feed.IdentifierExists = "no";
            feed.GoogleProductCategory = "1267";
            feed.Gtin = "000";
            feed.Mpn = "111";
            feed.Link = feed.Url;

            return feed;
        }

        public static GoogleFeedModel ToGoogleFeed(this VehicleFullDetailsEntity vehicle, FrontendOptions frontendOptions, FeedCountryDetailsOptions countryInfoOptions)
        {
            var feed = new GoogleFeedModel();

            feed.VehicleId = vehicle.Id;
            feed.Description = $"{vehicle.Year} MODEL {vehicle.Make} {vehicle.Model} {vehicle.FuelType} {vehicle.Transmission}";
            feed.ItemTitle = feed.Description;
            feed.ItemSubtitle = " Yanında";
            feed.ItemCategory = GetBodyStyle(vehicle.BodyType);
            if(vehicle.PriceBeforeDiscount > 0)
            {
                feed.Price = $"{vehicle.PriceBeforeDiscount} {countryInfoOptions.Currency}";
                feed.SalePrice = $"{vehicle.Price} {countryInfoOptions.Currency}";
            }
            else
            {
                feed.Price = $"{vehicle.Price} {countryInfoOptions.Currency}";
            }

            feed.ItemAddress = countryInfoOptions.Address;
            feed.ContextualKeywords = $"satılık {feed.ItemCategory}";

            feed.Url = GenerateFeedUrl(vehicle, frontendOptions);
            feed.ImageUrl = GetImageUrl(vehicle.MainImageUrl);

            return feed;
        }

        private static string GenerateFeedUrl(VehicleFullDetailsEntity feed, FrontendOptions frontendOptions)
        {
            var urlString = $"{frontendOptions.BaseUrl}/cars/{feed.Make}/{feed.Model}/{feed.Id}";
            return urlString.ToUrl().ToString();
        }

        private static Uri ToUrl(this string urlString)
        {
            var urlSuccessfullyParsed = Uri.TryCreate(urlString, System.UriKind.Absolute, out var feedUrl);

            if (!urlSuccessfullyParsed)
                throw new UriFormatException($"{urlString} has invalid uri format.");

            return feedUrl;
        }

        private static string GetImageUrl(string mainImageUrl)
        {
            if (string.IsNullOrEmpty(mainImageUrl))
                return string.Empty;

            var mainUrl = mainImageUrl.Replace("documents", "resizedimages");
            var pointExtensionPosition = mainUrl.LastIndexOf('.');
            return $"{mainUrl.Substring(0, pointExtensionPosition)}_vdp_retina{mainUrl.Substring(pointExtensionPosition)}";
        }

        private static string GetBodyStyle(string bodyType)
        {
            var bt = bodyType?.ToUpper();

            switch (bt)
            {
                case "STATION WAGON":
                    return "WAGON";
                case "SUV":
                case "SEDAN":
                case "COUPE":
                case "HATCHBACK":
                    return bt;
                default:
                    return "OTHER";
            }
        }
    }
}

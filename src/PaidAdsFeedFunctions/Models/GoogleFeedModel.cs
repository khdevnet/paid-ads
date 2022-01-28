using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration.Attributes;

namespace PaidAdsFeedFunctions.Models
{
    public class GoogleFeedModel
    {
        [Name("ID")]
        public string VehicleId { get; set; }
        [Name("ID2")]
        public string Id2 { get; set; }
        [Name("Final URL")]
        public string Url { get; set; }
        [Name("Image URL")]
        public string ImageUrl { get; set; }
        [Name("Item title")]
        public string ItemTitle { get; set; }
        [Name("Item category")]
        public string ItemCategory { get; set; }
        [Name("Item subtitle")]
        public string ItemSubtitle { get; set; }
        [Name("Item description")]
        public string Description { get; set; }
        [Name("Price")]
        public string Price { get; set; }
        [Name("Sale price")]
        public string SalePrice { get; set; }
        [Name("Item address (HQ)")]
        public string ItemAddress { get; set; }
        [Name("Contextual keywords")]
        public string ContextualKeywords { get; set; }


        [Name("Custom parameter")]
        public string CustomParametr { get; set; }
        [Name("Final mobile URL")]
        public string MobileUrl { get; set; }
        [Name("Tracking template")]
        public string TrackingTemplate { get; set; }


    }
}

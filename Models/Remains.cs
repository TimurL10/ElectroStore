using System;
using System.Collections.Generic;
using System.Text;

namespace ElectroStore.Models
{
    public class Remains
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public string ItemNameInURL { get; set; }
        public string URL { get; set; }
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }
        public string Visibility { get; set; }
        public string Discount { get; set; }
        public string TegTitle { get; set; }
        public string MetaTegKeywords { get; set; }
        public string MetaTegDescription { get; set; }
        public string CategoryInSite { get; set; }
        public string WeightCoefficient { get; set; }
        public string Currancy { get; set; }
        public string NDS { get; set; }
        public string Measure { get; set; }
        public string Gabarit { get; set; }
        public string ImageURL { get; set; }
        public string VendorCode { get; set; }
        public double PriceRetail { get; set; }
        public int Remain { get; set; }
        public int Weight { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ElectroStore.Models
{
    public class NomenclatureGet
    {
        public int NomenclatureId { get; set; }
        public List<string> Classifiers { get; set; }
        public List<Attribute> Attributes { get; set; }
        public List<string> Metaproperties { get; set; }
        public int FeaturesId { get; set; } //null
        public int FeaturesAttributesId { get; set; } //null
        public List<string> Gallery { get; set; }
        public List<string> Gallery600 { get; set; }
        public List<string> Barcodes { get; set; }
        public string TypeOfProduct { get; set; }
        public string TypeOfProductId { get; set; }
        //public List<Object> propertiesHavingValue { get; set; }
        public List<Attribute> FeaturesAttributes { get; set; }
        public Unit Unit { get; set; }
        public List<Image> Images { get; set; }
        public List<Images600> Images600 { get; set; }
        public Youtube Youtube { get; set; }
        public List<Pack> Packs { get; set; }

        public string CodeTNVED { get; set; }
        //public List<Object> features { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string NameForPrint { get; set; }
        public bool InStock { get; set; }
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int Vat { get; set; }
        public bool RequiredGTD { get; set; }
        public bool AlcoholContaining { get; set; }
        public bool RequiredGISM { get; set; }
        public bool Excisable { get; set; }
        public string BrandId { get; set; }
        public string BrandName { get; set; }
        public string BrandCommercial { get; set; }
        public string ArticulElevel { get; set; }
        public string ManufacturerCode { get; set; }
        public string ManufacturerId { get; set; }
        public string ManufacturerName { get; set; }
        public string Modified { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public int DeliveryTime { get; set; }
        public Weight Weight { get; set; }
        public Volume volume { get; set; }
        public int featureCount { get; set; }
    }
}

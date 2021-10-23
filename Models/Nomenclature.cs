using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ElectroStore.Models
{
    
    public class Nomenclature
    {      
        public int NomenclatureId { get; set; }
        public int ClassFilterId { get; set; } //public List<Object> classifiers { get; set; }
        public int AttributesValueId { get; set; } //public List<Attribute> attributes { get; set; }
        public int MetapropertiesId { get; set; } //public List<Object> metaproperties { get; set; }
        public int FeaturesId { get; set; } //null
        public int featuresAttributesId { get; set; } //null
        public int Gallery { get; set; } //public List<Object> gallery { get; set; }
        public int Gallery600 { get; set; } //public List<Object> gallery600 { get; set; }
        public string Barcodes { get; set; }  //public List<string> barcodes { get; set; }
        public string TypeOfProduct { get; set; }
        public string TypeOfProductId { get; set; }
        //public List<Object> propertiesHavingValue { get; set; }
        //public List<Object> featuresAttributes { get; set; }
        public int Unit { get; set; }  //public Unit unit { get; set; }
        public int ImagesId { get; set; }  //ForeignKey public List<Image> images { get; set; }
        public int Images600Id { get; set; }  //public List<Images600> images600 { get; set; }
        public int YoutubeId { get; set; }  //public Youtube youtube { get; set; }
        public Guid PacksId { get; set; }  //public List<Pack> packs { get; set; }

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
        public DateTime Modified { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public int DeliveryTime { get; set; }
        public Weight Weight { get; set; }
        public Volume volume { get; set; }
        public int featureCount { get; set; }
    }

    public class Unit
    {
        public int NomenclatureId { get; set; }
        public int UnitId { get; set; }
        public string okei { get; set; }
        public string name { get; set; }
        public string fullName { get; set; }
        public string interName { get; set; }
        public int baseUnitCount { get; set; }
        public int unitCount { get; set; }

        public Nomenclature Nomenclature { get; set; }
    }

    public class Image
    {
        [ForeignKey("Nomenclature")]
        public int RefNomenclatureId { get; set; }
        public int id { get; set; }
        public string link { get; set; }

        public Nomenclature Nomenclature { get; set; }
    }

    public class Images600
    {
        [ForeignKey("Nomenclature")]
        public int RefNomenclatureId { get; set; }
        public int id { get; set; }
        public string link { get; set; }

        public Nomenclature Nomenclature { get; set; }

    }

    public class Youtube
    {
        [ForeignKey("Nomenclature")]
        public int RefNomenclatureId { get; set; }
        public int id { get; set; }
        public string code { get; set; }
        public string link { get; set; }
        public string linkHTML { get; set; }

        public Nomenclature Nomenclature { get; set; }

    }

    public class Weight
    {
        public int WeightId { get; set; } //must to be guid
        public int UnitId { get; set; }
        public float unitCount { get; set; }
        public int baseUnitCount { get; set; }
    }

    public class Volume
    {
        [ForeignKey("Nomenclature")]
        public int RefNomenclatureId { get; set; }
        public int id { get; set; }
        public Unit unit { get; set; }
        public double unitCount { get; set; }
        public int baseUnitCount { get; set; }

        public Nomenclature Nomenclature { get; set; }

    }

    public class Pack
    {
        [ForeignKey("Nomenclature")]
        public int RefNomenclatureId { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public int amountInPack { get; set; }
        public double height { get; set; }
        public double width { get; set; }
        public double depth { get; set; }
        public double weight { get; set; }
        public double volume { get; set; }

        public Nomenclature Nomenclature { get; set; }

    }

    public class ValueId
    {
        [ForeignKey("Attribute")]
        public int RefAttributeId { get; set; }
        public int ValueIdKey { get; set; }
        public string id { get; set; }
        public string value { get; set; }

        public Attribute Attribute { get; set; }
    }

    public class Attribute
    {
        [ForeignKey("Nomenclature")]
        public int RefNomenclatureId { get; set; }
        public int id { get; set; }
        public int value { get; set; }
        public string attributeId { get; set; }
        public string name { get; set; }
        public ValueId valueId { get; set; }

        public Nomenclature Nomenclature { get; set; }

    }

    public class RootNomenclature
    {
        public int id { get; set; }
        public List<NomenclatureGet> nomenclatures { get; set; }
    }

    public class AttributesValues
    {
        public int AttributesValuesId { get; set; }
        public int Id { get; set; }
        public string Value { get; set; }
    }
}

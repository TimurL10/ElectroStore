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
        //public List<Object> propertiesHavingValue { get; set; }
        //public List<Object> featuresAttributes { get; set; }
        public Unit unit { get; set; }
        //public List<string> barcodes { get; set; }
        public List<Image> images { get; set; }
        //public List<Object> gallery { get; set; }
        public List<Images600> images600 { get; set; }
        //public List<Object> gallery600 { get; set; }
        public Youtube youtube { get; set; }
        //public List<Object> classifiers { get; set; }
        //public List<Object> features { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string nameForPrint { get; set; }
        public bool inStock { get; set; }
        public string categoryId { get; set; }
        public string categoryName { get; set; }
        public int vat { get; set; }
        public string requiredGTD { get; set; }
        public string alcoholContaining { get; set; }
        public string requiredGISM { get; set; }
        public string excisable { get; set; }
        public string brandId { get; set; }
        public string brandName { get; set; }
        public string brandCommercial { get; set; }
        public string articulElevel { get; set; }
        public string manufacturerCode { get; set; }
        public string manufacturerId { get; set; }
        public string manufacturerName { get; set; }
        public string modified { get; set; }
        public string type { get; set; }
        public string description { get; set; }
        public int deliveryTime { get; set; }
        public Weight weight { get; set; }
        public Volume volume { get; set; }
        public List<Pack> packs { get; set; }
        //public List<Object> metaproperties { get; set; }
        public int featureCount { get; set; }
        public List<Attribute> attributes { get; set; }
    }

    public class Unit
    {
        [ForeignKey("Nomenclature")]
        public int RefNomenclatureId { get; set; }
        public int id { get; set; }
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
        [ForeignKey("Nomenclature")]
        public int RefNomenclatureId { get; set; }
        public int id { get; set; }
        public Unit unit { get; set; }
        public double unitCount { get; set; }
        public int baseUnitCount { get; set; }

        public Nomenclature Nomenclature { get; set; }

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
        public List<Nomenclature> nomenclatures { get; set; }
    }
}

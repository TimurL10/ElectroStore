using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ElectroStore.Models
{
    public class StockOfGoods
    {
        public Guid NomenclaturaId { get; set; }
        public string articul { get; set; }
        public string Id { get; set; }
        public double Price { get; set; }
        public string Name { get; set; }
        public double PriceBasic { get; set; }
        public int  Vat { get; set; }
        public string UnitCode { get; set; }
        public string UnitName { get; set; }
        public int  Unitkratnost { get; set; }
        public int Stock { get; set; }
        public int Stockamount { get; set; }
        public int StockAmountAdd { get; set; }
        public string ShippingDate { get; set; }
        public List<ShippingDateDetails> ShippingDateDetails { get; set; }
        public List<DetailsStruct> StockDetails { get; set; }
        public string Identificator { get; set; }
        public string idCategoria { get; set; }
        public string NameCategoria { get; set; }
    }
   
    public class DetailsStruct
    {
        public int Id { get; set; }
        public bool Main { get; set; }
        public int Amount { get; set; }
        public string Warehouse { get; set; }
        public Guid IdWarehouse { get; set; }
    }

    public class ShippingDateDetails
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }
    }

    public class RootS
    {
        public List<StockOfGoods> stockOfGoods { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ElectroStore.Models
{
    public class Prices
    {
        public string Id { get; set; }
        public string NomenclaturaId { get; set; }
        public string Articul { get; set; }
        public int Price { get; set; }
        public int PriceBasic { get; set; }
        public string Name { get; set; }
        public int Vat { get; set; }
    }

    public class RootPrice
    {
        public List<Prices> PricesList { get; set; }
    }
}

           
using System.Collections.Generic;

namespace ElectroStore.Models
{
    public class pricesOffline
    {
        public string Id {get;set;}
        public int Amount { get; set; }
    }

    public class RootPricesOffline
    {
        public List<pricesOffline> goods = new List<pricesOffline>();

    }




}

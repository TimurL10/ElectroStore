using System;
using System.Collections.Generic;
using System.Text;

namespace ElectroStore.Models
{
    public class GetPricesByItemsId
    {
        public string Id {get;set;}
        public int amount { get; set; }
    }

    public class RootG
    {
        public List<GetPricesByItemsId> goods { get; set; }
    }


}

using System;
using System.Collections.Generic;
using System.Text;

namespace ElectroStore.Models
{
    public class Item
    {
        public double ItemId { get; set; }
        public DateTime Modified { get; set; }
        public double Article { get; set; }
        public int Amount { get; set; }

    }
}

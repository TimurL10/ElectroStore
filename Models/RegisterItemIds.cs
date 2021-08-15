using System;
using System.Collections.Generic;
using System.Text;

namespace ElectroStore.Models
{
    public class RegisterItemIds
    {
        public string Id { get; set; }
        public string Modified { get; set; }
    }

    public class RootR
    {
        public List<RegisterItemIds> Update = new List<RegisterItemIds>();
    }
}

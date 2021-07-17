using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElectroStore.Models
{
    [JsonObject]
    public class GetIdByArticles
    {
        public string id { get; set; }
        public string article { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string BrandId { get; set; }
        public string CategoryId { get; set; }
    }

    public class Root
    {
        public List<GetIdByArticles> result { get; set; }
    }
}

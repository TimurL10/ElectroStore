using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ElectroStore.DAL;
using System.Collections;
using System.Diagnostics;

namespace ElectroStore.Models
{
    public class ApiConnector
    {
        static string Path_one = "http://swop.krokus.ru/ExchangeBase/hs/catalog/getidbyarticles";
        static string Path_two = "http://swop.krokus.ru/ExchangeBase/hs/catalog/stockOfGoods";
        static string Path_reg = "http://swop.krokus.ru/ExchangeBase/hs/catalog/nomenclature?fieldSet=min";
        
        public static void GetIdByArticles(string articles)
        {
            try
            {
                Root desData = new Root();
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Authorization", "Basic 0JPQmtCQ0KHQotCe0KDQmNCv0JvQmNCh0KLQkNCg0J7QntCeNzcyNjQzOTY5NzpGdTZfdHU1anlj");
                    var response = client.PostAsync(Path_one, new StringContent(articles, Encoding.UTF8, "application/json")).Result;
                    var result = response.Content.ReadAsStringAsync().Result;
                    // Десериализация полученного JSON-объекта
                    desData = JsonConvert.DeserializeObject<Root>(result);
                }
                using (var itemContext = new ItemsContext())
                {
                    foreach (var t in desData.result)
                        itemContext.GetIdByArticles.Add(t);
                    itemContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, ex.Source, ex.InnerException);
            }
            
        }
        public static void GetPrices()
        {

            RootS stockOfGoods = new RootS();
            
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Basic 0JPQmtCQ0KHQotCe0KDQmNCv0JvQmNCh0KLQkNCg0J7QntCeNzcyNjQzOTY5NzpGdTZfdHU1anlj");
                var response = client.PostAsync(Path_two, new StringContent(ConvertToJSON(), Encoding.UTF8, "application/json")).Result;
                var result = response.Content.ReadAsStringAsync().Result;
                stockOfGoods = JsonConvert.DeserializeObject<RootS>(result);
            }
            using (var itemContext = new ItemsContext())
            {
                foreach (var t in stockOfGoods.stockOfGoods)
                    itemContext.StockOfGoods.Add(t);
                itemContext.SaveChanges();
            }
        }
        public static string ConvertToJSON()
        {
            RootG rootG = new RootG();
            ExcelOperations _excelOperation = new ExcelOperations();
            ArrayList arrayList = new ArrayList();
            List<GetPricesByItemsId> getPricesBy = new List<GetPricesByItemsId>();
            //GetPricesByItemsId getPrices = new GetPricesByItemsId();
            using (var itemContext = new ItemsContext())
            {
                foreach (var a in itemContext.GetIdByArticles)
                {
                    GetPricesByItemsId getPrices = new GetPricesByItemsId() { amount = 0, Id = a.id};
                    getPricesBy.Add(getPrices);
                }
                rootG.goods = getPricesBy;

            }

            string ArticlesJsonFormat = JsonConvert.SerializeObject(rootG, Formatting.None);
            return ArticlesJsonFormat;
        }

        public static void RegisterItemsId()
        {
            RootR rootR = new RootR();
            RootPrice rootPrice = new RootPrice();

            using (var itemContext = new ItemsContext())
            {
                foreach (var a in itemContext.GetIdByArticles)
                {
                    RegisterItemIds registerItemIds = new RegisterItemIds() { Id = a.id, Modified = DateTime.Now.ToString() };
                    rootR.Update.Add(registerItemIds);
                }
            }

            string registerItemIdsJsonFormat = JsonConvert.SerializeObject(rootR, Formatting.None);
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Basic 0JPQmtCQ0KHQotCe0KDQmNCv0JvQmNCh0KLQkNCg0J7QntCeNzcyNjQzOTY5NzpGdTZfdHU1anlj");
                var response = client.PostAsync(Path_reg, new StringContent(registerItemIdsJsonFormat, Encoding.UTF8, "application/json")).Result;
                var result = response.Content.ReadAsStringAsync().Result;
                rootPrice = JsonConvert.DeserializeObject<RootPrice>(result);
            }

            using (var itemContext = new ItemsContext())
            {
                foreach (var a in rootPrice.PricesList)
                    itemContext.Prices.Add(a);
                itemContext.SaveChanges();

            }

        }   


    }
}

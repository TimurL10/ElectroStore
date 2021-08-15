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
using ElectroStore.Services;
using Microsoft.EntityFrameworkCore;

namespace ElectroStore.Models
{
    public class ApiConnector
    {
        static string Path_one = "http://swop.krokus.ru/ExchangeBase/hs/catalog/getidbyarticles";
        static string Path_two = "http://swop.krokus.ru/ExchangeBase/hs/catalog/stockOfGoods";
        static string Path_reg = "http://swop.krokus.ru/ExchangeBase/hs/catalog/nomenclature?fieldSet=min";
        static string Nomenclature = "http://swop.krokus.ru/ExchangeBase/hs/catalog/nomenclature?fieldSet=max";
        static string GetPricePath = "http://swop.krokus.ru/ExchangeBase/hs/catalog/pricesOffline";
        static IExcelOperation excel;
        public static ExcelOperations ExcelOperations = new ExcelOperations();

        //Get Id by Articles from xslx
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
                    itemContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                    foreach (var t in desData.result)
                    {

                        if (itemContext.GetIdByArticles.Find(t.id) == null)
                            itemContext.GetIdByArticles.Add(t);
                        else
                            itemContext.GetIdByArticles.Update(t);
                    }
                        
                    itemContext.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, ex.Source, ex.InnerException);
            }
            
        }
        //получаем цены по id товара
        public static void GetPrices()
        {           
            RootS stockOfGoods = new RootS();            
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Basic 0JPQmtCQ0KHQotCe0KDQmNCv0JvQmNCh0KLQkNCg0J7QntCeNzcyNjQzOTY5NzpGdTZfdHU1anlj");
                var response = client.PostAsync(Path_two, new StringContent(ConvertItemIdsToJSON(), Encoding.UTF8, "application/json")).Result;
                var result = response.Content.ReadAsStringAsync().Result;
                stockOfGoods = JsonConvert.DeserializeObject<RootS>(result);
            }

            using (var itemContext = new ItemsContext())
            {
                itemContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking; 
                foreach (var t in stockOfGoods.stockOfGoods)
                {
                    if (itemContext.StockOfGoods.Find(t.Id) == null)
                        itemContext.StockOfGoods.Add(t);
                    else
                        itemContext.StockOfGoods.Update(t);
                }
                    
                itemContext.SaveChanges();
            }
        }

        public static string ConvertItemIdsToJSON()
        {
            RootPricesOffline rootG = new RootPricesOffline();
            ExcelOperations _excelOperation = new ExcelOperations();
            ArrayList arrayList = new ArrayList();
            List<pricesOffline> getPricesBy = new List<pricesOffline>();
            using (var itemContext = new ItemsContext())
            {
                foreach (var a in itemContext.GetIdByArticles)
                {
                    pricesOffline getPrices = new pricesOffline() { Id = a.id, Amount = 0};
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

        //получаем номенклатуру по айди
        public static void GetNomenclature()
        {
            int j = 0;
            string kratnostzakaza = "";
            List<StockOfGoods> stockOfGoodsList = new List<StockOfGoods>();
            Random random = new Random();
            List<Remains> remainsList = new List<Remains>();
            RootNomenclature rootNomenclature = new RootNomenclature();
            GetPrice getPrice = new GetPrice();
            int packCount = 0;
            string seriaName = "";
            string manufacture = "";
            string images = "";

            using (var itemContext = new ItemsContext())
            {
                foreach (var a in itemContext.GetIdByArticles)
                    getPrice.ids.Add(a.id);
                foreach (var s in itemContext.StockOfGoods)
                    stockOfGoodsList.Add(s);                
            }
            string IdsJsonFormat = JsonConvert.SerializeObject(getPrice, Formatting.None);

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Basic 0JPQmtCQ0KHQotCe0KDQmNCv0JvQmNCh0KLQkNCg0J7QntCeNzcyNjQzOTY5NzpGdTZfdHU1anlj");
                var responce = client.PostAsync(Nomenclature, new StringContent(IdsJsonFormat, Encoding.UTF8, "application/json")).Result;
                var result = responce.Content.ReadAsStringAsync().Result;
                rootNomenclature = JsonConvert.DeserializeObject<RootNomenclature>(result);
            }

            foreach (var s in rootNomenclature.nomenclatures)
            {
                j++;
                Remains remains = new Remains();
                for (int i = 0; i < s.packs.Count; i++)
                {
                    packCount = s.packs[i].amountInPack;
                }
                for (int i = 0; i < s.attributes.Count; i++)
                {
                    if (s.attributes.Count == 3)
                    {
                        kratnostzakaza = " Кратность заказа: " + s.attributes[0].value + " ";
                        seriaName = s.attributes[1].name + ": " + s.attributes[1].valueId.value;
                        manufacture = s.attributes[2].name + ": " + s.attributes[2].valueId.value;
                        images = s.images600[0].link ??="";
                    }
                    else if (s.attributes.Count == 2)
                    {
                        kratnostzakaza = " Кратность заказа: " + s.attributes[0].value + " ";
                        manufacture = s.attributes[1].name + ": " + s.attributes[1].valueId.value;
                        if (s.images600.Count > 0)
                            images = s.images600[0].link ??="";
                    }                  
                }

                remains.Id = random.Next(123, 99999) + j;
                remains.ItemName = s.name;
                remains.ItemNameInURL = s.manufacturerCode + "_" + stockOfGoodsList.Find(m => m.Id == s.id).idCategoria;
                remains.ShortDescription = s.name;
                remains.FullDescription = s.categoryName + " Кол-во в упаковке: " + packCount + " шт." + " " + kratnostzakaza + seriaName + " " + manufacture;
                remains.Visibility = "выставлен";
                remains.Discount = "";
                remains.TegTitle = "";
                remains.MetaTegKeywords = "";
                remains.MetaTegDescription = "";
                if (stockOfGoodsList.Find(m => m.Id == s.id).idCategoria == "6341")
                    remains.CategoryInSite = "Каталог/Розетки/Legrand";
                remains.WeightCoefficient = "";
                remains.Currancy = "₽";
                remains.NDS = "";
                remains.Measure = "";
                remains.Gabarit = "";
                remains.ImageURL = images;
                remains.VendorCode = stockOfGoodsList.Find(m => m.Id == s.id).articul;
                remains.PriceRetail = stockOfGoodsList.Find(m => m.Id == s.id).PriceBasic;
                remains.Remain = stockOfGoodsList.Find(m => m.Id == s.id).Stockamount;
                remains.Weight = "";
                remains.Param1 = " Кол-во в упаковке: " + packCount + " шт." + " " + kratnostzakaza;
                remains.Param2 = seriaName;
                remains.Param3 = manufacture;
                remainsList.Add(remains);
            }

            //ExcelOperations.PreparePricesToLoad(remainsList);

            using (var itemContext = new ItemsContext())
            {
                itemContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

                foreach (var s in remainsList)
                {
                    if (itemContext.Remains.Find(s.Id) == null)
                        itemContext.Remains.Add(s);
                    else
                        itemContext.Remains.Update(s);
                }
                    
                itemContext.SaveChanges();
            }
        }
        


    }
}

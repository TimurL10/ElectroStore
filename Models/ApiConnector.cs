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
using Microsoft.Extensions.Configuration;

namespace ElectroStore.Models
{
    public class ApiConnector : IApiConnector
    {
        static string Path_one = "http://swop.krokus.ru/ExchangeBase/hs/catalog/getidbyarticles";
        static string Path_two = "http://swop.krokus.ru/ExchangeBase/hs/catalog/stockOfGoods?format=:string";
        static string Path_reg = "http://swop.krokus.ru/ExchangeBase/hs/catalog/nomenclature?fieldSet=min";
        static string Nomenclature = "http://swop.krokus.ru/ExchangeBase/hs/catalog/nomenclature?fieldSet=max";
        static string GetPricePath = "http://swop.krokus.ru/ExchangeBase/hs/catalog/pricesOffline";
        static public IConfiguration config;
        public static ExcelOperations ExcelOperations = new ExcelOperations();
        static public DbRepository DbRepository = new DbRepository();
        static public IDbRepository dbRepository;



        //Получает из Элевел Айди по артикулям взятым из эксель.
        public  void GetIdByArticles(string articles)
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
                    //desData = JsonConvert.DeserializeObject<Root>(result);

                    DbRepository.Dal_UpdateWithNewElevelids(result);
                }
                


            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, ex.Source, ex.InnerException);
            }
            
        }

        //получаем цены по id товара
        public  void GetPrices()
        {           
            RootS stockOfGoods = new RootS();
            var jsonRemains = DbRepository.GetRemainsForPrices();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Basic 0JPQmtCQ0KHQotCe0KDQmNCv0JvQmNCh0KLQkNCg0J7QntCeNzcyNjQzOTY5NzpGdTZfdHU1anlj");
                var response = client.PostAsync(Path_two, new StringContent(jsonRemains, Encoding.UTF8, "application/json")).Result;
                var result = response.Content.ReadAsStringAsync().Result;
                DbRepository.InsertPrice(result);
            }           
        }

        public  void RegisterItemsId()
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

        public  void GetNomenclature()
        {
            var JsonidsList = DbRepository.CheckNewNomenclatureItems();
            var obj = new { ids = JsonidsList };
            var json = JsonConvert.SerializeObject(obj);
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Basic 0JPQmtCQ0KHQotCe0KDQmNCv0JvQmNCh0KLQkNCg0J7QntCeNzcyNjQzOTY5NzpGdTZfdHU1anlj");
                var responce = client.PostAsync(Nomenclature, new StringContent(json, Encoding.UTF8, "application/json")).Result;
                var result = responce.Content.ReadAsStringAsync().Result;
                DbRepository.InsertNomenclature(result);
            }
        }

        public  void GetRemainsForExel()
        {
            var remains = DbRepository.GetRemainsForExel();
            ExcelOperations excelOperations = new ExcelOperations();
            excelOperations.BuildExcelFileWithPrices(remains);

        }
    }
}

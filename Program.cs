using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ElectroStore.Services;
using ElectroStore.Models;
using System.Collections.Generic;
using System.Collections;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ElectroStore
{
    public class Program
    {
        public static ApiConnector ApiConnector;
        
        static void Main(string[] args)
        {
            //ApiConnector.GetIdByArticles(GetArticles());
            ApiConnector.GetPrices();
            //ApiConnector.RegisterItemsId();
           //ApiConnector.GetNomenclature();
           ApiConnector.GetRemainsForExel();
        }

        static public string GetArticles()
        {
            Root root;
            ExcelOperations _excelOperation = new ExcelOperations();
            root.articles = _excelOperation.ReadArticlesFromExcel();
            root.typeOfSearch = "Артикул";
            string ArticlesJsonFormat = JsonConvert.SerializeObject(root, Formatting.None);

            return ArticlesJsonFormat;
        }

        public struct Root
        {
            public List<string> articles;
            public string typeOfSearch;
        }   

            
    }

    public struct Goods
    {
        public string Id;
        public int Amount;
    }
}

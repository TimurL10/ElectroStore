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
using ElectroStore.DAL;

namespace ElectroStore
{
    public class Program
    {
        

        static void Main(string[] args)
        {
            Host.CreateDefaultBuilder()
           .ConfigureServices(ConfigureServices)
           .ConfigureServices(services => services.AddSingleton<Executor>())
           .Build()
           .Services
           .GetService<Executor>()
           .Execute();

            Console.WriteLine("Starting a matrix..");

        }

        private static void ConfigureServices(HostBuilderContext hostContext, IServiceCollection services)
        {
            services.AddSingleton<IApiConnector, ApiConnector>();
            services.AddSingleton<IDbRepository, DbRepository>();

            //services.AddSingleton<IDbContext, DbContext>();
        }


        public class Executor 
        {
            public static IApiConnector _apiConnector;
            public static IDbRepository _dbRepository;

            public Executor(IDbRepository dbRepository, IApiConnector apiConnector)
            {
                _dbRepository = dbRepository;
                _apiConnector = apiConnector;
            }

            public void Execute()
            {
                _apiConnector.GetIdByArticles(GetArticles()); 
                
                // ApiConnector.GetPrices();
                //  ApiConnector.RegisterItemsId();
                // ApiConnector.GetNomenclature();
                // ApiConnector.GetRemainsForExel();
            }

            static public string GetArticles()
            {
                Root root;
                ExcelOperations _excelOperation = new ExcelOperations();
                //root.articles = _excelOperation.ReadArticlesFromExcel(); //Раньше брали из xsl файла.
                root.articles = _dbRepository.GetArticles();
                root.typeOfSearch = "Артикул";
                string ArticlesJsonFormat = JsonConvert.SerializeObject(root, Formatting.None);

                return ArticlesJsonFormat;
            }

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

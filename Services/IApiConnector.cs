using System;
using System.Collections.Generic;
using System.Text;

namespace ElectroStore.Services
{
    public interface IApiConnector
    {
        public  void GetIdByArticles(string articles);
        public  void GetPrices();
        public  void RegisterItemsId();
        public  void GetNomenclature();
        public  void GetRemainsForExel();

    }
}

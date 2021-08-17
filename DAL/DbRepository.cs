using Dapper;
using ElectroStore.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ElectroStore.DAL
{
    public class DbRepository
    {
        private string _configuration;
        private IConfiguration configuration;

        public DbRepository()
        {
            _configuration = "Server = LAPTOP-94EIKF8P\\SQLEXPRESS; Integrated Security = SSPI; Database = ElectroStoreDb1;";
        }

        internal IDbConnection dbConnection
        {
            get
            {
                return new SqlConnection(_configuration);
            }

        }

        public void InsertRemain(Remains remains)
        {
            using (IDbConnection connection = dbConnection)
            {
                connection.Execute(@"Insert Into Remains
                                    ([ItemName],[ItemNameInURL],[URL],[ShortDescription],[FullDescription] ,[Visibility]
                                    ,[Discount],[TegTitle],[MetaTegKeywords],[MetaTegDescription],[CategoryInSite]
                                    ,[WeightCoefficient],[Currancy],[NDS],[Measure],[Gabarit],[ImageURL]
                                    ,[VendorCode],[PriceRetail],[Remain],[Weight],[Param1],[Param2],[Param3])

                                    VALUES(@ItemName, @ItemNameInUrl, @URL, @ShortDescription, @FullDescription, @Visibility,
                                            @Discount, @TegTitle, @MetaTegKeywords,@MetaTegDescription, @CategoryInSite, 
                                            @WeightCoefficient, @Currancy, @NDS, @Measure, @Gabarit,
                                            @ImageURL, @VendorCode, @PriceRetail, @Remain, @Weight, @Param1, @Param2, @Param3)", remains);
            }
        }

        //public void UpdateRemain(Remains remains)
        //{
        //    using (IDbConnection connection = dbConnection)
        //    {
        //        connection.Execute($"Update [Remains] set NNT = {mrcs.Nnt},Price = {mrcs.Price} where NNT = {mrcs.Nnt}", mrcs);
        //    }
        //}

        public List<GetIdByArticles> GetRemainsForPrices()
        {
            using (IDbConnection connection = dbConnection)
            {
                return connection.Query<GetIdByArticles>("select * from [ElectroStoreDb1].[dbo].[GetIdByArticles] where article not in (select vendorcode from[ElectroStoreDb1].[dbo].[Remains])").AsList();
            }
        }
    }
}

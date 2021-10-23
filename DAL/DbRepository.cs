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
            _configuration = "Server = LAPTOP-94EIKF8P\\SQLEXPRESS; Integrated Security = SSPI; Database = 7gostore_db;Connection Timeout=380;";
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

        public string GetRemainsForPrices()
        {
            using (IDbConnection connection = dbConnection)
            {
                return connection.Query<string>("select id,'0' as amount from GetIdByArticles for json path,root('goods')").ToString();
            }
        }

        public void InsertNomenclature(string nomenclatureObj)
        {
            using (IDbConnection connection = dbConnection)
            {
                SqlCommand scCommand = new SqlCommand("InsertNomenclature", (SqlConnection)connection);
                scCommand.CommandType = CommandType.StoredProcedure;
                //scCommand.Parameters.Add("@Email", SqlDbType.Structured).Value = nomenclatures;
                SqlParameter parameter = new SqlParameter();
                scCommand.Parameters.AddWithValue("@nomenclatureObj", nomenclatureObj);
                //parameter.ParameterName = "@value";
                //parameter.DbType = DbType.Boolean;
                //parameter.Direction = ParameterDirection.Output;
                connection.Open();
                scCommand.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void InsertPrice(string Prices)
        {
            using (IDbConnection connection = dbConnection)
            {
                SqlCommand scCommand = new SqlCommand("InsertPrice", (SqlConnection)connection);
                scCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter parameter = new SqlParameter();
                scCommand.Parameters.AddWithValue("@price", Prices);
                //parameter.ParameterName = "@value";
                //parameter.DbType = DbType.Boolean;
                //parameter.Direction = ParameterDirection.Output;
                connection.Open();
                scCommand.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}

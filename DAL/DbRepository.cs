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

        public string GetIdsForNomenclatureReceive()
        {
            string idsList = ""; 
            using (IDbConnection connection = dbConnection)
            {
                IDataReader reader = null;
                SqlCommand scCommand = new SqlCommand("GetIdsForNomenclatureReceive", (SqlConnection)connection);
                scCommand.CommandType = CommandType.StoredProcedure;
                //scCommand.Parameters.Add("@Email", SqlDbType.Structured).Value = nomenclatures;
                SqlParameter parameter = new SqlParameter();
                //Parameters.AddWithValue("@nomenclatureObj", nomenclatureObj);
                parameter.ParameterName = "@idsList";
                //parameter.DbType = DbType.Boolean;
                parameter.Direction = ParameterDirection.Output;
                connection.Open();
                //scCommand.ExecuteNonQuery();
                reader = scCommand.ExecuteReader();
                
                while (reader.Read())
                {
                    idsList = (string)reader[0];
                }
                connection.Close();
            }
            return idsList;

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

        public string GetRemainsForPrices()
        {
            string idsList = "";
            using (IDbConnection connection = dbConnection)
            {
                IDataReader reader = null;
                SqlCommand scCommand = new SqlCommand("GetRemainsForPrices", (SqlConnection)connection);
                scCommand.CommandType = CommandType.StoredProcedure;
                scCommand.Parameters.Add("@json", SqlDbType.NVarChar,50000).Direction = ParameterDirection.Output;
                connection.Open();
                scCommand.ExecuteNonQuery();
                idsList = (string)scCommand.Parameters["@json"].Value;                
                connection.Close();

            }
            return idsList;
        }

        public string CheckNewNomenclatureItems()
        {
            string idsList = "";
            using (IDbConnection connection = dbConnection)
            {
                IDataReader reader = null;
                SqlCommand scCommand = new SqlCommand("CheckNewNomenclatureItems", (SqlConnection)connection);
                scCommand.CommandType = CommandType.StoredProcedure;
                scCommand.Parameters.Add("@idsList", SqlDbType.NVarChar, 50000).Direction = ParameterDirection.Output;
                connection.Open();
                scCommand.ExecuteNonQuery();
                idsList = (string)scCommand.Parameters["@idsList"].Value;
                connection.Close();

            }
            return idsList;
        }
    }
}

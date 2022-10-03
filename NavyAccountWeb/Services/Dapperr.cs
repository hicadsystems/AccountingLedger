using Dapper;
using NavyAccountWeb.IServices;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace NavyAccountWeb.Services
{
    public class Dapperr : IDapper
    {
        private readonly IConfiguration _config;
        private string constring;
        public Dapperr(IConfiguration config)
        {
            _config = config;
            constring = config.GetConnectionString("DefaultConnection");
        }
        public void Dispose()
        {
        }

        public int Execute(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            IDbConnection db = new SqlConnection(constring);
            var response = db.Execute(sp, param: parms, commandType: CommandType.StoredProcedure);
            return response;
        }

        public string ExecuteString(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            IDbConnection db = new SqlConnection(constring);
            var response = db.Query(sp, param: parms, commandType: CommandType.StoredProcedure);
            return response.ToString();
        }
        public T Get<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            IDbConnection db = new SqlConnection(constring);
            return db.Query<T>(sp, parms, commandType: commandType).FirstOrDefault();
        }

        public List<T> GetAll<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            IDbConnection db = new SqlConnection(constring);
            return db.Query<T>(sp, parms, commandType: commandType).ToList();
        }

        public async Task<List<T>> GetAllAsync<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            IDbConnection db = new SqlConnection(constring);
            var result = await db.QueryAsync<T>(sp, parms, commandType: commandType);
            return result.ToList();
        }

        public DbConnection GetDbconnection()
        {
            return new SqlConnection(constring);
        }

        public T Insert<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            T result;
            IDbConnection db = new SqlConnection(constring);
            try
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                var tran = db.BeginTransaction();
                try
                {
                    result = db.Query<T>(sp, parms, commandType: commandType, transaction: tran).FirstOrDefault();
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    var err = ex.Message;
                    tran.Rollback();
                    throw;
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                throw;
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                    db.Close();
            }

            return result;
        }

        public T Update<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            T result;
            IDbConnection db = new SqlConnection(constring);
            try
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                var tran = db.BeginTransaction();
                try
                {
                    result = db.Query<T>(sp, parms, commandType: commandType, transaction: tran).FirstOrDefault();
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    var err = ex.Message;
                    tran.Rollback();
                    throw;
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                throw;
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                    db.Close();
            }

            return result;
        }
    }
}

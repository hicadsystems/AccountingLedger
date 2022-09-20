using Microsoft.Extensions.Configuration;
using NavyAccountCore.Core.Data;
using System.Data.SqlClient;
using System.Data;
using NavyAccountCore.Core.Repositories;
using NavyAccountCore.IRepositories;
using NavyAccountCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NavyAccountCore.Repositories
{
    public class TrialBalanceReportView : Repository<V_TRIALBALANCE>, ITrialBalanceReportView
    {
        private readonly INavyAccountDbContext context;
        private readonly string _connectionstring;

        public TrialBalanceReportView(INavyAccountDbContext context, IConfiguration configuration) : base(context)
        {
            this.context = context;
            _connectionstring = configuration.GetConnectionString("DefaultConnection");
        }

        public IEnumerable<V_TRIALBALANCE> GetTrialBalanceReport()
        {

            var result = context.V_TRIALBALANCEs.ToList();

            return result;
        }

        public IEnumerable<V_TRIALBALANCE> GetTrialBalanceReportProcedure(string fundcode)
        {
            List<V_TRIALBALANCE> dbresult = new List<V_TRIALBALANCE>();

            try
            {
                using (SqlConnection SqlConn = new SqlConnection(_connectionstring))
                {
                    SqlCommand cmd = new SqlCommand("npf_trial_balance", SqlConn)
                    {
                        CommandTimeout = 1200,
                        CommandType = CommandType.StoredProcedure,
                    };

                    SqlParameter fundtype = new SqlParameter
                    {
                        ParameterName = "@fundtype",
                        SqlDbType = SqlDbType.NVarChar,
                        Value = fundcode,
                        Direction = ParameterDirection.Input
                    };

                    SqlParameter report_type = new SqlParameter
                    {
                        ParameterName = "@report_type",
                        SqlDbType = SqlDbType.NVarChar,
                        Value = "1",
                        Direction = ParameterDirection.Input
                    };

                    cmd.Parameters.Add(fundtype);
                    cmd.Parameters.Add(report_type);

                    SqlConn.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();

                    while (sdr.Read())
                    {
                        V_TRIALBALANCE resultset = new V_TRIALBALANCE 
                        {
                            maincode = Convert.ToString(sdr["maincode"]),
                            maindesc = Convert.ToString(sdr["maindesc"]),
                            acctcode = Convert.ToString(sdr["acctcode"]),
                            subdesc = Convert.ToString(sdr["codedesc"]),
                            opbalance = Convert.ToDecimal(sdr["opbalance"]),
                            openDB = Convert.ToDecimal(sdr["openDB"]),
                            openCR = Convert.ToDecimal(sdr["openCR"]),
                            adbbalance = Convert.ToDecimal(sdr["dbbalance"]),
                            crbalance = Convert.ToDecimal(sdr["crbalance"]),
                            amount = Convert.ToDecimal(sdr["amount"]),
                        };

                        dbresult.Add(resultset);
                    }

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return dbresult;
        }

        public IEnumerable<V_TRIALBALANCE> GetBalSheet_MainTrialBalanceProcedure(string fundcode)
        {
            List<V_TRIALBALANCE> dbresult = new List<V_TRIALBALANCE>();

            try
            {
                using (SqlConnection SqlConn = new SqlConnection(_connectionstring))
                {
                    SqlCommand cmd = new SqlCommand("npf_main_trialbalance", SqlConn)
                    {
                        CommandTimeout = 1200,
                        CommandType = CommandType.StoredProcedure,
                    };

                    SqlParameter fundtype = new SqlParameter
                    {
                        ParameterName = "@fundtype",
                        SqlDbType = SqlDbType.NVarChar,
                        Value = fundcode,
                        Direction = ParameterDirection.Input
                    };

                    SqlParameter report_type = new SqlParameter
                    {
                        ParameterName = "@report_type",
                        SqlDbType = SqlDbType.NVarChar,
                        Value = "1",
                        Direction = ParameterDirection.Input
                    };

                    cmd.Parameters.Add(fundtype);
                    cmd.Parameters.Add(report_type);

                    SqlConn.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();

                    while (sdr.Read())
                    {
                        V_TRIALBALANCE resultset = new V_TRIALBALANCE
                        {
                            maincode = Convert.ToString(sdr["maincode"]),
                            maindesc = Convert.ToString(sdr["maindesc"]),
                            acctcode = Convert.ToString(sdr["acctcode"]),
                            subdesc = Convert.ToString(sdr["codedesc"]),
                            opbalance = Convert.ToDecimal(sdr["opbalance"]),
                            openDB = Convert.ToDecimal(sdr["openDB"]),
                            openCR = Convert.ToDecimal(sdr["openCR"]),
                            adbbalance = Convert.ToDecimal(sdr["dbbalance"]),
                            crbalance = Convert.ToDecimal(sdr["crbalance"]),
                            amount = Convert.ToDecimal(sdr["amount"]),
                        };

                        dbresult.Add(resultset);
                    }

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return dbresult;
        }

        public IEnumerable<V_TRIALBALANCE> GetBalSheet_StoredProcedure(string fundcode)
        {
            List<V_TRIALBALANCE> dbresult = new List<V_TRIALBALANCE>();

            try
            {
                using (SqlConnection SqlConn = new SqlConnection(_connectionstring))
                {
                    SqlCommand cmd = new SqlCommand("npf_Balance_Sheet", SqlConn)
                    {
                        CommandTimeout = 1200,
                        CommandType = CommandType.StoredProcedure,
                    };

                    SqlParameter fundtype = new SqlParameter
                    {
                        ParameterName = "@fundtype",
                        SqlDbType = SqlDbType.NVarChar,
                        Value = fundcode,
                        Direction = ParameterDirection.Input
                    };

                    SqlParameter report_type = new SqlParameter
                    {
                        ParameterName = "@report_type",
                        SqlDbType = SqlDbType.NVarChar,
                        Value = "1",
                        Direction = ParameterDirection.Input
                    };

                    cmd.Parameters.Add(fundtype);
                    cmd.Parameters.Add(report_type);

                    SqlConn.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();

                    while (sdr.Read())
                    {
                        V_TRIALBALANCE resultset = new V_TRIALBALANCE
                        {
                            maincode = Convert.ToString(sdr["maincode"]),
                            maindesc = Convert.ToString(sdr["maindesc"]),
                            acctcode = Convert.ToString(sdr["acctcode"]),
                            subdesc = Convert.ToString(sdr["codedesc"]),
                            opbalance = Convert.ToDecimal(sdr["opbalance"]),
                            openDB = Convert.ToDecimal(sdr["openDB"]),
                            openCR = Convert.ToDecimal(sdr["openCR"]),
                            adbbalance = Convert.ToDecimal(sdr["dbbalance"]),
                            crbalance = Convert.ToDecimal(sdr["crbalance"]),
                            amount = Convert.ToDecimal(sdr["amount"]),
                        };

                        dbresult.Add(resultset);
                    }

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return dbresult;
        }

    }
}

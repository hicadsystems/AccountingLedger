using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NavyAccountCore.Core.Data;
using NavyAccountCore.Core.Entities;
using NavyAccountCore.Models;
using NavyAccountWeb.IServices;
using NavyAccountWeb.ViewModels;

namespace NavyAccountWeb.Controllers.Api.Transaction
{
    [Route("api/FinancialDoc")]
    [ApiController]
    public class FinancialDocController : ControllerBase
    {
        private readonly string _connectionstring;
        private readonly IFundTypeCodeService fundTypeService;
        private readonly IFinancialDocService financialDocService;
        private readonly IUnitOfWork unitOfWork;
        public FinancialDocController(IUnitOfWork unitOfWork,IFinancialDocService financialDocService,IConfiguration configuration, IFundTypeCodeService fundTypeService)
        {
            _connectionstring = configuration.GetConnectionString("DefaultConnection");
            this.fundTypeService = fundTypeService;
            this.financialDocService = financialDocService;
            this.unitOfWork = unitOfWork;
        }

        [Route("FinancialDocUpdate")]
        [HttpPut]
        public async Task<IActionResult> PutFinancialDocAsync([FromBody] npf_ledger val)
        {
            
            using (SqlConnection sqls = new SqlConnection(_connectionstring))
            {
                using (SqlCommand cmd = new SqlCommand("npf_approve_loan", sqls))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@globaluser", User.Identity.Name));

                    await sqls.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();

                    return Ok();
                }
            }

        }

        [Route("laodFinanCialDocument")]
        [HttpPost]
        public IActionResult createReceiptOrJournalAsync([FromBody] FinancialDocumentObj value)
        {
            //SqlTransaction trans = null;
            try
            {


                
                using (SqlConnection sqls = new SqlConnection(_connectionstring))
                { 
                    sqls.Open();
                    //trans = sqls.BeginTransaction();
                    using (SqlCommand cmd = new SqlCommand("npf_Update_ledgerCR", sqls))
                    {
                        foreach (var fdoc in value.transactionsCR)
                        {
                            cmd.Parameters.Clear();
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@globaluser", User.Identity.Name));
                            cmd.Parameters.Add(new SqlParameter("@fundcode", value.fundCode));
                            cmd.Parameters.Add(new SqlParameter("@Doctype", value.documentType));
                            cmd.Parameters.Add(new SqlParameter("@Creditcode", fdoc.code));
                            cmd.Parameters.Add(new SqlParameter("@Creditamt", fdoc.creditAmount));
                            cmd.Parameters.Add(new SqlParameter("@Docdate", value.documentDate));
                            cmd.Parameters.Add(new SqlParameter("@docRem", fdoc.remarks));
                            cmd.Parameters.Add(new SqlParameter("@Docno", value.documentNo));
                            cmd.Parameters.Add(new SqlParameter("@Refno", value.referenceNo));
                            cmd.Parameters.Add(new SqlParameter("@batchno", value.documentNo));

                            //sqls.OpenAsync();
                            cmd.ExecuteNonQuery();
                        }

                        //return Ok(new { responseCode = 200, responseDescription = "Created Successfully" });
                    }
                    
                    using (SqlCommand cmd2 = new SqlCommand("npf_Update_ledgerDB", sqls))
                    {
                        
                        foreach (var fdoc in value.transactionsDB)
                        {
                            cmd2.Parameters.Clear();
                            cmd2.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd2.Parameters.Add(new SqlParameter("@globaluser", User.Identity.Name));
                            cmd2.Parameters.Add(new SqlParameter("@fundcode", value.fundCode));
                            cmd2.Parameters.Add(new SqlParameter("@Doctype", value.documentType));
                            cmd2.Parameters.Add(new SqlParameter("@Debitcode", fdoc.code));
                            cmd2.Parameters.Add(new SqlParameter("@Debitamt", fdoc.debitAmount));
                            cmd2.Parameters.Add(new SqlParameter("@Docdate", value.documentDate));
                            cmd2.Parameters.Add(new SqlParameter("@docRem", fdoc.remarks));
                            cmd2.Parameters.Add(new SqlParameter("@Docno", value.documentNo));
                            cmd2.Parameters.Add(new SqlParameter("@Refno", value.referenceNo));
                            cmd2.Parameters.Add(new SqlParameter("@batchno", value.documentNo));

                            //sqls.OpenAsync();
                            cmd2.ExecuteNonQuery();
                        }

                        
                    }

                    //trans.Commit();

                    var getFundType = fundTypeService.GetFundTypeCodeByCode(value.fundCode);
                    if (getFundType != null) {

                        if (value.documentType == "Receipt") {
                            getFundType.receiptno = Int32.Parse(value.documentNo.Substring(1, (value.documentNo.Length-1)));
                        }

                        if (value.documentType == "Journal")
                        {
                            getFundType.jvno = Int32.Parse(value.documentNo.Substring(1, (value.documentNo.Length - 1)));
                        }

                        if (value.documentType == "Payment")
                        {
                            getFundType.paymentno = Int32.Parse(value.documentNo.Substring(1, (value.documentNo.Length - 1)));
                        }
                        _ = fundTypeService.UpdateFundType(getFundType);
                    }
                }
                return Ok(new { responseCode = 200, responseDescription = "Document Captured Successfully" });
            }
            catch (Exception ex)
            {
               
                return Ok(new { responseCode = 500, responseDescription = "Failed" });
            }
        }
        [Route("getAllDocument/{docno}")]
        [HttpGet]
        public IEnumerable<DocumentEnquiryVM> Get(string docno)
        {
            return financialDocService.GetbyFinancialdocByRpt(docno);

        }
        [Route("getAllDocument2/{docno}")]
        [HttpGet]
        public IEnumerable<DocumentEnquiryVM> Get2(string docno)
        {
            return financialDocService.GetbyFinancialdocByRpt2(docno);

        }
        [Route("getAllDocumentReversal/{docno}")]
        [HttpGet]
        public IActionResult GetReverasl(string docno)
        {

            using (SqlConnection sqls = new SqlConnection(_connectionstring))
            {
                using (SqlCommand cmd = new SqlCommand("npf_doc_reversal", sqls))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@globaluser", User.Identity.Name));
                    cmd.Parameters.Add(new SqlParameter("@docno", docno));

                     sqls.OpenAsync();
                     cmd.ExecuteNonQuery();

                   
                }
            }
            return Ok(new { responseCode = 200, responseDescription = "Document Captured Successfully" });
        }
        [Route("getAllRecieptNo")]
        [HttpGet]
        public IEnumerable<DocumentEnquiryVM> GetReciptno()
        {
            return unitOfWork.npfHistories.GetHistoryDistinct();

        }



    }
}
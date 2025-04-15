using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NavyAccountCore.Core.AuditService;
using NavyAccountCore.Core.Data;
using NavyAccountCore.Models;
using NavyAccountWeb.Services;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace NavyAccountWeb.Controllers
{
    [SessionTimeout]
    public class PersonController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public PersonController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public ActionResult CreatePerson(int id=0)
        {
            if (id == 0)
            {
                ViewBag.personid = 0;
            }
            else {
                ViewBag.personid = id;
            }
            //ViewBag.Rank = rank.GetAll(); ;
            return View();
        }
        [HttpGet]
        public ActionResult ViewPerson()
        {
           
            return View();
        }

        [HttpGet]
        public ActionResult InActivePerson()
        {

            return View();
        }

        [HttpGet]
        public IActionResult PersonUpload()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> PersonUpload(IFormFile formFile, CancellationToken cancellationToken)
        {
            try
            {
                if (formFile == null || formFile.Length <= 0)
                {
                    TempData["message"] = "No File Uploaded";
                    //return BadRequest("File not an Excel Format");
                    return View();
                    //return BadRequest("No File Uploaded");
                }

                if (!Path.GetExtension(formFile.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
                {
                    TempData["message"] = "File not an Excel Format";
                    //return BadRequest("File not an Excel Format");
                    return View();
                }
                string user = User.Identity.Name;
                int fundTypeId = (int)HttpContext.Session.GetInt32("fundtypeid");
                string fundTypeCode = HttpContext.Session.GetString("fundtypecode");
                var listapplication = new List<PersonUploadModel>();
                var listapplicationofrecordnotavailable = new List<PersonUploadModel>();

                using (var stream = new MemoryStream())
                {
                    await formFile.CopyToAsync(stream, cancellationToken);

                    using (var package = new ExcelPackage(stream))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets["Sheet1"];
                        var rowCount = worksheet.Dimension.Rows;
                        string SVC_NO = String.IsNullOrEmpty(worksheet.Cells[1, 1].ToString()) ? "" : worksheet.Cells[1, 1].Value.ToString().Trim();
                        string RANK = String.IsNullOrEmpty(worksheet.Cells[1, 2].ToString()) ? "" : worksheet.Cells[1, 2].Value.ToString().Trim();
                        string SURNAME = String.IsNullOrEmpty(worksheet.Cells[1, 3].ToString()) ? "" : worksheet.Cells[1, 3].Value.ToString().Trim();
                        string OTHERNAME = String.IsNullOrEmpty(worksheet.Cells[1, 4].ToString()) ? "" : worksheet.Cells[1, 4].Value.ToString().Trim();
                        string SEX = String.IsNullOrEmpty(worksheet.Cells[1, 5].ToString()) ? "" : worksheet.Cells[1, 5].Value.ToString().Trim();
                        string EMAIL = String.IsNullOrEmpty(worksheet.Cells[1, 6].ToString()) ? "" : worksheet.Cells[1, 6].Value.ToString().Trim();
                        string HOMEADDRESS = String.IsNullOrEmpty(worksheet.Cells[1, 7].ToString()) ? "" : worksheet.Cells[1, 7].Value.ToString().Trim();
                        string BIRTHDATE = String.IsNullOrEmpty(worksheet.Cells[1, 8].ToString()) ? "" : worksheet.Cells[1, 8].Value.ToString().Trim();
                        string DATEEMPLOYED = String.IsNullOrEmpty(worksheet.Cells[1, 9].ToString()) ? "" : worksheet.Cells[1, 9].Value.ToString().Trim();
                        string PHONENUMBER = String.IsNullOrEmpty(worksheet.Cells[1, 10].ToString()) ? "" : worksheet.Cells[1, 10].Value.ToString().Trim();
                        string BANK = String.IsNullOrEmpty(worksheet.Cells[1, 11].ToString()) ? "" : worksheet.Cells[1, 11].Value.ToString().Trim();
                        string ACOUNTNUMBER = String.IsNullOrEmpty(worksheet.Cells[1, 12].ToString()) ? "" : worksheet.Cells[1, 12].Value.ToString().Trim();

                        if (SVC_NO != "SVC_NO" || RANK != "RANK" || SURNAME != "SURNAME" || OTHERNAME != "OTHERNAME")
                        {
                            return BadRequest("File not in the Right format");
                        }
                        for (int row = 2; row <= rowCount; row++)
                        {
                            if (worksheet.Cells[1, 1].Value == null)
                                worksheet.Cells[1, 1].Value = "";

                            if (worksheet.Cells[1, 2].Value == null)
                                worksheet.Cells[1, 2].Value = "";

                            if (worksheet.Cells[1, 3].Value == null)
                                worksheet.Cells[1, 3].Value = "";

                            if (worksheet.Cells[1, 4].Value == null)
                                worksheet.Cells[1, 4].Value = "";
                            if (worksheet.Cells[1, 5].Value == null)
                                worksheet.Cells[1, 5].Value = "";
                            if (worksheet.Cells[1, 6].Value == null)
                                worksheet.Cells[1, 6].Value = "";
                            if (worksheet.Cells[1, 7].Value == null)
                                worksheet.Cells[1, 7].Value = "";
                            if (worksheet.Cells[1, 8].Value == null)
                                worksheet.Cells[1, 8].Value = "";
                            if (worksheet.Cells[1, 9].Value == null)
                                worksheet.Cells[1, 9].Value = "";
                            if (worksheet.Cells[1, 10].Value == null)
                                worksheet.Cells[1, 10].Value = "";
                            if (worksheet.Cells[1, 11].Value == null)
                                worksheet.Cells[1, 11].Value = "";
                            if (worksheet.Cells[1, 12].Value == null)
                                worksheet.Cells[1, 12].Value = "";

                            if (worksheet.Cells[row, 1].Value == null)
                                worksheet.Cells[row, 1].Value = "";

                            if (worksheet.Cells[row, 2].Value == null)
                                worksheet.Cells[row, 2].Value = "";

                            if (worksheet.Cells[row, 3].Value == null)
                                worksheet.Cells[row, 3].Value = "";

                            if (worksheet.Cells[row, 4].Value == null)
                                worksheet.Cells[row, 4].Value = "";

                            if (worksheet.Cells[row, 5].Value == null)
                                worksheet.Cells[row, 5].Value = "";

                            if (worksheet.Cells[row, 6].Value == null)
                                worksheet.Cells[row, 6].Value = "";
                            if (worksheet.Cells[row, 7].Value == null)
                                worksheet.Cells[row, 7].Value = "";

                            if (worksheet.Cells[row, 8].Value == null)
                                worksheet.Cells[row, 8].Value = "";

                            if (worksheet.Cells[row, 9].Value == null)
                                worksheet.Cells[row, 9].Value = "";
                            if (worksheet.Cells[row, 10].Value == null)
                                worksheet.Cells[row, 10].Value = "";
                            if (worksheet.Cells[row, 11].Value == null)
                                worksheet.Cells[row, 11].Value = "";
                            if (worksheet.Cells[row, 12].Value == null)
                                worksheet.Cells[row, 12].Value = "";


                            string svcno1 = String.IsNullOrEmpty(worksheet.Cells[row, 1].Value.ToString()) ? "" : worksheet.Cells[row, 1].Value.ToString().Trim();
                            string rank1 = String.IsNullOrEmpty(worksheet.Cells[row, 2].Value.ToString()) ? "" : worksheet.Cells[row, 2].Value.ToString().Trim();
                            string surname1 = String.IsNullOrEmpty(worksheet.Cells[row, 3].Value.ToString()) ? "" : worksheet.Cells[row, 3].Value.ToString().Trim();
                            string othername1 = String.IsNullOrEmpty(worksheet.Cells[row, 4].Value.ToString()) ? "" : worksheet.Cells[row, 4].Value.ToString().Trim();
                            string sex = String.IsNullOrEmpty(worksheet.Cells[row, 5].Value.ToString()) ? "" : worksheet.Cells[row, 5].Value.ToString().Trim();
                            string email = String.IsNullOrEmpty(worksheet.Cells[row, 6].Value.ToString()) ? "" : worksheet.Cells[row, 6].Value.ToString().Trim();
                            string homeaddress = String.IsNullOrEmpty(worksheet.Cells[row, 7].Value.ToString()) ? "" : worksheet.Cells[row, 7].Value.ToString().Trim();

                            string birthdate = String.IsNullOrEmpty(worksheet.Cells[row, 8].Value.ToString()) ? "" : worksheet.Cells[row, 8].Value.ToString().Trim();
                            string dateemployed = String.IsNullOrEmpty(worksheet.Cells[row, 9].Value.ToString()) ? "" : worksheet.Cells[row, 9].Value.ToString().Trim();
                            string phonenumber = String.IsNullOrEmpty(worksheet.Cells[row, 10].Value.ToString()) ? "" : worksheet.Cells[row, 10].Value.ToString().Trim();
                            string bank = String.IsNullOrEmpty(worksheet.Cells[row, 11].Value.ToString()) ? "" : worksheet.Cells[row, 11].Value.ToString().Trim();

                            string accountnumber = String.IsNullOrEmpty(worksheet.Cells[row, 12].Value.ToString()) ? "" : worksheet.Cells[row, 12].Value.ToString().Trim();


                            if (String.IsNullOrEmpty(worksheet.Cells[row, 1].Value.ToString()) ||
                               String.IsNullOrEmpty(worksheet.Cells[row, 2].Value.ToString()) ||
                               String.IsNullOrEmpty(worksheet.Cells[row, 3].Value.ToString()) ||
                               String.IsNullOrEmpty(worksheet.Cells[row, 4].Value.ToString()) ||
                               String.IsNullOrEmpty(worksheet.Cells[row, 5].Value.ToString()) ||
                               String.IsNullOrEmpty(worksheet.Cells[row, 6].Value.ToString()) ||
                               String.IsNullOrEmpty(worksheet.Cells[row, 7].Value.ToString()) ||
                               String.IsNullOrEmpty(worksheet.Cells[row, 8].Value.ToString()) ||
                               String.IsNullOrEmpty(worksheet.Cells[row, 9].Value.ToString()) ||
                               String.IsNullOrEmpty(worksheet.Cells[row, 10].Value.ToString()))
                            {
                                listapplicationofrecordnotavailable.Add(new PersonUploadModel
                                {
                                    SVC_NO = svcno1,
                                    Rank = rank1,
                                    LastName = surname1,
                                    FirstName = othername1,
                                    HomeAddress=homeaddress,
                                    Email=email,
                                    PhoneNumber=phonenumber,
                                    Sex=sex,
                                    Bank=bank,
                                    AccountNo=accountnumber,
                                    BirthDate=Convert.ToDateTime(birthdate),
                                    DateEmployed =Convert.ToDateTime(dateemployed),

                                });

                            }
                            else
                            {
                                //check if already in the list -- a possibility
                                listapplication.Add(new PersonUploadModel
                                {
                                    SVC_NO = svcno1,
                                    Rank = rank1,
                                    LastName = surname1,
                                    FirstName = othername1,
                                    HomeAddress = homeaddress,
                                    Email = email,
                                    PhoneNumber = phonenumber,
                                    Sex = sex,
                                    Bank = bank,
                                    AccountNo = accountnumber,
                                    BirthDate = Convert.ToDateTime(birthdate),
                                    DateEmployed = Convert.ToDateTime(dateemployed),


                                });
                            }

                        }
                        string userp = User.Identity.Name;

                        PersonUpload procesUpload2 = new PersonUpload(listapplication, _unitOfWork, userp);
                        await procesUpload2.processUploadInThread();
                        TempData["message"] = "Uploaded Successfully";

                    }

                }
                if (listapplicationofrecordnotavailable.Count > 0)
                {

                    var stream = new MemoryStream();

                    using (var package = new ExcelPackage(stream))
                    {
                        var workSheet = package.Workbook.Worksheets.Add("Sheet2");
                        workSheet.Cells.LoadFromCollection(listapplicationofrecordnotavailable, true);
                        package.Save();
                    }
                    stream.Position = 0;
                    string excelName = $"PersonErrorList-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";

                    //return File(stream, "application/octet-stream", excelName);  
                    return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
                }


                return View();
            }
            catch (Exception ex)
            {
                TempData["message"] = "Fail To Uplaod";
                throw;
            }


        }
        [HttpGet]
        public IActionResult UploadChangeSvc()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UploadChangeSvc(IFormFile formFile, CancellationToken cancellationToken)
        {
            try
            {
                if (formFile == null || formFile.Length <= 0)
                {
                    TempData["message"] = "No File Uploaded";
                    //return BadRequest("File not an Excel Format");
                    return View();
                    //return BadRequest("No File Uploaded");
                }

                if (!Path.GetExtension(formFile.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
                {
                    TempData["message"] = "File not an Excel Format";
                    //return BadRequest("File not an Excel Format");
                    return View();
                }
                string user = User.Identity.Name;
                int fundTypeId = (int)HttpContext.Session.GetInt32("fundtypeid");
                string fundTypeCode = HttpContext.Session.GetString("fundtypecode");
                var listapplication = new List<UploadChangeModel>();
                var listapplicationofrecordnotavailable = new List<UploadChangeModel>();

                using (var stream = new MemoryStream())
                {
                    await formFile.CopyToAsync(stream, cancellationToken);

                    using (var package = new ExcelPackage(stream))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets["Sheet1"];
                        var rowCount = worksheet.Dimension.Rows;
                        string OLDSVC_NO = String.IsNullOrEmpty(worksheet.Cells[1, 1].ToString()) ? "" : worksheet.Cells[1, 1].Value.ToString().Trim();
                        string NEWSVC_NO = String.IsNullOrEmpty(worksheet.Cells[1, 2].ToString()) ? "" : worksheet.Cells[1, 2].Value.ToString().Trim();
                        string RANK = String.IsNullOrEmpty(worksheet.Cells[1, 3].ToString()) ? "" : worksheet.Cells[1, 3].Value.ToString().Trim();
 
                        if (OLDSVC_NO != "OLDSVC_NO" || NEWSVC_NO != "NEWSVC_NO" || RANK != "RANK")
                        {
                            return BadRequest("File not in the Right format");
                        }
                        for (int row = 2; row <= rowCount; row++)
                        {
                            if (worksheet.Cells[1, 1].Value == null)
                                worksheet.Cells[1, 1].Value = "";

                            if (worksheet.Cells[1, 2].Value == null)
                                worksheet.Cells[1, 2].Value = "";

                            if (worksheet.Cells[1, 3].Value == null)
                                worksheet.Cells[1, 3].Value = "";

                            

                            if (worksheet.Cells[row, 1].Value == null)
                                worksheet.Cells[row, 1].Value = "";

                            if (worksheet.Cells[row, 2].Value == null)
                                worksheet.Cells[row, 2].Value = "";

                            if (worksheet.Cells[row, 3].Value == null)
                                worksheet.Cells[row, 3].Value = "";

                           

                            string oldsvcno1 = String.IsNullOrEmpty(worksheet.Cells[row, 1].Value.ToString()) ? "" : worksheet.Cells[row, 1].Value.ToString().Trim();
                            string newsvcno = String.IsNullOrEmpty(worksheet.Cells[row, 2].Value.ToString()) ? "" : worksheet.Cells[row, 2].Value.ToString().Trim();
                            string rank = String.IsNullOrEmpty(worksheet.Cells[row, 3].Value.ToString()) ? "" : worksheet.Cells[row, 3].Value.ToString().Trim();
                            

                            if (String.IsNullOrEmpty(worksheet.Cells[row, 1].Value.ToString()) ||
                               String.IsNullOrEmpty(worksheet.Cells[row, 2].Value.ToString()) ||
                               String.IsNullOrEmpty(worksheet.Cells[row, 3].Value.ToString()))
                            {
                                listapplicationofrecordnotavailable.Add(new UploadChangeModel
                                {
                                    OLDSVC_NO = oldsvcno1,
                                    NEWSVC_NO = newsvcno,
                                    RANK = rank,

                                });

                            }
                            else
                            {
                                listapplication.Add(new UploadChangeModel
                                {
                                    OLDSVC_NO = oldsvcno1,
                                    NEWSVC_NO = newsvcno,
                                    RANK = rank,
                                });
                            }

                        }
                        string userp = User.Identity.Name;

                        ChangeUpload procesUpload2 = new ChangeUpload(listapplication, _unitOfWork, userp);
                        await procesUpload2.processUploadInThread2();
                        TempData["message"] = "Uploaded Successfully";

                    }

                }
                if (listapplicationofrecordnotavailable.Count > 0)
                {

                    var stream = new MemoryStream();

                    using (var package = new ExcelPackage(stream))
                    {
                        var workSheet = package.Workbook.Worksheets.Add("Sheet2");
                        workSheet.Cells.LoadFromCollection(listapplicationofrecordnotavailable, true);
                        package.Save();
                    }
                    stream.Position = 0;
                    string excelName = $"PersonErrorList-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";

                    //return File(stream, "application/octet-stream", excelName);  
                    return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
                }


                return View();
            }
            catch (Exception ex)
            {
                TempData["message"] = "Fail To Uplaod";
                throw;
            }


        }

    }
}
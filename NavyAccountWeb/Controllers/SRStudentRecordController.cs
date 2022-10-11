using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NavyAccountCore.Core.Data;
using NavyAccountCore.Models;
using NavyAccountWeb.Services;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MoreLinq;
using System.Threading;
using System.Threading.Tasks;
using Wkhtmltopdf.NetCore;

namespace NavyAccountWeb.Controllers
{
    public class SRStudentRecordController : Controller
    {
        private readonly string _connectionstring;
        private readonly IGeneratePdf generatePdf;
        private readonly IUnitOfWork unitofWork;
        private readonly INavyAccountDbContext context;
        public SRStudentRecordController(INavyAccountDbContext context,IGeneratePdf generatePdf, IConfiguration configuration, IUnitOfWork unitofWork)
        {
            this.generatePdf = generatePdf;
            this.unitofWork = unitofWork;
            this.context = context;
            _connectionstring = configuration.GetConnectionString("DefaultConnection");
        }
        // GET: SRStudentRecordController
        public ActionResult ViewStudent()
        {
            return View();
        }

        // GET: SRStudentRecordController/Details/5
        public ActionResult OldStudent()
        {
            return View();
        }

        // GET: SRStudentRecordController/Create
        public ActionResult Create(int id=0)
        {
            if (id == 0)
            {
                ViewBag.studentid = 0;
            }
            else
            {
                ViewBag.studentid = id;
            }
            return View();
        }

        // POST: SRStudentRecordController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(ViewStudent));
            }
            catch
            {
                return View();
            }
        }

        // GET: SRStudentRecordController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SRStudentRecordController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(ViewStudent));
            }
            catch
            {
                return View();
            }
        }
        public IActionResult studentupload()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> StudentBatchUpload(IFormFile formFile, CancellationToken cancellationToken, string batch)
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
            var listofstudent= new List<StudentRecordVM>();
            var liststudentofrecordnotavailable = new List<StudentRecordVM>();

            using (var stream = new MemoryStream())
            {
                await formFile.CopyToAsync(stream, cancellationToken);

                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets["Sheet1"];
                    var rowCount = worksheet.Dimension.Rows;
                    string REG_NUMBER = String.IsNullOrEmpty(worksheet.Cells[1, 1].ToString()) ? "" : worksheet.Cells[1, 1].Value.ToString().Trim();
                    string SURNAME = String.IsNullOrEmpty(worksheet.Cells[1, 2].ToString()) ? "" : worksheet.Cells[1, 2].Value.ToString().Trim();
                    string FIRSTNAME = String.IsNullOrEmpty(worksheet.Cells[1, 3].ToString()) ? "" : worksheet.Cells[1, 3].Value.ToString().Trim();
                    string MIDDLENAME = String.IsNullOrEmpty(worksheet.Cells[1, 4].ToString()) ? "" : worksheet.Cells[1, 4].Value.ToString().Trim();
                    string SEX = String.IsNullOrEmpty(worksheet.Cells[1, 5].ToString()) ? "" : worksheet.Cells[1, 5].Value.ToString().Trim();
                    string AGE = String.IsNullOrEmpty(worksheet.Cells[1, 6].ToString()) ? "" : worksheet.Cells[1, 6].Value.ToString().Trim();
                    string CLASSCATEGORY = String.IsNullOrEmpty(worksheet.Cells[1, 7].ToString()) ? "" : worksheet.Cells[1, 7].Value.ToString().Trim();
                    string PARENTSTATUS = String.IsNullOrEmpty(worksheet.Cells[1, 8].ToString()) ? "" : worksheet.Cells[1, 8].Value.ToString().Trim();
                    string SCHOOL = String.IsNullOrEmpty(worksheet.Cells[1, 9].ToString()) ? "" : worksheet.Cells[1, 9].Value.ToString().Trim();
                    string COMMENCEMENTDATE = String.IsNullOrEmpty(worksheet.Cells[1, 10].ToString()) ? "" : worksheet.Cells[1, 10].Value.ToString().Trim();
                    string CLASS = String.IsNullOrEmpty(worksheet.Cells[1, 11].ToString()) ? "" : worksheet.Cells[1, 11].Value.ToString().Trim();
                    string PHONENUMBER = String.IsNullOrEmpty(worksheet.Cells[1, 12].ToString()) ? "" : worksheet.Cells[1, 12].Value.ToString().Trim();
                    string EMAIL = String.IsNullOrEmpty(worksheet.Cells[1, 13].ToString()) ? "" : worksheet.Cells[1, 13].Value.ToString().Trim();


                    if (REG_NUMBER != "Reg_Number" || SCHOOL != "SchoolCode" || SURNAME != "Surname")
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
                        if (worksheet.Cells[1, 13].Value == null)
                            worksheet.Cells[1, 13].Value = "";

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
                        if (worksheet.Cells[row, 13].Value == null)
                            worksheet.Cells[row, 13].Value = "";

                        string reg_number = String.IsNullOrEmpty(worksheet.Cells[row, 1].Value.ToString()) ? "" : worksheet.Cells[row, 1].Value.ToString().Trim();
                        string surname = String.IsNullOrEmpty(worksheet.Cells[row, 2].Value.ToString()) ? "" : worksheet.Cells[row, 2].Value.ToString().Trim();
                        string firstname = String.IsNullOrEmpty(worksheet.Cells[row, 3].Value.ToString()) ? "" : worksheet.Cells[row, 3].Value.ToString().Trim();
                        string middlename = String.IsNullOrEmpty(worksheet.Cells[row, 4].Value.ToString()) ? "" : worksheet.Cells[row, 4].Value.ToString().Trim();
                        string sex = String.IsNullOrEmpty(worksheet.Cells[row, 5].Value.ToString()) ? "" : worksheet.Cells[row, 5].Value.ToString().Trim();
                        string age = String.IsNullOrEmpty(worksheet.Cells[row, 6].Value.ToString()) ? "" : worksheet.Cells[row, 6].Value.ToString().Trim();
                        string classcategory = String.IsNullOrEmpty(worksheet.Cells[row, 7].Value.ToString()) ? "" : worksheet.Cells[row, 7].Value.ToString().Trim();
                        string parentstatus = String.IsNullOrEmpty(worksheet.Cells[row, 8].Value.ToString()) ? "" : worksheet.Cells[row, 8].Value.ToString().Trim();
                        string school = String.IsNullOrEmpty(worksheet.Cells[row, 9].Value.ToString()) ? "" : worksheet.Cells[row, 9].Value.ToString().Trim();

                        string commencementdate = String.IsNullOrEmpty(worksheet.Cells[row, 10].Value.ToString()) ? "" : worksheet.Cells[row, 10].Value.ToString().Trim();
                        string class1 = String.IsNullOrEmpty(worksheet.Cells[row, 11].Value.ToString()) ? "" : worksheet.Cells[row, 11].Value.ToString().Trim();
                        string phonenumber = String.IsNullOrEmpty(worksheet.Cells[row, 12].Value.ToString()) ? "" : worksheet.Cells[row, 12].Value.ToString().Trim();
                        string email = String.IsNullOrEmpty(worksheet.Cells[row, 13].Value.ToString()) ? "" : worksheet.Cells[row, 13].Value.ToString().Trim();

                        if (String.IsNullOrEmpty(worksheet.Cells[row, 1].Value.ToString()) ||
                           String.IsNullOrEmpty(worksheet.Cells[row, 2].Value.ToString()) ||
                           String.IsNullOrEmpty(worksheet.Cells[row, 3].Value.ToString()) ||
                           String.IsNullOrEmpty(worksheet.Cells[row, 5].Value.ToString()) ||
                           String.IsNullOrEmpty(worksheet.Cells[row, 6].Value.ToString()) ||
                           String.IsNullOrEmpty(worksheet.Cells[row, 7].Value.ToString()) ||
                           String.IsNullOrEmpty(worksheet.Cells[row, 8].Value.ToString()))
                        {
                            liststudentofrecordnotavailable.Add(new StudentRecordVM
                            {
                                Reg_Number = reg_number,
                                Surname = surname,
                                FirstName = firstname,
                                MiddleName = middlename,
                                Sex = sex,
                                Age = Convert.ToInt32(age),
                                CommencementDate = commencementdate,
                                ClassName = class1,
                                ClassCategory = classcategory,
                                ParentalStatus = parentstatus,
                                SchoolCode = school,
                                PhoneNumber = phonenumber,
                                Email = email
                            });

                        }
                        else
                        {
                            listofstudent.Add(new StudentRecordVM
                            {
                                Reg_Number = reg_number,
                                Surname = surname,
                                FirstName = firstname,
                                MiddleName = middlename,
                                Sex = sex,
                                Age = Convert.ToInt32(age),
                                CommencementDate = commencementdate,
                                ClassName = class1,
                                ClassCategory = classcategory,
                                ParentalStatus=parentstatus,
                                SchoolCode=school,
                                PhoneNumber=phonenumber,
                                Email=email

                            });
                        }

                    }
                    string userp = User.Identity.Name;

                    StudentUpload procesUpload2 = new StudentUpload(listofstudent, unitofWork,context, userp);
                    await procesUpload2.processUploadInThread();
                    TempData["message"] = "Uploaded Successfully";

                }

            }
            if (liststudentofrecordnotavailable.Count > 0)
            {

                var stream = new MemoryStream();

                using (var package = new ExcelPackage(stream))
                {
                    var workSheet = package.Workbook.Worksheets.Add("Sheet2");
                    workSheet.Cells.LoadFromCollection(liststudentofrecordnotavailable, true);
                    package.Save();
                }
                stream.Position = 0;
                string excelName = $"StudentErrorList-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";

                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
            }
            return View();
        }

        // GET: SRStudentRecordController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SRStudentRecordController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(ViewStudent));
            }
            catch
            {
                return View();
            }
        }
    }
}

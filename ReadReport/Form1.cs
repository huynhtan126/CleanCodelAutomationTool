using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ReadReport.JsonReport;

namespace ReadReport
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Report_Click(object sender, EventArgs e)
        {
            ChromeOptions options = new ChromeOptions();
            //options.AddArguments("user-data-dir=/path/to/your/custom/profile");
            options.AddArguments("--user-data-dir=C:\\Users\\huynh\\AppData\\Local\\Google\\Chrome\\User Data");
            options.AddArguments("--profile-directory=Profile 1");
            using (var driver = new ChromeDriver(options))
            {
                var minvalue = int.Parse(min.Value.ToString());
                var maxvalue = int.Parse(max.Value.ToString());
                var pathfolder = System.Reflection.Assembly.GetExecutingAssembly().Location;

                var fileor = new FileInfo(pathfolder);
                pathfolder = fileor.Directory.ToString();
                var thongtinfile = pathfolder + "\\TemplateReport\\TemplateReport.xlsx";
                if (!File.Exists(thongtinfile)) { MessageBox.Show("Not found template file."); return; }
                ExcelPackage package = new ExcelPackage(new FileInfo(thongtinfile));
                ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();
                //ExcelPackage package = new ExcelPackage();
                //ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Report");

                // get number of rows and columns in the sheet
                var cotTangdan = 1;
                var hangTangdan = 10;
                for (int i = minvalue; i <= maxvalue; i++)
                {
                    try
                    {
                        var pathIssue = "https://gitlab.tgl-cloud.com/PrimaSolutions/newcadgrp/newcad/-/issues/" + i;
                        driver.Navigate().GoToUrl(pathIssue +

                     ".json");
                        var json_content = driver.FindElement(By.CssSelector("body > pre")).Text;
                        var json = JsonConvert.DeserializeObject<Root>(json_content);
                        worksheet.Cells[hangTangdan + 1, 1].Value = cotTangdan;
                        cotTangdan++;
                        var danhSachLabel = json.labels;
                        #region Cot thu 2 

                        //lay ra crash bug
                        var listCrashlable = danhSachLabel.Where(x => x.title.ToString().Contains("Crash")).ToList();
                        if (listCrashlable.Count > 0)
                        {
                            worksheet.Cells[hangTangdan + 1, 2].Value = "CrashBug";
                        }

                        var listConectionlabel = danhSachLabel.Where(x => x.title.ToString().StartsWith("C")).ToList();
                        if (listConectionlabel.Count > 0) // neu co
                        {
                            worksheet.Cells[hangTangdan + 1, 2].Value = listConectionlabel[0].title;
                        }

                        var listMainlabel = danhSachLabel.Where(x => x.title.ToString().StartsWith("M")).ToList();
                        if (listMainlabel.Count > 0) // neu co
                        {
                            worksheet.Cells[hangTangdan + 1, 2].Value = listMainlabel[0].title;
                        }

                        var listSystemlabel = danhSachLabel.Where(x => x.title.ToString().StartsWith("S")).ToList();
                        if (listSystemlabel.Count > 0) // neu co
                        {
                            worksheet.Cells[hangTangdan + 1, 2].Value = listSystemlabel[0].title;
                        }

                        var listAPIlabel = danhSachLabel.Where(x => x.title.ToString().StartsWith("A")).ToList();
                        if (listAPIlabel.Count > 0) // neu co
                        {
                            worksheet.Cells[hangTangdan + 1, 2].Value = listAPIlabel[0].title;
                        }
                        #endregion
                        #region Cot thu 3 

                        var listProduct = danhSachLabel.Where(x => x.title.ToString().Contains("Production")).ToList();
                        if (listProduct.Count > 0)
                        {
                            worksheet.Cells[hangTangdan + 1, 3].Value = "Production";
                        }
                        else
                        {
                            worksheet.Cells[hangTangdan + 1, 3].Value = "Staging";

                        }
                        #endregion
                        #region Cot thu 4
                        worksheet.Cells[hangTangdan + 1, 4].Value = json.title;
                        #endregion
                        #region Cot thu 5
                        worksheet.Cells[hangTangdan + 1, 5].Value = "Luôn luôn";
                        #endregion
                        #region Cot thu 6
                        worksheet.Cells[hangTangdan + 1, 6].Value = "Functional";
                        #endregion
                        #region Cot thu 7
                        var listStatus = danhSachLabel.Where(x => x.title.ToString().Contains("_Done") || x.title.ToString().Contains("_Root")).ToList();
                        if (listStatus.Count > 0)
                        {
                            worksheet.Cells[hangTangdan + 1, 7].Value = "Closed";
                        }
                        else
                        {
                            worksheet.Cells[hangTangdan + 1, 7].Value = "Open";

                        }
                        #endregion
                        #region Cot thu 8
                        worksheet.Cells[hangTangdan + 1, 8].Value = "Major";
                        #endregion
                        #region Cot thu 9
                        worksheet.Cells[hangTangdan + 1, 9].Value = "High";
                        #endregion
                        #region Cot thu 10
                        worksheet.Cells[hangTangdan + 1, 10].Value = json.created_at.ToShortDateString();
                        #endregion
                        #region Cot thu 11
                        worksheet.Cells[hangTangdan + 1, 11].Value = "Bảo Thoa";
                        #endregion
                        #region Cot thu 12
                        worksheet.Cells[hangTangdan + 1, 12].Value = "Huỳnh Tân";
                        #endregion
                        #region Cot thu 13
                        worksheet.Cells[hangTangdan + 1, 13].Value = json.assignees[0].name;
                        #endregion
                        #region Cot thu 15
                        worksheet.Cells[hangTangdan + 1, 15].Value = json.updated_at.ToShortDateString();
                        #endregion
                        #region Cot thu 16
                        var formula =
                        "=HYPERLINK(" + @"""" + pathIssue + @"""," + @"""Link" + @"""" + ")";
                        worksheet.Cells[hangTangdan + 1, 16].Formula = formula;
                        #endregion
                        #region Cot thu 17
                        var split = Regex.Split(json.description.ToString(), "Root");
                        if (split.Count() > 1) // nếu có root cause
                        {
                            worksheet.Cells[hangTangdan + 1, 17].Value = "Root " + split[1];
                        }
                        #endregion
                        ////File.WriteAllText("C:\\TGL\\394.html", driver.PageSource);
                        //worksheet.Cells[i + 1, 2].Value = json.assignees[0].name;
                        //worksheet.Cells[i + 1, 3].Value = json.description;
                        //var isRootCause = json.description.ToUpper().Contains("ROOT");
                        //worksheet.Cells[i + 1, 4].Value = isRootCause;
                        //worksheet.Cells[i + 1, 5].Value = json.created_at.ToShortDateString();

                        //worksheet.Cells[i + 1, 5].Value = json.labels[0].title;

                    }
                    catch (Exception ex)
                    {

                    }
                    hangTangdan++;
                }

                #region Save as
                var date = DateTime.Now;
                var thongtinfileSaveAs = pathfolder + "\\TemplReport" + date.ToString("yyyyMMddhhmm") +
                    ".xlsx";
                package.SaveAs(new System.IO.FileInfo(thongtinfileSaveAs));
                #endregion
                #region Mo file bat ky 
                Process.Start(thongtinfileSaveAs);
                #endregion
            }
        }
    }
}

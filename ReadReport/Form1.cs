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
                var thongtinfile = "C:\\TGL\\Report.xlsx";

                ExcelPackage package = new ExcelPackage();
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Report");

                // get number of rows and columns in the sheet

                for (int i = minvalue; i <= maxvalue; i++)
                {
                    try
                    {
                        driver.Navigate().GoToUrl("https://gitlab.tgl-cloud.com/PrimaSolutions/newcadgrp/newcad/-/issues/" +
                     i +
                     ".json");
                        var json_content = driver.FindElement(By.CssSelector("body > pre")).Text;
                        var json = JsonConvert.DeserializeObject<Root>(json_content);
                        File.WriteAllText("C:\\TGL\\394.html", driver.PageSource);
                        worksheet.Cells[i+1,1].Value = "Issue "+i;
                        worksheet.Cells[i+1,2].Value = json.assignees[0].name;
                        worksheet.Cells[i + 1, 3].Value = json.description;
                        var isRootCause = json.description.ToUpper().Contains("ROOT");
                        worksheet.Cells[i + 1, 4].Value = isRootCause;
                        worksheet.Cells[i + 1, 5].Value = json.created_at.ToShortDateString();
                        
                        worksheet.Cells[i+1,5].Value = json.labels[0].title;

                    }
                    catch (Exception ex)
                    {

                    }

                }

                #region Save as

                package.SaveAs(new System.IO.FileInfo(thongtinfile));
                #endregion
                #region Mo file bat ky 
                Process.Start(thongtinfile);
                #endregion
            }
        }
    }
}

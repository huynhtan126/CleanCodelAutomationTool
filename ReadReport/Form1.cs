using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Logical;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Selenium;
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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ReadReport.JsonReport;
using Keys = OpenQA.Selenium.Keys;
using Timer = System.Threading.Timer;

namespace ReadReport
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static void Report_(int min, int max, bool all, bool thoa , bool hoan,string custom)
        {
            ChromeOptions options = new ChromeOptions();
            //options.AddArguments("user-data-dir=/path/to/your/custom/profile");
            //options.setBinary(getChromeLocation());
            //options.AddArguments("--remote-debugging-port=3456");
            //options.AddArguments("--user-data-dir=C:\\Users\\huynh\\AppData\\Local\\Google\\Chrome\\User Data");
            string localAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            options.AddArguments("--user-data-dir=" + localAppDataPath + "\\Chromium\\User Data");
            options.AddArguments("--profile-directory=Profile 2");

            //DefaultSelenium selenium = new DefaultSelenium("localhost", 4444, "*custom path/to/chromium", "www.google.com");
            //ChromeOptions options = new ChromeOptions();
            var chromeBrowserPath = localAppDataPath + @"\Chromium\Application\chrome.exe";
            options.BinaryLocation = chromeBrowserPath; // This tells ChromeDriver where to find Chrome browser

            ChromeDriverService service = ChromeDriverService.CreateDefaultService();
            // You generally don't need to set the port manually unless troubleshooting specific issues
            // service.Port = 3546;

            //selenium.Start();
            using (var driver = new ChromeDriver(service, options))
            {

                var minvalue = min; ;
                var maxvalue = max; ;
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
                    worksheet.Cells[hangTangdan + 1, 1].Value = cotTangdan;
                    cotTangdan++;
                    try
                    {
                        var pathIssue = "https://gitlab.tgl-cloud.com/PrimaSolutions/newcadgrp/newcad/-/issues/" + i;
                        driver.Navigate().GoToUrl(pathIssue +

                     ".json");
                        var json_content = driver.FindElement(By.CssSelector("body > pre")).Text;
                        var json = JsonConvert.DeserializeObject<Root>(json_content);
                        var danhSachLabel = json.labels;
                        if (!all)
                        {

                            if (thoa)
                            {
                                try
                                {
                                    if (json.author_id.ToString() != "73")
                                    {
                                        continue;
                                    }

                                }
                                catch (Exception ex)
                                {
                                    continue;
                                }
                            }
                            if (hoan)
                            {
                                try
                                {
                                    if (json.author_id.ToString() != "230")
                                    {
                                        continue;
                                    }

                                }
                                catch (Exception ex)
                                {
                                    continue;
                                }
                            }

                        }
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
                        var listStatus = danhSachLabel.Where(x => x.title.ToString().Contains("_Done") || x.title.ToString().Contains("_Root") || x.title.ToString().Contains("_Request")).ToList();
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
                        //worksheet.Cells[hangTangdan + 1, 11].Value = "Bảo Thoa";
                        var creator = "Unknow";
                        try
                        {
                            if (json.author_id.ToString() == "73")
                            {
                                creator = "Bảo Thoa";
                            }
                            if (json.author_id.ToString() == "230")
                            {
                                creator = "Khải Hoàn";
                            }
                        }
                        catch (Exception ex)
                        {

                        }

                        worksheet.Cells[hangTangdan + 1, 11].Value = creator;
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
                        #region Cot thu 18
                        {

                            var listConectionlabel1 = danhSachLabel.Where(x => x.title.ToString().ToUpper().Contains("CLIENT")).ToList();
                            if (listConectionlabel1.Count > 0) // neu co
                            {
                                worksheet.Cells[hangTangdan + 1, 18].Value = listConectionlabel1[0].title;
                            }
                        }
                        #endregion
                        #region Cot thu 19
                        {
                            var listPlan = danhSachLabel.Where(x => x.title.ToString().ToUpper().Contains("PLAN")).ToList();
                            if (listPlan.Count > 0)
                            {
                                worksheet.Cells[hangTangdan + 1, 19].Value = listPlan[0].title;
                            }

                        }
                        {
                            var listPlan = danhSachLabel.Where(x => x.title.ToString().ToUpper().Contains("KH")).ToList();
                            if (listPlan.Count > 0)
                            {
                                worksheet.Cells[hangTangdan + 1, 19].Value = listPlan[0].title;
                            }

                        }
                        {
                            var listPlan = danhSachLabel.Where(x => x.title.ToString().ToUpper().Contains("ARES")).ToList();
                            if (listPlan.Count > 0)
                            {
                                worksheet.Cells[hangTangdan + 1, 19].Value = listPlan[0].title;
                            }

                        }
                        {
                            var listPlan = danhSachLabel.Where(x => x.title.ToString().ToUpper().Contains("WAIT FOR CUS")).ToList();
                            if (listPlan.Count > 0)
                            {
                                worksheet.Cells[hangTangdan + 1, 19].Value = listPlan[0].title;
                            }

                        }
                        #endregion
                        #region Cot thu 20
                        {
                            var listPlan = danhSachLabel.Where(x => x.title.ToString().ToUpper().Contains(custom.ToUpper())).ToList();
                            if (listPlan.Count > 0)
                            {
                                worksheet.Cells[hangTangdan + 1, 20].Value = listPlan[0].title;
                            }

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
        private void Report_Click(object sender, EventArgs e)
        {
            Report_(int.Parse(min.Value.ToString()), int.Parse(max.Value.ToString()), radioButton1.Checked, radioButton2.Checked, radioButton3.Checked, textBox1.Text);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            min.Select();
            Timer timer = new Timer(GenerateReport, null, GetTimeUntil(), TimeSpan.FromHours(24));
            Console.WriteLine("Chương trình đang chạy, nhấn Enter để dừng...");
          
        }

        static TimeSpan GetTimeUntil()
        {
            DateTime now = DateTime.Now;
            DateTime next7h05 = new DateTime(now.Year, now.Month, now.Day, 19, 35, 0);

            if (now > next7h05)
                next7h05 = next7h05.AddDays(1); // nếu đã qua 7:05 hôm nay → chọn ngày mai

            return next7h05 - now;
        }
        void GenerateReport(object state)
        {
            Console.WriteLine($"Báo cáo xuất ngày: {DateTime.Now}");
            Report_Click(null,null);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            ChromeOptions options = new ChromeOptions();
            //options.AddArguments("user-data-dir=/path/to/your/custom/profile");
            //options.setBinary(getChromeLocation());
            //options.AddArguments("--remote-debugging-port=3456");
            //options.AddArguments("--user-data-dir=C:\\Users\\huynh\\AppData\\Local\\Google\\Chrome\\User Data");
            string localAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            options.AddArguments("--user-data-dir=" + localAppDataPath + "\\Chromium\\User Data");
            options.AddArguments("--profile-directory=Profile 2");

            //DefaultSelenium selenium = new DefaultSelenium("localhost", 4444, "*custom path/to/chromium", "www.google.com");
            //ChromeOptions options = new ChromeOptions();
            var chromeBrowserPath = localAppDataPath + @"\Chromium\Application\chrome.exe";
            options.BinaryLocation = chromeBrowserPath; // This tells ChromeDriver where to find Chrome browser

            ChromeDriverService service = ChromeDriverService.CreateDefaultService();
            // You generally don't need to set the port manually unless troubleshooting specific issues
            // service.Port = 3546;

            //selenium.Start();
            using (var driver = new ChromeDriver(service, options))
            {

                var minvalue = int.Parse(min.Value.ToString());
                var maxvalue = int.Parse(max.Value.ToString());
                var pathfolder = System.Reflection.Assembly.GetExecutingAssembly().Location;

                for (int i = minvalue; i <= maxvalue; i++)
                {
                    try
                    {


                        var pathIssue = "https://gitlab.tgl-cloud.com/PrimaSolutions/newcadgrp/newcad/-/issues/" + i;
                        driver.Navigate().GoToUrl(pathIssue +

                     ".json");

                        var json_content = driver.FindElement(By.CssSelector("body > pre")).Text;
                        var json = JsonConvert.DeserializeObject<Root>(json_content);
                        var danhSachLabel = json.labels;

                        if (danhSachLabel != null)
                        {
                            var check = danhSachLabel.Where(x => x.title.ToString().ToUpper().Contains(textBox3.Text.ToUpper())).ToList();
                            
                          
                            if (check.Count==1)
                            {
                                continue;
                            }
                            else
                            {
                                
                                driver.Navigate().GoToUrl(pathIssue);
                                driver.FindElement(By.XPath("//span[contains(.,\'Edit\')]")).Click();
                                driver.FindElement(By.XPath("//ul/div/div/div/div[2]/input")).SendKeys(textBox2.Text);
                                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
                                wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));

                                driver.FindElement(By.XPath("//ul/div/div/div/div[2]/input")).SendKeys(Keys.Enter);
                                driver.FindElement(By.XPath("//ul/div/div/div/div[2]/input")).SendKeys(Keys.Enter);
                                driver.FindElement(By.XPath("//ul/div/div/div/div[2]/input")).SendKeys(Keys.Enter);
                                driver.FindElement(By.XPath("//ul/div/div/div/div[2]/input")).SendKeys(Keys.Enter);
                                driver.FindElement(By.XPath("//ul/div/div/div/div[2]/input")).SendKeys(Keys.Enter);
                                driver.FindElement(By.XPath("//ul/div/div/div/div[2]/input")).SendKeys(Keys.Enter);
                                driver.FindElement(By.XPath("//ul/div/div/div/div[2]/input")).SendKeys(Keys.Enter);
                                WebDriverWait wait1 = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
                                wait1.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
                                driver.FindElement(By.XPath("//form/div[2]/div/div")).Click();
                                WebDriverWait wait2 = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
                                wait2.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));

                            }
                        }
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
            }
        }
    }
}

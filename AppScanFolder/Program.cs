using System;
using System.IO;
using System.Linq;
using System.Drawing;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using ZetaLongPaths;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Information;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Logical;

namespace FolderToExcelApp
{
    class Program
    {
        public static int countFolderSuccesses = 0;
        public static int countFolderEmpty = 0;
        public static int countFolderErrors = 0;
        static void Main(string[] args)
        {
            // Thiết lập License cho EPPlus (Bắt buộc từ bản 5.0 trở lên)
            //ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

             countFolderSuccesses = 0;
             countFolderEmpty = 0;
             countFolderErrors = 0;

            Console.InputEncoding = System.Text.Encoding.Unicode;
            Console.OutputEncoding = System.Text.Encoding.Unicode;

            Console.Write("Nhập đường dẫn thư mục: ");
            string inputPath = Console.ReadLine();
  
            if (string.IsNullOrEmpty(inputPath) || !Directory.Exists(inputPath))
            {
                Console.WriteLine("Đường dẫn không tồn tại!");
                return;
            }

            string outputPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SoDoPhanCap.xlsx");

            try
            {
                using (var package = new ExcelPackage())
                {
                    var sheet = package.Workbook.Worksheets.Add("Sơ đồ");

                    // Tiêu đề cột
                    //sheet.Cells[1, 1].Value = "Cấu trúc thư mục (Đỏ = Thư mục trống, Cam là thư mục có file ,Vàng là thư mục bị lỗi)";
                    //sheet.Cells[1, 1].Value = "Cấu trúc thư mục (Đỏ = Chưa làm, Cam là thư mục có file đang điều chỉnh  ,Xanh là thư mục đã pass testcase)";
                    sheet.Cells[1, 1].Value = "フォルダ構成（赤：未着手、オレンジ：修正中、緑：テスト完了）";
                    sheet.Cells[1, 2].Value = "Liên kết mở file/thư mục";
                    using (var range = sheet.Cells[1, 1, 1, 2])
                    {
                        range.Style.Font.Bold = true;
                        range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                    }

                    int currentRow = 2;
                    ZlpDirectoryInfo rootDir = new ZlpDirectoryInfo(inputPath);

                    ExportWithSymbols(rootDir, sheet, ref currentRow, 0, "");

                    sheet.OutLineSummaryBelow = false;
                    sheet.Cells.AutoFitColumns();

                    package.SaveAs(new FileInfo(outputPath));
                    Process.Start(outputPath);
                }
                Console.WriteLine($"\nThành công! Lưu tại: {outputPath}");
            }
            catch (Exception ex) {
                Console.WriteLine($"Lỗi: {ex.Message}"); 
            }
            Console.WriteLine($"Folder Empty : {countFolderEmpty}"); 
            Console.WriteLine($"Folder Error : {countFolderErrors}"); 
            Console.WriteLine($"Folder Success : {countFolderSuccesses}"); 
      
            Console.WriteLine("Nhấn phím bất kỳ để thoát...");
            Console.ReadKey();
        }

        static void ExportWithSymbols(ZlpDirectoryInfo dir, ExcelWorksheet sheet, ref int row, int level, string indent)
        {
            var nameCell = sheet.Cells[row, 1];
            var linkCell = sheet.Cells[row, 2];

            sheet.Row(row).OutlineLevel = level;

            // Cột A: Tên thư mục kèm icon
            nameCell.Value = indent + (level == 0 ? "" : " ") + "📁 " + dir.Name;
            nameCell.Style.Font.Bold = true;

            // Cột B: Hyperlink mở thư mục
            linkCell.Value = "Mở thư mục";
            try
            {
                linkCell.Hyperlink = new Uri(dir.FullName);
            }
            catch (Exception ex)
            {

            }
          
            linkCell.Style.Font.UnderLine = true;
            linkCell.Style.Font.Color.SetColor(Color.Blue);

            bool isEmpty = false;
            bool isError = false;

            
            List<ZlpDirectoryInfo> subDirectories = new List<ZlpDirectoryInfo>();
            List<ZlpFileInfo> files = new List<ZlpFileInfo>();

            try
            {
                subDirectories = dir.GetDirectories().ToList();
                files = dir.GetFiles().ToList();

        

                if (!subDirectories.Any() && !files.Any()) isEmpty = true;
            }
            catch (Exception ex) { 
                Console.WriteLine($"Lỗi: {ex.Message}"); isError = true; 
            }

            if (isError)
            {
                nameCell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                nameCell.Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                nameCell.Style.Font.Color.SetColor(Color.White);
                countFolderErrors++;
            }
            else if(isEmpty)
            {
                nameCell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                nameCell.Style.Fill.BackgroundColor.SetColor(Color.Red);
                nameCell.Style.Font.Color.SetColor(Color.White);
                countFolderEmpty++;
            }
            else
            {
                nameCell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                nameCell.Style.Fill.BackgroundColor.SetColor(Color.Orange);
                nameCell.Style.Font.Color.SetColor(Color.White);
                countFolderSuccesses++;
            }
            row++;

            int totalItems = subDirectories.Count + files.Count;
            int count = 0;

            // Xử lý thư mục con
            foreach (var subDir in subDirectories)
            {
                count++;
                bool isLast = (count == totalItems);
                string marker = isLast ? "└── " : "├── ";
                string childIndent = indent + (level == 0 ? "" : (indent.EndsWith("└── ") ? "    " : "│   "));

                ExportWithSymbols(subDir, sheet, ref row, level + 1, childIndent + marker);
            }

            // Xử lý File
            foreach (var file in files)
            {
                count++;
                bool isLast = (count == totalItems);
                string marker = isLast ? "└── " : "├── ";
                string fileIndent = indent + (level == 0 ? "" : (indent.EndsWith("└── ") ? "    " : "│   "));

                sheet.Row(row).OutlineLevel = level + 1;

                // Cột A: Tên file
                sheet.Cells[row, 1].Value = fileIndent + marker + "📄 " + file.Name;

                // Cột B: Hyperlink mở file
                sheet.Cells[row, 2].Value = "Mở file";
                try
                {
                    sheet.Cells[row, 2].Hyperlink = new Uri(file.FullName);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Lỗi: {ex.Message}");
                }
                sheet.Cells[row, 2].Style.Font.UnderLine = true;
                sheet.Cells[row, 2].Style.Font.Color.SetColor(Color.Blue);

                row++;
            }
        }
    }
}
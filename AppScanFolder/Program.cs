using System;
using System.IO;
using System.Linq;
using System.Drawing; // Thêm thư viện này để dùng Color
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.ComponentModel;

namespace FolderToExcelApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            Console.InputEncoding = System.Text.Encoding.Unicode;
            Console.OutputEncoding = System.Text.Encoding.Unicode;

            Console.Write("Nhập đường dẫn thư mục: ");
            string inputPath = Console.ReadLine();

            if (!Directory.Exists(inputPath)) return;

            string outputPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "SoDoPhanCap.xlsx");

            try
            {
                using (var package = new ExcelPackage())
                {
                    var sheet = package.Workbook.Worksheets.Add("Sơ đồ");

                    sheet.Cells[1, 1].Value = "Cấu trúc thư mục (Đỏ = Thư mục trống)";
                    sheet.Cells[1, 1].Style.Font.Bold = true;

                    int currentRow = 2;
                    DirectoryInfo rootDir = new DirectoryInfo(inputPath);

                    ExportWithSymbols(rootDir, sheet, ref currentRow, 0, "");

                    sheet.OutLineSummaryBelow = false;
                    sheet.Cells.AutoFitColumns();
                    package.SaveAs(new FileInfo(outputPath));
                }
                Console.WriteLine($"\nThành công! Lưu tại: {outputPath}");
            }
            catch (Exception ex) { Console.WriteLine($"Lỗi: {ex.Message}"); }
            Console.ReadKey();
        }

        static void ExportWithSymbols(DirectoryInfo dir, ExcelWorksheet sheet, ref int row, int level, string indent)
        {
            var currentCell = sheet.Cells[row, 1];
            sheet.Row(row).OutlineLevel = level;
            currentCell.Value = indent + (level == 0 ? "" : " ") + "📁 " + dir.Name;
            currentCell.Style.Font.Bold = true;

            bool isEmpty = false;
            var subDirectories = new System.Collections.Generic.List<DirectoryInfo>();
            var files = new System.Collections.Generic.List<FileInfo>();

            try
            {
                subDirectories = dir.GetDirectories().ToList();
                files = dir.GetFiles().ToList();

                // Kiểm tra nếu thư mục không có cả folder con và file
                if (!subDirectories.Any() && !files.Any())
                {
                    isEmpty = true;
                }
            }
            catch (Exception ex) { Console.WriteLine($"Lỗi: {ex.Message}"); }

            // Nếu trống thì fill màu đỏ
            if (isEmpty)
            {
                currentCell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                currentCell.Style.Fill.BackgroundColor.SetColor(Color.Red);
                currentCell.Style.Font.Color.SetColor(Color.White); // Chuyển chữ trắng cho dễ nhìn trên nền đỏ
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
                // Ghi đệ quy thư mục con
                ExportWithSymbols(subDir, sheet, ref row, level + 1, indent + (level == 0 ? "" : (indent.EndsWith("└── ") ? "    " : "│   ")) + marker);
            }

            // Xử lý File
            foreach (var file in files)
            {
                count++;
                bool isLast = (count == totalItems);
                string marker = isLast ? "└── " : "├── ";

                sheet.Row(row).OutlineLevel = level + 1;
                sheet.Cells[row, 1].Value = indent + (level == 0 ? "" : (indent.EndsWith("└── ") ? "    " : "│   ")) + marker + "📄 " + file.Name;
                row++;
            }
        }
    }
}
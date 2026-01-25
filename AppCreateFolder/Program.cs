using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using OfficeOpenXml; // Thư viện EPPlus
using ZetaLongPaths;

namespace AppCreateFolder
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1. Lấy đường dẫn thư mục nơi file .exe đang chạy
            string assemblyPath = AppDomain.CurrentDomain.BaseDirectory;

            Console.WriteLine($"Thư mục Assembly: {assemblyPath}");

            // 2. Tìm file .xlsx đầu tiên trong thư mục đó
            DirectoryInfo d = new DirectoryInfo(assemblyPath);
            FileInfo excelFile = d.GetFiles("*.xlsx").FirstOrDefault();

            if (excelFile != null)
            {
                string excelFilePath = excelFile.FullName;
                Console.WriteLine($"Đã tìm thấy file Excel: {excelFile.Name}");

                Console.InputEncoding = System.Text.Encoding.Unicode;
                Console.OutputEncoding = System.Text.Encoding.Unicode;

                Console.Write("Nhập đường dẫn thư mục: ");
                string inputPath = Console.ReadLine();
                // Gọi hàm xử lý Excel của bạn ở đây
                ProcessExcel(excelFilePath, inputPath);
            }
            else
            {
                Console.WriteLine("Lỗi: Không tìm thấy file .xlsx nào trong thư mục assembly!");
            }

            Console.ReadKey();
        }

        static void ProcessExcel(string excelFilePath, string rootDirectory)
        {
            // 1. Đường dẫn file Excel và thư mục gốc muốn tạo folder con
            //string excelFilePath = @"C:\Users\Admin\Desktop\DanhSach.xlsx";
            //string rootDirectory = @"G:\.shortcut-targets-by-id\1j30Cjkw2rMmDc38p56T1kIubX_PRuu9G\0043_AIHoldings\01_NewCAD\08.Training new member\5.Folder Structure\Organizations\AiHoldings\AiCad-sf\TL_Document_NEW\Version1.0.2501.33";

            // Đảm bảo thư mục gốc tồn tại
            if (!Directory.Exists(rootDirectory))
            {
                Directory.CreateDirectory(rootDirectory);
            }

            try
            {
                FileInfo fileInfo = new FileInfo(excelFilePath);

                // 2. Cấu hình EPPlus để đọc file
                using (ExcelPackage package = new ExcelPackage(fileInfo))
                {
                    // Lấy Sheet đầu tiên (index trong EPPlus 4 bắt đầu từ 1)
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                    List<string> excelItems = new List<string>();
                    int rowCount = worksheet.Dimension.Rows; // Tổng số hàng có dữ liệu
                    int colIndex = 1; // Giả sử giá trị nằm ở cột 1

                    Console.WriteLine($"Bắt đầu đọc {rowCount} hàng...");

                    for (int row = 1; row <= rowCount; row++)
                    {
                        try
                        {



                            // Lấy giá trị ô, xóa khoảng trắng thừa
                            var cellValue = worksheet.Cells[row, colIndex].Value?.ToString()?.Trim();

                            if (!string.IsNullOrEmpty(cellValue))
                            {
                                // Loại bỏ các ký tự đặc biệt không hợp lệ trong tên folder (như ?, :, *, v.v.)
                                //string safeFolderName = string.Join("_", cellValue.Split(Path.GetInvalidFileNameChars()));
                                string safeFolderName = RemoveSpecialCharacters(cellValue);
                                safeFolderName = safeFolderName.Replace(" ", string.Empty);
                                string newFolderPath = Path.Combine(rootDirectory, safeFolderName);
                                excelItems.Add(safeFolderName);
                                // 3. Tiến hành tạo folder
                                if (!Directory.Exists(newFolderPath))
                                {
                                    var folder = new ZlpDirectoryInfo(newFolderPath);
                                    folder.Create();
                                    //Directory.CreateDirectory(newFolderPath);
                                    Console.WriteLine($"Đã tạo: {safeFolderName}");
                                }
                                else
                                {
                                    Console.WriteLine($"Bỏ qua: {safeFolderName} (Đã tồn tại)");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Lỗi: " + ex.Message);
                        }
                    }


                    // 2. Lấy danh sách folder con hiện có thực tế
                    var existingFolders = Directory.GetDirectories(rootDirectory)
                                                   .Select(path => Path.GetFileName(path))
                                                   .ToList();

                    // 3. So sánh đối chiếu
                    var missingFolders = excelItems.Except(existingFolders).ToList(); // Có trong Excel nhưng thiếu folder
                    var extraFolders = existingFolders.Except(excelItems).ToList();   // Có folder nhưng không có trong Excel
                    var matchedFolders = excelItems.Intersect(existingFolders).ToList(); // Khớp cả hai

                    // 4. Xuất báo cáo
                    Console.WriteLine("--- BÁO CÁO KIỂM TRA ---");
                    Console.WriteLine($"Tổng số hàng trong Excel: {excelItems.Count}");
                    Console.WriteLine($"Tổng số folder con hiện có: {existingFolders.Count}");
                    Console.WriteLine("------------------------");

                    Console.WriteLine($"V Khớp hoàn toàn: {matchedFolders.Count}");

                    if (missingFolders.Any())
                    {
                        Console.WriteLine($"X Thiếu (Chưa tạo): {missingFolders.Count}");
                        missingFolders.ForEach(f => Console.WriteLine($"   - Thiếu: {f}"));
                    }

                    if (extraFolders.Any())
                    {
                        Console.WriteLine($"! Dư thừa (Không có trong Excel): {extraFolders.Count}");
                        extraFolders.ForEach(f => Console.WriteLine($"   - Dư: {f}"));
                    }

                    if (!missingFolders.Any() && !extraFolders.Any())
                    {
                        Console.WriteLine("=> KẾT QUẢ: Hoàn hảo! Folder và Excel khớp 100%.");
                    }
                }
                Console.WriteLine("Hoàn thành!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi: " + ex.Message);
            }

            Console.ReadKey();
        }

        public static string RemoveSpecialCharacters(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;

            // 1. Giữ lại chữ, số (Latinh & Nhật), gạch dưới, khoảng trắng.
            // 2. Loại bỏ các loại dấu ngoặc và ký tự đặc biệt khác.

            // Giải thích Pattern:
            // a-zA-Z0-9\s_ : Cơ bản
            // \u3040-\u309F\u30A0-\u30FF\u4E00-\u9FBF : Chữ Nhật (Hiragana, Katakana, Kanji)
            // \uFF10-\uFF19 : Số Full-width (０-９)
            // \uFF21-\uFF3A\uFF41-\uFF5A : Chữ cái Full-width (Ａ-Ｚ, ａ-ｚ)
            // \u3300-\u33FF : Các ký hiệu đơn vị (㎜, ㎝, ㎏...)

            string pattern = @"[^a-zA-Z0-9\s_\u3040-\u309F\u30A0-\u30FF\u4E00-\u9FBF\uFF10-\uFF19\uFF21-\uFF3A\uFF41-\uFF5A\u3300-\u33FF]";

            return Regex.Replace(input, pattern, "");
        }
    }
}
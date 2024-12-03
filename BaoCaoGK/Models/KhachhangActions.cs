using OfficeOpenXml;

namespace BaoCaoGK.Models
{
    public class KhachhangActions
    {
        private string filePath = @"D:\BaiTapASP\BaoCaoGK\BaoCaoGK\Datas\game1.xlsx";
        private FileInfo GetFileExcel()
        {
            return new FileInfo(filePath);
        }

        // Lấy tất cả các Khách hàng (GetAll)
        public List<Khachhang> GetAll()
        {
            var ds_kh = new List<Khachhang>();

            // Thiết lập LicenseContext
            //ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            OfficeOpenXml.ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            var file_excel = GetFileExcel();

            using (var package = new ExcelPackage(file_excel))
            {
                var worksheet = package.Workbook.Worksheets[0]; // Lấy về sheet đầu tiên
                var rowCount = worksheet.Dimension.Rows; // Đếm số dòng trong excel (có dữ liệu
                var colCount = worksheet.Dimension.Columns;

                for (int row = 2; row <= rowCount; row++) // Mỗi một dong trong danh là một khách hàng
                {
                    // Ex Row Data: 0 |   1   |    2       | 3  | ....
                    // Ex Row Data: 1 | 11234 | Nguyễn Văn | An | ....
                    Khachhang kh = new Khachhang();
                    kh.Tt = Int32.Parse(worksheet.Cells[row, 1].Text);
                    kh.Hodem = worksheet.Cells[row, 2].Text;
                    kh.Ten = worksheet.Cells[row, 3].Text;
                    kh.Tk = worksheet.Cells[row, 4].Text;
                    kh.Mk = int.Parse(worksheet.Cells[row, 5].Text);
                    kh.Sogio = double.Parse(worksheet.Cells[row, 6].Text);

                    ds_kh.Add(kh);
                }
            }

            return ds_kh;
        }

        // Lấy thông chi tiết của một khách hàng (GetByID)
        public Khachhang GetByID(int id)
        {
            var file_excel = GetFileExcel();
            Khachhang kh = null;

            using (var package = new ExcelPackage(file_excel))
            {
                var worksheet = package.Workbook.Worksheets[0];
                var rowCount = worksheet.Dimension.Rows;

                for (int row = 2; row <= rowCount; row++)
                {
                    if (Int32.Parse(worksheet.Cells[row, 1].Text) == id) // Kiểm tra ID
                    {
                        kh = new Khachhang
                        {
                            Tt = id,
                            Hodem = worksheet.Cells[row, 3].Text,
                            Ten = worksheet.Cells[row, 4].Text,
                        };
                        break; // Dừng vòng lặp khi tìm thấy khách hàng
                    }
                }
            }

            return kh; // Trả về khách hàng hoặc null nếu không tìm thấy
        }

        // Thêm (Add)
        public void Add(Khachhang kh)
        {
            var file_excel = GetFileExcel(); // Sử dụng phương thức để lấy FileInfo

            using (var package = new ExcelPackage(file_excel))
            {
                var worksheet = package.Workbook.Worksheets[0];
                var rowCount = worksheet.Dimension.Rows;

                // Thêm khách hàng mới vào hàng tiếp theo
                worksheet.Cells[rowCount + 1, 1].Value = kh.Tt;
                worksheet.Cells[rowCount + 1, 2].Value = kh.Hodem;
                worksheet.Cells[rowCount + 1, 3].Value = kh.Ten;
                worksheet.Cells[rowCount + 1, 4].Value = kh.Tk;
                worksheet.Cells[rowCount + 1, 5].Value = kh.Mk;
                worksheet.Cells[rowCount + 1, 6].Value = kh.Sogio;

                package.Save(); // Lưu thay đổi vào tệp
            }
        }

        // Cập nhật (UpdateByID)
        public void Update(Khachhang kh)
        {
            var file_excel = GetFileExcel(); // Sử dụng phương thức để lấy FileInfo

            using (var package = new ExcelPackage(file_excel))
            {
                var worksheet = package.Workbook.Worksheets[0];
                var rowCount = worksheet.Dimension.Rows;

                for (int row = 2; row <= rowCount; row++)
                {
                    if (Int32.Parse(worksheet.Cells[row, 1].Text) == kh.Tt) // Kiểm tra ID
                    {
                        // Cập nhật thông tin khach hang
                        worksheet.Cells[row, 2].Value = kh.Hodem;
                        worksheet.Cells[row, 3].Value = kh.Ten;
                        worksheet.Cells[row, 4].Value = kh.Tk;
                        worksheet.Cells[row, 5].Value = kh.Mk;
                        worksheet.Cells[row, 6].Value = kh.Sogio;


                        break; // Dừng vòng lặp khi đã cập nhật
                    }
                }

                package.Save(); // Lưu thay đổi vào tệp
            }
        }

        // Xóa tất cả (DeleteAll)
        public void DeleteAll()
        {
            var file_excel = GetFileExcel(); // Sử dụng phương thức để lấy FileInfo

            using (var package = new ExcelPackage(file_excel))
            {
                var worksheet = package.Workbook.Worksheets[0];
                var rowCount = worksheet.Dimension.Rows;

                // Xóa tất cả hàng từ hàng 2 trở đi
                for (int row = 2; row <= rowCount; row++)
                {
                    worksheet.DeleteRow(2); // Luôn xóa hàng thứ 2 cho đến khi không còn hàng
                }

                package.Save(); // Lưu thay đổi vào tệp
            }
        }

        // Xóa một khách hàng (DeleteByID)
        public void DeleteByID(int id)
        {
            var file_excel = GetFileExcel(); // Sử dụng phương thức để lấy FileInfo

            using (var package = new ExcelPackage(file_excel))
            {
                var worksheet = package.Workbook.Worksheets[0];
                var rowCount = worksheet.Dimension.Rows;

                for (int row = 2; row <= rowCount; row++)
                {
                    if (Int32.Parse(worksheet.Cells[row, 1].Text) == id) // Kiểm tra ID
                    {
                        worksheet.DeleteRow(row); // Xóa hàng khách hàng
                        break; // Dừng vòng lặp khi đã xóa
                    }
                }

                package.Save(); // Lưu thay đổi vào tệp
            }
        }
    }
}

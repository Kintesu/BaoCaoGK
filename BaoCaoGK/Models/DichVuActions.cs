using OfficeOpenXml;

namespace BaoCaoGK.Models
{
    public class DichVuActions
    {
        private string filePath = @"D:\BaiTapASP\BaoCaoGK\BaoCaoGK\Datas\dichvu.xlsx";
        private FileInfo GetFileExcel()
        {
            return new FileInfo(filePath);
        }

        // Lấy tất cả các dịch vụ (GetAll)
        public List<DichVu> GetAll()
        {
            var ds_dv = new List<DichVu>();

            // Thiết lập LicenseContext
            //ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            OfficeOpenXml.ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            var file_excel = GetFileExcel();

            using (var package = new ExcelPackage(file_excel))
            {
                var worksheet = package.Workbook.Worksheets[0]; // Lấy về sheet đầu tiên
                var rowCount = worksheet.Dimension.Rows; // Đếm số dòng trong excel (có dữ liệu
                var colCount = worksheet.Dimension.Columns;

                for (int row = 2; row <= rowCount; row++) // Mỗi một dong trong danh là một dịch vụ
                {
                    // Ex Row Data: 0 |   1   |    2       | 3  | ....
                    // Ex Row Data: 1 | 11234 | Nguyễn Văn | An | ....
                    DichVu dv = new DichVu();
                    dv.Stt = Int32.Parse(worksheet.Cells[row, 1].Text);
                    dv.Loaisp = worksheet.Cells[row, 2].Text;
                    dv.Tensp = worksheet.Cells[row, 3].Text;
                    dv.Soluong = double.Parse(worksheet.Cells[row, 4].Text);
                    dv.Giasp = double.Parse(worksheet.Cells[row, 5].Text);

                    ds_dv.Add(dv);
                }
            }

            return ds_dv;
        }

        // Lấy thông chi tiết của một dịch vụ (GetByID)
        public DichVu GetByID(int id)
        {
            var file_excel = GetFileExcel();
            DichVu dv = null;

            using (var package = new ExcelPackage(file_excel))
            {
                var worksheet = package.Workbook.Worksheets[0];
                var rowCount = worksheet.Dimension.Rows;

                for (int row = 2; row <= rowCount; row++)
                {
                    if (Int32.Parse(worksheet.Cells[row, 1].Text) == id) // Kiểm tra ID
                    {
                        dv = new DichVu
                        {
                            Stt = id,
                            Loaisp = worksheet.Cells[row, 3].Text,
                            Tensp = worksheet.Cells[row, 4].Text,
                        };
                        break; // Dừng vòng lặp dvi tìm thấy dịch vụ
                    }
                }
            }

            return dv; // Trả về dịch vụ hoặc null nếu dvông tìm thấy
        }

        // Thêm (Add)
        public void Add(DichVu dv)
        {
            var file_excel = GetFileExcel(); // Sử dụng phương thức để lấy FileInfo

            using (var package = new ExcelPackage(file_excel))
            {
                var worksheet = package.Workbook.Worksheets[0];
                var rowCount = worksheet.Dimension.Rows;

                // Thêm dịch vụ mới vào hàng tiếp theo
                worksheet.Cells[rowCount + 1, 1].Value = dv.Stt;
                worksheet.Cells[rowCount + 1, 2].Value = dv.Loaisp;
                worksheet.Cells[rowCount + 1, 3].Value = dv.Tensp;
                worksheet.Cells[rowCount + 1, 4].Value = dv.Soluong;
                worksheet.Cells[rowCount + 1, 5].Value = dv.Giasp;

                package.Save(); // Lưu thay đổi vào tệp
            }
        }

        // Cập nhật (UpdateByID)
        public void Update(DichVu dv)
        {
            var file_excel = GetFileExcel(); // Sử dụng phương thức để lấy FileInfo

            using (var package = new ExcelPackage(file_excel))
            {
                var worksheet = package.Workbook.Worksheets[0];
                var rowCount = worksheet.Dimension.Rows;

                for (int row = 2; row <= rowCount; row++)
                {
                    if (Int32.Parse(worksheet.Cells[row, 1].Text) == dv.Stt) // Kiểm tra ID
                    {
                        // Cập nhật thông tin dvach hang
                        worksheet.Cells[row, 2].Value = dv.Loaisp;
                        worksheet.Cells[row, 3].Value = dv.Tensp;
                        worksheet.Cells[row, 4].Value = dv.Soluong;
                        worksheet.Cells[row, 5].Value = dv.Giasp;


                        break; // Dừng vòng lặp dvi đã cập nhật
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
                    worksheet.DeleteRow(2); // Luôn xóa hàng thứ 2 cho đến dvi dvông còn hàng
                }

                package.Save(); // Lưu thay đổi vào tệp
            }
        }

        // Xóa một dịch vụ (DeleteByID)
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
                        worksheet.DeleteRow(row); // Xóa hàng dịch vụ
                        break; // Dừng vòng lặp dvi đã xóa
                    }
                }

                package.Save(); // Lưu thay đổi vào tệp
            }
        }
    }
}

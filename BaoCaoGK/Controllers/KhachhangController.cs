using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using BaoCaoGK.Models;

namespace BaoCaoGK.Controllers
{
    public class KhachhangController : Controller
    {
        private readonly KhachhangActions _khActions;

        public KhachhangController()
        {
            _khActions = new KhachhangActions(); // Khởi tạo KhachhangActions
        }
        public IActionResult Detail(int id)
        {
            var kh = _khActions.GetByID(id);
            return View(kh);

        }
        public IActionResult GetAll()
        {
            List<Khachhang> ds = _khActions.GetAll();
            return View(ds);
        }

        public IActionResult Index()
        {
            var dsKhachhang = _khActions.GetAll(); // Lấy tất cả khách hàng
            return View(dsKhachhang); // Trả về view với danh sách khách hàng
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(); // Trả về view tạo khách hàng
        }
        [HttpPost]
        public IActionResult Create(Khachhang kh)
        {
            if (ModelState.IsValid)
            {
                _khActions.Add(kh); // Thêm khách hàng
                return RedirectToAction("Index"); // Chuyển hướng về danh sách
            }
            return View(kh); // Trả về view nếu có lỗi
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var khachhang = _khActions.GetByID(id);
            if (khachhang == null)
            {
                return NotFound(); // Trả về 404 nếu không tìm thấy
            }
            return View(khachhang); // Trả về view chỉnh sửa khách hàng
        }

        [HttpPost]
        public IActionResult Edit(Khachhang kh)
        {
            if (ModelState.IsValid)
            {
                _khActions.Update(kh); // Cập nhật khách hàng
                return RedirectToAction("Index"); // Chuyển hướng về danh sách
            }
            return View(kh); // Trả về view nếu có lỗi
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _khActions.DeleteByID(id); // Xóa khách hàng theo ID
            return RedirectToAction("Index"); // Chuyển hướng về danh sách
        }

        [HttpPost]
        public IActionResult DeleteAll()
        {
            _khActions.DeleteAll(); // Xóa tất cả khách hàng
            return RedirectToAction("Index"); // Chuyển hướng về danh sách
        }
    }
}

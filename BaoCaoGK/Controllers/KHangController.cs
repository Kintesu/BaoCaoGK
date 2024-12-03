using BaoCaoGK.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BaoCaoGK.Controllers
{
    
    [Route("api/KHang")] //     [Route("api/[controller]")]
    [ApiController]
    public class KHangAPIController : ControllerBase
    {
        private readonly KhachhangActions _khActions;

        public KHangAPIController()
        {
            _khActions = new KhachhangActions(); // Khởi tạo KhachhangActions
        }

        // GET: api/<KHangController>
        [HttpGet]
        public string Get()
        {
            var dsKhachhang = _khActions.GetAll(); // Lấy tất cả 
            var opt = new JsonSerializerOptions() { WriteIndented = true };
            string strJson = JsonSerializer.Serialize<IList<Khachhang>>(dsKhachhang, opt);
            return strJson;
        }

        // GET Tìm thông tin khách hàng theo id
        [HttpGet("{id}")]
        public string Get(int id)
        {
            var kh = _khActions.GetByID(id); // Lấy tất cả 
            var opt = new JsonSerializerOptions() { WriteIndented = true };
            string strJson = JsonSerializer.Serialize<Khachhang>(kh, opt);
            return strJson;
        }

        // POST 
        [HttpPost]
        public string Post([FromBody] Khachhang kh)
        {
            if (!ModelState.IsValid)
            {
                return JsonSerializer.Serialize(new { error = "Invalid model state" });
            }

            _khActions.Add(kh);
            var opt = new JsonSerializerOptions() { WriteIndented = true };
            return JsonSerializer.Serialize(kh, opt);
        }

        
        // PUT 
        [HttpPut("{id}")]
        public string Put(int id, [FromBody] Khachhang kh)
        {
            if (!ModelState.IsValid)
            {
                return JsonSerializer.Serialize(new { error = "Invalid model state" });
            }

            var existing_kh = _khActions.GetByID(id);
            if (existing_kh == null)
            {
                return JsonSerializer.Serialize(new { error = "Khachhang not found" });
            }

            // Update the existing Khachhang with new values
            existing_kh.Hodem = kh.Hodem;
            existing_kh.Ten = kh.Ten;
            // Add other properties as needed

            _khActions.Update(existing_kh);

            return JsonSerializer.Serialize(new { message = "Update successful" });
        }


        // DELETE 
        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            var existing_kh = _khActions.GetByID(id);
            if (existing_kh == null)
            {
                return JsonSerializer.Serialize(new { error = "Khachhang not found" });
            }

            _khActions.DeleteByID(id);

            return JsonSerializer.Serialize(new { message = "Delete successful" });
        }

        //DeleteAll
        //[HttpDelete]
        //public string DeleteAll()
        //{
        //    var allKhachhangs = _khActions.GetAll();
        //    if (allKhachhangs == null || !allKhachhangs.Any())
        //    {
        //        return JsonSerializer.Serialize(new { message = "No Khachhang records to delete" });
        //    }

        //    foreach (var kh in allKhachhangs)
        //    {
        //        _khActions.DeleteAll();
        //    }

        //    return JsonSerializer.Serialize(new { message = "All Khachhang records deleted successfully" });
        //}
    }
}

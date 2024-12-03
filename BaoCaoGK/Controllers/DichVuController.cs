using BaoCaoGK.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BaoCaoGK.Controllers
{
    [Route("api/DichVu")] //     [Route("api/[controller]")]
    [ApiController]
    public class DichVuAPIController : ControllerBase
    {
        private readonly DichVuActions _dvActions;

        public DichVuAPIController()
        {
            _dvActions = new DichVuActions(); // dvởi tạo DichVuActions
        }

        // GET: api/<DichVuController>
        [HttpGet]
        public string Get()
        {
            var dsDichVu = _dvActions.GetAll(); // Lấy tất cả 
            var opt = new JsonSerializerOptions() { WriteIndented = true };
            string strJson = JsonSerializer.Serialize<IList<DichVu>>(dsDichVu, opt);
            return strJson;
        }

        // GET Tìm thông tin dvách hàng theo id
        [HttpGet("{id}")]
        public string Get(int id)
        {
            var dv = _dvActions.GetByID(id); // Lấy tất cả 
            var opt = new JsonSerializerOptions() { WriteIndented = true };
            string strJson = JsonSerializer.Serialize<DichVu>(dv, opt);
            return strJson;
        }

        // POST 
        [HttpPost]
        public string Post([FromBody] DichVu dv)
        {
            if (!ModelState.IsValid)
            {
                return JsonSerializer.Serialize(new { error = "Invalid model state" });
            }

            _dvActions.Add(dv);
            var opt = new JsonSerializerOptions() { WriteIndented = true };
            return JsonSerializer.Serialize(dv, opt);
        }


        // PUT 
        [HttpPut("{id}")]
        public string Put(int id, [FromBody] DichVu dv)
        {
            if (!ModelState.IsValid)
            {
                return JsonSerializer.Serialize(new { error = "Invalid model state" });
            }

            var existing_dv = _dvActions.GetByID(id);
            if (existing_dv == null)
            {
                return JsonSerializer.Serialize(new { error = "DichVu not found" });
            }

            // Update the existing DichVu with new values
            existing_dv.Loaisp = dv.Loaisp;
            existing_dv.Tensp = dv.Tensp;
            // Add other properties as needed

            _dvActions.Update(existing_dv);

            return JsonSerializer.Serialize(new { message = "Update successful" });
        }


        // DELETE 
        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            var existing_dv = _dvActions.GetByID(id);
            if (existing_dv == null)
            {
                return JsonSerializer.Serialize(new { error = "DichVu not found" });
            }

            _dvActions.DeleteByID(id);

            return JsonSerializer.Serialize(new { message = "Delete successful" });
        }

        //DeleteAll
        //[HttpDelete]
        //public string DeleteAll()
        //{
        //    var allDichVus = _dvActions.GetAll();
        //    if (allDichVus == null || !allDichVus.Any())
        //    {
        //        return JsonSerializer.Serialize(new { message = "No DichVu records to delete" });
        //    }

        //    foreach (var dv in allDichVus)
        //    {
        //        _dvActions.DeleteAll();
        //    }

        //    return JsonSerializer.Serialize(new { message = "All DichVu records deleted successfully" });
        //}
    }
}

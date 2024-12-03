using System.ComponentModel.DataAnnotations;

namespace BaoCaoGK.Models
{
    public class DichVu
    {
        int stt;
        string loaisp;
        string tensp;
        double soluong;
        double giasp;

        public int Stt { get => stt; set => stt = value; }
        public string Loaisp { get => loaisp; set => loaisp = value; }
        public string Tensp { get => tensp; set => tensp = value; }
        public double Soluong { get => soluong; set => soluong = value; }
        public double Giasp { get => giasp; set => giasp = value; }
    }
}

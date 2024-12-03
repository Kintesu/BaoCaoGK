using System.ComponentModel.DataAnnotations;

namespace BaoCaoGK.Models
{
    public class Khachhang
    {
        int tt;
        string hodem;
        string ten;
        string tk;
        int mk;
        double sogio;

        public int Tt { get => tt; set => tt = value; }
        public string Hodem { get => hodem; set => hodem = value; }
        public string Ten { get => ten; set => ten = value; }
        public string Tk { get => tk; set => tk = value; }
        public int Mk { get => mk; set => mk = value; }
        public double Sogio { get => sogio; set => sogio = value; }
    }
}

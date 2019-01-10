using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.IO.Models
{
    public class PhieuCongTac
    {
        public string ID { get; set; }
        public DateTime ThoiGian { get; set; }
        public string PhatHienBoi { get; set; }
        public string DiaChi { get; set; }
        public string Phuong { get; set; }
        public string Quan { get; set; }
        public string SoDienThoai{ get; set; }
        public string DMA { get; set; }
        public string DoiTuong { get; set; }
        public List<PhuiDao> PhuiDaos { get; set; }
        public List<VatTu> VatTus { get; set; }
        public string DoiTruong { get; set; }
    }

        public class PhuiDao
    {
        public float? Dai { get; set; }
        public float? Rong { get; set; }
        public float? Sau { get; set; }
    }

    public class VatTu
    {
        public string TenVatTu { get; set; }
        public float SoLuong { get; set; }
        public string DonVi { get; set; }
        public string LoaiVatTu { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.DataProvider.Models
{
    public class DMA
    {
        public string MaDMA { get; set; }
        public string TenDMA { get; set; }
    }
    public class SuCoTheoDMA
    {
        public string TenDMA { get; set; }
        public string IDSuCo { get; set; }
        public Nullable<short> DiemBe { get; set; }
        public Nullable<short> SuaBe { get; set; }
    }
    public class TonThatModel
    {
        public int SoLuongKH { get; set; }
        public Double SanLuongKH { get; set; }
    }

    public class ChiTietSanLuongDMA
    {
        public DateTime ThoiGian { get; set; }
        public Double GiaTri { get; set; }
    }

    public class ILIModel
    {
        public Double CAPL { get; set; }
        public Double MAAPL { get; set; }
        public Double LM { get; set; }
        public Double NC { get; set; }
        public Double LP { get; set; }
        public Double? P { get; set; }
        public Double ILI { get; set; }
        public String Group { get; set; }
        public String Summary { get; set; }
    }
}

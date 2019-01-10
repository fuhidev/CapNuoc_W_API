using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.DataProvider.Models
{
    public class TieuThuKhachHang
    {
        public string DANHBA { get; set; }
        public Nullable<int> TIEUTHU { get; set; }
        public Nullable<int> KY { get; set; }
        public Nullable<int> NAM { get; set; }
    }
}

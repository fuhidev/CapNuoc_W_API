//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebAPI.DataProvider.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class SUCO
    {
        public int OBJECTID { get; set; }
        public System.Guid GlobalID { get; set; }
        public string IDSuCo { get; set; }
        public Nullable<short> TrangThai { get; set; }
        public string NguoiPhanAnh { get; set; }
        public string SDTPhanAnh { get; set; }
        public Nullable<System.DateTime> TGPhanAnh { get; set; }
        public string DoiQuanLy { get; set; }
        public Nullable<short> HinhThucPhatHien { get; set; }
        public Nullable<short> ThongTinPhanAnh { get; set; }
        public Nullable<System.DateTime> TGKhacPhuc { get; set; }
        public string NhomKhacPhuc { get; set; }
        public Nullable<short> PhanLoaiSuCo { get; set; }
        public string DiaChi { get; set; }
        public string MaDuong { get; set; }
        public string MaQuan { get; set; }
        public string MaPhuong { get; set; }
        public string MaDMA { get; set; }
        public Nullable<short> LoaiSuCo { get; set; }
        public Nullable<short> VatLieu { get; set; }
        public string NguyenNhan { get; set; }
        public Nullable<short> DuongKinhOng { get; set; }
        public Nullable<decimal> ApLuc { get; set; }
        public Nullable<decimal> DoSauLungOng { get; set; }
        public string GhiChu { get; set; }
        public System.Data.Entity.Spatial.DbGeometry SHAPE { get; set; }
    }
}

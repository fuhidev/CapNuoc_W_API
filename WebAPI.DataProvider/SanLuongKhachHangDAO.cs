using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.DataProvider.EF;
using WebAPI.DataProvider.Models;

namespace WebAPI.DataProvider
{
    public class SanLuongKhachHangDAO
    {
        public List<TieuThuKhachHang> SanLuong(int nam, int ky, int gioiHanSoLuong, int? tieuThuTu, int? tieuThuDen, int? chiSoTu, int? chiSoDen, int? giaBieuTu, int? giaBieuDen)
        {
            return null;
            try
            {
                using (var context = new DocSoEntities())
                {
                    //return(
                    //    from w in context.vTieuThuDongHoNuocKhachHangs
                    //    where
                    //    w.NAM == nam
                    //    && w.KY == ky
                    //        &&
                    //        (
                    //            (
                    //            (tieuThuTu.HasValue == true && w.TIEUTHU.HasValue == true && w.TIEUTHU.Value >= tieuThuTu.Value)//nếu như có giá trị tiêu thụ từ thì bắt buộc giá trị TieuThu trong dữ liệu phải khác null và giá trị đó phải >= giá trị tiêu thụ từ
                    //            || (tieuThuTu.HasValue == false)//nếu không có giá trị tiêu thụ từ thì bỏ qua phần kiểm tra giá trị tiêu thụ trong cơ sở dữ liệu
                    //            )
                    //            && (
                    //            (
                    //            tieuThuDen.HasValue == true//nếu như tiêu thụ đến có giá trị
                    //                                       //thì giá trị tiêu thụ trong csdl có 2 trường hợp = null hoặc khác null
                    //            && (
                    //                (w.TIEUTHU.HasValue == true && w.TIEUTHU.Value <= tieuThuDen.Value)//nếu khác null thì giá trị này phải <= giá triệu tiêu thụ đến
                    //                ||
                    //                (w.TIEUTHU.HasValue == false)//nếu không có giá trị thì bỏ qua không cần kiểm tra vì null mặc định là bé nhất rồi
                    //                )
                    //            )
                    //            || tieuThuDen.HasValue == false
                    //            )
                    //        )
                    //         &&
                    //        (
                    //            (
                    //                (
                    //                chiSoTu.HasValue == true && w.CSMOI.HasValue == true && w.CSMOI.Value >= chiSoTu.Value)//nếu như có giá trị chỉ số từ thì bắt buộc giá trị CODEMOI trong dữ liệu phải khác null và giá trị đó phải >= giá trị chỉ số từ
                    //                || (chiSoTu.HasValue == false)//nếu không có giá trị chỉ số từ thì bỏ qua phần kiểm tra giá trị chỉ số trong cơ sở dữ liệu
                    //                )
                    //                &&(
                    //                (
                    //                chiSoDen.HasValue == true//nếu như chỉ số đến có giá trị
                    //                                            //thì giá trị chỉ số trong csdl có 2 trường hợp = null hoặc khác null
                    //                && (
                    //                    (w.CSMOI.HasValue == true && w.CSMOI.Value <= chiSoDen.Value)//nếu khác null thì giá trị này phải <= giá triệu chỉ số đến
                    //                    ||
                    //                    (w.CSMOI.HasValue == false)//nếu không có giá trị thì bỏ qua không cần kiểm tra vì null mặc định là bé nhất rồi
                    //                )
                    //                || chiSoDen.HasValue == false
                    //                )
                    //            )
                    //        )
                    //    select
                    //    w
                    //    //new vTieuThuDongHoNuocKhachHang()
                    //    //{
                    //    //    DANHBA = w.DANHBA,
                    //    //    TIEUTHU = w.TIEUTHU
                    //    //}
                    //    ).Take(gioiHanSoLuong).ToList();

                }
            }
            catch (Exception e)
            {
                //return new List<vTieuThuDongHoNuocKhachHang>();
            }
        }

        public List<TieuThuKhachHang> SanLuongTheoNam(string danhBa)
        {
            try
            {
                using (var context = new DocSoEntities())
                {
                    var query = String.Format(@"SELECT TOP 12 DANHBA,TIEUTHU,KY = CAST(KY AS INT), NAM = CAST(NAM AS INT) FROM vTieuThuDongHoNuocKhachHang 
WHERE DANHBA IS NOT NULL AND DANHBA = '{0}' 
ORDER BY NAM DESC, KY DESC", danhBa);
                    query = query.Replace("\r\n", "");
                    query = query.Replace("\t", "");
                    return context.Database.SqlQuery<TieuThuKhachHang>(query).ToList();

                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public double TongSanLuong()
        {
            try
            {
                using (var context = new DocSoEntities())
                {
                    var query = String.Format(@"SELECT CAST(SUM(TIEUTHU) AS FLOAT) FROM vTieuThuDongHoNuocKhachHang");
                    query = query.Replace("\r\n", "");
                    query = query.Replace("\t", "");
                    var result =  context.Database.SqlQuery<Double?>(query).SingleOrDefault();
                    return result.HasValue ? result.Value : 0;

                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<TieuThuKhachHang> SanLuongDMATheoThoiGian(string dma, int nam, int ky)
        {
            try
            {
                using (var context = new DocSoEntities())
                {
                    var query = String.Format(@"SELECT DANHBA,TIEUTHU FROM vTieuThuDongHoNuocKhachHang 
WHERE DANHBA IS NOT NULL AND Nam = {0} AND KY = {1} AND DMA = '{2}'", nam, ky, dma);
                    query = query.Replace("\r\n", "");
                    query = query.Replace("\t", "");
                    return context.Database.SqlQuery<TieuThuKhachHang>(query).ToList();

                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<TieuThuKhachHang> XepHangSanLuongTheoKy(int nam, int ky, int? gioiHanHienThi)
        {
            try
            {
                using (var context = new DocSoEntities())
                {
                    var query = String.Format(@"
                    SELECT TOP {0} DANHBA,TIEUTHU FROM vTieuThuDongHoNuocKhachHang 
                    WHERE DANHBA IS NOT NULL AND Nam = {1} AND KY = {2} 
                    ORDER BY TIEUTHU DESC", gioiHanHienThi.HasValue ? gioiHanHienThi.Value : 100, nam, ky);
                    query = query.Replace("\r\n", "");
                    query = query.Replace("\t", "");
                    return context.Database.SqlQuery<TieuThuKhachHang>(query).ToList();

                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}

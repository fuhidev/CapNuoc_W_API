using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using WebAPI.DataProvider.EF;
using WebAPI.DataProvider.Models;

namespace WebAPI.DataProvider
{
    public class TonThatDB
    {

        /// <summary>
        /// Lấy bảng Logger
        /// </summary>
        /// <param name="loggerID">Mã logger</param>
        /// <returns></returns>
        private string CreateLoggerTable(string loggerID)
        {
            return "t_Data_Logger_" + loggerID + "_02";
        }


        public TonThatModel SanLuongKhachHangTrenDMA(string maDMA, int nam, int ky)
        {
            try
            {

                var sanLuongKHDAO = new SanLuongKhachHangDAO();
                var duLieuKhachHang = sanLuongKHDAO.SanLuongDMATheoThoiGian(maDMA, nam, ky);

                int soLuongKH = duLieuKhachHang.Count();

                double tongSanLuong =
                    soLuongKH > 0 ?
                    duLieuKhachHang.Sum(s => (s.TIEUTHU.HasValue == true ? (double)s.TIEUTHU.Value : 0.0))
                : 0;

                return new TonThatModel()
                {
                    SanLuongKH = tongSanLuong,
                    SoLuongKH = soLuongKH
                };
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Double TieuThuDMA(string dma, int nam, int ky)
        {
            try
            {
                using (var context = new LoggerEntities())
                {
                    var loggerDMA = context.LoggerDMAs.SingleOrDefault(s => s.MaDMA == dma);

                    //lấy Logger ID
                    if (loggerDMA != null && !String.IsNullOrEmpty(loggerDMA.MaDMA))
                    {
                        var table = this.CreateLoggerTable(loggerDMA.SiteID);
                        var query = String.Format(
@"SELECT SUM(Value) as Value 
FROM {0} 
WHERE DATEPART(YEAR,TimeStamp) = {1} AND DATEPART(MONTH,TimeStamp) = {2}",
table, nam, ky
);

                        query = query.Replace("\r\n", "");//xóa \r\n
                        query = query.Replace("\t", "");//xóa \t

                        var result = context.Database.SqlQuery<Double?>(query).DefaultIfEmpty(0).FirstOrDefault();
                        if (result.HasValue)
                            return Math.Round(result.Value, 2);//làm tròn 3 chữ số
                        else return 0;
                    }
                    else
                    {
                        return 0;//không có dữ liệu
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public double TieuThuDMA(string dma)
        {
            try
            {
                using (var context = new LoggerEntities())
                {
                    var loggerID = context.t_Channel_Configurations.Where(w =>
                    w.Description == dma //lọc dma
                    && w.ChannelId.EndsWith("_03") == true //lọc channel đuôi là _3
                    ).Select(s => s.LoggerId).DefaultIfEmpty(null).FirstOrDefault();

                    //lấy Logger ID
                    if (loggerID != null)
                    {
                        var table = this.CreateLoggerTable(loggerID);
                        var query = String.Format(
@"SELECT SUM(Value) as Value 
FROM {0}",
table
);

                        query = query.Replace("\r\n", "");//xóa \r\n
                        query = query.Replace("\t", "");//xóa \t

                        var result = context.Database.SqlQuery<Double?>(query).DefaultIfEmpty(0).FirstOrDefault();
                        return result.HasValue ? result.Value : 0;
                    }
                    else
                    {
                        return 0;//không có dữ liệu
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<WebAPI.DataProvider.Models.DMA> SelectLoggerDMA()
        {
            try
            {
                using (var context = new GISEntities())
                {
                    return context.Database.SqlQuery<WebAPI.DataProvider.Models.DMA>("select distinct MaDMA,TenDMA from sde.DMA where MADMA is not null").ToList();
                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public double SanLuongDMA(string dma, DateTime tuNgay, DateTime denNgay)
        {
            try
            {
                using (var context = new LoggerEntities())
                {
                    var tuNgaySQL = tuNgay.ToString("yyyy-MM-dd");
                    var denNgaySQL = denNgay.ToString("yyyy-MM-dd");
                    var query = String.Format("EXEC SanLuongDMA '{0}','{1}','{2}'", dma, tuNgaySQL, denNgaySQL);
                    var result = context.Database.SqlQuery<Double?>(query).DefaultIfEmpty(0).FirstOrDefault();
                    return result.HasValue ? result.Value : 0;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public double TongSanLuongDMA()
        {
            try
            {
                using (var context = new LoggerEntities())
                {
                    var result = context.Database.SqlQuery<Double?>("TongSanLuongDMA").SingleOrDefault();
                    return result.HasValue ? result.Value : 0;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public ILIModel ILI(float P)
        {
            try
            {
                using (var context = new GISEntities())
                {
                    var sanLuongDAO = new SanLuongKhachHangDAO();
                    var tongSanLuongDMA = this.TongSanLuongDMA();
                    var tongTieuThu = sanLuongDAO.TongSanLuong();
                    var query = context.Database.SqlQuery<ILIModel>(String.Format("exec ILI {0}, {1}, {2}", P, tongSanLuongDMA, tongTieuThu));
                    var resultQuery = query.DefaultIfEmpty(new ILIModel
                    {
                        CAPL = 0,
                        MAAPL = 0,
                        LM = 0,
                        NC = 0,
                        LP = 0,
                        P = 0,
                        Group = string.Empty,
                        Summary = string.Empty
                    }).SingleOrDefault();

                    resultQuery.Group = (resultQuery.ILI >= 1 && resultQuery.ILI <= 4) ? "A" :
                       (resultQuery.ILI > 4 && resultQuery.ILI <= 8) ? "B" :
                           (resultQuery.ILI > 8 && resultQuery.ILI <= 16) ? "C" :
                               resultQuery.ILI > 16 ? "D" : "Không xác định";
                    resultQuery.Summary =
                        resultQuery.Group.Equals("A") == true ? @"Giảm thất thoát nước hơn nữa có thể không hiệu quả về mặt kinh tế. Cần phân tích kỹ để xác định những cải thiện hiệu quả về mặt chi phí" :
                            resultQuery.Group == "B" ? @"Có tiềm năng cải thiện. Xem xét: quản lý áp lực, các biện pháp kiểm soát rò rỉ chủ động tốt hơn và bảo dưỡng tốt hơn." :
                                resultQuery.Group == "C" ?
                                    @"Chỉ cho phép nếu lực lượng nước nhiều và giá rẻ. Cần củng cố các nỗ lực giảm NRW" :
                                    resultQuery.Group == "D" ? @"Sử dụng các nguồn lực thiếu hiệu quả. Các chương trình giảm NRW là bắt buộc và ưu tiên." :
                                        "Không có đánh giá";
                    return resultQuery;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<ChiTietSanLuongDMA> LayChiTietSanLuongDMA(string dma, DateTime tuNgay, DateTime denNgay)
        {
            try
            {
                using (var context = new LoggerEntities())
                {
                    var tuNgaySQL = tuNgay.ToString("yyyy-MM-dd");
                    var denNgaySQL = denNgay.ToString("yyyy-MM-dd");
                    var query = String.Format("EXEC LuuLuongChiTietDMA '{0}','{1}','{2}'", dma, tuNgaySQL, denNgaySQL);
                    return context.Database.SqlQuery<ChiTietSanLuongDMA>(query).ToList();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<ChiTietSanLuongDMA> LayChiTietApLucDMA(string dma, DateTime tuNgay, DateTime denNgay)
        {
            try
            {
                using (var context = new LoggerEntities())
                {
                    var tuNgaySQL = tuNgay.ToString("yyyy-MM-dd");
                    var denNgaySQL = denNgay.ToString("yyyy-MM-dd");
                    var query = String.Format("EXEC ApLucChiTietDMA '{0}','{1}','{2}'", dma, tuNgaySQL, denNgaySQL);
                    return context.Database.SqlQuery<ChiTietSanLuongDMA>(query).ToList();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<SuCoTheoDMA> LayDanhSachSuCoTheoDMA(DateTime tuNgay, DateTime denNgay)
        {
            try
            {
                using (var context = new GISEntities())
                {
                    var tuNgaySQL = tuNgay.ToString("yyyy-MM-dd");
                    var denNgaySQL = denNgay.ToString("yyyy-MM-dd");
                    var query = String.Format(@"select sc.IDSuCo,SuaBe = LuongNuocThatThoatChuQuan,DiemBe = LuongNuocThatThoatKhachQuan,dma.TenDMA 
                        from sde.SUCO sc inner join sde.DMA dma on dma.SHAPE.STContains(sc.SHAPE) = 1 
                        where CAST(sc.NgayXayRa AS DATE) between '{0}' AND '{1}' OR CAST(sc.NgayKhacPhuc AS DATE) between '{0}' AND '{1}'", tuNgaySQL,denNgaySQL);
                    query = query.Replace("\r\n","");
                    query = query.Replace("\t","");
                    
                    return context.Database.SqlQuery<SuCoTheoDMA>(query).ToList();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }


    }

}

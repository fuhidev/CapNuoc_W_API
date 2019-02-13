using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.DataProvider.EF;
using WebAPI.DataProvider.Models;

namespace WebAPI.DataProvider
{
    public class SuCoDB : IAction<SUCO, string>
    {
        public SUCO Create(SUCO model)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Thực hiện cập nhật tiếp nhận sự cố  
        /// </summary>
        /// <param name="objectId">Objectid của đối tượng sự cố</param>
        /// <returns>IDSuCo</returns>
        public String HintNhomKhacPhuc(string pNhomKhacPhuc)
        {
            using (var context = new GISEntities())
            {
                var result = context.SUCOes.FirstOrDefault(f => !String.IsNullOrEmpty(f.NhomKhacPhuc) && f.NhomKhacPhuc.StartsWith(pNhomKhacPhuc));
                return result != null ? result.NhomKhacPhuc : "";
            }
        }
        public List<String> GetDistinctNhomKhacPhuc()
        {
            using (var context = new GISEntities())
            {
                var result = context.SUCOes.Where(w=>!String.IsNullOrEmpty(w.NhomKhacPhuc)).Select(s=>s.NhomKhacPhuc).Distinct().ToList();
                return result;
            }
        }

        public List<TBKhachHangMatNuoc> CustomersByDMA(string dma, DateTime from, DateTime to)
        {
            try
            {
                using (var context = new GISEntities())
                {
                    //var result = from dt in context.TBKHACHHANGMATNUOCs
                    //             join hc in context.HANHCHINHs on dt.MAPHUONG equals hc.IDHanhChinh
                    //             //join dhkh in context.DONGHOKHACHHANGs on dt.DBDONGHONUOC equals dhkh.DBDONGHONUOC
                    //             where
                    //             !String.IsNullOrEmpty(dt.DBDONGHONUOC)
                    //             && dt.DBDONGHONUOC != " "
                    //              && dt.TENDMA == dma
                    //             && dt.TuNgay.HasValue && dt.DenNgay.HasValue
                    //             && dt.TuNgay.Value >= @from
                    //             && dt.DenNgay.Value <= to
                    //             select new TBKhachHangMatNuoc
                    //             {
                    //                 MAPHUONG = hc.TenHanhChinh,
                    //                 MAQUAN = hc.TenQuan,
                    //                 CODONGHO = dt.CODONGHO,
                    //                 DBDONGHONUOC = dt.DBDONGHONUOC,
                    //                 MADUONG = dt.MADUONG,
                    //                 NGAYCAPNHAT = dt.NGAYCAPNHAT,
                    //                 OBJECTID = dt.OBJECTID,
                    //                 SODIENTHOAI = dt.SODIENTHOAI,
                    //                 SONHA = dt.SONHA,
                    //                 TENDMA = dt.TENDMA,
                    //                 TENDUONG = dt.TENDUONG,
                    //                 TENTHUEBAO = dt.TENTHUEBAO,
                    //                 TuNgay = dt.TuNgay,
                    //                 DenNgay = dt.DenNgay
                    //                 //Geometry = new Point
                    //                 //{
                    //                 //    x = dhkh.SHAPE.XCoordinate.Value,
                    //                 //    y = dhkh.SHAPE.YCoordinate.Value,
                    //                 //}
                    //             };
                    var query = String.Format(@"SELECT 
                                             MAPHUONG = HC.TenHanhChinh,
                                             MAQUAN = HC.TenQuan,
                                             CODONGHO = KH.CODONGHO,
                                             DBDONGHONUOC = KH.DBDONGHONUOC,
                                             MADUONG = KH.MADUONG,
                                             NGAYCAPNHAT = KH.NGAYCAPNHAT,
                                             OBJECTID = KH.OBJECTID,
                                             SODIENTHOAI = KH.SODIENTHOAI,
                                             SONHA = KH.SONHA,
                                             TENDMA = KH.TENDMA,
                                             TENDUONG = KH.TENDUONG,
                                             TENTHUEBAO = KH.TENTHUEBAO,
                                             TuNgay = KH.TuNgay,DenNgay = KH.DenNgay
                                            FROM sde.TBKHACHHANGMATNUOC KH
                                            INNER JOIN sde.HANHCHINH HC ON KH.MAPHUONG = HC.IDHanhChinh
                                            WHERE KH.TENDMA= '{0}' AND CAST(KH.NGAYCAPNHAT AS DATE) BETWEEN '{1}' AND '{2}'
                                            AND KH.DBDONGHONUOC is not null and KH.DBDONGHONUOC <> ' '",
                                            dma, from.ToString("yyyy-MM-dd"), to.ToString("yyyy-MM-dd"));

                    return context.Database.SqlQuery<TBKhachHangMatNuoc>(Helper.Query(query)).ToList();
                    //return result.Take(100).ToList();
                    //return result.Take(100).ToList();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Delete(string id)
        {
            throw new NotImplementedException();
        }

        public SUCO Get(string id)
        {
            using (var context = new GISEntities())
            {
                return context.SUCOes.FirstOrDefault(f => f.IDSuCo.Equals(id));
            }
        }

        public IEnumerable<SUCO> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<VatTuSuCo> SelectMaterialsBySuCo(string idSuCo)
        {
            try
            {
                using (var context = new GISEntities())
                {
                    var query = from hsvt in context.HOSOVATTUSUCOes
                                join vt in context.VATTUs on hsvt.MaVatTu equals vt.MaVatTu
                                where hsvt.IDSuCo == idSuCo
                                select new VatTuSuCo
                                {
                                    MaVatTu = vt.MaVatTu,
                                    TenVatTu = vt.TenVatTu,
                                    SoLuong = hsvt.SoLuong,
                                    DonViTinh = vt.DonViTinh,
                                };
                    return query.ToList();

                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Update(string id, SUCO model)
        {
            throw new NotImplementedException();
        }
    }

    public class HoSoVatTuSuCoDB : IAction<HOSOVATTUSUCO, int>
    {
        public HOSOVATTUSUCO Create(HOSOVATTUSUCO model)
        {
            try
            {
                using (var context = new GISEntities())
                {
                    if (model != null)
                    {
                        var query = String.Format(@"
                        INSERT INTO SDE.HOSOVATTUSUCO 
                        SELECT 
	                        ,IDSuCo = @idSuCo
	                        ,SoLuong = @soLuong
	                        ,MaVatTu = @maVatTu
                        FROM SDE.HOSOVATTUSUCO
                        ");
                        query = Helper.Query(query);
                        var result = context.Database.ExecuteSqlCommand(query,
                            new SqlParameter("@idSuCo", model.IDSuCo),
                            new SqlParameter("@soLuong", model.SoLuong),
                            new SqlParameter("@maVatTu", model.MaVatTu)
                            );
                        if (result > 0)
                        {
                            return model;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                using (var context = new GISEntities())
                {
                    var model = context.HOSOVATTUSUCOes.FirstOrDefault(f => f.RID.Equals(id));
                    if (model != null)
                    {
                        context.HOSOVATTUSUCOes.Remove(model);
                        return context.SaveChanges() > 0;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Delete(string idSuCo, string maVatTu)
        {
            try
            {
                using (var context = new GISEntities())
                {
                    var sql = String.Format("DELETE FROM SDE.HOSOVATTUSUCO WHERE IDSuCo = @idSuCo AND MaVatTu = @maVatTu");
                    return context.Database.ExecuteSqlCommand(sql,
                        new SqlParameter("@idSuCo", idSuCo),
                        new SqlParameter("@maVatTu", maVatTu)
                        ) > 0;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public HOSOVATTUSUCO Get(int id)
        {
            try
            {
                using (var context = new GISEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    return context.HOSOVATTUSUCOes.FirstOrDefault(f => f.RID.Equals(id));
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public HOSOVATTUSUCO Get(string idSuCo, string maVatTu)
        {
            try
            {
                using (var context = new GISEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    return context.HOSOVATTUSUCOes.FirstOrDefault(f => f.IDSuCo.Equals(idSuCo) && f.MaVatTu.Equals(maVatTu));
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<HOSOVATTUSUCO> GetAll()
        {
            try
            {
                using (var context = new GISEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    return context.HOSOVATTUSUCOes.ToList();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Update(int id, HOSOVATTUSUCO model)
        {
            throw new NotImplementedException();
        }

        public bool Update(string idSuCo, string maVatTu, int soLuong)
        {
            try
            {
                using (var context = new GISEntities())
                {
                    var baseModel = context.HOSOVATTUSUCOes.FirstOrDefault(f => f.IDSuCo.Equals(idSuCo) && f.MaVatTu.Equals(maVatTu));
                    if (baseModel == null)
                    {
                        throw new NullReferenceException("Không tìm thấy đối tượng trong cơ sở dữ liệu");
                    }
                    else
                    {
                        var sql = String.Format("UPDATE sde.HOSOVATTUSUCO SET SoLuong = @soLuong WHERE IDSuCo = @idSuCo AND MaVatTu = @maVatTu");
                        return context.Database.ExecuteSqlCommand(sql,
                            new SqlParameter("@soLuong", soLuong),
                            new SqlParameter("@idSuCo", idSuCo),
                            new SqlParameter("@maVatTu", maVatTu)
                            ) > 0;
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }

    public class VatTuDB : IAction<VATTU, string>
    {
        public VATTU Create(VATTU model)
        {
            throw new NotImplementedException();
        }

        public bool Delete(string id)
        {
            throw new NotImplementedException();
        }

        public VATTU Get(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<VATTU> GetAll()
        {
            try
            {
                using (var context = new GISEntities())
                {
                    return context.VATTUs.Where(w => !String.IsNullOrEmpty(w.MaVatTu)).OrderBy(o => o.TenVatTu)
                        .Distinct().ToList();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Update(string id, VATTU model)
        {
            throw new NotImplementedException();
        }
    }

}


public class VatTuSuCo
{
    public string MaVatTu { get; set; }
    public string TenVatTu { get; set; }
    public decimal? SoLuong { get; set; }
    public string DonViTinh { get; set; }
}
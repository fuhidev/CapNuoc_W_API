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
        public string TiepNhanSuCo(int objectId)
        {
            using (var context = new GISEntities())
            {
                SUCO model = context.SUCOes.FirstOrDefault(f => f.OBJECTID == objectId);

                // nếu sự cố khác trạng thái hoàn thành thì không cho cập nhật
                if (model.TrangThai.HasValue && model.TrangThai.Value == (short)SuCo.TrangThai.HoanThanh)
                {
                    throw new Exception("Sự cố có trạng thái không phù hợp để thực hiện thao tác này");
                }
                // cập nhật idsuco
                var sql = Helper.Query(@"declare @geometry geometry
                set @geometry = (select top 1 Shape from sde.SUCO where OBJECTID = @p0) 

                DECLARE @hanhChinh TABLE(
                    MaPhuong varchar(10),
                    MaQuan varchar(10)
                );

                INSERT INTO @hanhChinh 
                select top 1 MaPhuong = IDHanhChinh,MaQuan = MaHuyen FROM sde.HANHCHINH a where a.SHAPE.STContains(@geometry) = 1 

                DECLARE @IDSuCo varchar(20) 
                DECLARE @MaQuan varchar(20)
                
                SET @IDSuCo = (CASE WHEN (SELECT MaQuan FROM @hanhChinh) = '777' THEN 'PCN2' ELSE 'PCN1' END) + '/T'+CONVERT(VARCHAR(2),MONTH(GETDATE()))+'/'+CONVERT(VARCHAR(4),YEAR(GETDATE()))+'/' 

                SET @IDSuCo = (SELECT @IDSuCo+ISNULL((Select top 1 FORMAT(CONVERT(int,REVERSE(SUBSTRING(REVERSE(IDSuCo),1,4))) + 1,'0000') from sde.SuCo where IDSuCo LIKE @IDSuCo+'%' order by IDSuCo desc),'0001')) 

                update sde.SUCO set 
                IDSuCo = @IDSuCo,
                TrangThai = 0,
                MaPhuong = (SELECT MaPhuong FROM @hanhChinh),
                MaQuan = (SELECT MaQuan FROM @hanhChinh),
                MaDMA = ((select top 1 MADMA FROM sde.DMA a where a.SHAPE.STContains(@geometry) = 1)),
                DoiQuanLy = CASE WHEN (SELECT MaQuan FROM @hanhChinh) = '777' THEN 'QLCN2' ELSE 'QLCN1' END,
                TGPhanAnh = GETUTCDATE() 
                WHERE OBJECTID = @p0");

                var result = context.Database.ExecuteSqlCommand(sql, objectId);
                if (result > 0)
                {
                    return context.Database.SqlQuery<string>("SELECT IDSuCo FROM sde.SuCo WHERE OBJECTID = @p0", objectId).FirstOrDefault();
                }
                else
                {
                    context.Database.ExecuteSqlCommand("DELETE FROM SDE.SuCo WHERE OBJECTID = @p0", objectId);
                    throw new Exception("Có lỗi xảy ra trong quá trình thực hiện");
                }
            }
        }

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

        public string GenerateIDSuCo(string doiQuanLy = "QLCN1")
        {

            try
            {
                using (var context = new GISEntities())
                {
                    var now = DateTime.Now;
                    string day = String.Format("{0}/T{1}/{2}/",
                        doiQuanLy.ToLower().Replace("qlcn", "PCN"),
                        now.Month, now.Year);

                    string query = "Select top 1 IDSuCo from sde.SuCo where IDSuCo LIKE '" + day + "%' order by IDSuCo desc";

                    var idSuco = context.Database.SqlQuery<string>(query).FirstOrDefault();

                    if (String.IsNullOrEmpty(idSuco))
                        return day + "0001";
                    else
                    {
                        string id = idSuco;
                        string[] array = id.Split('/');
                        if (array.Count() == 4)
                        {
                            var stt = array[3];
                            return day + ((int.Parse(stt) + 1).ToString().PadLeft(4, '0'));
                        }
                        else
                        {
                            return day + "0001";
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

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
	                        OBJECTID = ISNULL(MAX(OBJECTID)+1,1) 
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
                    var model = context.HOSOVATTUSUCOes.FirstOrDefault(f => f.OBJECTID.Equals(id));
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
                    return context.HOSOVATTUSUCOes.FirstOrDefault(f => f.OBJECTID.Equals(id));
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
            if (model == null)
            {
                throw new ArgumentNullException();
            }
            try
            {
                using (var context = new GISEntities())
                {
                    var baseModel = context.HOSOVATTUSUCOes.FirstOrDefault(f => f.OBJECTID.Equals(id));
                    if (baseModel == null)
                    {
                        throw new NullReferenceException("Không tìm thấy đối tượng trong cơ sở dữ liệu");
                    }
                    else
                    {
                        model.OBJECTID = id;
                        context.Entry(baseModel).CurrentValues.SetValues(model);
                        //context.Entry(model).State = System.Data.Entity.EntityState.Modified;
                        //context.SYS_Layer.Attach(model);
                        return context.SaveChanges() > 0;
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
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
    public decimal SoLuong { get; set; }
    public string DonViTinh { get; set; }
}
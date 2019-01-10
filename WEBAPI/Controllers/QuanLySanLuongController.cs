using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.DataProvider;
using WebAPI.DataProvider.Models;

namespace WebAPI.Controllers
{
    [Authorize]
    [RoutePrefix("api/QuanLySanLuong")]
    public class QuanLySanLuongController : ApiController
    {
        public SanLuongKhachHangDAO dataContext;
        public QuanLySanLuongController()
        {
            this.dataContext = new SanLuongKhachHangDAO();
        }
        [Route("SelectXepHangSanLuongTheoKy")]
        public HttpResponseMessage SelectXepHangSanLuongTheoKy(int nam, int ky, int? gioiHanSoLuong)
        {
            try
            {
                var results = this.dataContext.XepHangSanLuongTheoKy(nam, ky, gioiHanSoLuong);
                return Request.CreateResponse(HttpStatusCode.OK, results);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message);
            }
            
        }

        [Route("SelectSanLuong")]
        public HttpResponseMessage SelectSanLuong(int nam, int ky, int gioiHanSoLuong,int? tieuThuTu, int? tieuThuDen, int? chiSoTu, int? chiSoDen, int? giaBieuTu, int? giaBieuDen)
        {
            try
            {
                var results = this.dataContext.SanLuong(nam, ky, gioiHanSoLuong,tieuThuTu, tieuThuDen, chiSoTu, chiSoDen, giaBieuTu, giaBieuDen);
                return Request.CreateResponse(HttpStatusCode.OK, results);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message);
            }

        }

        [Route("SelectSanLuongNam")]
        public HttpResponseMessage SelectSanLuongNam(string danhBa)
        {
            try
            {
                var results = this.dataContext.SanLuongTheoNam(danhBa);
                return Request.CreateResponse(HttpStatusCode.OK, results);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }
    }
}
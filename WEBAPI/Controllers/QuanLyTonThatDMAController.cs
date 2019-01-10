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
    [RoutePrefix("api/QuanLyTonThatDMA")]
    public class QuanLyTonThatDMAController : ApiController
    {
        private TonThatDB context = new TonThatDB();


        [Route("SelectSanLuong")]
        [HttpGet]
        public HttpResponseMessage SelectSanLuong(string maDMA, int? nam, int? ky)
        {
            try
            {
                Double result = 0;

                if (nam.HasValue && ky.HasValue)
                {
                    result = this.context.TieuThuDMA(maDMA, nam.Value, ky.Value);
                }
                else
                {
                    result = this.context.TieuThuDMA(maDMA);
                }
                return Request.CreateResponse(HttpStatusCode.OK, result);

            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }

        [Route("SelectSanLuongDMA")]
        [HttpGet]
        public HttpResponseMessage SelectSanLuongDMA(string maDMA, string tuNgay, string denNgay)
        {
            try
            {
                Double result = 0;
                var time = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                var dtTuNgay = DateTime.ParseExact(tuNgay, "yyyy-MM-dd",
                                           System.Globalization.CultureInfo.InvariantCulture);
                var dtDenNgay = DateTime.ParseExact(denNgay, "yyyy-MM-dd",
                                           System.Globalization.CultureInfo.InvariantCulture);
                result = this.context.SanLuongDMA(maDMA, dtTuNgay, dtDenNgay);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }

        [Route("LayChiTietSanLuongDMA")]
        [HttpGet]
        public HttpResponseMessage LayChiTietSanLuongDMA(string maDMA, string tuNgay, string denNgay)
        {
            try
            {
                var time = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                var dtTuNgay = DateTime.ParseExact(tuNgay, "yyyy-MM-dd",
                                           System.Globalization.CultureInfo.InvariantCulture);
                var dtDenNgay = DateTime.ParseExact(denNgay, "yyyy-MM-dd",
                                           System.Globalization.CultureInfo.InvariantCulture);
                var result = this.context.LayChiTietSanLuongDMA(maDMA, dtTuNgay, dtDenNgay);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }

        [Route("LayChiTietApLucDMA")]
        [HttpGet]
        public HttpResponseMessage LayChiTietApLucDMA(string maDMA, string tuNgay, string denNgay)
        {
            try
            {
                var dtTuNgay = DateTime.ParseExact(tuNgay, "yyyy-MM-dd",
                                           System.Globalization.CultureInfo.InvariantCulture);
                var dtDenNgay = DateTime.ParseExact(denNgay, "yyyy-MM-dd",
                                           System.Globalization.CultureInfo.InvariantCulture);
                var result = this.context.LayChiTietApLucDMA(maDMA, dtTuNgay, dtDenNgay);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }

        [Route("ILI")]
        [HttpGet]
        public HttpResponseMessage ILI(float P)
        {
            try
            {
                var result = this.context.ILI(P);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }
        [Route("SelectSanLuongKhachHangTrenDMA")]
        [HttpGet]
        public HttpResponseMessage SelectSanLuongKhachHangTrenDMA(string maDMA, int nam, int ky)
        {
            try
            {
                TonThatModel result = new TonThatModel()
                {
                    SoLuongKH = 0,
                    SanLuongKH = 0
                };

                result = this.context.SanLuongKhachHangTrenDMA(maDMA, nam, ky);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }
        [Route("SelectLoggerDMA")]
        [HttpGet]
        public HttpResponseMessage SelectLoggerDMA()
        {
            try
            {
                var result = this.context.SelectLoggerDMA();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }
        [Route("DSSCTheoDMA")]
        [HttpGet]
        public HttpResponseMessage SelectDSSCTheoDMA(string tuNgay,string denNgay)
        {
            try
            {
                var dtTuNgay = DateTime.ParseExact(tuNgay, "yyyy-MM-dd",
                                          System.Globalization.CultureInfo.InvariantCulture);
                var dtDenNgay = DateTime.ParseExact(denNgay, "yyyy-MM-dd",
                                           System.Globalization.CultureInfo.InvariantCulture);
                var result = this.context.LayDanhSachSuCoTheoDMA(dtTuNgay,dtDenNgay);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }
    }
}
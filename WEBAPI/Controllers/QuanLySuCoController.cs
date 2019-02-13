using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Http.Description;
using WebAPI.DataProvider;
using WebAPI.DataProvider.EF;
using WebAPI.DataProvider.GIS;
using WebAPI.DataProvider.Models;
using WebAPI.IO;
using WebAPI.IO.Models;
using static WebAPI.DataProvider.Models.SuCoModel;

namespace WebAPI.Controllers
{
    [Authorize]
    [RoutePrefix("QuanLySuCo")]
    public class QuanLySuCoController : ApiController
    {
        private SuCoDB context = new SuCoDB();

        [Route("inphieucongtac")]
        [AllowAnonymous]
        [HttpGet]
        public HttpResponseMessage InPhieuCongTac(string idSuCo)
        {

            try
            {

                var io = new ExlPhieuCongTac();
                var path = HostingEnvironment.ApplicationPhysicalPath + @"/Resources/SuCo/phieucongtac.xlsx";
                var phieuCongTac = new PhieuCongTac();
                var suCoModel = this.context.Get(idSuCo);
                if(suCoModel == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Không tìm thấy điểm sự cố có mã " + idSuCo);
                }
                phieuCongTac.ThoiGian = DateTime.Now;
                phieuCongTac.SoDienThoai = suCoModel.SDTPhanAnh;
                phieuCongTac.PhatHienBoi = suCoModel.HinhThucPhatHien.HasValue && suCoModel.HinhThucPhatHien.Value > 0 && suCoModel.HinhThucPhatHien.Value < 4 ?
                    suCoModel.HinhThucPhatHien.Value == (short)HinhThucPhatHien.KHACH ? "Khách hàng" :
                    suCoModel.HinhThucPhatHien.Value == (short)HinhThucPhatHien.TCTB ? "Đội TCTB" : " Đội TCXL" : "Không xác định";
                phieuCongTac.DoiTuong = suCoModel.DoiQuanLy;
                phieuCongTac.DiaChi = suCoModel.DiaChi;
                phieuCongTac.ID = idSuCo;

                // hành chính	
                if (!String.IsNullOrEmpty(suCoModel.MaPhuong))
                {
                    var hcContext = new HanhChinhDB();
                    var hcModel = hcContext.Get(suCoModel.MaPhuong);
                    if (hcModel != null)
                    {
                        phieuCongTac.Phuong = String.IsNullOrEmpty(hcModel.TenHanhChinh)?"":hcModel.TenHanhChinh.Replace("Phường",String.Empty);
                        phieuCongTac.Quan = String.IsNullOrEmpty(hcModel.TenHuyen)?"":hcModel.TenHuyen.Replace("Quận",String.Empty);

                    }
                }


                // cập nhật tên đội trưởng
                if (phieuCongTac.DoiTuong.Equals("QLCN1"))
                {
                    phieuCongTac.DoiTruong = "A";
                    phieuCongTac.DoiTuong = "P. QUẢN LÝ CẤP NƯỚC 1";
                }
                else if (phieuCongTac.DoiTuong.Equals("QLCN2"))
                {
                    phieuCongTac.DoiTruong = "B";
                    phieuCongTac.DoiTuong = "P. QUẢN LÝ CẤP NƯỚC 2";
                }

                // nếu đã sửa thì modify phiếu hoàn công
                var hoSoVatTus = this.context.SelectMaterialsBySuCo(idSuCo);

                phieuCongTac.VatTus = hoSoVatTus.Select(s => new VatTu
                {
                    DonVi = s.DonViTinh,
                    SoLuong = (float)s.SoLuong,
                    TenVatTu = s.TenVatTu
                }).ToList();

                var binaryFile = io.CreatePhieuCongTac(path, phieuCongTac);

                var stream = new MemoryStream(binaryFile);
                // processing the stream.

                var result = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(stream.ToArray())
                };

                result.Content.Headers.ContentDisposition =
                    new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                    {
                        FileName = "phieucongtac.xlsx"
                    };
                result.Content.Headers.ContentType =
                    new MediaTypeHeaderValue("application/octet-stream");

                return result;
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
    }

    [Route("SelectMaterialsBySuCo")]
        [HttpGet]
        public HttpResponseMessage SelectMaterialsBySuCo(string id)
        {
            try
            {
                var result = this.context.SelectMaterialsBySuCo(id);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }
        [Route("CustomersByDMA")]
        [HttpGet]
        public HttpResponseMessage CustomersByDMA(string dma, long tu, long den)
        {
            try
            {
                var dtTuNgay = new DateTime(1970, 1, 1) + TimeSpan.FromMilliseconds(tu);
                var dtDenNgay = new DateTime(1970, 1, 1) + TimeSpan.FromMilliseconds(den);
                var result = this.context.CustomersByDMA(dma, dtTuNgay, dtDenNgay);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }


        [Route("HintNhomKhacPhuc/{nhomKhacPhuc}")]
        [ResponseType(typeof(string))]
        [HttpGet]
        public HttpResponseMessage HintNhomKhacPhuc(string nhomKhacPhuc)
        {
            try
            {
                var result = this.context.HintNhomKhacPhuc(nhomKhacPhuc);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }

        [Route("GetDistinctNhomKhacPhuc")]
        [ResponseType(typeof(IEnumerable<string>))]
        [HttpGet]
        public HttpResponseMessage GetDistinctNhomKhacPhuc()
        {
            try
            {
                var result = this.context.GetDistinctNhomKhacPhuc();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }
    }
}
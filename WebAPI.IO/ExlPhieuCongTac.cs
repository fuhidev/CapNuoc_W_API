using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.IO.CellPosition;
using WebAPI.IO.Models;

namespace WebAPI.IO
{
    public class ExlPhieuCongTac
    {
        /// <summary>
        /// Tạo phiếu công tác
        /// </summary>
        /// <param name="path">Đường dẫn tệp gốc</param>
        /// <param name="model">Phiếu công tác</param>
        /// <returns>Binary tệp</returns>
        public byte[] CreatePhieuCongTac(string path, PhieuCongTac model)
        {
            FileInfo fileInfo = new FileInfo(path);
            ExcelPackage p = new ExcelPackage(fileInfo);
            ExcelWorksheet myWorksheet = p.Workbook.Worksheets.First();

            if (model.ThoiGian != null)
            {
                myWorksheet.Cells[CPPhieuCongTac.ThoiGian].Value = 
                    String.Format("Quận 5. Ngày {0} tháng {1} năm {2}",model.ThoiGian.Day,model.ThoiGian.Month,model.ThoiGian.Year);
            }
            else
            {
                myWorksheet.Cells[CPPhieuCongTac.ThoiGian].Value = String.Empty;
            }

            if (!String.IsNullOrEmpty(model.ID))
            {
                myWorksheet.Cells[CPPhieuCongTac.ID].Value = model.ID;
            }
            else
            {
                myWorksheet.Cells[CPPhieuCongTac.ID].Value = String.Empty;
            }

            if (!String.IsNullOrEmpty(model.DiaChi))
            {
                myWorksheet.Cells[CPPhieuCongTac.DiaChi].Value = model.DiaChi;
            }
            else
            {
                myWorksheet.Cells[CPPhieuCongTac.DiaChi].Value = String.Empty;
            }

            if (!String.IsNullOrEmpty(model.SoDienThoai))
            {
                myWorksheet.Cells[CPPhieuCongTac.SoDienThoai].Value = model.SoDienThoai;
            }
            else
            {
                myWorksheet.Cells[CPPhieuCongTac.SoDienThoai].Value = String.Empty;
            }

            if (!String.IsNullOrEmpty(model.PhatHienBoi))
            {
                myWorksheet.Cells[CPPhieuCongTac.PhatHienBoi].Value = model.PhatHienBoi;
            }
            else
            {
                myWorksheet.Cells[CPPhieuCongTac.PhatHienBoi].Value = String.Empty;
            }

            if (model.Phuong != null)
            {
                myWorksheet.Cells[CPPhieuCongTac.Phuong].Value = model.Phuong;
            }
            else
            {
                myWorksheet.Cells[CPPhieuCongTac.Phuong].Value = String.Empty;
            }

            if (!String.IsNullOrEmpty(model.Quan))
            {
                myWorksheet.Cells[CPPhieuCongTac.Quan].Value = model.Quan;
            }
            else
            {
                myWorksheet.Cells[CPPhieuCongTac.Quan].Value = String.Empty;
            }

            if (!String.IsNullOrEmpty(model.Quan))
            {
                myWorksheet.Cells[CPPhieuCongTac.Quan].Value = model.Quan;
            }
            else
            {
                myWorksheet.Cells[CPPhieuCongTac.Quan].Value = String.Empty;
            }

            if (!String.IsNullOrEmpty(model.DMA))
            {
                myWorksheet.Cells[CPPhieuCongTac.DMA].Value = model.DMA;
            }
            else
            {
                myWorksheet.Cells[CPPhieuCongTac.DMA].Value = String.Empty;
            }

            if (!String.IsNullOrEmpty(model.DoiTuong))
            {
                myWorksheet.Cells[CPPhieuCongTac.DoiTuong].Value = model.DoiTuong;
            }
            else
            {
                myWorksheet.Cells[CPPhieuCongTac.DoiTuong].Value = String.Empty;
            }

            if (!String.IsNullOrEmpty(model.DoiTruong))
            {
                myWorksheet.Cells[CPPhieuCongTac.DoiTruong].Value = model.DoiTruong;
            }
            else
            {
                myWorksheet.Cells[CPPhieuCongTac.DoiTruong].Value = String.Empty;
            }

            if (model.PhuiDaos != null && model.PhuiDaos.Count > 0)
            {
                var dx = CPPhieuCongTac.TenVatTu.Substring(0, 1); // cột x của dài đầu tiên
                var rx = CPPhieuCongTac.SoLuongVatTu.Substring(0, 2); // cột x của rộng đầu tiên
                var sx = CPPhieuCongTac.DonViVatTu.Substring(0, 2); // cột x của sâu đầu tiên

                var dy = int.Parse(CPPhieuCongTac.TenVatTu.Substring(1)); // cột y của dài đầu tiên
                var ry = int.Parse(CPPhieuCongTac.SoLuongVatTu.Substring(2));// cột y của rộng đầu tiên
                var sy = int.Parse(CPPhieuCongTac.DonViVatTu.Substring(2));// cột y của sâu đầu tiên
                for (var i = 0; i < model.PhuiDaos.Count; i++)
                {
                    // lấy cột Y, giữ lại cột X, tăng số Y theo số lượng phui đào, bước nhảy là 2
                    var pd = model.PhuiDaos[i];

                    if (pd.Dai.HasValue)
                    {
                        myWorksheet.Cells[dx + (dy + i * 2)].Value = pd.Dai.Value;
                    }
                    else
                    {
                        myWorksheet.Cells[dx + (dy + i * 2)].Value = String.Empty;
                    }

                    if (pd.Rong.HasValue)
                    {
                        myWorksheet.Cells[rx + (ry + i * 2)].Value = pd.Rong.Value;
                    }
                    else
                    {
                        myWorksheet.Cells[rx + (ry + i * 2)].Value = String.Empty;
                    }

                    if (pd.Sau.HasValue)
                    {
                        myWorksheet.Cells[sx + (sy + i * 2)].Value = pd.Sau.Value;
                    }
                    else
                    {
                        myWorksheet.Cells[sx + (sy + i * 2)].Value = String.Empty;
                    }
                }
            }

            if (model.VatTus != null && model.VatTus.Count > 0)
            {
                var tx = CPPhieuCongTac.TenVatTu.Substring(0, 1); // cột x của tên vật tư đầu tiên
                var sx = CPPhieuCongTac.SoLuongVatTu.Substring(0, 1); // cột x của số lượng vật tư đầu tiên
                var dx = CPPhieuCongTac.DonViVatTu.Substring(0, 1); // cột x của đơn vị vật tư đầu tiên

                var ty = int.Parse(CPPhieuCongTac.TenVatTu.Substring(1)); // cột y của tên vật tư đầu tiên
                var sy = int.Parse(CPPhieuCongTac.SoLuongVatTu.Substring(1));// cột y của số lượng vật tư đầu tiên
                var dy = int.Parse(CPPhieuCongTac.DonViVatTu.Substring(1));// cột y của đơn vị vật tư đầu tiên
                for (var i = 0; i < model.VatTus.Count; i++)
                {
                    // lấy cột Y, giữ lại cột X, tăng số Y theo số lượng phui đào, bước nhảy là 2
                    var vt = model.VatTus[i];

                    if (!String.IsNullOrEmpty(vt.TenVatTu))
                    {
                        myWorksheet.Cells[tx + (ty + i * 2)].Value = vt.TenVatTu;
                    }
                    else
                    {
                        myWorksheet.Cells[tx + (ty + i * 2)].Value = String.Empty;
                    }

                    myWorksheet.Cells[sx + (sy + i * 2)].Value = vt.SoLuong;

                    if (!String.IsNullOrEmpty(vt.DonVi))
                    {
                        myWorksheet.Cells[dx + (dy + i * 2)].Value = vt.DonVi;
                    }
                    else
                    {
                        myWorksheet.Cells[dx + (dy + i * 2)].Value = String.Empty;
                    }
                }
            }


            return p.GetAsByteArray();
        }

    }
}

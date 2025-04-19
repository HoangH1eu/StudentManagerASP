using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuanLyViecLamSinhVien
{
    public partial class ThongTinChiTiet : System.Web.UI.Page
    {
        private DataAccessHelper dbHelper = new DataAccessHelper();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadThongTinSinhVien();
            }
        }

        private void LoadThongTinSinhVien()
        {
            try
            {
                string maSinhVien = Session["MaSinhVien"]?.ToString();
                if (string.IsNullOrEmpty(maSinhVien))
                {
                    Response.Redirect("~/Login.aspx");
                    return;
                }

                string query = @"
        SELECT 
            sv.MaSinhVien, 
            sv.HoTen, 
            sv.NgaySinh, 
            sv.GioiTinh, 
            sv.Email, 
            sv.SoDienThoai, 
            sv.Khoa, 
            sv.Lop, 
            sv.NgayTotNghiep, 
            k.TenKhoa, 
            ttvl.ViTri, 
            ttvl.TenCongTy
        FROM 
            SinhVien sv
        LEFT JOIN 
            Khoa k ON sv.MaKhoa = k.MaKhoa
        LEFT JOIN 
            ThongTinViecLam ttvl ON sv.MaSinhVien = ttvl.MaSinhVien
        WHERE 
            sv.MaSinhVien = @MaSinhVien";

                SqlParameter[] parameters = { new SqlParameter("@MaSinhVien", maSinhVien) };
                DataTable dt = dbHelper.ExecuteQuery(query, parameters);

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    lblMaSinhVien.Text = row["MaSinhVien"].ToString();
                    lblHoTen.Text = row["HoTen"].ToString();
                    lblNgaySinh.Text = row["NgaySinh"] != DBNull.Value ? Convert.ToDateTime(row["NgaySinh"]).ToString("dd/MM/yyyy") : "Chưa cập nhật";
                    lblGioiTinh.Text = row["GioiTinh"].ToString();
                    lblKhoa.Text = row["TenKhoa"].ToString();
                    lblKhoaHoc.Text = row["Khoa"]?.ToString() ?? "Chưa cập nhật";
                    lblLop.Text = row["Lop"]?.ToString() ?? "Chưa cập nhật";
                    lblNgayTotNghiep.Text = row["NgayTotNghiep"] != DBNull.Value ? Convert.ToDateTime(row["NgayTotNghiep"]).ToString("dd/MM/yyyy") : "Chưa cập nhật";
                    lblEmail.Text = row["Email"].ToString();
                    lblSoDienThoai.Text = row["SoDienThoai"].ToString();
                    lblViTri.Text = row["ViTri"]?.ToString() ?? "Chưa cập nhật";
                    lblCongTy.Text = row["TenCongTy"]?.ToString() ?? "Chưa cập nhật";
                }
                else
                {
                    lblMessage.Text = "Không tìm thấy thông tin sinh viên.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Lỗi khi tải thông tin: " + ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

    }

}
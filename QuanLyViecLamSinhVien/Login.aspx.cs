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
    public partial class Login : System.Web.UI.Page
    {
        private DataAccessHelper dbHelper = new DataAccessHelper(); // Khởi tạo DataAccessHelper

        protected void btnDangNhapSinhVien_Click(object sender, EventArgs e)
        {
            string maSinhVien = txtMaSinhVien.Text.Trim();
            string matKhau = txtMatKhauSinhVien.Text.Trim();

            if (string.IsNullOrEmpty(maSinhVien) || string.IsNullOrEmpty(matKhau))
            {
                lblThongBaoSinhVien.Text = "Vui lòng nhập đầy đủ thông tin.";
                return;
            }

            try
            {
                string query = "SELECT VaiTro FROM NguoiDung WHERE MaSinhVien = @MaSinhVien AND MatKhau = @MatKhau";
                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@MaSinhVien", maSinhVien),
            new SqlParameter("@MatKhau", matKhau)
                };

                object vaiTro = dbHelper.ExecuteScalar(query, parameters);

                if (vaiTro != null && vaiTro.ToString() == "SinhVien")
                {
                    Session["MaSinhVien"] = maSinhVien;
                    Session["VaiTro"] = "SinhVien";
                    Response.Redirect("StudentDashboard.aspx");
                }
                else
                {
                    lblThongBaoSinhVien.Text = "Mã sinh viên hoặc mật khẩu không đúng.";
                    txtMaSinhVien.Text = string.Empty; // Xóa ô nhập mã sinh viên
                    txtMatKhauSinhVien.Text = string.Empty; // Xóa ô nhập mật khẩu
                }
            }
            catch (Exception ex)
            {
                lblThongBaoSinhVien.Text = "Lỗi hệ thống: " + ex.Message;
            }
        }

        protected void btnDangNhapAdmin_Click(object sender, EventArgs e)
        {
            string tenDangNhap = txtTenDangNhap.Text.Trim();
            string matKhau = txtMatKhauAdmin.Text.Trim();

            if (string.IsNullOrEmpty(tenDangNhap) || string.IsNullOrEmpty(matKhau))
            {
                lblThongBaoAdmin.Text = "Vui lòng nhập đầy đủ thông tin.";
                return;
            }

            try
            {
                string query = "SELECT VaiTro FROM NguoiDung WHERE TenDangNhap = @TenDangNhap AND MatKhau = @MatKhau";
                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@TenDangNhap", tenDangNhap),
            new SqlParameter("@MatKhau", matKhau)
                };

                object vaiTro = dbHelper.ExecuteScalar(query, parameters);

                if (vaiTro != null && vaiTro.ToString() == "Admin")
                {
                    Session["TenDangNhap"] = tenDangNhap;
                    Session["VaiTro"] = "Admin";
                    Response.Redirect("AdminDashboard.aspx");
                }
                else
                {
                    lblThongBaoAdmin.Text = "Tên đăng nhập hoặc mật khẩu không đúng.";
                    txtTenDangNhap.Text = string.Empty; // Xóa ô nhập tên đăng nhập
                    txtMatKhauAdmin.Text = string.Empty; // Xóa ô nhập mật khẩu
                }
            }
            catch (Exception ex)
            {
                lblThongBaoAdmin.Text = "Lỗi hệ thống: " + ex.Message;
            }
        }

    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuanLyViecLamSinhVien
{
    public partial class RegisterAccount : System.Web.UI.Page
    {
        private DataAccessHelper dbHelper = new DataAccessHelper();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadKhoa();
            }

        }
        private void LoadKhoa()
        {
            try
            {
                string query = "SELECT MaKhoa, TenKhoa FROM Khoa";
                DataTable dt = dbHelper.ExecuteQuery(query);

                ddlMaKhoa.DataSource = dt;
                ddlMaKhoa.DataTextField = "TenKhoa";  // Hiển thị tên khoa
                ddlMaKhoa.DataValueField = "MaKhoa";  // Giá trị là mã khoa
                ddlMaKhoa.DataBind();

                // Thêm tùy chọn mặc định
                ddlMaKhoa.Items.Insert(0, new ListItem("-- Chọn Khoa --", ""));
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Lỗi khi tải danh sách khoa: " + ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy dữ liệu từ form
                string maSinhVien = txtMaSinhVien.Text.Trim();
                string hoTen = txtHoTen.Text.Trim();
                string email = txtEmail.Text.Trim();
                string matKhau = txtMatKhau.Text.Trim();
                string xacNhanMatKhau = txtXacNhanMatKhau.Text.Trim();
                string ngaySinhText = txtNgaySinh.Text.Trim();
                string gioiTinh = ddlGioiTinh.SelectedValue;
                string diaChi = txtDiaChi.Text.Trim();
                string soDienThoai = txtSoDienThoai.Text.Trim();
                string maKhoa = ddlMaKhoa.SelectedValue;

                // Kiểm tra dữ liệu hợp lệ
                if (string.IsNullOrEmpty(maSinhVien) || string.IsNullOrEmpty(hoTen) ||
                    string.IsNullOrEmpty(email) || string.IsNullOrEmpty(matKhau) ||
                    string.IsNullOrEmpty(xacNhanMatKhau) || string.IsNullOrEmpty(maKhoa))
                {
                    lblMessage.Text = "Vui lòng điền đầy đủ thông tin.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                if (matKhau != xacNhanMatKhau)
                {
                    lblMessage.Text = "Mật khẩu và xác nhận mật khẩu không khớp.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                DateTime? ngaySinh = null;
                if (!string.IsNullOrEmpty(ngaySinhText))
                {
                    if (!DateTime.TryParse(ngaySinhText, out DateTime parsedDate))
                    {
                        lblMessage.Text = "Ngày sinh không hợp lệ.";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        return;
                    }
                    ngaySinh = parsedDate;
                }

                // Thêm sinh viên vào bảng SinhVien
                string insertSinhVienQuery = @"
                    INSERT INTO SinhVien (MaSinhVien, HoTen, Email, MaKhoa, NgaySinh, GioiTinh, DiaChi, SoDienThoai)
                    VALUES (@MaSinhVien, @HoTen, @Email, @MaKhoa, @NgaySinh, @GioiTinh, @DiaChi, @SoDienThoai)";
                var sinhVienParams = new SqlParameter[]
                {
                    new SqlParameter("@MaSinhVien", maSinhVien),
                    new SqlParameter("@HoTen", hoTen),
                    new SqlParameter("@Email", email),
                    new SqlParameter("@MaKhoa", maKhoa),
                    new SqlParameter("@NgaySinh", (object)ngaySinh ?? DBNull.Value),
                    new SqlParameter("@GioiTinh", gioiTinh),
                    new SqlParameter("@DiaChi", string.IsNullOrEmpty(diaChi) ? (object)DBNull.Value : diaChi),
                    new SqlParameter("@SoDienThoai", string.IsNullOrEmpty(soDienThoai) ? (object)DBNull.Value : soDienThoai)
                };
                dbHelper.ExecuteNonQuery(insertSinhVienQuery, sinhVienParams);

                // Thêm tài khoản vào bảng NguoiDung
                string insertNguoiDungQuery = @"
                    INSERT INTO NguoiDung (MaSinhVien, TenDangNhap, MatKhau, VaiTro)
                    VALUES (@MaSinhVien, @TenDangNhap, @MatKhau, 'SinhVien')";
                var nguoiDungParams = new SqlParameter[]
                {
                    new SqlParameter("@MaSinhVien", maSinhVien),
                    new SqlParameter("@TenDangNhap", maSinhVien),
                    new SqlParameter("@MatKhau", matKhau)
                };
                dbHelper.ExecuteNonQuery(insertNguoiDungQuery, nguoiDungParams);

                lblMessage.Text = "Đăng ký tài khoản thành công!";
                lblMessage.ForeColor = System.Drawing.Color.Green;

                // Xóa dữ liệu form sau khi đăng ký
                txtMaSinhVien.Text = "";
                txtHoTen.Text = "";
                txtEmail.Text = "";
                txtMatKhau.Text = "";
                txtXacNhanMatKhau.Text = "";
                txtNgaySinh.Text = "";
                ddlGioiTinh.SelectedIndex = 0;
                txtDiaChi.Text = "";
                txtSoDienThoai.Text = "";
                ddlMaKhoa.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Lỗi khi đăng ký: " + ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }


        private void ClearForm()
        {
            txtMaSinhVien.Text = "";
            txtHoTen.Text = "";
            txtEmail.Text = "";
            txtMatKhau.Text = "";
            txtXacNhanMatKhau.Text = "";
            txtNgaySinh.Text = "";
            ddlGioiTinh.SelectedIndex = 0;
            txtDiaChi.Text = "";
            txtSoDienThoai.Text = "";
            ddlMaKhoa.SelectedIndex = 0;
        }


        protected void btnBackToLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Login.aspx");
        }
    }
}
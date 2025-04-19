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
    public partial class AddSinhVien : System.Web.UI.Page
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
            string query = "SELECT MaKhoa, TenKhoa FROM Khoa";
            DataTable dt = dbHelper.ExecuteQuery(query, null);
            ddlKhoa.DataSource = dt;
            ddlKhoa.DataTextField = "TenKhoa";
            ddlKhoa.DataValueField = "MaKhoa";
            ddlKhoa.DataBind();
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("QuanLySinhVien.aspx"); // Quay lại trang quản lý sinh viên 
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string query = @"
            INSERT INTO SinhVien (MaSinhVien, HoTen, NgaySinh, GioiTinh, MaKhoa, SoDienThoai, NgayTotNghiep)
            VALUES (@MaSinhVien, @HoTen, @NgaySinh, @GioiTinh, @MaKhoa, @SoDienThoai, @NgayTotNghiep)";

                SqlParameter[] parameters = {
            new SqlParameter("@MaSinhVien", txtMaSinhVien.Text.Trim()),
            new SqlParameter("@HoTen", txtHoTen.Text.Trim()),
            new SqlParameter("@NgaySinh", string.IsNullOrEmpty(txtNgaySinh.Text) ? (object)DBNull.Value : DateTime.Parse(txtNgaySinh.Text)),
            new SqlParameter("@GioiTinh", ddlGioiTinh.SelectedValue),
            new SqlParameter("@MaKhoa", ddlKhoa.SelectedValue),
            new SqlParameter("@SoDienThoai", txtSoDienThoai.Text.Trim()),
            new SqlParameter("@NgayTotNghiep", string.IsNullOrEmpty(txtNgayTotNghiep.Text) ? (object)DBNull.Value : DateTime.Parse(txtNgayTotNghiep.Text))
        };

                dbHelper.ExecuteNonQuery(query, parameters);

                lblMessage.Text = "Thêm sinh viên thành công!";
                lblMessage.ForeColor = System.Drawing.Color.Green;
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Lỗi khi thêm sinh viên: " + ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

    }
}
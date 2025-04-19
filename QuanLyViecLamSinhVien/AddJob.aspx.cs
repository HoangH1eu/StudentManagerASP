using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuanLyViecLamSinhVien
{
    public partial class AddJob : System.Web.UI.Page
    {
        private DataAccessHelper dbHelper = new DataAccessHelper();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnSaveJob_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy dữ liệu từ form
                string tenCongTy = txtTenCongTy.Text.Trim();
                string chuyenNganh = txtChuyenNganh.Text.Trim();
                string mucLuongText = txtMucLuong.Text.Trim();
                string maSinhVien = txtMaSinhVien.Text.Trim();
                string viTri = txtViTri.Text.Trim();
                string ngayNhanViecText = txtNgayNhanViec.Text.Trim();

                // Kiểm tra dữ liệu hợp lệ
                if (string.IsNullOrEmpty(tenCongTy) || string.IsNullOrEmpty(chuyenNganh) ||
                    string.IsNullOrEmpty(mucLuongText) || string.IsNullOrEmpty(maSinhVien) ||
                    string.IsNullOrEmpty(viTri) || string.IsNullOrEmpty(ngayNhanViecText))
                {
                    lblMessage.Text = "Vui lòng điền đầy đủ thông tin.";
                    return;
                }

                if (!decimal.TryParse(mucLuongText, out decimal mucLuong))
                {
                    lblMessage.Text = "Mức lương phải là số.";
                    return;
                }

                if (!DateTime.TryParse(ngayNhanViecText, out DateTime ngayNhanViec))
                {
                    lblMessage.Text = "Ngày nhận việc không hợp lệ.";
                    return;
                }

                // Kiểm tra MaSinhVien có tồn tại không
                string checkStudentQuery = "SELECT COUNT(*) FROM SinhVien WHERE MaSinhVien = @MaSinhVien";
                var checkParameters = new SqlParameter[]
                {
            new SqlParameter("@MaSinhVien", maSinhVien)
                };

                int studentExists = Convert.ToInt32(dbHelper.ExecuteScalar(checkStudentQuery, checkParameters));
                if (studentExists == 0)
                {
                    lblMessage.Text = "Mã sinh viên không tồn tại.";
                    return;
                }

                // Thêm việc làm vào cơ sở dữ liệu
                string query = @"
            INSERT INTO ThongTinViecLam (TenCongTy, ChuyenNganh, MucLuong, MaSinhVien, ViTri, NgayNhanViec)
            VALUES (@TenCongTy, @ChuyenNganh, @MucLuong, @MaSinhVien, @ViTri, @NgayNhanViec)";
                var parameters = new SqlParameter[]
                {
            new SqlParameter("@TenCongTy", tenCongTy),
            new SqlParameter("@ChuyenNganh", chuyenNganh),
            new SqlParameter("@MucLuong", mucLuong),
            new SqlParameter("@MaSinhVien", maSinhVien),
            new SqlParameter("@ViTri", viTri),
            new SqlParameter("@NgayNhanViec", ngayNhanViec)
                };

                int rowsAffected = dbHelper.ExecuteNonQuery(query, parameters);

                if (rowsAffected > 0)
                {
                    lblMessage.Text = "Thêm việc làm thành công.";
                    lblMessage.ForeColor = System.Drawing.Color.Green;

                    // Xóa dữ liệu cũ
                    txtTenCongTy.Text = "";
                    txtChuyenNganh.Text = "";
                    txtMucLuong.Text = "";
                    txtMaSinhVien.Text = "";
                    txtViTri.Text = "";
                    txtNgayNhanViec.Text = "";
                }
                else
                {
                    lblMessage.Text = "Thêm việc làm thất bại.";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Lỗi: " + ex.Message;
            }
        }




        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("QuanLyViecLam.aspx"); // Quay lại trang quản lý việc làm
        }

    }
}
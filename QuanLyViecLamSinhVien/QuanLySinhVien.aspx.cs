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
    public partial class QuanLySinhVien : System.Web.UI.Page
    {
            private DataAccessHelper dbHelper = new DataAccessHelper();

            protected void Page_Load(object sender, EventArgs e)
            {
                if (!IsPostBack)
                {
                    LoadSinhVien();
                    LoadKhoa();
            }
            }

        private void LoadSinhVien()
        {
            string query = @"
        SELECT sv.MaSinhVien, 
               sv.HoTen, 
               sv.NgaySinh, 
               sv.GioiTinh, 
               k.TenKhoa, 
               sv.Khoa, 
               sv.Lop, 
               sv.NgayTotNghiep 
        FROM SinhVien sv
        INNER JOIN Khoa k ON sv.MaKhoa = k.MaKhoa";

            DataTable dt = dbHelper.ExecuteQuery(query, null);

            // Kiểm tra cột có tồn tại trong DataTable hay không
            if (!dt.Columns.Contains("Khoa"))
            {
                throw new Exception("Cột 'Khoa' không tồn tại trong nguồn dữ liệu.");
            }

            gvSinhVien.DataSource = dt;
            gvSinhVien.DataBind();
        }





        protected void btnAddNew_Click(object sender, EventArgs e)
            {
                Response.Redirect("AddSinhVien.aspx");
            }

            protected void gvSinhVien_RowEditing(object sender, GridViewEditEventArgs e)
            {
                gvSinhVien.EditIndex = e.NewEditIndex;
                LoadSinhVien();
             }
        protected void gvSinhVien_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridViewRow row = gvSinhVien.Rows[e.RowIndex];

                string maSinhVien = ((Label)row.FindControl("lblMaSinhVien")).Text;
                string hoTen = ((TextBox)row.FindControl("txtHoTen")).Text;
                string ngaySinh = ((TextBox)row.FindControl("txtNgaySinh")).Text;
                string gioiTinh = ((DropDownList)row.FindControl("ddlGioiTinh")).SelectedValue;
                string tenKhoa = ((TextBox)row.FindControl("txtTenKhoa")).Text;
                string ngayTotNghiep = ((TextBox)row.FindControl("txtNgayTotNghiep")).Text;
                string khoa = ((TextBox)row.FindControl("txtKhoa")).Text; // Lấy dữ liệu từ ô nhập Khóa
                string lop = ((TextBox)row.FindControl("txtLop")).Text;   // Lấy dữ liệu từ ô nhập Lớp

                // Xử lý giá trị ngày tháng
                object ngaySinhValue = string.IsNullOrEmpty(ngaySinh) ? (object)DBNull.Value : DateTime.Parse(ngaySinh);
                object ngayTotNghiepValue = string.IsNullOrEmpty(ngayTotNghiep) ? (object)DBNull.Value : DateTime.Parse(ngayTotNghiep);

                // Lấy mã khoa từ tên khoa
                string queryKhoa = "SELECT MaKhoa FROM Khoa WHERE TenKhoa = @TenKhoa";
                var maKhoa = dbHelper.ExecuteScalar(queryKhoa, new SqlParameter[] {
            new SqlParameter("@TenKhoa", tenKhoa)
        });

                if (maKhoa != null)
                {
                    string query = @"
                UPDATE SinhVien 
                SET HoTen = @HoTen, 
                    NgaySinh = @NgaySinh, 
                    GioiTinh = @GioiTinh, 
                    MaKhoa = @MaKhoa, 
                    NgayTotNghiep = @NgayTotNghiep, 
                    Khoa = @Khoa, 
                    Lop = @Lop
                WHERE MaSinhVien = @MaSinhVien";

                    dbHelper.ExecuteNonQuery(query, new SqlParameter[] {
                new SqlParameter("@HoTen", hoTen),
                new SqlParameter("@NgaySinh", ngaySinhValue),
                new SqlParameter("@GioiTinh", gioiTinh),
                new SqlParameter("@MaKhoa", maKhoa),
                new SqlParameter("@NgayTotNghiep", ngayTotNghiepValue),
                new SqlParameter("@Khoa", khoa),
                new SqlParameter("@Lop", lop),
                new SqlParameter("@MaSinhVien", maSinhVien)
            });

                    lblMessage.Text = "Cập nhật thông tin sinh viên thành công.";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lblMessage.Text = "Không tìm thấy khoa tương ứng.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }

                gvSinhVien.EditIndex = -1;
                LoadSinhVien();
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Lỗi khi cập nhật thông tin sinh viên: " + ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }



        protected void gvSinhVien_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvSinhVien.EditIndex = -1;
            LoadSinhVien();
        }

        protected void gvSinhVien_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                // Lấy mã sinh viên từ GridView
                string maSinhVien = gvSinhVien.DataKeys[e.RowIndex].Value.ToString();

                // Xóa dữ liệu liên quan trong bảng NguoiDung
                string deleteNguoiDungQuery = "DELETE FROM NguoiDung WHERE MaSinhVien = @MaSinhVien";
                SqlParameter[] parametersNguoiDung = {
            new SqlParameter("@MaSinhVien", maSinhVien)
        };
                dbHelper.ExecuteNonQuery(deleteNguoiDungQuery, parametersNguoiDung);

                // Xóa sinh viên
                string deleteSinhVienQuery = "DELETE FROM SinhVien WHERE MaSinhVien = @MaSinhVien";
                SqlParameter[] parametersSinhVien = {
            new SqlParameter("@MaSinhVien", maSinhVien)
        };
                dbHelper.ExecuteNonQuery(deleteSinhVienQuery, parametersSinhVien);

                lblMessage.Text = "Xóa thành công!";
                lblMessage.ForeColor = System.Drawing.Color.Green;

                // Tải lại dữ liệu
                LoadSinhVien();
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Lỗi khi xóa: " + ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim(); // Lấy giá trị từ TextBox tìm kiếm

            // Câu lệnh SQL để tìm sinh viên theo mã hoặc tên
            string query = @"SELECT sv.MaSinhVien, sv.HoTen, sv.NgaySinh, sv.GioiTinh, k.TenKhoa 
                     FROM SinhVien sv
                     JOIN Khoa k ON sv.MaKhoa = k.MaKhoa
                     WHERE sv.HoTen LIKE @Keyword OR sv.MaSinhVien LIKE @Keyword";

            // Tham số cho câu lệnh SQL
            var parameters = new SqlParameter[]
            {
        new SqlParameter("@Keyword", "%" + keyword + "%") // Tìm kiếm gần đúng
            };

            // Thực hiện truy vấn và đổ dữ liệu vào GridView
            DataTable dt = dbHelper.ExecuteQuery(query, parameters);
            gvSinhVien.DataSource = dt;
            gvSinhVien.DataBind();
        }
        protected void btnClearSearch_Click(object sender, EventArgs e)
        {
            txtSearch.Text = string.Empty; // Xóa nội dung ô tìm kiếm
            LoadSinhVien(); // Tải lại danh sách sinh viên đầy đủ
        }
        private void LoadKhoa()
        {
            string query = "SELECT MaKhoa, TenKhoa FROM Khoa";
            DataTable dt = dbHelper.ExecuteQuery(query);

            ddlKhoa.DataSource = dt;
            ddlKhoa.DataTextField = "TenKhoa";  // Tên hiển thị
            ddlKhoa.DataValueField = "MaKhoa";  // Giá trị thực tế
            ddlKhoa.DataBind();

            ddlKhoa.Items.Insert(0, new ListItem("-- Tất cả Khoa --", "")); // Thêm tùy chọn mặc định
        }
        protected void ddlKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedMaKhoa = ddlKhoa.SelectedValue;

            string query = @"
        SELECT sv.MaSinhVien, 
               sv.HoTen, 
               sv.NgaySinh, 
               sv.GioiTinh, 
               sv.Khoa, 
               sv.Lop, 
               sv.NgayTotNghiep, 
               k.TenKhoa
        FROM SinhVien sv
        INNER JOIN Khoa k ON sv.MaKhoa = k.MaKhoa";

            if (!string.IsNullOrEmpty(selectedMaKhoa))
            {
                query += " WHERE sv.MaKhoa = @MaKhoa";
            }

            SqlParameter[] parameters = !string.IsNullOrEmpty(selectedMaKhoa)
                ? new SqlParameter[] { new SqlParameter("@MaKhoa", selectedMaKhoa) }
                : null;

            DataTable dt = dbHelper.ExecuteQuery(query, parameters);

            // Kiểm tra nếu thiếu cột
            if (!dt.Columns.Contains("Khoa"))
            {
                throw new Exception("Dữ liệu trả về không có cột 'Khoa'");
            }

            gvSinhVien.DataSource = dt;
            gvSinhVien.DataBind();
        }



    }
}

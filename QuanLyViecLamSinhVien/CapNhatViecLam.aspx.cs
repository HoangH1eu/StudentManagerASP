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
    public partial class CapNhatViecLam : System.Web.UI.Page
    {
        private DataAccessHelper dbHelper = new DataAccessHelper();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadViecLam();
            }
        }

        private void LoadViecLam()
        {
            string maSinhVien = Session["MaSinhVien"]?.ToString();
            if (string.IsNullOrEmpty(maSinhVien))
            {
                Response.Redirect("~/Login.aspx");
            }

            string query = @"SELECT TenCongTy, ViTri, MucLuong, NgayNhanViec, ChuyenNganh  
                             FROM ThongTinViecLam 
                             WHERE MaSinhVien = @MaSinhVien";
            SqlParameter[] parameters = { new SqlParameter("@MaSinhVien", maSinhVien) };
            DataTable dt = dbHelper.ExecuteQuery(query, parameters);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                txtTenCongTy.Text = row["TenCongTy"].ToString();
                txtViTri.Text = row["ViTri"].ToString();
                txtMucLuong.Text = row["MucLuong"].ToString();
                txtNgayNhanViec.Text = Convert.ToDateTime(row["NgayNhanViec"]).ToString("yyyy-MM-dd");
            }
        }

        protected void btnSaveJob_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy mã sinh viên từ session
                string maSinhVien = Session["MaSinhVien"]?.ToString();
                if (string.IsNullOrEmpty(maSinhVien))
                {
                    lblMessage.Text = "Không tìm thấy mã sinh viên. Vui lòng đăng nhập lại.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                // Lấy dữ liệu từ giao diện
                string tenCongTy = txtTenCongTy.Text.Trim();
                string viTri = txtViTri.Text.Trim();
                string mucLuongText = txtMucLuong.Text.Trim();
                string ngayNhanViecText = txtNgayNhanViec.Text.Trim();
                string chuyenNganh = ddlChuyenNganh.SelectedValue;

                // Kiểm tra dữ liệu nhập
                if (string.IsNullOrEmpty(tenCongTy) || string.IsNullOrEmpty(viTri) || string.IsNullOrEmpty(chuyenNganh))
                {
                    lblMessage.Text = "Vui lòng nhập đầy đủ thông tin.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                if (!decimal.TryParse(mucLuongText, out decimal parsedMucLuong))
                {
                    lblMessage.Text = "Mức lương không hợp lệ.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                DateTime? ngayNhanViec = null;
                if (!string.IsNullOrEmpty(ngayNhanViecText))
                {
                    if (!DateTime.TryParse(ngayNhanViecText, out DateTime parsedNgayNhanViec))
                    {
                        lblMessage.Text = "Ngày nhận việc không hợp lệ.";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        return;
                    }
                    ngayNhanViec = parsedNgayNhanViec;
                }

                // Kiểm tra xem đã có thông tin việc làm hay chưa
                string checkQuery = "SELECT COUNT(*) FROM ThongTinViecLam WHERE MaSinhVien = @MaSinhVien";
                SqlParameter[] checkParameters = { new SqlParameter("@MaSinhVien", maSinhVien) };
                int count = (int)dbHelper.ExecuteScalar(checkQuery, checkParameters);

                string query;
                SqlParameter[] parameters;

                if (count > 0)
                {
                    // Nếu đã có thông tin việc làm -> Cập nhật
                    query = @"
                UPDATE ThongTinViecLam
                SET TenCongTy = @TenCongTy, ViTri = @ViTri, MucLuong = @MucLuong, 
                    NgayNhanViec = @NgayNhanViec, ChuyenNganh = @ChuyenNganh
                WHERE MaSinhVien = @MaSinhVien";

                    parameters = new SqlParameter[]
                    {
                new SqlParameter("@TenCongTy", tenCongTy),
                new SqlParameter("@ViTri", viTri),
                new SqlParameter("@MucLuong", parsedMucLuong),
                new SqlParameter("@NgayNhanViec", (object)ngayNhanViec ?? DBNull.Value),
                new SqlParameter("@ChuyenNganh", chuyenNganh),
                new SqlParameter("@MaSinhVien", maSinhVien)
                    };
                }
                else
                {
                    // Nếu chưa có thông tin việc làm -> Thêm mới
                    query = @"
                INSERT INTO ThongTinViecLam (MaSinhVien, TenCongTy, ViTri, MucLuong, NgayNhanViec, ChuyenNganh)
                VALUES (@MaSinhVien, @TenCongTy, @ViTri, @MucLuong, @NgayNhanViec, @ChuyenNganh)";

                    parameters = new SqlParameter[]
                    {
                new SqlParameter("@MaSinhVien", maSinhVien),
                new SqlParameter("@TenCongTy", tenCongTy),
                new SqlParameter("@ViTri", viTri),
                new SqlParameter("@MucLuong", parsedMucLuong),
                new SqlParameter("@NgayNhanViec", (object)ngayNhanViec ?? DBNull.Value),
                new SqlParameter("@ChuyenNganh", chuyenNganh)
                    };
                }

                // Thực hiện truy vấn
                dbHelper.ExecuteNonQuery(query, parameters);

                lblMessage.Text = "Thông tin việc làm đã được cập nhật thành công.";
                lblMessage.ForeColor = System.Drawing.Color.Green;
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Lỗi khi cập nhật thông tin: " + ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }


    }
}
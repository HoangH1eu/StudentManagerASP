using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuanLyViecLamSinhVien
{
    public partial class CapNhatThongTin : System.Web.UI.Page
    {
        private DataAccessHelper dbHelper = new DataAccessHelper();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadKhoa();
                LoadSinhVien();
            }
        }

        private void LoadKhoa()
        {
            try
            {
                string query = "SELECT MaKhoa, TenKhoa FROM Khoa";
                DataTable dt = dbHelper.ExecuteQuery(query, null);

                ddlKhoa.DataSource = dt;
                ddlKhoa.DataTextField = "TenKhoa";
                ddlKhoa.DataValueField = "MaKhoa";
                ddlKhoa.DataBind();

                ddlKhoa.Items.Insert(0, new ListItem("-- Chọn Khoa --", ""));
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Lỗi khi tải danh sách khoa: " + ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void LoadSinhVien()
        {
            try
            {
                string maSinhVien = Session["MaSinhVien"]?.ToString();
                if (string.IsNullOrEmpty(maSinhVien))
                {
                    Response.Redirect("~/Login.aspx");
                    return;
                }

                string query = "SELECT * FROM SinhVien WHERE MaSinhVien = @MaSinhVien";
                SqlParameter[] parameters = { new SqlParameter("@MaSinhVien", maSinhVien) };
                DataTable dt = dbHelper.ExecuteQuery(query, parameters);

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];

                    txtMaSinhVien.Text = row["MaSinhVien"].ToString();
                    txtHoTen.Text = row["HoTen"].ToString();
                    txtNgaySinh.Text = row["NgaySinh"] != DBNull.Value
                        ? Convert.ToDateTime(row["NgaySinh"]).ToString("yyyy-MM-dd")
                        : "";
                    ddlGioiTinh.SelectedValue = row["GioiTinh"].ToString();
                    ddlKhoa.SelectedValue = row["MaKhoa"].ToString();
                    txtNgayTotNghiep.Text = row["NgayTotNghiep"] != DBNull.Value ? Convert.ToDateTime(row["NgayTotNghiep"]).ToString("yyyy-MM-dd"): "";
                    txtEmail.Text = row["Email"]?.ToString(); // Thêm email
                    txtKhoa.Text = row["Khoa"].ToString(); // Hiển thị Khóa
                    txtLop.Text = row["Lop"].ToString();   // Hiển thị Lớp
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Lỗi khi tải thông tin sinh viên: " + ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string query = @"
        UPDATE SinhVien 
        SET HoTen = @HoTen, 
            NgaySinh = @NgaySinh, 
            GioiTinh = @GioiTinh, 
            MaKhoa = @MaKhoa, 
            SoDienThoai = @SoDienThoai, 
            Email = @Email,
            Khoa = @Khoa,
            Lop = @Lop,
            NgayTotNghiep = @NgayTotNghiep
        WHERE MaSinhVien = @MaSinhVien";

                SqlParameter[] parameters = {
            new SqlParameter("@MaSinhVien", txtMaSinhVien.Text.Trim()),
            new SqlParameter("@HoTen", txtHoTen.Text.Trim()),
            new SqlParameter("@NgaySinh", string.IsNullOrEmpty(txtNgaySinh.Text)
                ? (object)DBNull.Value
                : DateTime.Parse(txtNgaySinh.Text)),
            new SqlParameter("@GioiTinh", ddlGioiTinh.SelectedValue),
            new SqlParameter("@MaKhoa", ddlKhoa.SelectedValue),
            new SqlParameter("@SoDienThoai", string.IsNullOrEmpty(txtSoDienThoai.Text)
                ? (object)DBNull.Value
                : txtSoDienThoai.Text.Trim()),
            new SqlParameter("@Email", string.IsNullOrEmpty(txtEmail.Text)
                ? (object)DBNull.Value
                : txtEmail.Text.Trim()),
            new SqlParameter("@Khoa", string.IsNullOrEmpty(txtKhoa.Text)
                ? (object)DBNull.Value
                : txtKhoa.Text.Trim()), // Thêm tham số Khoa
            new SqlParameter("@Lop", string.IsNullOrEmpty(txtLop.Text)
                ? (object)DBNull.Value
                : txtLop.Text.Trim()), // Thêm tham số Lớp
            new SqlParameter("@NgayTotNghiep", string.IsNullOrEmpty(txtNgayTotNghiep.Text)
                ? (object)DBNull.Value
                : DateTime.Parse(txtNgayTotNghiep.Text))
        };

                dbHelper.ExecuteNonQuery(query, parameters);

                lblMessage.Text = "Thông tin đã được cập nhật!";
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

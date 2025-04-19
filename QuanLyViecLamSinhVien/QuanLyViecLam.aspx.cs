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
    public partial class QuanLyViecLam : System.Web.UI.Page
    {
        private DataAccessHelper dbHelper = new DataAccessHelper();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }
        private void LoadData()
        {
            string query = @"
        SELECT 
            ThongTinViecLam.MaViecLam,
            ThongTinViecLam.MaSinhVien,
            SinhVien.HoTen AS TenSinhVien,
            ThongTinViecLam.ViTri,
            ThongTinViecLam.TenCongTy,
            ThongTinViecLam.MucLuong,
            ThongTinViecLam.NgayNhanViec,
            ThongTinViecLam.ChuyenNganh
        FROM ThongTinViecLam
        LEFT JOIN SinhVien ON ThongTinViecLam.MaSinhVien = SinhVien.MaSinhVien";

            DataTable dt = dbHelper.ExecuteQuery(query);

            gvViecLam.DataSource = dt;
            gvViecLam.DataBind();
        }

        protected void gvViecLam_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && gvViecLam.EditIndex == e.Row.RowIndex)
            {
                DropDownList ddlChuyenNganh = e.Row.FindControl("ddlChuyenNganh") as DropDownList;
                if (ddlChuyenNganh != null)
                {
                    // Danh sách chuyên ngành tĩnh hoặc từ cơ sở dữ liệu
                    ddlChuyenNganh.Items.Add(new ListItem("Công Nghệ Thông Tin", "Công Nghệ Thông Tin"));
                    ddlChuyenNganh.Items.Add(new ListItem("Kỹ Thuật Điện", "Kỹ Thuật Điện"));
                    ddlChuyenNganh.Items.Add(new ListItem("Quản Trị Kinh Doanh", "Quản Trị Kinh Doanh"));
                    ddlChuyenNganh.Items.Add(new ListItem("Kế Toán", "Kế Toán"));

                    string currentChuyenNganh = DataBinder.Eval(e.Row.DataItem, "ChuyenNganh")?.ToString();
                    ListItem selectedItem = ddlChuyenNganh.Items.FindByText(currentChuyenNganh);
                    if (selectedItem != null)
                    {
                        ddlChuyenNganh.SelectedValue = selectedItem.Value;
                    }
                }
            }
        }


        private void LoadViecLam()
        {
            string query = @"SELECT MaViecLam, TenCongTy, ViTri, MucLuong, NgayNhanViec FROM ThongTinViecLam";
            DataTable dt = dbHelper.ExecuteQuery(query, null);
            gvViecLam.DataSource = dt;
            gvViecLam.DataBind();
        }

        protected void btnAddJob_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddJob.aspx");
        }

        protected void gvViecLam_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvViecLam.EditIndex = e.NewEditIndex;
            LoadData();
        }

        protected void gvViecLam_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                // Lấy DataKey
                if (gvViecLam.DataKeys[e.RowIndex] == null)
                {
                    lblMessage.Text = "Không tìm thấy mã việc làm.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                string maViecLam = gvViecLam.DataKeys[e.RowIndex].Value.ToString();

                // Tìm các Control
                TextBox txtViTri = gvViecLam.Rows[e.RowIndex].FindControl("txtViTri") as TextBox;
                TextBox txtTenCongTy = gvViecLam.Rows[e.RowIndex].FindControl("txtTenCongTy") as TextBox;
                TextBox txtMucLuong = gvViecLam.Rows[e.RowIndex].FindControl("txtMucLuong") as TextBox;
                TextBox txtNgayNhanViec = gvViecLam.Rows[e.RowIndex].FindControl("txtNgayNhanViec") as TextBox;
                DropDownList ddlChuyenNganh = gvViecLam.Rows[e.RowIndex].FindControl("ddlChuyenNganh") as DropDownList;

                if (txtViTri == null || txtTenCongTy == null || txtMucLuong == null || txtNgayNhanViec == null || ddlChuyenNganh == null)
                {
                    lblMessage.Text = "Không tìm thấy các trường dữ liệu.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                // Lấy giá trị từ Control
                string viTri = txtViTri.Text.Trim();
                string tenCongTy = txtTenCongTy.Text.Trim();
                string mucLuong = txtMucLuong.Text.Trim();
                string ngayNhanViecText = txtNgayNhanViec.Text.Trim();
                string chuyenNganh = ddlChuyenNganh.SelectedValue;

                // Kiểm tra dữ liệu
                if (!decimal.TryParse(mucLuong, out decimal parsedMucLuong))
                {
                    lblMessage.Text = "Mức lương không hợp lệ.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                if (!DateTime.TryParse(ngayNhanViecText, out DateTime ngayNhanViec))
                {
                    lblMessage.Text = "Ngày nhận việc không hợp lệ.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                // Cập nhật dữ liệu
                string updateQuery = @"
            UPDATE ThongTinViecLam
            SET ViTri = @ViTri, TenCongTy = @TenCongTy, MucLuong = @MucLuong, NgayNhanViec = @NgayNhanViec, ChuyenNganh = @ChuyenNganh
            WHERE MaViecLam = @MaViecLam";

                var parameters = new SqlParameter[]
                {
            new SqlParameter("@ViTri", viTri),
            new SqlParameter("@TenCongTy", tenCongTy),
            new SqlParameter("@MucLuong", parsedMucLuong),
            new SqlParameter("@NgayNhanViec", ngayNhanViec),
            new SqlParameter("@ChuyenNganh", chuyenNganh),
            new SqlParameter("@MaViecLam", maViecLam)
                };

                dbHelper.ExecuteNonQuery(updateQuery, parameters);

                lblMessage.Text = "Cập nhật thành công!";
                lblMessage.ForeColor = System.Drawing.Color.Green;

                gvViecLam.EditIndex = -1;
                LoadData();
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Lỗi khi cập nhật: " + ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }






        protected void gvViecLam_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                // Lấy Mã Việc Làm từ GridView
                string maViecLam = gvViecLam.DataKeys[e.RowIndex].Value.ToString();

                // Câu truy vấn xóa việc làm
                string deleteQuery = "DELETE FROM ThongTinViecLam WHERE MaViecLam = @MaViecLam";
                var parameters = new SqlParameter[]
                {
            new SqlParameter("@MaViecLam", maViecLam)
                };

                // Thực hiện truy vấn
                dbHelper.ExecuteNonQuery(deleteQuery, parameters);

                lblMessage.Text = "Xóa thành công!";
                lblMessage.ForeColor = System.Drawing.Color.Green;

                // Tải lại dữ liệu sau khi xóa
                LoadData();
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Lỗi khi xóa: " + ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }
        protected void gvViecLam_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvViecLam.EditIndex = -1;
            LoadData();
        }
    }
}
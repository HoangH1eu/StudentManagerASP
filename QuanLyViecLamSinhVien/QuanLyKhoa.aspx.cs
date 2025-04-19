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
    public partial class QuanLyKhoa : System.Web.UI.Page
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
            string query = @"SELECT k.MaKhoa, k.TenKhoa, 
                            (SELECT COUNT(*) FROM SinhVien WHERE SinhVien.MaKhoa = k.MaKhoa) AS SoSinhVien
                            FROM Khoa k";
            DataTable dt = dbHelper.ExecuteQuery(query);
            gvKhoa.DataSource = dt;
            gvKhoa.DataBind();
        }
        protected void btnAddKhoa_Click(object sender, EventArgs e)
        {
            try
            {
                string maKhoa = txtMaKhoa.Text.Trim().ToUpper();
                string tenKhoa = txtTenKhoa.Text.Trim();

                if (string.IsNullOrEmpty(maKhoa) || string.IsNullOrEmpty(tenKhoa))
                {
                    lblMessage.Text = "Vui lòng nhập đầy đủ thông tin.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                // Kiểm tra mã khoa đã tồn tại
                string queryCheck = "SELECT COUNT(*) FROM Khoa WHERE MaKhoa = @MaKhoa";
                int count = (int)dbHelper.ExecuteScalar(queryCheck, new SqlParameter[]
                {
            new SqlParameter("@MaKhoa", maKhoa)
                });

                if (count > 0)
                {
                    lblMessage.Text = "Mã khoa đã tồn tại.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                // Thêm khoa mới
                string queryInsert = "INSERT INTO Khoa (MaKhoa, TenKhoa) VALUES (@MaKhoa, @TenKhoa)";
                SqlParameter[] parameters = {
            new SqlParameter("@MaKhoa", maKhoa),
            new SqlParameter("@TenKhoa", tenKhoa)
        };

                dbHelper.ExecuteNonQuery(queryInsert, parameters);

                lblMessage.Text = "Thêm khoa thành công.";
                lblMessage.ForeColor = System.Drawing.Color.Green;

                // Xóa dữ liệu trong form
                txtMaKhoa.Text = string.Empty;
                txtTenKhoa.Text = string.Empty;

                // Tải lại danh sách khoa
                LoadKhoa();
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Lỗi: " + ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }



        protected void gvKhoa_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvKhoa.EditIndex = e.NewEditIndex;
            LoadKhoa();
        }

        protected void gvKhoa_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridViewRow row = gvKhoa.Rows[e.RowIndex];
                string maKhoa = gvKhoa.DataKeys[e.RowIndex].Value.ToString();
                string tenKhoa = ((TextBox)row.Cells[1].Controls[0]).Text.Trim();

                if (string.IsNullOrEmpty(tenKhoa))
                {
                    lblMessage.Text = "Tên khoa không được để trống.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                string query = "UPDATE Khoa SET TenKhoa = @TenKhoa WHERE MaKhoa = @MaKhoa";
                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@TenKhoa", tenKhoa),
                    new SqlParameter("@MaKhoa", maKhoa)
                };

                dbHelper.ExecuteNonQuery(query, parameters);
                lblMessage.Text = "Cập nhật khoa thành công.";
                lblMessage.ForeColor = System.Drawing.Color.Green;

                gvKhoa.EditIndex = -1;
                LoadKhoa();
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Lỗi khi cập nhật: " + ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void gvKhoa_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvKhoa.EditIndex = -1;
            LoadKhoa();
        }

        protected void gvKhoa_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                string maKhoa = gvKhoa.DataKeys[e.RowIndex].Value.ToString();
                string query = "DELETE FROM Khoa WHERE MaKhoa = @MaKhoa";
                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@MaKhoa", maKhoa)
                };

                dbHelper.ExecuteNonQuery(query, parameters);
                lblMessage.Text = "Xóa khoa thành công.";
                lblMessage.ForeColor = System.Drawing.Color.Green;

                LoadKhoa();
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Lỗi khi xóa: " + ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}
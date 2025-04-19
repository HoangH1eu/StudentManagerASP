using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuanLyViecLamSinhVien
{
    public partial class ThongKe : System.Web.UI.Page
    {
        private DataAccessHelper dbHelper = new DataAccessHelper();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadKhoa();
                ThongKeTongQuat();
            }
        }

        private void LoadKhoa()
        {
            try
            {
                string query = "SELECT MaKhoa, TenKhoa FROM Khoa";
                DataTable dt = dbHelper.ExecuteQuery(query);

                ddlKhoa.DataSource = dt;
                ddlKhoa.DataTextField = "TenKhoa";
                ddlKhoa.DataValueField = "MaKhoa";
                ddlKhoa.DataBind();

                ddlKhoa.Items.Insert(0, new ListItem("-- Tất cả --", ""));
            }
            catch (Exception ex)
            {
                lblThongKeTyLe.Text = "Lỗi khi tải danh sách khoa: " + ex.Message;
                lblThongKeTyLe.ForeColor = System.Drawing.Color.Red;
            }
        }
        protected void ddlKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string selectedKhoa = ddlKhoa.SelectedValue;

                // Nếu người dùng chọn "Tất cả khoa"
                if (string.IsNullOrEmpty(selectedKhoa))
                {
                    lblThongKeTyLe.Text = "Hiển thị tất cả sinh viên từ mọi khoa.";
                    lblThongKeTyLe.ForeColor = System.Drawing.Color.Green;

                    ThongKeTongQuat(); // Hiển thị toàn bộ dữ liệu
                }
                else
                {
                    lblThongKeTyLe.Text = $"Hiển thị dữ liệu cho khoa: {ddlKhoa.SelectedItem.Text}.";
                    lblThongKeTyLe.ForeColor = System.Drawing.Color.Green;

                    // Lọc dữ liệu theo khoa
                    string query = @"
                SELECT sv.MaSinhVien, sv.HoTen, sv.Khoa, sv.Lop, vl.TenCongTy, vl.ViTri, vl.ChuyenNganh, vl.MucLuong, vl.NgayNhanViec
                FROM SinhVien sv
                LEFT JOIN ThongTinViecLam vl ON sv.MaSinhVien = vl.MaSinhVien
                WHERE sv.MaKhoa = @MaKhoa";

                    SqlParameter[] parameters = { new SqlParameter("@MaKhoa", selectedKhoa) };
                    DataTable dt = dbHelper.ExecuteQuery(query, parameters);

                    gvThongKeChiTiet.DataSource = dt;
                    gvThongKeChiTiet.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblThongKeTyLe.Text = "Lỗi khi lọc dữ liệu theo khoa: " + ex.Message;
                lblThongKeTyLe.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void ddlThongKeLoai_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue = ddlThongKeLoai.SelectedValue;

            filterContainer.Visible = false;
            filterByCompany.Visible = false;
            filterByPosition.Visible = false;
            filterBySalary.Visible = false;
            filterByKhoa.Visible = false;
            filterByLop.Visible = false;

            lblThongKeTyLe.Visible = false;

            gvThongKeChiTiet.DataSource = null;
            gvThongKeChiTiet.DataBind();

            switch (selectedValue)
            {
                case "TongQuat":
                    ThongKeTongQuat();
                    break;
                case "TheoCongTy":
                    filterContainer.Visible = true;
                    filterByCompany.Visible = true;
                    break;
                case "TheoViTri":
                    filterContainer.Visible = true;
                    filterByPosition.Visible = true;
                    break;
                case "TheoMucLuong":
                    filterContainer.Visible = true;
                    filterBySalary.Visible = true;
                    break;
                case "DungChuyenNganh":
                    ThongKeDungChuyenNganh();
                    break;
                case "TheoKhoa":
                    filterContainer.Visible = true;
                    filterByKhoa.Visible = true;
                    break;
                case "TheoLop":
                    filterContainer.Visible = true;
                    filterByLop.Visible = true;
                    break;
            }
        }

        private void ThongKeTongQuat()
        {
            string query = @"
                SELECT SinhVien.MaSinhVien, SinhVien.HoTen, SinhVien.Khoa, SinhVien.Lop, ThongTinViecLam.ChuyenNganh, 
                       ThongTinViecLam.TenCongTy, ThongTinViecLam.ViTri, ThongTinViecLam.MucLuong, ThongTinViecLam.NgayNhanViec
                FROM SinhVien
                LEFT JOIN ThongTinViecLam ON SinhVien.MaSinhVien = ThongTinViecLam.MaSinhVien";
            DataTable dt = dbHelper.ExecuteQuery(query);
            gvThongKeChiTiet.DataSource = dt;
            gvThongKeChiTiet.DataBind();
        }
        protected void btnFilterByCompany_Click(object sender, EventArgs e)
        {
            string tenCongTy = txtCongTy.Text.Trim();
            string query = @"
        SELECT sv.MaSinhVien, sv.HoTen, sv.Khoa, sv.Lop, vl.TenCongTy, vl.ViTri, vl.ChuyenNganh, vl.MucLuong, vl.NgayNhanViec
        FROM SinhVien sv
        INNER JOIN ThongTinViecLam vl ON sv.MaSinhVien = vl.MaSinhVien
        WHERE vl.TenCongTy = @TenCongTy";

            SqlParameter[] parameters = { new SqlParameter("@TenCongTy", tenCongTy) };
            DataTable dt = dbHelper.ExecuteQuery(query, parameters);

            gvThongKeChiTiet.DataSource = dt;
            gvThongKeChiTiet.DataBind();
        }

        protected void btnFilterByKhoa_Click(object sender, EventArgs e)
        {
            string khoa = txtKhoa.Text.Trim();

            if (string.IsNullOrEmpty(khoa))
            {
                lblMessage.Text = "Vui lòng nhập Khóa để lọc.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            string query = @"
                SELECT SinhVien.MaSinhVien, SinhVien.HoTen, SinhVien.Khoa, SinhVien.Lop, ThongTinViecLam.ChuyenNganh, 
                       ThongTinViecLam.TenCongTy, ThongTinViecLam.ViTri, ThongTinViecLam.MucLuong, ThongTinViecLam.NgayNhanViec
                FROM SinhVien
                LEFT JOIN ThongTinViecLam ON SinhVien.MaSinhVien = ThongTinViecLam.MaSinhVien
                WHERE SinhVien.Khoa = @Khoa";
            SqlParameter[] parameters = { new SqlParameter("@Khoa", khoa) };
            DataTable dt = dbHelper.ExecuteQuery(query, parameters);

            gvThongKeChiTiet.DataSource = dt;
            gvThongKeChiTiet.DataBind();
        }

        protected void btnFilterByLop_Click(object sender, EventArgs e)
        {
            string lop = txtLop.Text.Trim();

            if (string.IsNullOrEmpty(lop))
            {
                lblMessage.Text = "Vui lòng nhập Lớp để lọc.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            string query = @"
                SELECT SinhVien.MaSinhVien, SinhVien.HoTen, SinhVien.Khoa, SinhVien.Lop, ThongTinViecLam.ChuyenNganh, 
                       ThongTinViecLam.TenCongTy, ThongTinViecLam.ViTri, ThongTinViecLam.MucLuong, ThongTinViecLam.NgayNhanViec
                FROM SinhVien
                LEFT JOIN ThongTinViecLam ON SinhVien.MaSinhVien = ThongTinViecLam.MaSinhVien
                WHERE SinhVien.Lop = @Lop";
            SqlParameter[] parameters = { new SqlParameter("@Lop", lop) };
            DataTable dt = dbHelper.ExecuteQuery(query, parameters);

            gvThongKeChiTiet.DataSource = dt;
            gvThongKeChiTiet.DataBind();
        }
        protected void btnFilterByPosition_Click(object sender, EventArgs e)
        {
            string viTri = txtViTri.Text.Trim();
            string query = @"
        SELECT sv.MaSinhVien, sv.HoTen, sv.Khoa, sv.Lop, vl.TenCongTy, vl.ViTri, vl.ChuyenNganh, vl.MucLuong, vl.NgayNhanViec
        FROM SinhVien sv
        INNER JOIN ThongTinViecLam vl ON sv.MaSinhVien = vl.MaSinhVien
        WHERE vl.ViTri = @ViTri";

            SqlParameter[] parameters = { new SqlParameter("@ViTri", viTri) };
            DataTable dt = dbHelper.ExecuteQuery(query, parameters);

            gvThongKeChiTiet.DataSource = dt;
            gvThongKeChiTiet.DataBind();
        }
        protected void btnFilterBySalary_Click(object sender, EventArgs e)
        {
            decimal.TryParse(txtMucLuongToiThieu.Text, out decimal mucLuongToiThieu);
            decimal.TryParse(txtMucLuongToiDa.Text, out decimal mucLuongToiDa);

            string query = @"
        SELECT sv.MaSinhVien, sv.HoTen, sv.Khoa, sv.Lop, vl.TenCongTy, vl.ViTri, vl.ChuyenNganh, vl.MucLuong, vl.NgayNhanViec
        FROM SinhVien sv
        INNER JOIN ThongTinViecLam vl ON sv.MaSinhVien = vl.MaSinhVien
        WHERE vl.MucLuong BETWEEN @MucLuongToiThieu AND @MucLuongToiDa";

            SqlParameter[] parameters = {
        new SqlParameter("@MucLuongToiThieu", mucLuongToiThieu),
        new SqlParameter("@MucLuongToiDa", mucLuongToiDa)
    };
            DataTable dt = dbHelper.ExecuteQuery(query, parameters);

            gvThongKeChiTiet.DataSource = dt;
            gvThongKeChiTiet.DataBind();
        }

        private void ThongKeDungChuyenNganh()
        {
            string query = @"
                SELECT SinhVien.MaSinhVien, SinhVien.HoTen, SinhVien.Khoa, SinhVien.Lop, Khoa.TenKhoa AS ChuyenNganh, 
                       ThongTinViecLam.TenCongTy, ThongTinViecLam.ViTri, ThongTinViecLam.MucLuong, ThongTinViecLam.NgayNhanViec
                FROM ThongTinViecLam
                INNER JOIN SinhVien ON ThongTinViecLam.MaSinhVien = SinhVien.MaSinhVien
                INNER JOIN Khoa ON SinhVien.MaKhoa = Khoa.MaKhoa
                WHERE Khoa.TenKhoa = ThongTinViecLam.ChuyenNganh";
            DataTable dt = dbHelper.ExecuteQuery(query);
            gvThongKeChiTiet.DataSource = dt;
            gvThongKeChiTiet.DataBind();
        }
    }
}

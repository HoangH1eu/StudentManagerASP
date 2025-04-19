using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuanLyViecLamSinhVien
{
    public partial class MasterBase : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["VaiTro"] != null)
                {
                    string vaiTro = Session["VaiTro"].ToString();

                    // Hiển thị tên người dùng dựa trên vai trò
                    if (vaiTro == "Admin")
                    {
                        lblUserName.Text = "Quản trị viên: " + Session["TenDangNhap"];
                    }
                    else if (vaiTro == "SinhVien")
                    {
                        lblUserName.Text = "Sinh viên: " + Session["MaSinhVien"];
                    }
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
            }
        }
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            // Xóa session và quay về trang đăng nhập
            Session.Clear();
            Response.Redirect("~/Login.aspx");
        }
    }
}
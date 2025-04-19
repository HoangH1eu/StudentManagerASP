using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuanLyViecLamSinhVien
{
    public partial class StudentDashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Kiểm tra vai trò sinh viên
                if (Session["VaiTro"] == null || Session["VaiTro"].ToString() != "SinhVien")
                {
                    Response.Redirect("~/Login.aspx");
                }

                // Có thể thêm logic để hiển thị thông tin sinh viên tại đây
                //lblWelcomeMessage.Text = "Chào mừng bạn, " + Session["MaSinhVien"];
            }
        }
    }
}
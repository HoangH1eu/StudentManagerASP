using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuanLyViecLamSinhVien
{
    public partial class AdminDashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Kiểm tra vai trò admin
                if (Session["VaiTro"] == null || Session["VaiTro"].ToString() != "Admin")
                {
                    Response.Redirect("~/Login.aspx");
                }
            }
        }
    }
}
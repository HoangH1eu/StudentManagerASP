using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuanLyViecLamSinhVien
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
                // Xóa tất cả session
                Session.Clear();
                Session.Abandon();

                // Chuyển hướng về màn hình đăng nhập
                Response.Redirect("~/Login.aspx");
            
        }
    }
}
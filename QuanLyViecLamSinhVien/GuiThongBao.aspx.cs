using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Mail;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyViecLamSinhVien
{
    public partial class GuiThongBao : System.Web.UI.Page
    {
        private DataAccessHelper dbHelper = new DataAccessHelper();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDanhSachSinhVien();
            }
        }

        private void LoadDanhSachSinhVien()
        {
            try
            {
                string query = "SELECT MaSinhVien, HoTen + ' (' + Email + ')' AS DisplayText FROM SinhVien WHERE Email IS NOT NULL";
                DataTable dt = dbHelper.ExecuteQuery(query);

                ddlSinhVien.DataSource = dt;
                ddlSinhVien.DataTextField = "DisplayText";
                ddlSinhVien.DataValueField = "MaSinhVien";
                ddlSinhVien.DataBind();

                ddlSinhVien.Items.Insert(0, new ListItem("Tất Cả Sinh Viên", "ALL"));
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Lỗi khi tải danh sách sinh viên: " + ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnGuiThongBao_Click(object sender, EventArgs e)
        {
            try
            {
                string tieuDe = txtTieuDe.Text.Trim();
                string noiDung = txtNoiDung.Text.Trim();
                string maSinhVien = ddlSinhVien.SelectedValue;

                if (string.IsNullOrEmpty(tieuDe) || string.IsNullOrEmpty(noiDung))
                {
                    lblMessage.Text = "Vui lòng nhập đầy đủ tiêu đề và nội dung.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                // Lấy danh sách email cần gửi
                List<string> emailList = new List<string>();
                if (maSinhVien == "ALL")
                {
                    string query = "SELECT Email FROM SinhVien WHERE Email IS NOT NULL";
                    DataTable dt = dbHelper.ExecuteQuery(query);

                    foreach (DataRow row in dt.Rows)
                    {
                        emailList.Add(row["Email"].ToString());
                    }
                }
                else
                {
                    string query = "SELECT Email FROM SinhVien WHERE MaSinhVien = @MaSinhVien";
                    SqlParameter[] parameters = { new SqlParameter("@MaSinhVien", maSinhVien) };
                    DataTable dt = dbHelper.ExecuteQuery(query, parameters);

                    if (dt.Rows.Count > 0)
                    {
                        emailList.Add(dt.Rows[0]["Email"].ToString());
                    }
                }

                // Gửi email
                foreach (string email in emailList)
                {
                    SendEmail(email, tieuDe, noiDung);
                }

                lblMessage.Text = "Gửi thông báo thành công!";
                lblMessage.ForeColor = System.Drawing.Color.Green;

                // Xóa dữ liệu form sau khi gửi
                txtTieuDe.Text = "";
                txtNoiDung.Text = "";
                ddlSinhVien.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Lỗi khi gửi thông báo: " + ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void SendEmail(string toEmail, string subject, string body)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("bachvu3103@gmail.com", "Quản Lý Việc Làm");
                mail.To.Add(toEmail);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential("bachvu3103@gmail.com", "bach03012003");
                smtp.EnableSsl = true;

                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi gửi email đến " + toEmail + ": " + ex.Message);
            }
        }

    }
}
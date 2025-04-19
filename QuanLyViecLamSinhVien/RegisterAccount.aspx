<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegisterAccount.aspx.cs" Inherits="QuanLyViecLamSinhVien.RegisterAccount" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Đăng ký tài khoản sinh viên</title>
    <link href="Style2.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="register-container">
            <h2>Đăng ký tài khoản sinh viên</h2>
            <asp:Label ID="lblMessage" runat="server" CssClass="error-message"></asp:Label>
            <table>
                <tr>
                    <td>Mã Sinh Viên:</td>
                    <td><asp:TextBox ID="txtMaSinhVien" runat="server" CssClass="input-box" /></td>
                </tr>
                <tr>
                    <td>Họ Tên:</td>
                    <td><asp:TextBox ID="txtHoTen" runat="server" CssClass="input-box" /></td>
                </tr>
                <tr>
                    <td>Email:</td>
                    <td><asp:TextBox ID="txtEmail" runat="server" CssClass="input-box" /></td>
                </tr>
                <tr>
                    <td>Mật Khẩu:</td>
                    <td><asp:TextBox ID="txtMatKhau" runat="server" TextMode="Password" CssClass="input-box" /></td>
                </tr>
                <tr>
                    <td>Xác Nhận Mật Khẩu:</td>
                    <td><asp:TextBox ID="txtXacNhanMatKhau" runat="server" TextMode="Password" CssClass="input-box" /></td>
                </tr>
                <tr>
                    <td>Ngày Sinh:</td>
                    <td><asp:TextBox ID="txtNgaySinh" runat="server" CssClass="input-box" TextMode="Date" /></td>
                </tr>
                <tr>
                    <td>Giới Tính:</td>
                    <td>
                        <asp:DropDownList ID="ddlGioiTinh" runat="server" CssClass="input-box">
                            <asp:ListItem Text="Nam" Value="Nam" />
                            <asp:ListItem Text="Nữ" Value="Nữ" />
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>Địa Chỉ:</td>
                    <td><asp:TextBox ID="txtDiaChi" runat="server" CssClass="input-box" /></td>
                </tr>
                <tr>
                    <td>Số Điện Thoại:</td>
                    <td><asp:TextBox ID="txtSoDienThoai" runat="server" CssClass="input-box" /></td>
                </tr>
                <tr>
                    <td>Khoa:</td>
                    <td>
                        <asp:DropDownList ID="ddlMaKhoa" runat="server" CssClass="input-box"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>Khóa:</td>
                    <td><asp:TextBox ID="txtKhoa" runat="server" CssClass="input-box" /></td>
                </tr>
                <tr>
                    <td>Lớp:</td>
                    <td><asp:TextBox ID="txtLop" runat="server" CssClass="input-box" /></td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center; padding-top: 10px;">
                        <asp:Button ID="btnRegister" runat="server" Text="Đăng Ký" CssClass="btn-primary" OnClick="btnRegister_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center; padding-top: 10px;">
                        <asp:Button ID="btnBackToLogin" runat="server" CssClass="btn-secondary" Text="Quay về trang đăng nhập" OnClick="btnBackToLogin_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>

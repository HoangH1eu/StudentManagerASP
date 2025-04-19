<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="QuanLyViecLamSinhVien.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8"/>
    <link href="StyleLog.css" rel="stylesheet" />
    <link href="Content/bootstrap.css" rel="stylesheet" />
    <title>Đăng Nhập</title>
</head>
<body>
     <form id="form1" runat="server">
        <div class="login-container">
            <div class="form-container" id="formContainer">
                <!-- Form đăng nhập sinh viên -->
                <div class="form" id="formSinhVien">
                    <h2>Đăng Nhập - Sinh Viên</h2>
                    <table>
                        <tr>
                            <td>Mã Sinh Viên:</td>
                            <td><asp:TextBox ID="txtMaSinhVien" runat="server" Width="100%"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>Mật Khẩu:</td>
                            <td><asp:TextBox ID="txtMatKhauSinhVien" runat="server" TextMode="Password" Width="100%"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: center; padding-top: 10px;">
                                <asp:Button ID="btnDangNhapSinhVien" CssClass="btn-primary" runat="server" Text="Đăng Nhập" OnClick="btnDangNhapSinhVien_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: center; color: red;">
                                <asp:Label ID="lblThongBaoSinhVien" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <a class="toggle-link" onclick="toggleForm()">Đăng nhập với tư cách khác</a>
                       <a href="RegisterAccount.aspx" class="toggle-link">Đăng ký tài khoản cho sinh viên mới</a>
                </div>

                <!-- Form đăng nhập admin -->
                <div class="form" id="formAdmin">
                    <h2>Đăng Nhập - Admin</h2>
                    <table>
                        <tr>
                            <td>Tên Đăng Nhập:</td>
                            <td><asp:TextBox ID="txtTenDangNhap" runat="server" Width="100%"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>Mật Khẩu:</td>
                            <td><asp:TextBox ID="txtMatKhauAdmin" runat="server" TextMode="Password" Width="100%"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: center; padding-top: 10px;">
                                <asp:Button ID="btnDangNhapAdmin"  CssClass="btn-primary" runat="server" Text="Đăng Nhập" OnClick="btnDangNhapAdmin_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: center; color: red;">
                                <asp:Label ID="lblThongBaoAdmin" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <a class="toggle-link" onclick="toggleForm()">Quay lại đăng nhập sinh viên</a>
                </div>
            </div>
        </div>
        <script>
            function toggleForm() {
                const formContainer = document.getElementById('formContainer');
                formContainer.classList.toggle('show-admin');

                // Lưu trạng thái form vào Local Storage
                const isAdmin = formContainer.classList.contains('show-admin');
                localStorage.setItem('loginForm', isAdmin ? 'admin' : 'student');
            }

            // Khi tải lại trang, hiển thị form theo trạng thái đã lưu
            window.onload = function () {
                const savedForm = localStorage.getItem('loginForm');
                if (savedForm === 'admin') {
                    document.getElementById('formContainer').classList.add('show-admin');
                } else {
                    document.getElementById('formContainer').classList.remove('show-admin');
                }
            };
        </script>
    </form>
</body>
</html>

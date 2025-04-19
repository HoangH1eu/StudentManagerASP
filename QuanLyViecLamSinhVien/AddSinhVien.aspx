<%@ Page Title="" Language="C#" MasterPageFile="~/MasterBase.Master" AutoEventWireup="true" CodeBehind="AddSinhVien.aspx.cs" Inherits="QuanLyViecLamSinhVien.AddSinhVien" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    Thêm Sinh Viên
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SidebarContent" runat="server">
    <li><a href="QuanLySinhVien.aspx">Quản lý sinh viên</a></li>
    <li><a href="QuanLyViecLam.aspx">Quản lý việc làm</a></li>
    <li><a href="ThongKe.aspx">Thống kê</a></li>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Thêm sinh viên mới</h2>
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
            <td>Ngày Sinh:</td>
            <td><asp:TextBox ID="txtNgaySinh" runat="server" CssClass="input-box" TextMode="Date" /></td>
        </tr>
        <tr>
            <td>Giới Tính:</td>
            <td>
                <asp:DropDownList ID="ddlGioiTinh" runat="server" CssClass="dropdown">
                    <asp:ListItem Text="Nam" Value="Nam" />
                    <asp:ListItem Text="Nữ" Value="Nữ" />
                </asp:DropDownList>
            </td>
        </tr>
          <tr>
            <td>Số Điện Thoại:</td>
             <td><asp:TextBox ID="txtSoDienThoai" runat="server" CssClass="input-box" /></td>
        </tr>
        <tr>
            <td>Khoa:</td>
            <td><asp:DropDownList ID="ddlKhoa" runat="server" CssClass="dropdown" /></td>
        </tr>
        <tr>
            <td>Ngày Tốt Nghiệp:</td>
            <td><asp:TextBox ID="txtNgayTotNghiep" runat="server" CssClass="input-box" TextMode="Date" /></td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="btnSave" runat="server" Text="Lưu" CssClass="btn-primary" OnClick="btnSave_Click" />
            </td>
        </tr>
    </table>
    <asp:Button ID="btnBack" runat="server" Text="Quay Lại Trang Quản Lý" CssClass="btn-secondary" OnClick="btnBack_Click" />
    <asp:Label ID="lblMessage" runat="server" CssClass="message" />
</asp:Content>

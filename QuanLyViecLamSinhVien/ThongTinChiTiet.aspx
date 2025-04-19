<%@ Page Title="" Language="C#" MasterPageFile="~/MasterBase.Master" AutoEventWireup="true" CodeBehind="ThongTinChiTiet.aspx.cs" Inherits="QuanLyViecLamSinhVien.ThongTinChiTiet" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    Thông tin cá nhân
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SidebarContent" runat="server">
    <li><a href="CapNhatThongTin.aspx">Cập nhật thông tin</a></li>
    <li><a href="CapNhatViecLam.aspx">Cập nhật việc làm</a></li>
    <li><a href="ThongTinChiTiet.aspx">Xem thông tin cá nhân</a></li>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Thông tin chi tiết</h2>
    <asp:Label ID="lblMessage" runat="server" CssClass="message"></asp:Label>
    <table>
        <tr>
            <td><strong>Mã Sinh Viên:</strong></td>
            <td><asp:Label ID="lblMaSinhVien" runat="server" /></td>
        </tr>
        <tr>
            <td><strong>Họ Tên:</strong></td>
            <td><asp:Label ID="lblHoTen" runat="server" /></td>
        </tr>
        <tr>
            <td><strong>Ngày Sinh:</strong></td>
            <td><asp:Label ID="lblNgaySinh" runat="server" /></td>
        </tr>
        <tr>
            <td><strong>Giới Tính:</strong></td>
            <td><asp:Label ID="lblGioiTinh" runat="server" /></td>
        </tr>
        <tr>
            <td><strong>Khoa:</strong></td>
            <td><asp:Label ID="lblKhoa" runat="server" /></td>
        </tr>
        <tr>
            <td><strong>Khóa:</strong></td>
            <td><asp:Label ID="lblKhoaHoc" runat="server" /></td>
        </tr>
        <tr>
            <td><strong>Lớp:</strong></td>
            <td><asp:Label ID="lblLop" runat="server" /></td>
        </tr>
        <tr>
            <td><strong>Ngày Tốt Nghiệp:</strong></td>
            <td><asp:Label ID="lblNgayTotNghiep" runat="server" /></td>
        </tr>
        <tr>
            <td><strong>Email:</strong></td>
            <td><asp:Label ID="lblEmail" runat="server" /></td>
        </tr>
        <tr>
            <td><strong>Số Điện Thoại:</strong></td>
            <td><asp:Label ID="lblSoDienThoai" runat="server" /></td>
        </tr>
        <tr>
            <td><strong>Vị Trí:</strong></td>
            <td><asp:Label ID="lblViTri" runat="server" /></td>
        </tr>
        <tr>
            <td><strong>Công Ty:</strong></td>
            <td><asp:Label ID="lblCongTy" runat="server" /></td>
        </tr>
    </table>
</asp:Content>

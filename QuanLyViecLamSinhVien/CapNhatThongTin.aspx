<%@ Page Title="" Language="C#" MasterPageFile="~/MasterBase.Master" AutoEventWireup="true" CodeBehind="CapNhatThongTin.aspx.cs" Inherits="QuanLyViecLamSinhVien.CapNhatThongTin" %>
<asp:Content ID="Content2" ContentPlaceHolderID="SidebarContent" runat="server">
    <li><a href="CapNhatThongTin.aspx">Cập nhật thông tin</a></li>
    <li><a href="CapNhatViecLam.aspx">Cập nhật việc làm</a></li>
    <li><a href="ThongTinChiTiet.aspx">Xem thông tin cá nhân</a></li>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Cập nhật thông tin cá nhân</h2>
    <asp:Label ID="lblMessage" runat="server" CssClass="message" />
    <table>
        <tr>
            <td>Mã Sinh Viên:</td>
            <td><asp:TextBox ID="txtMaSinhVien" runat="server" Enabled="false" CssClass="input-box" /></td>
        </tr>
        <tr>
            <td>Họ Tên:</td>
            <td><asp:TextBox ID="txtHoTen" runat="server" CssClass="input-box" /></td>
        </tr>
        <tr>
            <td>Ngày Sinh:</td>
            <td><asp:TextBox ID="txtNgaySinh" runat="server"  CssClass="input-box" TextMode="Date"/></td>
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
            <td>Khoa:</td>
            <td><asp:DropDownList ID="ddlKhoa" runat="server" CssClass="dropdown" /></td>
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
            <td>Số Điện Thoại:</td>
            <td><asp:TextBox ID="txtSoDienThoai" runat="server" CssClass="input-box" /></td>
        </tr>
        <tr>
            <td>Ngày Tốt Nghiệp:</td>
        <td>
               <asp:TextBox ID="txtNgayTotNghiep" runat="server" CssClass="input-box" TextMode="Date" />
        </td>
        </tr>
        <tr>
            <td>Email:</td>
            <td><asp:TextBox ID="txtEmail" runat="server" CssClass="input-box" /></td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="btnSave" runat="server" Text="Lưu" CssClass="btn-primary" OnClick="btnSave_Click" />
            </td>
        </tr>
    </table>
</asp:Content>

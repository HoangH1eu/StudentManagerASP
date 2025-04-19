<%@ Page Title="" Language="C#" MasterPageFile="~/MasterBase.Master" AutoEventWireup="true" CodeBehind="CapNhatViecLam.aspx.cs" Inherits="QuanLyViecLamSinhVien.CapNhatViecLam" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    Cập nhật việc làm
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SidebarContent" runat="server">
    <li><a href="CapNhatThongTin.aspx">Cập nhật thông tin</a></li>
    <li><a href="CapNhatViecLam.aspx">Cập nhật việc làm</a></li>
    <li><a href="ThongTinChiTiet.aspx">Xem thông tin cá nhân</a></li>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Cập nhật việc làm</h2>
    <asp:Label ID="lblMessage" runat="server" CssClass="message"></asp:Label>
    <table>
        <tr>
            <td>Tên Công Ty:</td>
            <td><asp:TextBox ID="txtTenCongTy" runat="server" CssClass="input-box" /></td>
        </tr>
        <tr>
            <td>Vị Trí:</td>
            <td><asp:TextBox ID="txtViTri" runat="server" CssClass="input-box" /></td>
        </tr>
        <tr>
            <td>Mức Lương:</td>
            <td><asp:TextBox ID="txtMucLuong" runat="server" CssClass="input-box" /></td>
        </tr>
      <tr>
            <td>Ngày Nhận Việc:</td>
            <td><asp:TextBox ID="txtNgayNhanViec" runat="server" CssClass="input-box" TextMode="Date" /></td>
        </tr>
       <tr>
    <td>Chuyên Ngành:</td>
    <td>
        <asp:DropDownList ID="ddlChuyenNganh" runat="server" CssClass="dropdown">
            <asp:ListItem Text="-- Chọn Chuyên Ngành --" Value="" />
            <asp:ListItem Text="Công Nghệ Thông Tin" Value="Công Nghệ Thông Tin" />
            <asp:ListItem Text="Kế Toán" Value="Kế Toán" />
            <asp:ListItem Text="Quản Trị Kinh Doanh" Value="Quản Trị Kinh Doanh" />
        </asp:DropDownList>
    </td>
</tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="btnSaveJob" runat="server" Text="Lưu" CssClass="btn-primary" OnClick="btnSaveJob_Click" />
            </td>
        </tr>
    </table>
</asp:Content>

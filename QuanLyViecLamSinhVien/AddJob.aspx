<%@ Page Title="" Language="C#" MasterPageFile="~/MasterBase.Master" AutoEventWireup="true" CodeBehind="AddJob.aspx.cs" Inherits="QuanLyViecLamSinhVien.AddJob" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SidebarContent" runat="server">
    <li><a href="QuanLySinhVien.aspx">Quản lý sinh viên</a></li>
    <li><a href="QuanLyViecLam.aspx">Quản lý việc làm</a></li>
    <li><a href="ThongKe.aspx">Thống kê</a></li>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
  <h2>Thêm Việc Làm</h2>
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
        <td>Chuyên Ngành:</td>
        <td><asp:TextBox ID="txtChuyenNganh" runat="server" CssClass="input-box" /></td>
    </tr>
    <tr>
        <td>Mức Lương:</td>
        <td><asp:TextBox ID="txtMucLuong" runat="server" CssClass="input-box" /></td>
    </tr>
    <tr>
        <td>Mã Sinh Viên:</td>
        <td><asp:TextBox ID="txtMaSinhVien" runat="server" CssClass="input-box" /></td>
    </tr>
    <tr>
    <td>Ngày Nhận Việc:</td>
    <td><asp:TextBox ID="txtNgayNhanViec" runat="server" CssClass="input-box" placeholder="yyyy-MM-dd" /></td>
</tr>
    <tr>
        <td colspan="2" style="text-align: center;">
            <asp:Button ID="btnSaveJob" runat="server" Text="Lưu Việc Làm" CssClass="btn-primary" OnClick="btnSaveJob_Click" />
        </td>
    </tr>
    <tr>
        <td colspan="2" style="text-align: center; color: red;">
            <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
        </td>
    </tr>
</table>
<asp:Button ID="btnBack" runat="server" Text="Quay Lại Trang Quản Lý" CssClass="btn-secondary" OnClick="btnBack_Click" />


</asp:Content>

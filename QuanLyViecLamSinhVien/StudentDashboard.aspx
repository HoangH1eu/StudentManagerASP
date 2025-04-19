<%@ Page Title="" Language="C#" MasterPageFile="~/MasterBase.Master" AutoEventWireup="true" CodeBehind="StudentDashboard.aspx.cs" Inherits="QuanLyViecLamSinhVien.StudentDashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    Trang Cá Nhân Sinh Viên
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="SidebarContent" runat="server">
    <li><a href="CapNhatThongTin.aspx">Cập nhật thông tin</a></li>
    <li><a href="CapNhatViecLam.aspx">Cập nhật việc làm</a></li>
    <li><a href="ThongTinChiTiet.aspx">Xem thông tin cá nhân</a></li>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Chào mừng bạn đến trang cá nhân!</h2>
    <p>Chọn một mục trong menu để bắt đầu.</p>
</asp:Content>

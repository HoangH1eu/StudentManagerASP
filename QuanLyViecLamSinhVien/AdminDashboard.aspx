<%@ Page Title="" Language="C#" MasterPageFile="~/MasterBase.Master" AutoEventWireup="true" CodeBehind="AdminDashboard.aspx.cs" Inherits="QuanLyViecLamSinhVien.AdminDashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    Trang Quản Trị
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="SidebarContent" runat="server">
   <li><a href="QuanLySinhVien.aspx">Quản lý sinh viên</a></li>
<li><a href="QuanLyViecLam.aspx">Quản lý việc làm</a></li>
<li><a href="QuanLyKhoa.aspx">Quản lý Khoa</a></li>
<li><a href="ThongKe.aspx">Thống kê</a></li>
<li><a href="GuiThongBao.aspx">Gửi thông báo</a></li>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Chào mừng đến trang quản trị!</h2>
    <p>Chọn một mục trong menu để bắt đầu.</p>
</asp:Content>

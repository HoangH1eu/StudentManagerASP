<%@ Page Title="Gửi Thông Báo" Language="C#" MasterPageFile="~/MasterBase.Master" AutoEventWireup="true" CodeBehind="GuiThongBao.aspx.cs" Inherits="QuanLyViecLamSinhVien.GuiThongBao" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    Gửi Thông Báo
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="SidebarContent" runat="server">
        <li><a href="QuanLySinhVien.aspx">Quản lý sinh viên</a></li>
<li><a href="QuanLyViecLam.aspx">Quản lý việc làm</a></li>
<li><a href="QuanLyKhoa.aspx">Quản lý Khoa</a></li>
<li><a href="ThongKe.aspx">Thống kê</a></li>
<li><a href="GuiThongBao.aspx">Gửi thông báo</a></li>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Gửi Thông Báo</h2>
    <table>
        <tr>
            <td>Tiêu Đề:</td>
            <td><asp:TextBox ID="txtTieuDe" runat="server" CssClass="input-box" Width="100%"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Nội Dung:</td>
            <td><asp:TextBox ID="txtNoiDung" runat="server" CssClass="input-box" TextMode="MultiLine" Rows="5" Width="100%"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Gửi Đến:</td>
            <td>
                <asp:DropDownList ID="ddlSinhVien" runat="server" CssClass="dropdown">
                    <asp:ListItem Text="Tất Cả Sinh Viên" Value="ALL" />
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center; padding-top: 10px;">
                <asp:Button ID="btnGuiThongBao" runat="server" Text="Gửi Thông Báo" CssClass="btn-primary" OnClick="btnGuiThongBao_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center; color: red;">
                <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>

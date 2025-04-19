<%@ Page Title="Quản lý khoa" Language="C#" MasterPageFile="~/MasterBase.Master" AutoEventWireup="true" CodeBehind="QuanLyKhoa.aspx.cs" Inherits="QuanLyViecLamSinhVien.QuanLyKhoa" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    Quản lý khoa
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SidebarContent" runat="server">
    <li><a href="QuanLySinhVien.aspx">Quản lý sinh viên</a></li>
<li><a href="QuanLyViecLam.aspx">Quản lý việc làm</a></li>
<li><a href="QuanLyKhoa.aspx">Quản lý Khoa</a></li>
<li><a href="ThongKe.aspx">Thống kê</a></li>
<li><a href="GuiThongBao.aspx">Gửi thông báo</a></li>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Quản lý khoa</h2>

    <!-- Form thêm khoa -->
    <div class="form-container">
        <h3>Thêm khoa</h3>
        <table>
            <tr>
                <td>Mã Khoa:</td>
                <td><asp:TextBox ID="txtMaKhoa" runat="server" CssClass="input-box" /></td>
            </tr>
            <tr>
                <td>Tên Khoa:</td>
                <td><asp:TextBox ID="txtTenKhoa" runat="server" CssClass="input-box" /></td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center;">
                    <asp:Button ID="btnAddKhoa" runat="server" Text="Thêm Khoa" CssClass="btn-primary" OnClick="btnAddKhoa_Click" />
                </td>
            </tr>
        </table>
        <asp:Label ID="lblMessage" runat="server" CssClass="message"></asp:Label>
    </div>

    <!-- GridView hiển thị danh sách khoa -->
    <div class="grid-container">
        <asp:GridView ID="gvKhoa" runat="server" AutoGenerateColumns="False" CssClass="grid-view"
            OnRowEditing="gvKhoa_RowEditing" 
            OnRowUpdating="gvKhoa_RowUpdating" 
            OnRowCancelingEdit="gvKhoa_RowCancelingEdit" 
            OnRowDeleting="gvKhoa_RowDeleting" 
            DataKeyNames="MaKhoa">
            <Columns>
                <asp:BoundField DataField="MaKhoa" HeaderText="Mã Khoa" ReadOnly="True" />
                <asp:BoundField DataField="TenKhoa" HeaderText="Tên Khoa" />
                <asp:TemplateField HeaderText="Số Sinh Viên">
                    <ItemTemplate>
                        <%# Eval("SoSinhVien") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" ButtonType="Button" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
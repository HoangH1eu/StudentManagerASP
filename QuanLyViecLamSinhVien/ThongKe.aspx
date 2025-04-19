<%@ Page Title="Thống kê" Language="C#" MasterPageFile="~/MasterBase.Master" AutoEventWireup="true" CodeBehind="ThongKe.aspx.cs" Inherits="QuanLyViecLamSinhVien.ThongKe" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    Thống kê
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SidebarContent" runat="server">
    <li><a href="QuanLySinhVien.aspx">Quản lý sinh viên</a></li>
    <li><a href="QuanLyViecLam.aspx">Quản lý việc làm</a></li>
    <li><a href="QuanLyKhoa.aspx">Quản lý Khoa</a></li>
    <li><a href="ThongKe.aspx">Thống kê</a></li>
    <li><a href="GuiThongBao.aspx">Gửi thông báo</a></li>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="statistics-container">
        <h2>Thống kê sinh viên</h2>
        <asp:Label ID="lblMessage" runat="server" CssClass="message"></asp:Label>
        <!-- Điều hướng loại thống kê -->
        <div class="statistics-navigation">
            <asp:DropDownList ID="ddlThongKeLoai" runat="server" AutoPostBack="True" CssClass="dropdown" OnSelectedIndexChanged="ddlThongKeLoai_SelectedIndexChanged">
                <asp:ListItem Text="Chọn loại thống kê" Value="" />
                <asp:ListItem Text="Thống kê tổng quát" Value="TongQuat" />
                <asp:ListItem Text="Thống kê theo công ty" Value="TheoCongTy" />
                <asp:ListItem Text="Thống kê theo vị trí" Value="TheoViTri" />
                <asp:ListItem Text="Thống kê theo mức lương" Value="TheoMucLuong" />
                <asp:ListItem Text="Thống kê đúng chuyên ngành" Value="DungChuyenNganh" />
                <asp:ListItem Text="Thống kê theo Khóa" Value="TheoKhoa" />
                <asp:ListItem Text="Thống kê theo Lớp" Value="TheoLop" />
            </asp:DropDownList>
        </div>

        <!-- Khu vực nhập liệu -->
        <div id="filterContainer" runat="server" visible="false" class="filter-container">
            <div id="filterByCompany" runat="server" visible="false">
                <label for="txtCongTy">Nhập tên công ty:</label>
                <asp:TextBox ID="txtCongTy" runat="server" CssClass="input-box" placeholder="Nhập tên công ty" />
                <asp:Button ID="btnFilterByCompany" runat="server" Text="Thống kê" CssClass="btn-primary" OnClick="btnFilterByCompany_Click" />
            </div>

            <div id="filterByPosition" runat="server" visible="false">
                <label for="txtViTri">Nhập vị trí:</label>
                <asp:TextBox ID="txtViTri" runat="server" CssClass="input-box" placeholder="Nhập vị trí" />
                <asp:Button ID="btnFilterByPosition" runat="server" Text="Thống kê" CssClass="btn-primary" OnClick="btnFilterByPosition_Click" />
            </div>

            <div id="filterBySalary" runat="server" visible="false">
                <label for="txtMucLuongToiThieu">Mức lương tối thiểu:</label>
                <asp:TextBox ID="txtMucLuongToiThieu" runat="server" CssClass="input-box" placeholder="Nhập mức lương tối thiểu" />
                <label for="txtMucLuongToiDa">Mức lương tối đa:</label>
                <asp:TextBox ID="txtMucLuongToiDa" runat="server" CssClass="input-box" placeholder="Nhập mức lương tối đa" />
                <asp:Button ID="btnFilterBySalary" runat="server" Text="Thống kê" CssClass="btn-primary" OnClick="btnFilterBySalary_Click" />
            </div>

            <div id="filterByKhoa" runat="server" visible="false">
                <label for="txtKhoa">Nhập Khóa:</label>
                <asp:TextBox ID="txtKhoa" runat="server" CssClass="input-box" placeholder="Nhập Khóa" />
                <asp:Button ID="btnFilterByKhoa" runat="server" Text="Thống kê" CssClass="btn-primary" OnClick="btnFilterByKhoa_Click" />
            </div>

            <div id="filterByLop" runat="server" visible="false">
                <label for="txtLop">Nhập Lớp:</label>
                <asp:TextBox ID="txtLop" runat="server" CssClass="input-box" placeholder="Nhập Lớp" />
                <asp:Button ID="btnFilterByLop" runat="server" Text="Thống kê" CssClass="btn-primary" OnClick="btnFilterByLop_Click" />
            </div>
        </div>

        <!-- Dropdown để lọc khoa -->
        <div class="filter-container">
            <label for="ddlKhoa">Chọn khoa:</label>
            <asp:DropDownList ID="ddlKhoa" runat="server" CssClass="dropdown" AutoPostBack="True" OnSelectedIndexChanged="ddlKhoa_SelectedIndexChanged">
                <asp:ListItem Text="-- Tất cả khoa --" Value="" />
            </asp:DropDownList>
        </div>

        <!-- Bảng hiển thị kết quả -->
        <asp:GridView ID="gvThongKeChiTiet" runat="server" CssClass="grid-view" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="MaSinhVien" HeaderText="Mã Sinh Viên" />
                <asp:BoundField DataField="HoTen" HeaderText="Tên Sinh Viên" />
                <asp:BoundField DataField="Khoa" HeaderText="Khóa" />
                <asp:BoundField DataField="Lop" HeaderText="Lớp" />
                <asp:BoundField DataField="ChuyenNganh" HeaderText="Chuyên Ngành" />
                <asp:BoundField DataField="TenCongTy" HeaderText="Công Ty" />
                <asp:BoundField DataField="ViTri" HeaderText="Vị Trí" />
                <asp:BoundField DataField="MucLuong" HeaderText="Mức Lương" DataFormatString="{0:C}" />
                <asp:BoundField DataField="NgayNhanViec" HeaderText="Ngày Nhận Việc" DataFormatString="{0:dd/MM/yyyy}" />
            </Columns>
        </asp:GridView>
        <asp:Label ID="lblThongKeTyLe" runat="server" CssClass="thongke-label"></asp:Label>
    </div>
</asp:Content>

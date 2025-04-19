<%@ Page Title="" Language="C#" MasterPageFile="~/MasterBase.Master" AutoEventWireup="true" CodeBehind="QuanLySinhVien.aspx.cs" Inherits="QuanLyViecLamSinhVien.QuanLySinhVien" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    Quản lý sinh viên
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SidebarContent" runat="server">
     <li><a href="QuanLySinhVien.aspx">Quản lý sinh viên</a></li>
 <li><a href="QuanLyViecLam.aspx">Quản lý việc làm</a></li>
 <li><a href="QuanLyKhoa.aspx">Quản lý Khoa</a></li>
 <li><a href="ThongKe.aspx">Thống kê</a></li>
 <li><a href="GuiThongBao.aspx">Gửi thông báo</a></li>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Quản lý sinh viên</h2>
    <asp:Label ID="lblMessage" runat="server" CssClass="message"></asp:Label>
<div style="margin-bottom: 15px;">
    <asp:TextBox ID="txtSearch" runat="server" CssClass="input-box" placeholder="Nhập tên hoặc mã sinh viên" />
    <asp:Button ID="btnSearch" runat="server" Text="Tìm kiếm" CssClass="btn-primary" OnClick="btnSearch_Click" />
    <asp:Button ID="btnClearSearch" runat="server" Text="Xóa tìm kiếm" CssClass="btn-secondary" OnClick="btnClearSearch_Click" />
</div>

<div style="margin-bottom: 15px;">
    <asp:DropDownList ID="ddlKhoa" runat="server" AutoPostBack="True" CssClass="dropdown" OnSelectedIndexChanged="ddlKhoa_SelectedIndexChanged">
    </asp:DropDownList>
</div>

    <asp:GridView ID="gvSinhVien" runat="server" AutoGenerateColumns="False" CssClass="grid-view"
        OnRowEditing="gvSinhVien_RowEditing"
        OnRowCancelingEdit="gvSinhVien_RowCancelingEdit"
        OnRowUpdating="gvSinhVien_RowUpdating"
        OnRowDeleting="gvSinhVien_RowDeleting"
        DataKeyNames="MaSinhVien">
        <Columns>
            
            <asp:TemplateField HeaderText="Mã Sinh Viên">
                <ItemTemplate>
                    <asp:Label ID="lblMaSinhVien" runat="server" Text='<%# Eval("MaSinhVien") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

        
            <asp:TemplateField HeaderText="Họ Tên">
                <ItemTemplate>
                    <asp:Label ID="lblHoTen" runat="server" Text='<%# Eval("HoTen") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtHoTen" runat="server" Text='<%# Bind("HoTen") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>

          
            <asp:TemplateField HeaderText="Ngày Sinh">
                <ItemTemplate>
                    <asp:Label ID="lblNgaySinh" runat="server" Text='<%# Eval("NgaySinh", "{0:dd/MM/yyyy}") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtNgaySinh" runat="server" Text='<%# Bind("NgaySinh", "{0:yyyy-MM-dd}") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>

           
            <asp:TemplateField HeaderText="Giới Tính">
                <ItemTemplate>
                    <asp:Label ID="lblGioiTinh" runat="server" Text='<%# Eval("GioiTinh") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="ddlGioiTinh" runat="server" SelectedValue='<%# Bind("GioiTinh") %>'>
                        <asp:ListItem Text="Nam" Value="Nam"></asp:ListItem>
                        <asp:ListItem Text="Nữ" Value="Nữ"></asp:ListItem>
                    </asp:DropDownList>
                </EditItemTemplate>
            </asp:TemplateField>

           
            <asp:TemplateField HeaderText="Khoa">
                <ItemTemplate>
                    <asp:Label ID="lblTenKhoa" runat="server" Text='<%# Eval("TenKhoa") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtTenKhoa" runat="server" Text='<%# Bind("TenKhoa") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Ngày Tốt Nghiệp">
                 <ItemTemplate>
                     <asp:Label ID="lblNgayTotNghiep" runat="server" Text='<%# Eval("NgayTotNghiep", "{0:dd/MM/yyyy}") %>'></asp:Label>
                </ItemTemplate>
            <EditItemTemplate>
                 <asp:TextBox ID="txtNgayTotNghiep" runat="server" Text='<%# Bind("NgayTotNghiep", "{0:yyyy-MM-dd}") %>' TextMode="Date"></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>

          <asp:TemplateField HeaderText="Khóa">
    <ItemTemplate>
        <asp:Label ID="lblKhoa" runat="server" Text='<%# Eval("Khoa") %>'></asp:Label>
    </ItemTemplate>
    <EditItemTemplate>
        <asp:TextBox ID="txtKhoa" runat="server" Text='<%# Bind("Khoa") %>'></asp:TextBox>
    </EditItemTemplate>
</asp:TemplateField>


<asp:TemplateField HeaderText="Lớp">
    <ItemTemplate>
        <asp:Label ID="lblLop" runat="server" Text='<%# Eval("Lop") %>'></asp:Label>
    </ItemTemplate>
    <EditItemTemplate>
        <asp:TextBox ID="txtLop" runat="server" Text='<%# Bind("Lop") %>'></asp:TextBox>
    </EditItemTemplate>
</asp:TemplateField>



            
            <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" ButtonType="Button" />
        </Columns>
    </asp:GridView>

    <asp:Button ID="btnAddNew" runat="server" Text="Thêm Sinh Viên" CssClass="btn-primary" OnClick="btnAddNew_Click" />
</asp:Content>


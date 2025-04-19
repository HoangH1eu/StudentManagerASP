<%@ Page Title="" Language="C#" MasterPageFile="~/MasterBase.Master" AutoEventWireup="true" CodeBehind="QuanLyViecLam.aspx.cs" Inherits="QuanLyViecLamSinhVien.QuanLyViecLam" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    Quản lý việc làm
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SidebarContent" runat="server">
  <li><a href="QuanLySinhVien.aspx">Quản lý sinh viên</a></li>
 <li><a href="QuanLyViecLam.aspx">Quản lý việc làm</a></li>
 <li><a href="QuanLyKhoa.aspx">Quản lý Khoa</a></li>
 <li><a href="ThongKe.aspx">Thống kê</a></li>
 <li><a href="GuiThongBao.aspx">Gửi thông báo</a></li>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Quản lý việc làm</h2>
    <asp:Label ID="lblMessage" runat="server" CssClass="message"></asp:Label>
    <asp:GridView ID="gvViecLam" runat="server" AutoGenerateColumns="False" CssClass="grid-view"
        OnRowEditing="gvViecLam_RowEditing"
        OnRowUpdating="gvViecLam_RowUpdating"
        OnRowCancelingEdit="gvViecLam_RowCancelingEdit"
        OnRowDeleting="gvViecLam_RowDeleting"
        OnRowDataBound="gvViecLam_RowDataBound"
        DataKeyNames="MaViecLam">
        <Columns>
            <asp:BoundField DataField="MaViecLam" HeaderText="Mã Việc Làm" ReadOnly="True" HeaderStyle-Width="100px" ItemStyle-Width="100px" />
            <asp:BoundField DataField="MaSinhVien" HeaderText="Mã Sinh Viên" ReadOnly="True" HeaderStyle-Width="120px" ItemStyle-Width="120px" />
            <asp:BoundField DataField="TenSinhVien" HeaderText="Tên Sinh Viên" ReadOnly="True" HeaderStyle-Width="200px" ItemStyle-Width="200px" />
            
            
            <asp:TemplateField HeaderText="Vị Trí" HeaderStyle-Width="150px" ItemStyle-Width="150px">
                <ItemTemplate>
                    <asp:Label ID="lblViTri" runat="server" Text='<%# Eval("ViTri") %>' CssClass="label"></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtViTri" runat="server" Text='<%# Bind("ViTri") %>' CssClass="textbox"></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>

           
            <asp:TemplateField HeaderText="Công Ty" HeaderStyle-Width="200px" ItemStyle-Width="200px">
                <ItemTemplate>
                    <asp:Label ID="lblTenCongTy" runat="server" Text='<%# Eval("TenCongTy") %>' CssClass="label"></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtTenCongTy" runat="server" Text='<%# Bind("TenCongTy") %>' CssClass="textbox"></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>

            
            <asp:TemplateField HeaderText="Mức Lương" HeaderStyle-Width="100px" ItemStyle-Width="100px">
                <ItemTemplate>
                    <asp:Label ID="lblMucLuong" runat="server" Text='<%# Eval("MucLuong", "{0:C}") %>' CssClass="label"></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtMucLuong" runat="server" Text='<%# Bind("MucLuong") %>' CssClass="textbox"></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>

           
            <asp:TemplateField HeaderText="Ngày Nhận Việc" HeaderStyle-Width="150px" ItemStyle-Width="150px">
                <ItemTemplate>
                    <asp:Label ID="lblNgayNhanViec" runat="server" Text='<%# Eval("NgayNhanViec", "{0:dd/MM/yyyy}") %>' CssClass="label"></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtNgayNhanViec" runat="server" Text='<%# Bind("NgayNhanViec") %>' CssClass="textbox"></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>

            
            <asp:TemplateField HeaderText="Chuyên Ngành">
                <ItemTemplate>
                    <asp:Label ID="lblChuyenNganh" runat="server" Text='<%# Eval("ChuyenNganh") %>' CssClass="label"></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="ddlChuyenNganh" runat="server" CssClass="dropdown">
                        <asp:ListItem Text="Công Nghệ Thông Tin" Value="Công Nghệ Thông Tin" />
                        <asp:ListItem Text="Kế Toán" Value="Kế Toán" />
                        <asp:ListItem Text="Kỹ Thuật Điện" Value="Kỹ Thuật Điện" />
                        <asp:ListItem Text="Quản Trị Kinh Doanh" Value="Quản Trị Kinh Doanh" />
                    </asp:DropDownList>
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
        </Columns>
    </asp:GridView>
    <asp:Button ID="btnAddJob" runat="server" Text="Thêm Việc Làm" CssClass="btn-primary" OnClick="btnAddJob_Click" />
</asp:Content>


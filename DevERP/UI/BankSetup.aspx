<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BankSetup.aspx.cs" MaintainScrollPositionOnPostback="true" Inherits="DevERP.UI.BankSetup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="col-lg-12">
            <div class="row main panel panel-info">
                <div class="panel-heading">
                    <div class="panel-title">
                        <h1 class="title">Bank Entry</h1>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="col-sm-12">
                        <hr />
                    </div>
                    <div class="form-group col-sm-12">
                        <div class="col-sm-12 control-label" runat="server" id="successMessage"></div>
                    </div>
                    <div class="col-sm-12">
                        <div class="form-group col-sm-6">
                            <label class="col-sm-4 control-label">Bank Name<b class="text-danger">*</b></label>
                            <div class="col-sm-6">
                                <input runat="server" class="form-control" type="text" id="bankName" name="bankName" placeholder="Bank Name" />
                            </div>
                            <div class="col-sm-2 control-label"></div>
                        </div>
                        <div class="form-group col-sm-6">
                            <div class="col-sm-4">
                                <asp:Button CssClass="btn btn-default btn-block" type="submit" ID="SaveBank" Text="Add" runat="server" OnClick="SaveBank_OnClick" />
                            </div>
                            <div class="col-sm-8"></div>
                        </div>
                    </div>

                    <style>
                        #MainContent_BankGridView td, #MainContent_BankGridView th {
                            padding: 10px 0;
                        }
                    </style>
                    <div class="">
                        <asp:GridView ID="BankGridView" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" Width="100%" OnRowEditing="BankGridView_OnRowEditing" OnRowUpdating="BankGridView_OnRowUpdating" OnRowCancelingEdit="BankGridView_OnRowCancelingEdit">
                            <Columns>
                                <asp:TemplateField HeaderText="#SL NO">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bank ID" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="id" runat="server" Text='<%# Eval("BankId") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bank Name">
                                    <ItemTemplate>
                                        <asp:Label ID="bankName" runat="server" Text='<%# Eval("BankName") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="bankNameTextBox" runat="server" Text='<%# Eval("BankName") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkRemove" runat="server" CommandArgument = '<%# Eval("BankId")%>' OnClientClick="return confirm('Do you want to delete?')" Text="Delete" OnClick="lnkRemove_OnClick"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowEditButton="True" />
                            </Columns>
                            <AlternatingRowStyle BackColor="White" />
                            <EditRowStyle BackColor="#7C6F57" />
                            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#E3EAEB" />
                            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#F8FAFA" />
                            <SortedAscendingHeaderStyle BackColor="#246B61" />
                            <SortedDescendingCellStyle BackColor="#D4DFE1" />
                            <SortedDescendingHeaderStyle BackColor="#15524A" />

                        </asp:GridView>
                    </div>
                </div>
                <!-- Panel body-->
            </div>
            <!-- Main panel-->
        </div>
        <!-- col-sm-12-->
    </div>
    <!-- Container-->

</asp:Content>

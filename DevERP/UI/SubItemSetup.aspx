<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SubItemSetup.aspx.cs" MaintainScrollPositionOnPostback="true" Inherits="DevERP.UI.SubItemSetup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="col-lg-12">
            <div class="row main panel panel-info">
                <div class="panel-heading">
                    <div class="panel-title">
                        <h1 class="title">Sub Item Entry</h1>
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
                            <label class="col-sm-4 control-label">Item Name<b class="text-danger">*</b></label>
                            <div class="col-sm-6">
                                <asp:DropDownList runat="server" class="form-control" id="itemNameDropDown" name="ItemNameDropDown" AutoPostBack="True" OnSelectedIndexChanged="itemNameDropDown_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                            <div class="col-sm-2 control-label"></div>
                        </div>
                        <div class="form-group col-sm-6">
                            <label class="col-sm-4 control-label">Sub-Item Name<b class="text-danger">*</b></label>
                            <div class="col-sm-6">
                                <input runat="server" class="form-control" type="text" id="subItemName" name="subItemName" placeholder="Sub-Item Name" />
                            </div>
                            <div class="col-sm-2 control-label"></div>
                        </div>
                        <div class="form-group col-sm-12">
                            <div class="col-sm-2">
                                <asp:Button CssClass="btn btn-default btn-block" type="submit" ID="SaveSubItem" Text="Add" runat="server" OnClick="SaveSubItem_OnClick" />
                            </div>
                            <div class="col-sm-10"></div>
                        </div>
                    </div>

                    <style>
                        #MainContent_SubItemGridView td, #MainContent_SubItemGridView th {
                            padding: 10px 0;
                        }
                    </style>
                    <div class="">
                        <asp:GridView ID="SubItemGridView" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" Width="100%" OnRowEditing="SubItemGridView_OnRowEditing" OnRowUpdating="SubItemGridView_OnRowUpdating" OnRowCancelingEdit="SubItemGridView_OnRowCancelingEdit">
                            <Columns>
                                <asp:TemplateField HeaderText="#SL NO">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sub-Item ID" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="id" runat="server" Text='<%# Eval("SubItemId") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Name">
                                    <ItemTemplate>
                                        <asp:Label ID="subItemName" runat="server" Text='<%# Eval("SubItemName") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="subItemNameTextBox" runat="server" Text='<%# Eval("SubItemName") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkRemove" runat="server" CommandArgument = '<%# Eval("SubItemId")%>' OnClientClick="return confirm('Do you want to delete?')" Text="Delete" OnClick="lnkRemove_OnClick"></asp:LinkButton>
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

<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PartySetup.aspx.cs" Inherits="DevERP.UI.PartySetup" EnableEventValidation="false"%>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .myButtonTopMargin {
        }

        .panelMargin {
            margin-top: 0%;
        }

        .panel-primary > .panel-heading {
            color: #ffffff;
            background-color: #428bca;
            border-color: #428bca;
            text-align: center;
        }

        .button, html input[type="button"],
        input[type="reset"], input[type="submit"] {
            height: 100% !important;
        }

        .table {
            width: 100%;
            margin-bottom: 0px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="col-sm-10 col-sm-offset-1">
        <div class="panel panel-primary">
            <div class="panel-heading">Party Information</div>
            <div class="panel-body">
                <div class="form-horizontal">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="form-group">
                                <div class="col-sm-4">

                                    <label>Supplier/Customer</label>
                                    <asp:DropDownList runat="server" CssClass="form-control input-sm" ID="supplierCustomerDropDownList">
                                        <asp:ListItem Value="1">Customer</asp:ListItem>
                                        <asp:ListItem Value="2">Supplier</asp:ListItem>
                                    </asp:DropDownList>

                                    <%--<label>Party Id</label>--%>
                                    <input runat="server" type="text" class="form-control input-sm" id="partyIdText" Visible="False"/>

                                    <label>Organization Name</label>
                                    <input runat="server" type="text" class="form-control input-sm" id="organizationNameText" />

                                    <label>Conatct Person Name</label>
                                    <input runat="server" type="text" class="form-control input-sm" id="contactPersonNameText" />

                                    <label>Address</label>
                                    <input runat="server" type="text" class="form-control input-sm" id="addressText" />

                                    <label>Contact Number</label>
                                    <input runat="server" type="text" class="form-control input-sm" id="contactNumber" />

                                    <label>Email Address</label>
                                    <input runat="server" class="form-control input-sm" type="text" id="emailAddressText" />

                                    <label>Opening Balance</label>
                                    <input runat="server" type="text" class="form-control input-sm" id="openingBalanceText" />
                                    <br/>
                                    <asp:Button ID="saveButton" runat="server" class="btn btn-success myButtonTopMargin" Text="Save" OnClick="saveButton_Click" />
                                    <br />
                                    <asp:Literal ID="partyInfoLiteral" runat="server" Text="_"> </asp:Literal>
                                </div>
                           
                                <div class="col-sm-8">
                                    <div style="border: 1px solid #ededed; padding: 5px; height: 455px;">
                                        <div class="col-lg-12">
                                            <%--OnSelectedIndexChanged="itemGridView_SelectedIndexChanged" OnRowDataBound="ItemOnRowDataBound"--%>
                                            <asp:GridView ID="partyGridView" runat="server" UseAccessibleHeader="true"
                                                OnSelectedIndexChanged="partyGridView_SelectedIndexChanged" OnRowDataBound="PartyOnRowDataBound"
                                                CssClass="table-hover table-striped table table-bordered" GridLines="None"
                                                AutoGenerateColumns="False">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SL">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Bank ID" Visible="False">
                                                        <ItemTemplate>
                                                            <asp:Label ID="idLabel" runat="server" Text='<%# Eval("SupplierId") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Org. Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="orgNameLabel" runat="server" Text='<%# Eval("OrganizationName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Contact Person">
                                                        <ItemTemplate>
                                                            <asp:Label ID="contactPersonLabel" runat="server" Text='<%# Eval("ContactPerson") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                   
                                                </Columns>
                                                <HeaderStyle CssClass="GridviewScrollHeader" />
                                                <RowStyle CssClass="GridviewScrollItem" />
                                                <PagerStyle CssClass="GridviewScrollPager" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="../Scripts/ScrollableGridViewPlugin_ASP.NetAJAXmin.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#<%=partyGridView.ClientID %>').Scrollable({
                ScrollHeight: 320,
                IsInUpdatePanel: true
            });
        });
    </script>
   <%-- <div class="container">
        <div class="col-lg-12">
            <div class="row main panel panel-primary">
                <div class="panel-heading">
                    <div class="panel-title">
                        <h1 class="title">Party Entry</h1>
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
                            <label class="col-sm-4 control-label">Party Name<b class="text-danger">*</b></label>
                            <div class="col-sm-6">
                                <input runat="server" class="form-control" type="text" id="partyName" name="partyName" placeholder="Party Name" />
                            </div>
                            <div class="col-sm-2 control-label"></div>
                        </div>
                        <div class="form-group col-sm-6">
                            <label class="col-sm-4 control-label">Party Mobile No<b class="text-danger">*</b></label>
                            <div class="col-sm-6">
                                <input runat="server" class="form-control" type="text" id="partyMobile" name="partyMobile" placeholder="Ex 01676272718" />
                            </div>
                            <div class="col-sm-2 control-label"></div>
                        </div>
                    </div>
                    <div class="col-sm-12">
                        <div class="form-group col-sm-6">
                            <label class="col-sm-4 control-label">Party Address<b class="text-danger">*</b></label>
                            <div class="col-sm-6">
                                <textarea runat="server" class="form-control" type="text" id="partyAddress" name="partyAddress" placeholder="Write Party address here ..." ></textarea>
                            </div>
                            <div class="col-sm-2 control-label"></div>
                        </div>
                    </div>
                    <div class="form-group col-sm-12">
                        <div class="col-sm-2">
                            <asp:Button CssClass="btn btn-default btn-block" type="submit" ID="SaveParty" Text="Add" runat="server" OnClick="SaveParty_OnClick" />
                        </div>
                        <div class="col-sm-10"></div>
                    </div>

                    <style>
                        #MainContent_PartyGridView td, #MainContent_PartyGridView th {
                            padding: 10px 0;
                        }
                    </style>
                    <div class="">
                        <asp:GridView ID="PartyGridView" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" Width="100%" OnRowEditing="PartyGridView_OnRowEditing" OnRowUpdating="PartyGridView_OnRowUpdating" OnRowCancelingEdit="PartyGridView_OnRowCancelingEdit">
                            <RowStyle HorizontalAlign="Center" />
                            <Columns>
                                <asp:TemplateField HeaderText="#SL NO">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Party Id" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="id" runat="server" Text='<%# Eval("PartyId") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Party Name">
                                    <ItemTemplate>
                                        <asp:Label ID="partyName" runat="server" Text='<%# Eval("PartyName") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="partyNameTextBox" runat="server" Text='<%# Eval("PartyName") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Party Mobile">
                                    <ItemTemplate>
                                        <asp:Label ID="partyMobile" runat="server" Text='<%# Eval("PartyMobile") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="partyMobileTextBox" runat="server" Text='<%# Eval("PartyMobile") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Party Address">
                                    <ItemTemplate>
                                        <asp:Label ID="partyAddress" runat="server" Text='<%# Eval("PartyAddress") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="partyAddressTextBox" runat="server"  Text='<%# Eval("PartyAddress") %>' TextMode="MultiLine"></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkRemove" runat="server" CommandArgument='<%# Eval("PartyId")%>' OnClientClick="return confirm('Do you want to delete?')" Text="Delete" OnClick="lnkRemove_OnClick"></asp:LinkButton>
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
    <!-- Container-->--%>
</asp:Content>

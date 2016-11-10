<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" CodeBehind="ChequePass.aspx.cs" Inherits="DevERP.UI.ChequePass" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .error {
            color: red;
            display: inline-block !important;
        }
    </style>
    <script>
        $(document).ready(function () {
            $("#MainContent_fromDate").datepicker({
                autoclose: true,
                todayHighlight: true,
                format: "dd/mm/yyyy"
            });
            $("#MainContent_toDate").datepicker({
                autoclose: true,
                todayHighlight: true,
                format: "dd/mm/yyyy"
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="col-lg-12">
            <div class="row main panel panel-primary">
                <div class="panel-heading">
                    <div class="panel-title">
                        <h1 class="title">Cheque Pass</h1>
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
                        <div class="form-group col-sm-4">
                            <label class="col-sm-12 control-label">From</label>
                            <div class="col-sm-12">
                                <input runat="server" class="form-control" type="text" id="fromDate" name="fromDate" placeholder="DD/MM/YYYY" />
                            </div>
                        </div>
                        <div class="form-group col-sm-4">
                            <label class="col-sm-12 control-label">To</label>
                            <div class="col-sm-12">
                                <input runat="server" class="form-control" type="text" id="toDate" name="toDate" placeholder="DD/MM/YYYY" />
                            </div>
                        </div>
                        <div class="form-group col-sm-4">
                            <label class="col-sm-12 control-label">Cheque Status</label>
                            <div class="col-sm-12">
                                <asp:DropDownList CssClass="form-control" ID="StatusDropDown" name="StatusDropDown" runat="server" OnSelectedIndexChanged="StatusDropDown_OnSelectedIndexChanged" AutoPostBack="True">
                                    <asp:ListItem Text="All" Value="All"></asp:ListItem>
                                    <asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>
                                    <asp:ListItem Text="Pass" Value="Pass"></asp:ListItem>
                                    <asp:ListItem Text="Cancel" Value="Cancel"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="form-group col-sm-12">
                        <div class="col-sm-2">
                            <asp:Button CssClass="btn btn-default btn-block" type="submit" ID="SearchTransaction" Text="Search" runat="server" OnClick="SearchTransaction_OnClickTransaction_OnClick" />
                        </div>
                        <div class="col-sm-10"></div>
                    </div>



                    <style>
                        #MainContent_TransactionGridView td, #MainContent_TransactionGridView th {
                            padding: 10px 0;
                        }
                    </style>
                    <div class="">

                        <asp:GridView ID="TransactionGridView" runat="server" HorizontalAlignmnet="Center" CellPadding="4" ForeColor="#333333"
                            GridLines="None" AutoGenerateColumns="False" Width="100%" ShowFooter="true" AllowPaging="true" OnPageIndexChanging="TransactionGridView_OnPageIndexChanging" PageSize="5">
                            <Columns>
                                <asp:TemplateField HeaderText="#SL NO">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Transaction Id" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="id" runat="server" Text='<%# Eval("TransactionId") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Transaction Date">
                                    <ItemTemplate>
                                        <asp:Label ID="transactionDate" runat="server" Text='<%# Convert.ToDateTime(Eval("TransactionDate")).ToShortDateString() %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item">
                                    <ItemTemplate>
                                        <asp:Label ID="itemName" runat="server" Text='<%# Eval("ItemName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sub-Item">
                                    <ItemTemplate>
                                        <asp:Label ID="subItemName" runat="server" Text='<%# Eval("SubItemName") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="TotalLabel" runat="server" Text="Balance"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="amount" runat="server" Text='<%# Eval("Amount") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="balance" runat="server" Text="N/A"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Transaction Catagory">
                                    <ItemTemplate>
                                        <asp:Label ID="transactionCatagory" runat="server" Text='<%# Eval("TransactionCatagory") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Party">
                                    <ItemTemplate>
                                        <asp:Label ID="partyName" runat="server" Text='<%# Eval("PartyName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Transaction Type">
                                    <ItemTemplate>
                                        <asp:Label ID="transactionType" runat="server" Text='<%# Eval("TransactionType") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bank">
                                    <ItemTemplate>
                                        <asp:Label ID="bankName" runat="server" Text='<%# Eval("BankName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <asp:Label ID="status" runat="server" Text='<%# Eval("ChequeStatus") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Pass">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="PassButton" runat="server" CommandArgument='<%# Eval("TransactionId")%>' Text="Pass" OnClick="PassButton_OnClick"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cancel">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="CancelButton" runat="server" CommandArgument='<%# Eval("TransactionId")%>' OnClientClick="return confirm('Are You Sure?')" Text="Cancel" OnClick="CancelButton_OnClick"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <AlternatingRowStyle BackColor="White" />
                            <EditRowStyle BackColor="#7C6F57" />
                            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#E3EAEB" HorizontalAlign="Center" />
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

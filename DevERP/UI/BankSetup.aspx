<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BankSetup.aspx.cs" MaintainScrollPositionOnPostback="true" Inherits="DevERP.UI.BankSetup" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .myButtonTopMargin {
            margin-top: 5%;
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
            <div class="panel-heading">BANK INFORMATION</div>
            <div class="panel-body">
                <div class="form-horizontal">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="form-group">
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <div class="col-sm-4">
                                            <%--<div class="form-group">--%>
                                            <label>Bank ID<span style="color: red"> *</span></label>
                                            <input runat="server" class="form-control input-sm" type="text" id="bankId" />
                                            <%--</div>--%>
                                            <%--<div class="form-group">--%>
                                            <label>Bank Name<span style="color: red"> *</span></label>
                                            <input runat="server" class="form-control input-sm" type="text" id="bankName" />
                                            <%--</div>
                                    <div class="form-group">--%>
                                            <label>Contact Person<span style="color: red"> *</span></label>
                                            <input runat="server" type="text" class="form-control input-sm" id="contactNameText" />
                                            <%-- </div>
                                    <div class="form-group">--%>
                                            <label>Address</label>
                                            <input runat="server" type="text" class="form-control input-sm" id="addressText" />
                                            <%--</div>
                                    <div class="form-group">--%>
                                            <label>Phone<span style="color: red"> *</span></label>
                                            <input runat="server" type="text" class="form-control" id="contactNumber" />
                                            <%-- </div>
                                    <div class="form-group">--%>
                                            <label>E-Mail</label>
                                            <input runat="server" class="form-control input-sm" type="text" id="emailAddressText" />
                                            <%--</div>
                                    <div class="form-group">--%>
                                            <label>Card Commision(%)<span style="color: red"> *</span></label>
                                            <input runat="server" type="text" class="form-control input-sm" id="cardCommisionText" />
                                            <%--</div>
                                    <div class="form-group">--%>
                                            <div class="col-sm-12">
                                                <asp:Button ID="saveButton" runat="server" class="btn btn-success myButtonTopMargin" Text="Save" OnClick="saveButton_Click" />
                                            </div>
                                            <div class="col-sm-12">
                                                <br />
                                                <br />
                                                <asp:Literal ID="bankInfoLiteral" runat="server" Text="_"> </asp:Literal>
                                                <br />
                                            </div>
                                            <%--</div>--%>
                                        </div>
                                        <div class="col-sm-8">
                                            <div style="border: 1px solid #ededed; padding: 5px; height: 496px;">
                                                <div class="col-lg-12">
                                                    <%--OnSelectedIndexChanged="itemGridView_SelectedIndexChanged" OnRowDataBound="ItemOnRowDataBound"--%>
                                                    <asp:GridView ID="bankGridView" runat="server" UseAccessibleHeader="true"
                                                        OnSelectedIndexChanged="bankGridView_SelectedIndexChanged" OnRowDataBound="BankOnRowDataBound"
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
                                                                    <asp:Label ID="idLabel" runat="server" Text='<%# Eval("VarBankid") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Bank Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="bankNameLabel" runat="server" Text='<%# Eval("VarBankName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Contact Person">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="contactPersonLabel" runat="server" Text='<%# Eval("VarContractpersons") %>'></asp:Label>
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
                                    </ContentTemplate>
                                </asp:UpdatePanel>
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
            $('#<%=bankGridView.ClientID %>').Scrollable({
                ScrollHeight: 430,
                IsInUpdatePanel: true
            });
        });
    </script>
</asp:Content>

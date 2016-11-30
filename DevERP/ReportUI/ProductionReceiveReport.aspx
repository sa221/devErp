<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductionReceiveReport.aspx.cs" Inherits="DevERP.ReportUI.ProductionReceiveReport" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script>

        $(document).ready(function () {
            $("#MainContent_fromDate").datepicker({
                autoclose: true,
                todayHighlight: true,
                format: "dd/mm/yyyy"
            });
        })
    </script>
    <script>

        $(document).ready(function () {
            $("#MainContent_toDate").datepicker({
                autoclose: true,
                todayHighlight: true,
                format: "dd/mm/yyyy"
            });
        })
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="col-sm-12 col-sm-offset-0">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <h4>Production Receive Report</h4>
            </div>
            <div class="panel-body">
                <div class="col-sm-12">
                    <div class="form-group">
                        <div class="col-sm-2">
                            <label for="empId">Employee ID:</label>
                            <input type="text" runat="server" class="form-control input-sm" name="empId" id="empId" />
                        </div>
                        <div class="col-sm-3">
                            <label>Product Name:</label>
                            <asp:DropDownList ID="productDropDownList" runat="server" CssClass="form-control input-sm" AutoPostBack="False"></asp:DropDownList>

                        </div>
                        <div class="col-sm-3">
                            <label>From Date:</label>
                            <input runat="server" class="form-control" type="text" id="fromDate" name="fromDate" placeholder="Ex DD/MM/YYYY" />
                        </div>
                        <div class="col-sm-3">
                            <label>To Date:</label>
                            <input runat="server" class="form-control" type="text" id="toDate" name="toDate" placeholder="Ex DD/MM/YYYY" />
                        </div>
                        <div class="col-sm-1">
                            <br />
                            <asp:Button ID="showReportButton" runat="server" CssClass="btn btn-primary" Text="Report" OnClick="showReportButton_Click" />
                        </div>
                    </div>

                </div>
                <div class="col-sm-12">
                    <div class="form-group">
                    </div>
                </div>
                <div class="col-sm-10 col-sm-offset-1">
                    <CR:CrystalReportViewer ID="productionReportViewer" runat="server" AutoDataBind="True" EnableDatabaseLogonPrompt="False" GroupTreeImagesFolderUrl="" Height="1269px" ReportSourceID="CrystalReportSource2" ToolbarImagesFolderUrl="" ToolPanelView="None" ToolPanelWidth="200px" Width="881px" />
                    <CR:CrystalReportSource ID="CrystalReportSource2" runat="server">
                        <Report FileName="E:\DOTnet\deverp\DevERP\Reports\ProductionReceive.rpt">
                        </Report>
                    </CR:CrystalReportSource>
                    <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
                        <Report FileName="Reports\ProductionReceive.rpt">
                        </Report>
                    </CR:CrystalReportSource>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

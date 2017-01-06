<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DailyProductionStatementUI.aspx.cs" Inherits="DevERP.ReportUI.DailyProductionStatementUI" %>
<%@ Register TagPrefix="CR" Namespace="CrystalDecisions.Web" Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script>

        $(document).ready(function () {
            $("#MainContent_date").datepicker({
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
                        
                        <div class="col-sm-4">
                            <label>Date:</label>
                            <input runat="server" class="form-control" type="text" id="date" name="date" placeholder="Ex DD/MM/YYYY" />
                        </div>
                        <div class="col-sm-4"> <br />
                            <asp:Button ID="showReportButton" runat="server" CssClass="btn btn-primary" Text="Report" OnClick="showReportButton_Click" />
                        </div>
                        <div class="col-sm-4"></div>
                        </div>


                </div>
                <div class="col-sm-12">
                    <div class="form-group">
                    </div>
                </div>
                <div class="col-sm-10 col-sm-offset-1">
                    <CR:CrystalReportViewer ID="dailyStatementViewer" runat="server" AutoDataBind="true" EnableParameterPrompt="False" ToolPanelView="None" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>

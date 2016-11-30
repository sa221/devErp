<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductionEntry.aspx.cs" Inherits="DevERP.UI.ProductionEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script>

        $(document).ready(function () {
            $("#MainContent_productionDate").datepicker({
                autoclose: true,
                todayHighlight: true,
                format: "dd/mm/yyyy"
            });
        })
    </script>
    <script type="text/javascript">
        
        function sum() {
            var txtFirstNumberValue = document.getElementById("MainContent_productRate").value;
            var txtSecondNumberValue = document.getElementById("MainContent_productQuantity").value;
            var result = parseInt(txtFirstNumberValue) * parseInt(txtSecondNumberValue);
            if (!isNaN(result)) {
                document.getElementById("MainContent_totalTaka").value = result;
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="col-sm-10 col-sm-offset-1">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <h4>Production Receive</h4>
            </div>
            <div class="panel-body">
                <asp:Label ID="messageLabel" runat="server" Text=""></asp:Label>
                <div class="col-lg-12">
                    <div class="form-group">
                        <div class="col-sm-2">
                            <label for="empId">Employee ID:</label>
                            <input type="text" runat="server" class="form-control input-sm" name="empId" id="empId" />
                        </div>
                        <div class="col-sm-1">
                            <br />
                            <asp:Button ID="searchEmpButton" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="searchEmpButton_Click" />
                        </div>
                        <div class="col-sm-1">
                        </div>
                        <div class="col-sm-4">
                            <label>Date:</label>
                            <input runat="server" class="form-control" type="text" id="productionDate" name="productionDate" placeholder="Ex DD/MM/YYYY" />
                        </div>
                        <div class="col-sm-4">
                            <br />
                            <label id="empName" runat="server" style="background-color: #22FF99"></label>
                            <asp:TextBox ID="productionIdTextBox" runat="server" Visible="False"></asp:TextBox>
                            <asp:TextBox ID="previousQtyTextBox" runat="server" Visible="False"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-lg-12">
                    <div class="form-group">
                        <div class=" col-sm-4">
                            <label>Product Name:</label>
                            <asp:DropDownList ID="productDropDownList" runat="server" CssClass="form-control input-sm" AutoPostBack="True" OnSelectedIndexChanged="productDropDownList_SelectedIndexChanged"></asp:DropDownList>

                        </div>

                        <div class="col-sm-2">
                            <label>Rate:</label>
                            <input type="text" runat="server" class="form-control input-sm" name="productRate" id="productRate" />
                        </div>
                        <div class="col-sm-2">
                            <label>Quantity:</label>
                            <input type="text" runat="server" class="form-control input-sm" name="productQuantity" id="productQuantity" onkeyup="sum()" />
                        </div>
                        <div class="col-sm-2">
                            <label>Total:</label>
                            <input type="text" runat="server" class="form-control input-sm" name="totalTaka" id="totalTaka" />
                        </div>
                        <div class="col-sm-2">
                            <br />
                            <asp:Button ID="saveButton" runat="server" CssClass="btn btn-success" Text="Save" OnClick="saveButton_Click" />

                        </div>
                    </div>
                </div>
                <div class="col-sm-12">
                    <div class="form-group">
                        <div class="col-sm-12">
                            <br />
                            <span class="label label-warning" style="float: left; font-size: 20px; position: relative; background-color: #f0ad4e" id="failStatusLavel" runat="server"></span>
                            <span class="label label-success" style="float: left; font-size: 20px; position: relative; background-color: #5cb85c" id="successStatusLavel" runat="server"></span>
                            <br />
                            <br />
                        </div>
                    </div>

                </div>
                <div class="col-lg-12">
                    <div class="form-group">

                        <div class="col-sm-12" style="width: 100%;">
                            <asp:GridView ID="productionGridView" runat="server" OnRowCommand="productionGridView_RowCommand" AutoGenerateColumns="False" CssClass="table table-bordered table table-responsive" AllowPaging="true"
                                OnPageIndexChanging="OnPageIndexChanging" PageSize="5">
                                <RowStyle HorizontalAlign="Center"></RowStyle>
                                <Columns>
                                    <asp:TemplateField HeaderText="SL#">
                                        
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("ProductionDate","{0:d}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Product Name">
                                        <ItemTemplate>
                                            <asp:Label ID="Label6" runat="server" Text='<%# Eval("FullProductName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Product ID" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("ProductId") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Production ID" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="Label7" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Employee ID" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="empIdLabel" runat="server" Text='<%# Eval("EmpId") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rate">
                                        <ItemTemplate>
                                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("Rate") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Quantity">
                                        <ItemTemplate>
                                            <asp:Label ID="Label4" runat="server" Text='<%# Eval("Quantity") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total">
                                        <ItemTemplate>
                                            <asp:Label ID="Label5" runat="server" Text='<%# Eval("TotalRate") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Edit">
                                        <ItemTemplate>
                                            <asp:Button ID="btnEdit" runat="server" Width="50" CssClass="btn btn-primary" Text="Edit" CommandName="EditButton"
                                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delete">
                                            <ItemTemplate>
                                                <asp:Button ID="btnDelete" runat="server" Width="70" Text="Delete" CssClass="btn btn-danger"  CommandName="DeleteButton" 
                                                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                </Columns>

                                <PagerSettings FirstPageText="First " LastPageText="Last " Mode="NextPreviousFirstLast" NextPageText="Next " PreviousPageText="Prev" />

                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!--panel-body-->
    </div>

</asp:Content>

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
        //$(document).ready(function () {
        //    $('productQuantity').focusout(function () {
        //        var x = parseInt($('productRate').val());
        //        var y = parseInt($('#productQuantity').val());
        //        $('#total').val(y * x);
                

        //    });

        //});
        function sum() {
            var txtFirstNumberValue = parseInt(document.getElementById('productRate'));
            var txtSecondNumberValue = document.getElementById('productQuantity');
            var result = parseInt(txtFirstNumberValue) * parseInt(txtSecondNumberValue);
            if (!isNaN(result)) {
                document.getElementById("total").value = result;
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
                            <input type="text" runat="server" class="form-control input-sm" name="total" id="total" />
                        </div>
                        <div class="col-sm-2">
                            <br />
                            <button id="saveButton" type="button" class="btn btn-success"><strong>Save</strong></button>
                        </div>
                    </div>
                </div>
                <div class="col-lg-12">
                    <div class="form-group">

                        <div class="col-sm-12" style="width: 100%;">
                            <asp:GridView ID="productionGridView" runat="server" AutoGenerateColumns="False" CssClass="table  table-bordered" AllowPaging="True" PageSize="2">
                                <Columns>
                                    <asp:TemplateField HeaderText="SL#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Type Name">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("ProductType") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>

                                <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NextPreviousFirstLast" NextPageText="Next" PreviousPageText="Prev" />

                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!--panel-body-->
    </div>

</asp:Content>

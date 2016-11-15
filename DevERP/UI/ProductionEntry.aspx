<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductionEntry.aspx.cs" Inherits="DevERP.UI.ProductionEntry" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  
        <div class="col-sm-10 col-sm-offset-1">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h4>Vehicale Maintenance</h4>
                </div>
                <div class="panel-body">
                    <asp:Label ID="messageLabel" runat="server" Text=""></asp:Label>
                    <div class="form-group">
                        <div class=" col-sm-4">
                            
                            <label for="maintenanceId">Maintenance ID:</label>
                            <input type="text" runat="server" class="form-control input-sm" name="maintenanceId" id="maintenanceId" />
                        </div>
                        <div class=" col-sm-4 ">
                            <%--<span class="error pull-right" id="categoryNameError">
                                <img src="../Images/alert-triangle-red.png" alt="error message" style="display: none;" />
                            </span>
                            <label for="categoryname">Category name</label>
                            <input type="text" runat="server" class="form-control input-sm" name="categoryname" id="categoryname" />--%>
                        </div>

                        <div class="col-sm-4">
                            <label>Date:</label>
                            <div class='input-group date' id='datetimepicker1'>
                                <asp:TextBox ID="dateTextBox" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                <span class="input-group-addon" id="inputFrom">
                                    <span class="glyphicon glyphicon-calendar"></span>
                                </span>
                            </div>
                            <span class="error" id="fromTextBoxValidation"></span>
                        </div>
                    </div>


                    <div class="form-group">
                        <div class=" col-sm-4">
                            <label>Vehicle No:</label>
                            <asp:DropDownList ID="busNoDropDownList" runat="server" CssClass="form-control input-sm" AutoPostBack="False"></asp:DropDownList>
                            <span id="validatebusNoDropDownList" class="error"></span>
                        </div>

                        <div class="col-sm-4">
                            <label></label>
                            <asp:Button type="submit" class="btn btn-default historyMargin" ID="historyButton" Text="History Of Maintenance" runat="server"></asp:Button>
                        </div>
                    </div>
                    <div class="form-group">
                    </div>
                    <div class="form-group">
                        <div class=" col-sm-4">
                            
                            <label for="remarksTextBox">Remarks:</label>
                            <%--                            <input type="text" runat="server" class="form-control input-sm" name="remarksTextBox" id="remarksTextBox"/>--%>
                            <textarea class="form-control input-sm" name="remarksTextBox" id="remarksTextBox"></textarea>
                        </div>
                        <div class="col-sm-8" style="width: 100%;">
                            <asp:GridView ID="maintenanceHistoryGridView" runat="server" AutoGenerateColumns="False" CssClass="table  table-bordered" AllowPaging="True" PageSize="2">
                                <Columns>

                                    <asp:TemplateField HeaderText="Date">
                                        <ItemTemplate><%# Eval("GroupName") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Purpose">
                                        <ItemTemplate><%# Eval("GroupName") %></ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Item">
                                        <ItemTemplate><%# Eval("CategoryName") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Qty">
                                        <ItemTemplate><%# Eval("ItemCode") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Old Item">
                                        <ItemTemplate><%# Eval("ItemName") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Qty">
                                        <ItemTemplate><%# Eval("Unit") %></ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>

                                <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NextPreviousFirstLast" NextPageText="Next" PreviousPageText="Prev" />

                            </asp:GridView>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class=" col-sm-4">
                            <label>Departments:</label>
                            <asp:DropDownList ID="departmentsDropDownList" runat="server" CssClass="form-control input-sm" AutoPostBack="False"></asp:DropDownList>
                            <span id="departmentDropDownList" class="error"></span>
                        </div>
                        <div class=" col-sm-4">
                            <label>Service Location:</label>
                            <asp:DropDownList ID="locationsDropDownList" runat="server" CssClass="form-control input-sm" AutoPostBack="False"></asp:DropDownList>
                            <span id="locationDropDownList" class="error"></span>
                        </div>
                        <div class=" col-sm-4">
                            <label for="otherLocation">If Service Other Location:</label>
                            <input type="text" runat="server" class="form-control input-sm" name="otherLocation" id="otherLocation" />
                        </div>
                    </div>
                    <div class="form-group">
                        <div class=" col-sm-2">
                            <label>Parts Name:</label>
                            <asp:DropDownList ID="partsDropDownList" runat="server" CssClass="form-control input-sm" AutoPostBack="False"></asp:DropDownList>
                            <span id="partDropDownList" class="error"></span>
                        </div>
                        <div class=" col-sm-1">
                            <label for="qtyText">QTY:</label>
                            <input type="text" runat="server" class="form-control input-sm" name="qtyText" id="qtyText" />
                        </div>
                        <div class=" col-sm-1">
                            <label for="unitText">UNIT :</label>
                            <input type="text" runat="server" class="form-control input-sm" name="unitText" id="unitText" />
                        </div>
                        <div class=" col-sm-2">
                            <label for="lifeCycleText">Life Cycle:</label>
                            <input type="text" runat="server" class="form-control input-sm" name="lifeCycleText" id="lifeCycleText" />
                        </div>
                        <div class=" col-sm-3">

                            <label for="purposeText">Purpose:</label>
                            <input type="text" runat="server" class="form-control input-sm" name="purposeText" id="purposeText" />
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:CheckBox ID="oldPartsCheckBox" runat="server" Text=" Received Old Parts" Style="margin-left: 2%;" />
                    </div>
                    <div class="form-group">
                        <div class=" col-sm-2">
                            <label>Old Parts Name:</label>
                            <asp:DropDownList ID="oldPartsDropDownList" runat="server" CssClass="form-control input-sm" AutoPostBack="False"></asp:DropDownList>
                            <span id="oldPartDropDownList" class="error"></span>
                        </div>
                        <div class=" col-sm-1">
                            <label for="oldQtyText">QTY:</label>
                            <input type="text" runat="server" class="form-control input-sm" name="oldQtyText" id="oldQtyText" />
                        </div>
                        <div class=" col-sm-1">
                            <label for="oldUnitText">UNIT :</label>
                            <input type="text" runat="server" class="form-control input-sm" name="oldUnitText" id="oldUnitText" />
                        </div>
                        <div class=" col-sm-2">
                            <label for="oldLifeCycleText">Life Cycle:</label>
                            <input type="text" runat="server" class="form-control input-sm" name="oldLifeCycleText" id="oldLifeCycleText" />
                        </div>
                        <div class=" col-sm-3">

                            <label for="oldPurposeText">Purpose:</label>
                            <input type="text" runat="server" class="form-control input-sm" name="oldPurposeText" id="oldPurposeText" />
                        </div>
                        <div class=" col-sm-2">
                            <label for="receiveDateText">Rec. Date:</label>
                            <input type="text" runat="server" class="form-control input-sm" name="receiveDateText" id="receiveDateText" />
                        </div>

                        <div class="col-lg-1">
                            <button class="btn btn-default" id="addButton" type="button" style="margin-top: 39%;">Add</button>
                        </div>
                    </div>
                   

                    <div class="form-group">
                        <div class="col-sm-5"></div>
                        <div class="col-sm-1 ">
                            <br />
                            <%--<asp:Button type="submit" class="btn btn-success btn-sm" ID="ItemSaveButton" Text="Save" runat="server"></asp:Button>--%>
                            <asp:Button Text="Submit" runat="server" />
                        </div>
                        <div class="col-sm-1">
                            <br />
                            <asp:Button ID="reportButton" runat="server" Text="Report" CssClass="btn btn-info btn-sm" />
                        </div>
                        <div class="col-sm-1">
                            <button id="saveButton" type="button" class="btn btn-success"><strong>Create Schedule</strong></button>
                        </div>
                        <div class="col-sm-4"></div>

                    </div>





                </div>
            </div>
            <!--panel-body-->
        </div>

</asp:Content>

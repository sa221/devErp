<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PartsEntryUI.aspx.cs" Inherits="DevERP.UI.PartsEntryUI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../Content/media/css/jquery.dataTables.min.css" rel="stylesheet" />
    <script src="../Content/media/js/jquery.dataTables.min.js"></script>
    <script>
        $(document).ready(function () {
            $(function () {
                $("#MainContent_ItemGridView").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable({
                    "scrollY": "180px",
                    "scrollCollapse": true,
                    "paging": false

                });
            });

            $("#addButton").click(function () {
                if ($("#MainContent_groupnameNoDropDownList").val() == "0") {
                    $("#validategroupnameNoDropDownList").html("Please Select Group Name");
                    return false;
                } else {
                    $("#validategroupnameNoDropDownList").html("");
                }
            });
            $("#addButton").click(function () {
                if ($("#MainContent_categorynameDropDownList").val() == "0") {
                    $("#validatecategorynameDropDownList").html("Please Select Category Name");
                    return false;
                } else {
                    $("#validatecategorynameDropDownList").html("");
                }
            });


            function numberChecking(myValue) {
                var d = $.isNumeric(myValue);
                return d;
            }



            $("#addButton").click(function () {
                var data = $('#MainContent_partsNameTextBox').val();
                if (data == "") {
                    $("#partsNameTextBoxValidate").html("Please Insert Parts Name");
                    return false;
                } else {
                    var white = $.trim($('#MainContent_partsNameTextBox').val()).length;
                    if (white == "") {
                        $("#partsNameTextBoxValidate").html("White Spaces are not allowed ");
                        return false;
                    } else {
                        $("#partsNameTextBoxValidate").html("");
                    }
                }
            });

            $("#addButton").click(function () {
                var data = $('#MainContent_unit').val();
                if (data == "") {
                    $("#unitValidate").html("Please Insert Unit");
                    return false;
                } else {
                    var white = $.trim($('#MainContent_unit').val()).length;
                    if (white == "") {
                        $("#unitValidate").html("White Spaces are not allowed ");
                        return false;
                    } else {
                        $("#unitValidate").html("");
                    }
                }
            });

            $("#addButton").click(function () {
                var white = $.trim($('#MainContent_tenRateTextBox').val()).length;
                if (white == "") {
                    $("#tenRateTextBoxValidate").html("White Spaces are not allowed ");
                    return false;
                } else {
                    if (numberChecking($('#MainContent_tenRateTextBox').val()) === false) {
                        $("#tenRateTextBoxValidate").html("Please Insert Number");
                        return false;
                    } else {
                        $("#tenRateTextBoxValidate").html("");
                    }

                }
            });
            //$("#addButton").on("click", function () {
            //    var value = $('#bodyContentPlaceHolder_partsId').val();

            //    $("table tr").each(function (index) {
            //        if (index !== 0) {

            //            $row = $(this);

            //            var id = $row.find("td:first").text();

            //            if (id.indexOf(value) !== 0) {
            //                $row.hide();
            //            }
            //            else {
            //                $row.show();
            //            }
            //        }
            //    });
            //});

            $("#addButton").click(function () {

                if ($("#MainContent_groupnameNoDropDownList").val() != "0" && $("#MainContent_categorynameDropDownList").val() != "0"
                    && $('#MainContent_partsNameTextBox').val() != "" && $.trim($('#MainContent_partsNameTextBox').val()).length != ""
                    && $('#MainContent_unit').val() != "" && $.trim($('#MainContent_unit').val()).length
                    && $.trim($('#MainContent_tenRateTextBox').val()).length != "" && (numberChecking($('#MainContent_tenRateTextBox').val()) === true)
                    ) {
                    var groupId = $("#MainContent_groupnameNoDropDownList").val();
                    var categoryId = $("#MainContent_categorynameDropDownList").val();
                    var partsId = $('#MainContent_partsId').val();
                    var partsName = $('#MainContent_partsNameTextBox').val();
                    var unit = $('#MainContent_unit').val();
                    var tenRate = $('#MainContent_tenRateTextBox').val();
                    var usesLocation = $('#MainContent_usesLocation').val();
                    var lifeCycle = $('#MainContent_lifeCycleTextBox').val();
                    saveParts(groupId, categoryId, partsId, partsName, unit, tenRate, usesLocation, lifeCycle);
                }
            });

            function saveParts(groupId, categoryId, partsId, partsName, unit, tenRate, usesLocation, lifeCycle) {
                $.ajax({
                    type: 'Post',
                    contentType: "application/json; charset=utf-8",
                    url: "PartsEntryUI.aspx/SaveParts",
                    data: "{'groupId':'" + groupId + "'," +
                        "'categoryId':'" + categoryId + "'," +
                        "'partsName':'" + partsName + "'," +
                        "'unit':'" + unit + "'," +
                        "'tenRate':'" + tenRate + "'," +
                        "'usesLocation':'" + usesLocation + "'," +
                        "'lifeCycle':'" + lifeCycle + "'," +
                        "'partsId':'" + partsId +
                        "'}",
                    async: false,
                    success: function (data) {
                        //MakeFeildEmpty(); 

                        if (partsId == "") {
                            $("table tbody").append(
                                 "<tr role='row'>"
                                 + "<td>" + '<span itemstyle-cssclass="PartsCode">' + data.d + "</span></td>"
                                 + "<td>" + '<span itemstyle-cssclass="PartsName">' + partsName + "</span></td>"
                                 + "<td>" + '<span itemstyle-cssclass="UnitLabel">' + unit + "</span></td>"
                                 + "<td>" + '<span itemstyle-cssclass="TenRate">' + tenRate + "</span></td>"
                                 + "<td>" + '<span itemstyle-cssclass="LifeCycle">' + usesLocation + "</span></td>"
                                 + "<td>" + '<span itemstyle-cssclass="UsesLoc">' + lifeCycle + "</span></td>"
                                 + "<td>" + '<a class="Edit glyphicon glyphicon-pencil btn btn-warning btn-sm">Edit</a> '
                                 + ' <a class="Delete glyphicon glyphicon-trash btn btn-danger btn-sm">Delete</a>' + "<td>"
                                 + "</tr>"
                     );
                        }

                        $("#MainContent_messageLabel").html(data.d);
                    },
                    error: function () {
                        alert("Error");
                    }
                });
            };

            $("body").on("click", "[id*=ItemGridView] .Edit", function () {
                ClearValidatorErrorText();
                var partsCode = $(this).parent().siblings().eq(0).find("span").text();
                var partsName = $(this).parent().siblings().eq(1).find("span").text();
                var unit = $(this).parent().siblings().eq(2).find("span").text();
                var tenRate = $(this).parent().siblings().eq(3).find("span").text();
                var life = $(this).parent().siblings().eq(4).find("span").text();
                var location = $(this).parent().siblings().eq(5).find("span").text();
                $.ajax({
                    type: 'Post',
                    contentType: "application/json; charset=utf-8",
                    url: "PartsEntryUI.aspx/GetGroupAndCategoryByPartsId",
                    data: "{'partsCode':'" + partsCode + "'}",
                    async: false,
                    success: function (data) {
                        //alert(data.d.GroupId);
                        $("#MainContent_partsId").val(partsCode);
                        $("#MainContent_partsNameTextBox").val(partsName);
                        $("#MainContent_unit").val(unit);
                        $("#MainContent_tenRateTextBox").val(tenRate);
                        $("#MainContent_unit").val(unit);
                        $("#MainContent_lifeCycleTextBox").val(life);
                        $("#MainContent_usesLocation").val(location);

                        $('#MainContent_groupnameNoDropDownList >option:eq(' + data.d.GroupId + ')').prop('selected', true);
                        $('#MainContent_categorynameDropDownList >option:eq(' + data.d.CategoryId + ')').prop('selected', true);

                    },
                    error: function () {
                        alert("Error");
                    }
                });
                return false;
            });

            $("body").on("click", "[id*=ItemGridView] .Delete", function () {
                ClearValidatorErrorText();
                var partsName = $(this).parent().siblings().eq(1).find("span").text();
                var confirmMessg = confirm("Are You Sure To Delete '" + partsName + "'");
                if (confirmMessg == true) {
                    var partsCode = $(this).parent().siblings().eq(0).find("span").text();
                    var row = $(this).closest('tr');

                    $.ajax({
                        type: 'Post',
                        contentType: "application/json; charset=utf-8",
                        url: "PartsEntryUI.aspx/DeletePartsItemById",
                        data: "{'partsCode':'" + partsCode + "'}",
                        async: false,
                        success: function (data) {
                            row.hide();
                            $("#MainContent_messageLabel").html(data.d);
                        },
                        error: function () {
                            alert("Error");
                        }
                    });
                    return false;
                } else {
                    $("#MainContent_messageLabel").html("Delete Canceled");
                    return false;

                }

            });

            $("#clearButon").click(function () {
                MakeFeildEmpty();
                ClearValidatorErrorText();
            });
        });

        function MakeFeildEmpty() {
            $('#MainContent_groupnameNoDropDownList >option:eq(0)').prop('selected', true);
            $('#MainContent_categorynameDropDownList >option:eq(0)').prop('selected', true);
            $("#MainContent_partsId").val("");
            $("#MainContent_partsNameTextBox").val("");
            $("#MainContent_unit").val("");
            $("#MainContent_tenRateTextBox").val("");
            $("#MainContent_lifeCycleTextBox").val("");
            $("#MainContent_usesLocation").val("");
        }
        function ClearValidatorErrorText() {
            $("#validategroupnameNoDropDownList").html("");
            $("#validatecategorynameDropDownList").html("");
            $("#partsIDValidate").html("");
            $("#partsNameTextBoxValidate").html("");
            $("#unitValidate").html("");
            $("#tenRateTextBoxValidate").html("");
            $("#MainContent_messageLabel").html("");
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="col-sm-8 col-sm-offset-2">
        <div class="panel panel-primary" style="margin-top: 10px">
            <div class="panel-heading">Parts Infromation</div>
            <div class="panel-body">
                <div class="form-group">
                    <div class=" col-sm-3">

                        <label for="groupnameNoDropDownList">Group name<span style="color: red"> *</span></label>
                        <asp:DropDownList ID="groupnameNoDropDownList" CssClass="form-control input-sm" runat="server"></asp:DropDownList>
                        <span id="validategroupnameNoDropDownList" class="error"></span>
                    </div>
                    <div class=" col-sm-3 ">
                        <label for="categoryname">Category name<span style="color: red"> *</span></label>
                        <asp:DropDownList ID="categorynameDropDownList" CausesValidation="True" CssClass="form-control input-sm" runat="server"></asp:DropDownList>
                        <span id="validatecategorynameDropDownList" class="error"></span>
                    </div>
                    <div class=" col-sm-3">
                        <label for="partsId">Parts ID</label>
                        <input type="text" runat="server" class="form-control input-sm" readonly="readonly" name="partsId" id="partsId" />
                        <span id="partsIDValidate" class="error"></span>
                    </div>
                    <div class=" col-sm-3">
                        <label for="partsNameTextBox">Parts Name<span style="color: red"> *</span></label>
                        <input type="text" runat="server" class="form-control input-sm" name="partsNameTextBox" id="partsNameTextBox" />
                        <span id="partsNameTextBoxValidate" class="error"></span>
                    </div>
                </div>
                <div class="form-group">
                    <div class=" col-sm-3">
                        <label for="unit">Unit<span style="color: red"> *</span></label>
                        <input type="text" runat="server" class="form-control input-sm" name="unit" id="unit" />
                        <span id="unitValidate" class="error"></span>
                    </div>
                    <div class=" col-sm-3">
                        <label for="tenRateTextBox">Ten Rate<span style="color: red"> *</span></label>
                        <input type="text" runat="server" class="form-control input-sm" name="tenRateTextBox" id="tenRateTextBox" />
                        <span id="tenRateTextBoxValidate" class="error"></span>
                    </div>
                    <div class=" col-sm-3">
                        <label for="usesLocation">Uses Location</label>
                        <input type="text" runat="server" class="form-control input-sm" name="usesLocation" id="usesLocation" />

                    </div>
                    <div class=" col-sm-3">
                        <label for="lifeCycleTextBox">Life Cycle</label>
                        <input type="text" runat="server" class="form-control input-sm" name="lifeCycleTextBox" id="lifeCycleTextBox" />
                    </div>
                </div>
                <div class="form-group" style="margin-bottom: 0px;">
                    <div class="col-sm-4"></div>
                    <div class="col-sm-6">
                        <button type="button" id="clearButon" class="btn btn-primary btn-sm"><span class="glyphicon glyphicon-remove-circle"></span>Clear</button>&nbsp;
                            <button type="button" id="addButton" class="btn btn-success btn-sm"><span class="glyphicon glyphicon-ok"></span>Save</button>&nbsp;                           
                            <button type="button" id="reportButton" class="btn btn-info input-sm"><span class="glyphicon glyphicon-print"></span>Print</button>
                    </div>
                    <div class="col-sm-2"></div>
                    <div class="form-group">
                    </div>
                    <div class="col-sm-12">
                        <asp:Label ID="messageLabel" CssClass="error" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="form-group" style="">
                    <div class="col-sm-12" style="width: 100%;">
                        <asp:GridView ID="ItemGridView" runat="server" AutoGenerateColumns="False" CssClass="table  table-bordered">
                            <Columns>
                                <asp:TemplateField HeaderText="Parts ID">
                                    <ItemTemplate>
                                        <asp:Label ID="PartsCode" runat="server" ItemStyle-CssClass="PartsCode" Text='<%#Eval("PartsCode") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Parts Name">
                                    <ItemTemplate>
                                        <asp:Label ID="PartsName" runat="server" ItemStyle-CssClass="PartsName" Text='<%#Eval("PartsName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="UnitLabel" runat="server" ItemStyle-CssClass="UnitLabel" Text='<%#Eval("Unit") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="T.Rate">
                                    <ItemTemplate>
                                        <asp:Label ID="TenRate" runat="server" ItemStyle-CssClass="TenRate" Text='<%#Eval("TenRate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Life Cycle">
                                    <ItemTemplate>
                                        <asp:Label ID="LifeCycle" runat="server" ItemStyle-CssClass="LifeCycle" Text='<%#Eval("LifeCycle") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Use Location">
                                    <ItemTemplate>
                                        <asp:Label ID="UsesLoc" runat="server" ItemStyle-CssClass="UsesLoc" Text='<%#Eval("UsesLoc") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        &nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton Text="Edit" runat="server" CssClass="Edit glyphicon glyphicon-pencil btn btn-warning btn-sm" />&nbsp;&nbsp;
                                            <asp:LinkButton Text="Delete" runat="server" CssClass="Delete glyphicon glyphicon-trash btn btn-danger btn-sm" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <%--    <script src="../Scripts/MyScript/Parts.js"></script>--%>
        </div>
        <!--panel-body-->
    </div>
    <style>
        * {
            margin-top: 0px;
        }

        .error {
            color: red;
            font-style: italic;
        }

        .btn {
            font-size: 12px;
        }

        .error {
            font-weight: bold;
        }

        #MainContent_ItemGridView thead td {
            padding: 0px;
        }

        #MainContent_ItemGridView tbody td {
            padding: 0px;
        }

        tr {
            text-align: center;
        }
    </style>
    <!--panel-default-->
</asp:Content>

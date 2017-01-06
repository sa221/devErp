<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EmployeeEntry.aspx.cs" Inherits="DevERP.UI.EmployeeEntry" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .error {
            color: red;
            display: inline-block !important;
        }

        #employeePicPreview img, #employeeSignPreview img{
            height: 100%;
            width: 100%;
            display: block
        }
    </style>
    <script>
        $(document).ready(function () {
            $("#MainContent_nationalIdCardNo").focusout(function () {
                var input = $(this).val();
                ConvertDigit(input).success(function (data) {
                    $("#MainContent_nationalIdCardNoBangla").val(data.d);
                });
            });
            $("#MainContent_attandenceCardId").focusout(function () {
                var input = $(this).val();
                ConvertDigit(input).success(function (data) {
                    $("#MainContent_attandenceCardIdBangla").val(data.d);
                });
            });
            $("#MainContent_joiningDate").change(function () {
                var input = $(this).val();
                ConvertDigit(input).success(function (data) {
                    $("#MainContent_joiningDateBangla").val(data.d);
                });
            });
            $("#MainContent_firstName").focusout(function () {
                var input = $(this).val();
                Convert(input).success(function (data) {
                    $("#MainContent_firstNameBangla").val(data.d);
                });
            });
            $("#MainContent_lastName").focusout(function () {
                var input = $(this).val();
                Convert(input).success(function (data) {
                    $("#MainContent_lastNameBangla").val(data.d);
                });
            });
            $("#MainContent_grossalary").focusout(function () {
                var grossSalary = $(this).val();

                $("#MainContent_basicSalary").val(GetBasicSalary(grossSalary));
                $("#MainContent_houseRent").val(GetHouseRent(grossSalary));
                $("#MainContent_medicalSupport").val(MedicalSupport);
                $("#MainContent_convanceSupport").val(ConvanceSupport);
                $("#MainContent_foodSupport").val(FoodSupport);
            });
            $("#mainForm").validate({
                rules: {
                    ctl00$MainContent$employeeId: "required",
                    ctl00$MainContent$firstName: "required",
                    ctl00$MainContent$grossalary: {
                        required: true,
                        minlength: 4
                    }
                },
                messages: {
                    ctl00$MainContent$employeeId: "  Employee Id can not be empty",
                    ctl00$MainContent$firstName: "  First name should given",
                    ctl00$MainContent$grossalary: {
                        required: "  Gross salary should given",
                        minlength: "  please check again the salary"
                    }
                }
            });
            $("#MainContent_dateOfBirth").datepicker({
                autoclose: true,
                todayHighlight: true
            });
            $('#MainContent_marrigeDate').datepicker({
                autoclose: true,
                todayHighlight: true
            });
            $('#MainContent_joiningDate').datepicker({
                autoclose: true,
                todayHighlight: true
            });
            $("#MainContent_firstNameBangla").avro();
            $("#MainContent_lastNameBangla").avro();
            $("#MainContent_nationalIdCardNoBangla").avro();
            $("#MainContent_joiningDateBangla").avro();
            $("#MainContent_parmanentAddressBangla").avro();

            $("#MainContent_employeePic").on('change', function () {
                $("#employeePicPreview").html("");
                var regex = /^([a-zA-Z0-9\s_\\.\-:])+(.jpg|.jpeg|.gif|.png|.bmp)$/;
                if (regex.test($(this).val().toLowerCase())) {
                    if (typeof (FileReader) != "undefined") {
                        $("#employeePicPreview").show();
                        $("#employeePicPreview").append("<img />");
                        var reader = new FileReader();
                        reader.onload = function (e) {
                            $("#employeePicPreview img").attr("src", e.target.result);
                        }
                        reader.readAsDataURL($(this)[0].files[0]);
                    } else {
                        alert("This browser does not support image file format.");
                    }
                } else {
                    alert("Please upload a valid image file.");
                }
            });
            $("#MainContent_digitalSignature").on('change', function () {
                $("#employeeSignPreview").html("");
                var regex = /^([a-zA-Z0-9\s_\\.\-:])+(.jpg|.jpeg|.gif|.png|.bmp)$/;
                if (regex.test($(this).val().toLowerCase())) {
                    if (typeof (FileReader) != "undefined") {
                        $("#employeeSignPreview").show();
                        $("#employeeSignPreview").append("<img />");
                        var reader = new FileReader();
                        reader.onload = function (e) {
                            $("#employeeSignPreview img").attr("src", e.target.result);
                        }
                        reader.readAsDataURL($(this)[0].files[0]);
                    } else {
                        alert("This browser does not support image file format.");
                    }
                } else {
                    alert("Please upload a valid image file.");
                }
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
                        <h1 class="title">Employee Entry</h1>
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
                    
                    <div class="col-sm-12">
                        <div class="form-group col-sm-6">
                            <label class="col-sm-12 control-label" for="employeeId">Employee Id<b class="text-danger">*</b></label>
                            <div class="col-sm-6">
                                <input runat="server" class="form-control" type="text" id="employeeId" name="employeeId" placeholder="Employee Id" />
                                <asp:Button CssClass="btn btn-default" ID="empSearchButton" runat="server" Text="Search" formnovalidate OnClick="empSearchButton_OnClick_Click" CausesValidation="False" />
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-12">
                        <div class="form-group col-sm-6">
                            <label class="col-sm-12 control-label" for="name">Name<b class="text-danger">*</b></label>
                            <div class="col-sm-6">
                                <input runat="server" class="form-control" type="text" id="name" placeholder="Name" />
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-12">
                        <div class="form-group col-sm-6">
                            <label class="col-sm-12 control-label">Joining Date</label>
                            <div class="col-sm-6">
                                <input runat="server" class="form-control" type="text" id="joiningDate" name="joiningDate" placeholder="MM/DD/YYYY" />
                            </div>
                            <div runat="server" class="col-sm-6 messageContainer text-danger" id="joiningDateError" name="joiningDateError"></div>
                        </div>
                    </div>
                    <div class="col-sm-12">
                        <div class="form-group col-sm-6">
                            <label class="col-sm-12 control-label">Employee Catagory</label>
                            <div class="col-sm-6">
                                <asp:DropDownList CssClass="form-control" ID="catagoryDropDown" name="catagoryDropDown" runat="server">
                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Stuff" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Worker" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <%--<asp:RequiredFieldValidator CssClass="col-sm-9 text-danger" ID="employeeIdRequiredFieldValidator" runat="server" ErrorMessage="Can not be empty" ControlToValidate="employeeId"></asp:RequiredFieldValidator>--%>
                        </div>
                    </div>
                    <div class="col-sm-12">
                        <div class="form-group col-sm-6">
                            <label class="col-sm-12 control-label" for="dateOfBirth">Date Of Birth</label>
                            <div class="col-sm-6">
                                <input runat="server" class="form-control" type="text" id="dateOfBirth" name="dateOfBirth" placeholder="MM/DD/YYYY" />
                            </div>
                            <div runat="server" class="col-sm-6 messageContainer text-danger" name="dateOfBirthError" id="dateOfBirthError"></div>
                        </div>
                    </div>
                    <div class="col-sm-12">
                        <div class="form-group col-sm-6">
                            <label class="col-sm-12 control-label">Gender<b class="text-danger">*</b></label>
                            <div class="col-sm-6">
                                <asp:DropDownList CssClass="form-control" ID="gender" name="gender" runat="server">
                                    <asp:ListItem Text="Male" Value="male"></asp:ListItem>
                                    <asp:ListItem Text="Female" Value="female"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-sm-6 messageContainer text-danger" id="genderError"></div>
                        </div>
                        <div class="form-group col-sm-6">
                            <label class="col-sm-12 control-label">Blood Group</label>
                            <div class="col-sm-6">
                                <asp:DropDownList CssClass="form-control" ID="bloodGroup" name="bloodGroup" runat="server">
                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="A+" Value="A+"></asp:ListItem>
                                    <asp:ListItem Text="A-" Value="A-"></asp:ListItem>
                                    <asp:ListItem Text="B+" Value="B+"></asp:ListItem>
                                    <asp:ListItem Text="B-" Value="B-"></asp:ListItem>
                                    <asp:ListItem Text="O+" Value="o+"></asp:ListItem>
                                    <asp:ListItem Text="O-" Value="o-"></asp:ListItem>
                                    <asp:ListItem Text="AB+" Value="AB+"></asp:ListItem>
                                    <asp:ListItem Text="AB-" Value="AB-"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-sm-6 messageContainer text-danger" id="bloodGroupError"></div>
                        </div>
                    </div>
                <fieldset>
                    <legend>Basic Information</legend>
                    <div class="col-sm-12">
                        <div class="form-group col-sm-6">
                            <label class="col-sm-12 control-label" for="additionalId">Additional Id</label>
                            <div class="col-sm-6">
                                <input runat="server" class="form-control" type="text" id="additionalId" name="additionalId" placeholder="Additional Id" />
                            </div>
                            <div class="col-sm-6 messageContainer text-danger" id="additionalIdError"></div>
                        </div>
                        <div class="form-group col-sm-6">
                            <label class="col-sm-12 control-label" for="fathersName">Father's Name</label>
                            <div class="col-sm-6">
                                <input runat="server" class="form-control" type="text" id="fathersName" name="fathersName" placeholder="Father's Name" />
                            </div>
                            <div class="col-sm-6 messageContainer text-danger" id="fathersNameError"></div>
                        </div>
                    </div>
                    <div class="col-sm-12">
                        <div class="form-group col-sm-6">
                            <label class="col-sm-12 control-label" for="mothersName">Mother's Name </label>
                            <div class="col-sm-6">
                                <input runat="server" class="form-control" type="text" id="mothersName" name="mothersName" placeholder="Mother's Name " />
                            </div>
                            <div class="col-sm-6 messageContainer text-danger" id="mothersNameError"></div>
                        </div>
                    </div>
                    <div class="col-sm-12">

                        <div class="form-group col-sm-6">
                            <label class="col-sm-12 control-label">National IdCard/Birth Certificate No</label>
                            <div class="col-sm-6">
                                <input runat="server" class="form-control" type="text" id="nationalIdCardNo" name="nationalIdCardNo" placeholder="National Id Card No" />
                            </div>
                            <div class="col-sm-6 messageContainer text-danger" id="nationalIdCardNoError"></div>
                        </div>
                    </div>
                    <div class="col-sm-12">
                        <div class="form-group col-sm-6">
                            <label class="col-sm-12 control-label" for="presentAddress">Present Address</label>
                            <div class="col-sm-6">
                                <textarea runat="server" class="form-control" id="presentAddress" name="presentAddress"></textarea>
                            </div>
                            <div class="col-sm-6 messageContainer text-danger" id="presentAddressError"></div>
                        </div>
                        <div class="form-group col-sm-6">
                            <label class="col-sm-12 control-label" for="hobbies">Hobbies</label>
                            <div class="col-sm-6">
                                <input runat="server" class="form-control" type="text" id="hobbies" name="hobbies" placeholder="Hobbies" />
                            </div>
                            <div class="col-sm-6 messageContainer text-danger" id="hobbiesError"></div>
                        </div>
                    </div>
                    <div class="col-sm-12">
                        <div class="form-group col-sm-6">
                            <label class="col-sm-12 control-label" for="parmanentAddress">Parmanent Address</label>
                            <div class="col-sm-6">
                                <textarea runat="server" class="form-control" id="parmanentAddress" name="parmanentAddress"></textarea>
                            </div>
                            <div class="col-sm-6 messageContainer text-danger" id="parmanentAddressError"></div>
                        </div>
                        <div class="form-group col-sm-6">
                            <label class="col-sm-12 control-label" for="parmanentAddress">Parmanent Address (বাংলা)</label>
                            <div class="col-sm-6">
                                <textarea runat="server" class="form-control" id="parmanentAddressBangla" name="parmanentAddressBangla"></textarea>
                            </div>
                            <div class="col-sm-6 messageContainer text-danger" id="parmanentAddressBanglaError"></div>
                        </div>
                    </div>
                </fieldset>
                <fieldset>
                    <legend>Contact Information</legend>
                    <div class="col-sm-12">
                        <div class="form-group col-sm-6">
                            <label class="col-sm-12 control-label" for="officialPhone">Officeal Phone</label>
                            <div class="col-sm-6">
                                <input runat="server" class="form-control" type="text" id="officialPhone" name="officialPhone" placeholder="Official Phone" />
                            </div>
                            <div class="col-sm-6 messageContainer text-danger" id="officialPhoneError"></div>
                        </div>
                        <div class="form-group col-sm-6">
                            <label class="col-sm-12 control-label required" for="personalPhone">Personal Phone</label>
                            <div class="col-sm-6">
                                <input runat="server" class="form-control" type="text" id="personalPhone" name="personalPhone" placeholder="Personal Phone" />
                            </div>
                            <%--<asp:RequiredFieldValidator CssClass="col-sm-6 text-danger" ID="RequiredFieldValidator4" runat="server" ErrorMessage="Can not be empty" ControlToValidate="personalPhone"></asp:RequiredFieldValidator>--%>
                        </div>
                    </div>
                    <div class="col-sm-12">
                        <div class="form-group col-sm-6">
                            <label class="col-sm-12 control-label" for="officialEmail">Officeal Email</label>
                            <div class="col-sm-6">
                                <input runat="server" class="form-control" type="text" id="officialEmail" name="officialEmail" placeholder="Official Email" />
                            </div>
                            <div class="col-sm-6 messageContainer text-danger" id="officialEmailError"></div>
                        </div>
                        <div class="form-group col-sm-6">
                            <label class="col-sm-12 control-label" for="personalEmail">Personal Email</label>
                            <div class="col-sm-6">
                                <input runat="server" class="form-control" type="text" id="personalEmail" name="personalEmail" placeholder="Personal Email" />
                            </div>
                            <div class="col-sm-6 messageContainer text-danger" id="personalEmailError"></div>
                        </div>
                    </div>
                    <div class="col-sm-12">
                        <div class="form-group col-sm-6">
                            <label class="col-sm-12 control-label">User Id</label>
                            <div class="col-sm-6">
                                <input runat="server" class="form-control" type="text" id="userId" name="userId" placeholder="User Id" />
                            </div>
                            <div class="col-sm-6 messageContainer text-danger" id="userIdError"></div>
                        </div>
                        <div class="form-group col-sm-6">
                            <label class="col-sm-12 control-label" for="fingerPrintId">Finger Print Id</label>
                            <div class="col-sm-6">
                                <input runat="server" class="form-control" type="text" id="fingerPrintId" name="fingerPrintId" placeholder="Finger Print Id " />
                            </div>
                            <div class="col-sm-6 messageContainer text-danger" id="fingerPrintIdError"></div>
                        </div>
                    </div>
                    <div class="col-sm-12">
                        <div class="form-group col-sm-6">
                            <label class="col-sm-12 control-label">Attendence Card No</label>
                            <div class="col-sm-6">
                                <input runat="server" class="form-control" type="text" id="attandenceCardId" name="attandenceCardId" placeholder="Attendence Card No" />
                            </div>
                            <div class="col-sm-6 messageContainer text-danger" id="attandenceCardError"></div>
                        </div>
                        <div class="form-group col-sm-6">
                            <label class="col-sm-12 control-label">Attendence Card No(বাংলা)</label>
                            <div class="col-sm-6">
                                <input runat="server" class="form-control" type="text" id="attandenceCardIdBangla" name="attandenceCardIdBangla" placeholder="Attendence Card No (বাংলা)" />
                            </div>
                            <div class="col-sm-6 messageContainer text-danger" id="attandenceCardBanglaError"></div>
                        </div>
                    </div>
                    <div class="col-sm-12">
                        <div class="form-group col-sm-6">
                            <label class="col-sm-12 control-label">Employee Pic</label>
                            <div class="col-sm-6">
                                <asp:FileUpload ID="employeePic" name="employeePic" runat="server" />
                            </div>
                            <div class="col-sm-6 messageContainer text-danger" id="employeePicError"></div>
                        </div>
                        <div class="form-group col-sm-6">
                            <label class="col-sm-12 control-label">Digital Signature</label>
                            <div class="col-sm-6">
                                <asp:FileUpload ID="digitalSignature" name="digitalSignature" runat="server" />
                            </div>
                            <div class="col-sm-6 messageContainer text-danger" id="digitalSignatureError"></div>
                        </div>
                    </div>
                    <div class="col-sm-12">
                        <div class="col-sm-3">
                            <div id="employeePicPreview"></div>
                        </div>
                        <div class="col-sm-3"></div>
                        <div class="col-sm-3">
                            <div id="employeeSignPreview"></div>
                        </div>
                        <div class="col-sm-3"></div>
                    </div>
                    <div class="col-sm-12">
                        <div class="form-group col-sm-12">
                            <div class="col-sm-3"></div>
                            <div class="col-sm-2">
                                <asp:Button CssClass="btn btn-default btn-block" type="submit" ID="SaveEmployee" Text="Save" runat="server" OnClick="SaveEmployee_OnClickyee_Click" />
                            </div>
                            <div class="col-sm-7"></div>
                        </div>
                    </div>
                </fieldset>
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

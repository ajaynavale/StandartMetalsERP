/// <reference path="jquery-3.3.1.js" />


// Customer.js

$(document).ready(function () {

    //Hide Update button when page loads
    $("#btnUpdate").hide();

    //Customer Code should not be editable
    $("#txtCustCode").prop("readonly", true);

    //Set Cursor on Customer Name
    $("#txtCustName").focus();



    // Button Click Events

    $("#btnNew").click(function () {

        ClearForm();

    });


    $("#btnCancel").click(function () {

        //location.reload();

        ClearForm();

    });


});

// Clear Form


function ClearForm() {

    $("#hdnId").val("");

    $("#txtCustCode").val("");

    $("#txtCustName").val("");

    $("#txtContactPerson").val("");

    $("#ddlBranch").val("");

    $("#txtAddress1").val("");

    $("#txtAddress2").val("");

    $("#ddlCity").val("");

    $("#ddlState").val("");

    $("#txtPinCode").val("");

    $("#ddlCountry").val("");

    $("#txtEmailId").val("");

    $("#txtTelephone").val("");

    $("#txtMobile").val("");

    $("#txtFax").val("");

    $("#txtWebsite").val("");

    $("#txtGSTIN").val("");

    $("#txtRemarks").val("");

    //$("#btnSave").show();

    //$("#btnUpdate").hide();

    //$("#txtCustName").focus();


    // Button Setting

    SaveMode();

    $("#ddlSearchCustomer").val("");

    $("#txtCustName").focus();

}


// Save Button Click


$("#btnSave").click(function () {

    if (Validation() == false) {
        return false;
    }

    SaveCustomer();

});



// Save Customer


function SaveCustomer() {

    var customer = {

        CustName: $("#txtCustName").val(),

        ContactPerson: $("#txtContactPerson").val(),

        BranchId: $("#ddlBranch").val(),

        Address1: $("#txtAddress1").val(),

        Address2: $("#txtAddress2").val(),

        CityId: $("#ddlCity").val(),

        StateId: $("#ddlState").val(),

        PinCode: $("#txtPinCode").val(),

        CountryId: $("#ddlCountry").val(),

        EmailId: $("#txtEmailId").val(),

        Telephone: $("#txtTelephone").val(),

        Mobile: $("#txtMobile").val(),

        Fax: $("#txtFax").val(),

        WebSite: $("#txtWebsite").val(),

        GSTIN: $("#txtGSTIN").val(),

        Remarks: $("#txtRemarks").val()

    };

    $.ajax({

        url: "/Master/Save",

        type: "POST",

        data: customer,

        success: function (response) {

            if (response > 0) {

                alert("Customer Saved Successfully.");

                //ClearForm();

                //location.reload();

                ClearForm();

            }
            else {

                alert("Customer could not be saved.");

            }

        },

        error: function () {

            alert("Server Error.");

        }

    });

}


// Validation


function Validation() {

    if ($("#txtCustName").val() == "") {

        alert("Please Enter Customer Name.");

        $("#txtCustName").focus();

        return false;
    }


    if ($("#txtContactPerson").val() == "") {

        alert("Please Enter Contact Person.");

        $("#txtContactPerson").focus();

        return false;
    }


    if ($("#ddlBranch").val() == "") {

        alert("Please Select Branch.");

        $("#ddlBranch").focus();

        return false;
    }


    if ($("#ddlCity").val() == "") {

        alert("Please Select City.");

        $("#ddlCity").focus();

        return false;
    }


    if ($("#ddlState").val() == "") {

        alert("Please Select State.");

        $("#ddlState").focus();

        return false;
    }


    if ($("#ddlCountry").val() == "") {

        alert("Please Select Country.");

        $("#ddlCountry").focus();

        return false;
    }


    return true;

}


// Search Button


$("#btnSearch").click(function () {

    SearchCustomer();

});




// Search Customer


function SearchCustomer() {

    var id = $("#ddlSearchCustomer").val();

    if (id == "" || id == "0") {

        alert("Please Select Customer.");

        $("#ddlSearchCustomer").focus();

        return;

    }

    $.ajax({

        url: "/Master/SearchCustomer",

        type: "GET",

        data: { id: id },

        success: function (data) {

            FillCustomer(data);

        },

        error: function () {

            alert("Server Error.");

        }

    });

}


// Fill Controls


function FillCustomer(data) {

    $("#hdnId").val(data.Id);

    $("#txtCustCode").val(data.CustCode);

    $("#txtCustName").val(data.CustName);

    $("#txtContactPerson").val(data.ContactPerson);

    $("#ddlBranch").val(data.BranchId);

    $("#txtAddress1").val(data.Address1);

    $("#txtAddress2").val(data.Address2);

    $("#ddlCity").val(data.CityId);

    $("#ddlState").val(data.StateId);

    $("#txtPinCode").val(data.PinCode);

    $("#ddlCountry").val(data.CountryId);

    $("#txtEmailId").val(data.EmailId);

    $("#txtTelephone").val(data.Telephone);

    $("#txtMobile").val(data.Mobile);

    $("#txtFax").val(data.Fax);

    $("#txtWebsite").val(data.WebSite);

    $("#txtGSTIN").val(data.GSTIN);

    $("#txtRemarks").val(data.Remarks);



    // Button Settings


    //$("#btnSave").hide();

    //$("#btnUpdate").show();

    UpdateMode();

}


// Update Button


$("#btnUpdate").click(function () {

    if (Validation() == false) {
        return false;
    }

    UpdateCustomer();

});




// Update Customer


function UpdateCustomer() {

    var customer = {

        Id: $("#hdnId").val(),

        CustName: $("#txtCustName").val(),

        ContactPerson: $("#txtContactPerson").val(),

        BranchId: $("#ddlBranch").val(),

        Address1: $("#txtAddress1").val(),

        Address2: $("#txtAddress2").val(),

        CityId: $("#ddlCity").val(),

        StateId: $("#ddlState").val(),

        PinCode: $("#txtPinCode").val(),

        CountryId: $("#ddlCountry").val(),

        EmailId: $("#txtEmailId").val(),

        Telephone: $("#txtTelephone").val(),

        Mobile: $("#txtMobile").val(),

        Fax: $("#txtFax").val(),

        WebSite: $("#txtWebsite").val(),

        GSTIN: $("#txtGSTIN").val(),

        Remarks: $("#txtRemarks").val()

    };


    $.ajax({

        url: "/Master/UpdateCustomerMaster",

        type: "POST",

        data: customer,

        success: function (response) {

            if (response > 0) {

                alert("Customer Updated Successfully.");

                //ClearForm();

                //location.reload();

                ClearForm();

            }
            else {

                alert("Customer could not be updated.");

            }

        },

        error: function () {

            alert("Server Error.");

        }

    });

}


// Enable Save Mode


function SaveMode() {

    $("#btnSave").show();

    $("#btnUpdate").hide();

}




// Enable Update Mode


function UpdateMode() {

    $("#btnSave").hide();

    $("#btnUpdate").show();

}
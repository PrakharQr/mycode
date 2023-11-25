$(document).ready(function () {
    Bindddl();
    GetPrdData();
    $("#TblPrdMaster").hide();
});
function Bindddl() {
    $.ajax({
        type: "POST",
        url: "/ProductMaster/GetBrand",
        data: "{}",
        success: function(data) {
            var s = '<option value="-1">Please Select a Brand</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<option value="' + data[i].Value + '">' + data[i].Text + '</option>';
            }
            $("#DdlBrand").html(s);
        }
    });
}
function GetPrdData() {
    $.ajax({
        type: "Post",
        url: "../ProductMaster/GetDataBind",
        data: "{}",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var i = 0;
            var row = "<table class='table table-bordered' id='TblProduct'><colgroup><col width='2%'><col width='10%'><col width='40%'><col width='30%'><col width='20%'></colgroup><tr><th>S.No</th><th>Brand</th><th>Product</th><th>Product Color</th><th>Date</th><th>Action</th></tr>";
            $.each(data, function (index, item) {
                i++;
                row += "<tr><td>" + i + "</td><td> <span class='badge badge-warning''>" + item.BrandName + "</span></td><td>" + item.PrdName + "</td><td><div style='height:10px;background-color:" + item.Prdcolor + "'></div></td><td>" + item.CreatedDate + "</td><td> <button type='button' value=" + item.Id + " id='btndel' onclick='RemoveRows(" + item.Id + ");'><i class='fa fa-trash-o'></i></button></td></tr>";
            });
            $("#divprdlist").html(row);
        }
    });
}
function RemoveRows(id) {
    if (!confirm('Are you sure?')) {
        return false;
    }
    else {
        $.ajax({
            type: "Post",
            url: "../ProductMaster/DeleteProduct",
            data: "{Id:" + id + "}",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                swal('', "Deleted!", 'success');
                GetPrdData();
            }
        });
      
        return true;
    }
}
$("body").on("click", "#btnAdd", function () {
    var TxtPrdName = $("#TxtPrdName");
    var TxtPrdColor = $("#TxtPrdColor");
    var ddlBrand = $("#DdlBrand");
    var ddlBrandtxt = $("#DdlBrand option:selected");
    if (ddlBrand.val() == '-1') {
        swal('', "Please Choose a  Brand!", 'warning');
    }
    else if (TxtPrdName.val() == '') {
        swal('', "Please Enter Product Name!", 'warning');
    }
    else {
        var Sno = $("#TblPrdMaster TBODY tr").length;
        var tBody = $("#TblPrdMaster > TBODY")[0];
        var row = tBody.insertRow(-1);
        var cell = $(row.insertCell(-1));
        cell.html(Sno);
        var cell = $(row.insertCell(-1));
        cell.html("<input type='hidden' id='hbrandid' value=" + ddlBrand.val() + ">" + ddlBrandtxt.text());
        var cell = $(row.insertCell(-1));
        cell.html(TxtPrdName.val());
        cell = $(row.insertCell(-1));
        cell.html("<div  style=background-color:" + TxtPrdColor.val() + ";height:10px>'</div>" + "<input type='hidden' id='hprdcolor' value=" + TxtPrdColor.val() + ">");
        cell = $(row.insertCell(-1));
        var btnRemove = $("<input />");
        btnRemove.attr("type", "button");
        btnRemove.attr("onclick", "Remove(this);");
        btnRemove.val("Remove");
        cell.append(btnRemove);
        TxtPrdName.val("");
        TxtPrdColor.val("");
        $("#btnSave").show();
        $("#TblPrdMaster").show();
    }
});
function Remove(button) {
    var row = $(button).closest("TR");
    var name = $("TD", row).eq(0).html();
    if (confirm("Do you want to delete: " + name)) {
        var table = $("#TblPrdMaster")[0];
        table.deleteRow(row[0].rowIndex);
    }
};
$("body").on("click", "#btnSave", function () {
    var Prdtabledata = [];
    $("#TblPrdMaster TBODY TR").each(function () {
        if ($(this).find("td:eq(1)").text() != '') {
            var rowData = {
                Brandid: $(this).find("#hbrandid").val(),
                PrdName: $(this).find("td:eq(2)").text(),
                Prdcolor: $(this).find("#hprdcolor").val()
            };
            Prdtabledata.push(rowData);
        }
    });
    $.ajax({
        type: "POST",
        url: "../ProductMaster/AddProductMaster",
        data: JSON.stringify(Prdtabledata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            alert(" Record(s) inserted.");
            $("#TblPrdMaster tr td").detach();
            $("#btnSave").hide();
            GetPrdData();
            $("#TblPrdMaster").hide();
        }
    });
});

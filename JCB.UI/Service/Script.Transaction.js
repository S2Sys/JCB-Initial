//http://weblogs.asp.net/dotnetstories/archive/2011/09/14/using-jquery-ajax-functions-to-retrieve-data-from-the-server.aspx


$(function () {
    $(".productSelect").change(function () {
        var n = $(this).val();
        //var params = "{'id':"+n+", 'lname':’rocks’}",
        var params = "{'id':'" + n + "'}";

        $.ajax({
            type: "POST",
            url: "/Service/DataManager.asmx/GetProduct",
            data: params,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {

                var p = data.d;
                // $('.pname').addClass('test');
                $('.pg').val(p.ProductGroup.UniqueId);
                $('.punit').val(p.Unit.UniqueId);
                $('.prate').val(p.RetailerPrice);
                $('.pname').val(p.Name);

                //
                $(".pg option:contains(" + p.ProductGroup.UniqueId + ")").attr('selected', 'selected');
                var result = $(".pg option:selected").text();

            },
            error: OnErrorCall
        });
    });
    $("#addItem").click(function () {

        //        var params = "{'customerId':'"+n+"','branchId':'"+n+"','mode':'"+n+
        //                    "','unit':'"+$('.punit').val()+"','proGuid':'"+n+"','pg':'"+ $('.pg').val()+"','pid':'"+n+
        //                    "','qty':'"+n+"','price':'"+$('.prate').val()+"','disPer':'"+n+"','disRS'"+n+
        //                    "':'"+n+"','tax':'"+n+"','tranDate':'"+n + "'}";


        var x = new Object();
        x.Active = true;
        x.CreatedBy = 0;
        // x.DiscountPer = disPer,
        //            Branch = MetaDatas.Find(delegate(MetaData d) { return d.Id == branchId; }),
        //            VatTax = tax,
        //            Quantity = qty,
        //            DiscountRS = disRS,
        //            InvoiceNo = ctlInvoice.Text,
        //            ProductId = pid,
        //            ReorderLevel = "",
        //            Mode = MetaDatas.Find(delegate(MetaData d) { return d.UniqueId == new Guid(mode); }).Id,
        //            ProductGroup = MetaDatas.Find(delegate(MetaData d) { return d.UniqueId == new Guid(pg); }),
        //            Product = pc.GetProduct(new Guid(proGuid)),
        //            Rate = price,
        //            UserId = 1,
        //            TransactionUniqueId = TransactionId,
        //            // CustomerId = 0,
        //            //SupplierId = 0,
        //            TransactionOn = DateTime.Now,
        //            Unit = (float)MetaDatas.Find(delegate(MetaData d) { return d.UniqueId == new Guid(unit); }).Id,
        //            CreatedOn = tranDate 

        // var trans = { newTrans: x };

        var n = 20;
        var bid = 24;
        //var params = "{'customerId':'" + n + "', 'branchId':'" + bid + "'}";
        //var params = "{'id':'" + n + "'}";
        console.info(x);
//        $.ajax({
//            type: "POST",
//            url: "/CreateTransaction.aspx/CartItems",
//            data: x,
//            contentType: "application/json; charset=utf-8",
//            dataType: "json",
//            success: function (data) {

//                var p = data.d;
//                alert(p);
//                // $('.pname').addClass('test');
//                $('.pg').val(p.ProductGroup.UniqueId);
//                $('.punit').val(p.Unit.UniqueId);
//                $('.prate').val(p.RetailerPrice);
//                $('.pname').val(p.Name);

//                //
//                $(".pg option:contains(" + p.ProductGroup.UniqueId + ")").attr('selected', 'selected');
//                var result = $(".pg option:selected").text();
//                alert(result);
//            },
//            error: OnErrorCall
//        });
    });
});

function OnErrorCall(response) {
    alert(response.status + " " + response.statusText);
}



$(document).ready(function () {
    $('#addItem1').click(function () {
        //$.blockUI({ message: '<h1> Processing...</h1>' });
        var ControlName = "Service/SampleBind.ascx";
        $.ajax({
            type: "POST",
            url: "/Service/DataManager.asmx/RenderUserControl",
            data: "{controlName:'" + ControlName + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
               // $.unblockUI();
                $('#result').html(response.d);
            },
            failure: function (msg) {
                //$.unblockUI();
                $('#result').html(msg);
            }
        });
    });
});   

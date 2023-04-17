

$(document).ready(function () {
    $(".btn-table").click(function () {
        var tableNumber = $(this).data("table");
        console.log(tableNumber);
        $("#table-number").text(tableNumber);
        $("#numberOrder").val(tableNumber);
        $("#order-form").show();
    });
});            
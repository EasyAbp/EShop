$(function () {

    var l = abp.localization.getResource('EasyAbpEShopPayments');

    var service = easyAbp.eShop.payments.payments.PaymentItem;

    var dataTable = $('#PaymentItemTable').DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        autoWidth: false,
        scrollCollapse: true,
        order: [[1, "asc"]],
        ajax: abp.libs.datatables.createAjax(service.getList),
        columnDefs: [
            { data: "itemType" },
            { data: "itemKey" },
            { data: "currency" },
            { data: "originalPaymentAmount" },
            { data: "paymentDiscount" },
            { data: "actualPaymentAmount" },
            { data: "refundAmount" },
        ]
    }));
});
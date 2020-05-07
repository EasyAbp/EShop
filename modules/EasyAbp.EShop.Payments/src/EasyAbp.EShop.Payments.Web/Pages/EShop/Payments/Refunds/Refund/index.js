$(function () {

    var l = abp.localization.getResource('EasyAbpEShopPayments');

    var service = easyAbp.eShop.payments.refunds.refund;

    var dataTable = $('#RefundTable').DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        autoWidth: false,
        scrollCollapse: true,
        order: [[1, "asc"]],
        ajax: abp.libs.datatables.createAjax(service.getList),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l('Detail'),
                                action: function (data) {
                                }
                            },
                        ]
                }
            },
            { data: "storeId" },
            { data: "orderId" },
            { data: "refundPaymentMethod" },
            { data: "externalTradingCode" },
            { data: "currency" },
            { data: "refundAmount" },
            { data: "customerRemark" },
            { data: "staffRemark" },
        ]
    }));
});
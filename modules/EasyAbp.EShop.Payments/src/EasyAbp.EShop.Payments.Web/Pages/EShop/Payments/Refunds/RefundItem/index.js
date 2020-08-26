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
        ajax: function (requestData, callback, settings) {
            if (callback) {
                service.get(refundId).then(function (result) {
                    callback({
                        recordsTotal: result.refundItems.length,
                        recordsFiltered: result.refundItems.length,
                        data: result.refundItems
                    });
                });
            }
        },
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
            { data: "paymentItemId" },
            { data: "refundAmount" },
            { data: "customerRemark" },
            { data: "staffRemark" },
        ]
    }));
});
$(function () {

    var l = abp.localization.getResource('EasyAbpEShopPayments');

    var service = easyAbp.eShop.payments.payments.payment;

    var dataTable = $('#PaymentItemTable').DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        autoWidth: false,
        scrollCollapse: true,
        order: [[1, "asc"]],
        ajax: function (requestData, callback, settings) {
            if (callback) {
                service.get(paymentId).then(function (result) {
                    callback({
                        recordsTotal: result.paymentItems.length,
                        recordsFiltered: result.paymentItems.length,
                        data: result.paymentItems
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
                            }
                        ]
                }
            },
            { data: "storeId" },
            { data: "itemType" },
            { data: "itemKey" },
            { data: "originalPaymentAmount" },
            { data: "paymentDiscount" },
            { data: "actualPaymentAmount" },
            { data: "refundAmount" },
            { data: "pendingRefundAmount" },
        ]
    }));
});
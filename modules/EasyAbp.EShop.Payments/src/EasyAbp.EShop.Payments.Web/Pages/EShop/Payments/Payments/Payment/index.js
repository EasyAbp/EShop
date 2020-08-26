$(function () {

    var l = abp.localization.getResource('EasyAbpEShopPayments');

    var service = easyAbp.eShop.payments.payments.payment;

    var dataTable = $('#PaymentTable').DataTable(abp.libs.datatables.normalizeConfiguration({
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
                                text: l('PaymentItem'),
                                action: function (data) {
                                    document.location.href = document.location.origin + '/EShop/Payments/Payments/PaymentItem?PaymentId=' + data.record.id;
                                }
                            }
                        ]
                }
            },
            { data: "paymentMethod" },
            { data: "externalTradingCode" },
            { data: "currency" },
            { data: "originalPaymentAmount" },
            { data: "paymentDiscount" },
            { data: "actualPaymentAmount" },
            { data: "refundAmount" },
            { data: "pendingRefundAmount" },
            { data: "completionTime" },
            { data: "canceledTime" },
        ]
    }));
});
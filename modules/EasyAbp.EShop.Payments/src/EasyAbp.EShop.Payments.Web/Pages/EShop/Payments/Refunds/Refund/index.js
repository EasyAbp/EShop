$(function () {

    var l = abp.localization.getResource('EasyAbpEShopPayments');

    var service = easyAbp.eShop.payments.refunds.refund;
    var createModal = new abp.ModalManager(abp.appPath + 'EShop/Payments/Refunds/Refund/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'EShop/Payments/Refunds/Refund/EditModal');

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
            { data: "paymentId" },
            { data: "paymentItemId" },
            { data: "refundPaymentMethod" },
            { data: "externalTradingCode" },
            { data: "currency" },
            { data: "refundAmount" },
            { data: "customerRemark" },
            { data: "staffRemark" },
        ]
    }));

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewRefundButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});
$(function () {

    var l = abp.localization.getResource('EasyAbpEShopOrders');

    var service = easyAbp.eShop.orders.orders.order;

    var dataTable = $('#OrderTable').DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        autoWidth: false,
        scrollCollapse: true,
        order: [[1, "asc"]],
        ajax: abp.libs.datatables.createAjax(service.getList, function () {
            return { storeId: storeId }
        }),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l('Detail'),
                                action: function (data) {
                                    detailModal.open({ id: data.record.id });
                                }
                            }
                        ]
                }
            },
            { data: "orderNumber" },
            { data: "customerUserId" },
            {
                data: "orderStatus",
                render: function (data, type, row) {
                    if (data === 1) {
                        return '<span class="status-pending-text">' + l('OrderOrderStatusPending') + '</span>'
                    }
                    if (data === 2) {
                        return '<span class="status-processing-text">' + l('OrderOrderStatusProcessing') + '</span>'
                    }
                    if (data === 4) {
                        return '<span class="status-completed-text">' + l('OrderOrderStatusCompleted') + '</span>'
                    }
                    if (data === 8) {
                        return '<span class="status-canceled-text">' + l('OrderOrderStatusCanceled') + '</span>'
                    }
                    return ''
                }
            },
            { data: "currency" },
            { data: "actualTotalPrice" },
        ]
    }));
});
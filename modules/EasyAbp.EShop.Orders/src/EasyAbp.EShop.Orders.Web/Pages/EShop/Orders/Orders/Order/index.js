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
        ajax: abp.libs.datatables.createAjax(service.getList),
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
            { data: "orderStatus" },
            { data: "totalPrice" },
        ]
    }));
});
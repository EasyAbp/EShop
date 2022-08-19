$(function () {

    var l = abp.localization.getResource('EasyAbpEShopPluginsFlashSales');

    var service = easyAbp.eShop.plugins.flashSales.flashSaleResults.flashSaleResult;
    var viewModal = new abp.ModalManager(abp.appPath + 'EShop/Plugins/FlashSales/FlashSaleResults/FlashSaleResult/ViewModal');

    var dataTable = $('#FlashSaleResultTable').DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        autoWidth: false,
        scrollCollapse: true,
        order: [[6, "desc"]],
        ajax: abp.libs.datatables.createAjax(service.getList),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l('ViewFlashSaleResult'),
                                action: function (data) {
                                    viewModal.open({ id: data.record.id });
                                }
                            }
                        ]
                }
            },
            {
                title: l('FlashSaleResultStoreId'),
                data: "storeId"
            },
            {
                title: l('FlashSaleResultPlanId'),
                data: "planId"
            },
            {
                title: l('FlashSaleResultStatus'),
                data: "status",
                render: function (data) {
                    return l('Enum:FlashSaleResultStatus.' + data);
                }
            },
            {
                title: l('FlashSaleResultReason'),
                data: "reason"
            },
            {
                title: l('FlashSaleResultUserId'),
                data: "userId"
            },
            {
                title: l('FlashSaleResultOrderId'),
                data: "orderId"
            },
            {
                title: l('FlashSaleResultCreationTime'),
                data: "creationTime",
                dataFormat: 'datetime'
            },
            {
                title: l('FlashSaleResultReducedInventoryTime'),
                data: "reducedInventoryTime",
                dataFormat: 'datetime'
            },
        ]
    }));

    viewModal.onResult(function () {
        dataTable.ajax.reload();
    });
});

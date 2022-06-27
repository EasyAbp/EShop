$(function () {

    var l = abp.localization.getResource('EasyAbpEShopPluginsFlashSales');

    var service = easyAbp.eShop.plugins.flashSales.flashSalesPlans.flashSalesPlan;
    var viewModal = new abp.ModalManager(abp.appPath + 'EShop/Plugins/FlashSales/FlashSalesResults/FlashSalesResult/ViewModal');

    var dataTable = $('#FlashSalesResultTable').DataTable(abp.libs.datatables.normalizeConfiguration({
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
                                text: l('ViewFlashSalesPlan'),
                                action: function (data) {
                                    viewModal.open({ id: data.record.id });
                                }
                            }
                        ]
                }
            },
            {
                title: l('FlashSalesResultStoreId'),
                data: "storeId"
            },
            {
                title: l('FlashSalesResultPlanId'),
                data: "planId"
            },
            {
                title: l('FlashSalesResultStatus'),
                data: "status"
            },
            {
                title: l('FlashSalesResultUserId'),
                data: "userId"
            },
            {
                title: l('FlashSalesResultOrderId'),
                data: "orderId"
            },
            {
                title: l('FlashSalesResultCreationTime'),
                data: "creationTime",
                dataFormat: 'datetime'
            },
        ]
    }));

    viewModal.onResult(function () {
        dataTable.ajax.reload();
    });
});
$(function () {

    var l = abp.localization.getResource('EasyAbpEShopPluginsFlashSales');

    var service = easyAbp.eShop.plugins.flashSales.flashSalesPlans.flashSalesPlan;
    var createModal = new abp.ModalManager(abp.appPath + 'EShop/Plugins/FlashSales/FlashSalesPlans/FlashSalesPlan/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'EShop/Plugins/FlashSales/FlashSalesPlans/FlashSalesPlan/EditModal');

    var dataTable = $('#FlashSalesPlanTable').DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        autoWidth: false,
        scrollCollapse: true,
        order: [[2, "asc"]],
        ajax: abp.libs.datatables.createAjax(service.getList),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l('Edit'),
                                visible: abp.auth.isGranted('EasyAbp.EShop.Plugins.FlashSales.FlashSalesPlan.Update'),
                                action: function (data) {
                                    editModal.open({ id: data.record.id });
                                }
                            },
                            {
                                text: l('Delete'),
                                visible: abp.auth.isGranted('EasyAbp.EShop.Plugins.FlashSales.FlashSalesPlan.Delete'),
                                confirmMessage: function (data) {
                                    return l('FlashSalesPlanDeletionConfirmationMessage', data.record.id);
                                },
                                action: function (data) {
                                    service.delete(data.record.id)
                                        .then(function () {
                                            abp.notify.info(l('SuccessfullyDeleted'));
                                            dataTable.ajax.reload();
                                        });
                                }
                            }
                        ]
                }
            },
            {
                title: l('FlashSalesPlanStoreId'),
                data: "storeId"
            },
            {
                title: l('FlashSalesPlanBeginTime'),
                data: "beginTime"
            },
            {
                title: l('FlashSalesPlanEndTime'),
                data: "endTime"
            },
            {
                title: l('FlashSalesPlanProductId'),
                data: "productId"
            },
            {
                title: l('FlashSalesPlanProductSkuId'),
                data: "productSkuId"
            },
            {
                title: l('FlashSalesPlanIsPublished'),
                data: "isPublished",
                dataFormat: 'boolean'
            },
        ]
    }));

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewFlashSalesPlanButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});

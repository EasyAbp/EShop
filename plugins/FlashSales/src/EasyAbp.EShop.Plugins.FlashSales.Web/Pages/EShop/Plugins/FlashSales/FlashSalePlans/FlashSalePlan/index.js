$(function () {

    var l = abp.localization.getResource('EasyAbpEShopPluginsFlashSales');

    var service = easyAbp.eShop.plugins.flashSales.flashSalePlans.flashSalePlan;
    var createModal = new abp.ModalManager(abp.appPath + 'EShop/Plugins/FlashSales/FlashSalePlans/FlashSalePlan/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'EShop/Plugins/FlashSales/FlashSalePlans/FlashSalePlan/EditModal');

    var dataTable = $('#FlashSalePlanTable').DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        autoWidth: false,
        scrollCollapse: true,
        order: [[2, "asc"]],
        ajax: abp.libs.datatables.createAjax(service.getList, function () {
            return {
                includeUnpublished: abp.auth.isGranted('EasyAbp.EShop.Plugins.FlashSales.FlashSalePlan.Manage')
            };
        }),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l('Edit'),
                                visible: abp.auth.isGranted('EasyAbp.EShop.Plugins.FlashSales.FlashSalePlan.Update'),
                                action: function (data) {
                                    editModal.open({ id: data.record.id });
                                }
                            },
                            {
                                text: l('Delete'),
                                visible: abp.auth.isGranted('EasyAbp.EShop.Plugins.FlashSales.FlashSalePlan.Delete'),
                                confirmMessage: function (data) {
                                    return l('FlashSalePlanDeletionConfirmationMessage', data.record.id);
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
                title: l('FlashSalePlanStoreId'),
                data: "storeId"
            },
            {
                title: l('FlashSalePlanBeginTime'),
                data: "beginTime",
                dataFormat: 'datetime'
            },
            {
                title: l('FlashSalePlanEndTime'),
                data: "endTime",
                dataFormat: 'datetime'
            },
            {
                title: l('FlashSalePlanProductId'),
                data: "productId"
            },
            {
                title: l('FlashSalePlanProductSkuId'),
                data: "productSkuId"
            },
            {
                title: l('FlashSalePlanIsPublished'),
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

    $('#NewFlashSalePlanButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});

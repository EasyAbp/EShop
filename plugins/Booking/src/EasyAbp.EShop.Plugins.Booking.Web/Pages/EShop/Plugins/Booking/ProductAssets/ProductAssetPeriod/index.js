$(function () {

    var l = abp.localization.getResource('EasyAbpEShopPluginsBooking');

    var service = easyAbp.eShop.plugins.booking.productAssets.productAsset;
    var createModal = new abp.ModalManager(abp.appPath + 'EShop/Plugins/Booking/ProductAssets/ProductAssetPeriod/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'EShop/Plugins/Booking/ProductAssets/ProductAssetPeriod/EditModal');

    var dataTable = $('#ProductAssetPeriodTable').DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        autoWidth: false,
        scrollCollapse: true,
        order: [[0, "asc"]],
        ajax: function (requestData, callback, settings) {
            if (callback) {
                service.get(productAssetId).then(function (result) {
                    callback({
                        recordsTotal: result.periods.length,
                        recordsFiltered: result.periods.length,
                        data: result.periods
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
                                text: l('Edit'),
                                visible: abp.auth.isGranted('EasyAbp.EShop.Plugins.Booking.ProductAsset.Update'),
                                action: function (data) {
                                    editModal.open({ productAssetId: productAssetId, periodId: data.record.periodId });
                                }
                            },
                            {
                                text: l('Delete'),
                                visible: abp.auth.isGranted('EasyAbp.EShop.Plugins.Booking.ProductAsset.Update'),
                                confirmMessage: function (data) {
                                    return l('ProductAssetPeriodDeletionConfirmationMessage', data.record.periodId);
                                },
                                action: function (data) {
                                    service.deletePeriod(productAssetId, data.record.periodId)
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
                title: l('ProductAssetPeriodPeriodId'),
                data: "periodId"
            },
            {
                title: l('ProductAssetPeriodPrice'),
                data: "price"
            },
        ]
    }));

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewProductAssetPeriodButton').click(function (e) {
        e.preventDefault();
        createModal.open({ productAssetId: productAssetId });
    });
});

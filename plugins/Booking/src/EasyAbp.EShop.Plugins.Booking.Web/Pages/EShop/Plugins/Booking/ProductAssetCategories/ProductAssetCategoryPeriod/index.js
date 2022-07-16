$(function () {

    var l = abp.localization.getResource('EasyAbpEShopPluginsBooking');

    var service = easyAbp.eShop.plugins.booking.productAssetCategories.productAssetCategory;
    var createModal = new abp.ModalManager(abp.appPath + 'EShop/Plugins/Booking/ProductAssetCategories/ProductAssetCategoryPeriod/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'EShop/Plugins/Booking/ProductAssetCategories/ProductAssetCategoryPeriod/EditModal');

    var dataTable = $('#ProductAssetCategoryPeriodTable').DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        autoWidth: false,
        scrollCollapse: true,
        order: [[0, "asc"]],
        ajax: function (requestData, callback, settings) {
            if (callback) {
                service.get(productAssetCategoryId).then(function (result) {
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
                                visible: abp.auth.isGranted('EasyAbp.EShop.Plugins.Booking.ProductAssetCategory.Update'),
                                action: function (data) {
                                    editModal.open({ productAssetCategoryId: productAssetCategoryId, periodId: data.record.periodId });
                                }
                            },
                            {
                                text: l('Delete'),
                                visible: abp.auth.isGranted('EasyAbp.EShop.Plugins.Booking.ProductAssetCategory.Update'),
                                confirmMessage: function (data) {
                                    return l('ProductAssetCategoryPeriodDeletionConfirmationMessage', data.record.periodId);
                                },
                                action: function (data) {
                                    service.deletePeriod(productAssetCategoryId, data.record.periodId)
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
                title: l('ProductAssetCategoryPeriodPeriodId'),
                data: "periodId"
            },
            {
                title: l('ProductAssetCategoryPeriodCurrency'),
                data: "currency"
            },
            {
                title: l('ProductAssetCategoryPeriodPrice'),
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

    $('#NewProductAssetCategoryPeriodButton').click(function (e) {
        e.preventDefault();
        createModal.open({ productAssetCategoryId: productAssetCategoryId });
    });
});

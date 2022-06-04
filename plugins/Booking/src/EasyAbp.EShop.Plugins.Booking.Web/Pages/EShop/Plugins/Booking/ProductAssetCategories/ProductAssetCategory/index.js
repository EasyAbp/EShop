$(function () {

    var l = abp.localization.getResource('EasyAbpEShopPluginsBooking');

    var service = easyAbp.eShop.plugins.booking.productAssetCategories.productAssetCategory;
    var createModal = new abp.ModalManager(abp.appPath + 'EShop/Plugins/Booking/ProductAssetCategories/ProductAssetCategory/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'EShop/Plugins/Booking/ProductAssetCategories/ProductAssetCategory/EditModal');

    var dataTable = $('#ProductAssetCategoryTable').DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        autoWidth: false,
        scrollCollapse: true,
        order: [[0, "asc"]],
        ajax: abp.libs.datatables.createAjax(service.getList, function () {
            return { storeId: storeId }
        }),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l('ProductAssetCategoryPeriods'),
                                action: function (data) {
                                    document.location.href = abp.appPath + 'EShop/Plugins/Booking/ProductAssetCategories/ProductAssetCategoryPeriod?ProductAssetCategoryId=' + data.record.id;
                                }
                            },
                            {
                                text: l('Edit'),
                                visible: abp.auth.isGranted('EasyAbp.EShop.Plugins.Booking.ProductAssetCategory.Update'),
                                action: function (data) {
                                    editModal.open({ id: data.record.id });
                                }
                            },
                            {
                                text: l('Delete'),
                                visible: abp.auth.isGranted('EasyAbp.EShop.Plugins.Booking.ProductAssetCategory.Delete'),
                                confirmMessage: function (data) {
                                    return l('ProductAssetCategoryDeletionConfirmationMessage', data.record.id);
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
                title: l('ProductAssetCategoryAssetCategoryId'),
                data: "assetCategoryId"
            },
            {
                title: l('ProductAssetCategoryPrice'),
                data: "price"
            },
            {
                title: l('ProductAssetCategoryPeriodSchemeId'),
                data: "periodSchemeId"
            },
            {
                title: l('ProductAssetCategoryFromTime'),
                data: "fromTime"
            },
            {
                title: l('ProductAssetCategoryToTime'),
                data: "toTime"
            },
            {
                title: l('ProductAssetCategoryProductId'),
                data: "productId"
            },
            {
                title: l('ProductAssetCategoryProductSkuId'),
                data: "productSkuId"
            },
        ]
    }));

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewProductAssetCategoryButton').click(function (e) {
        e.preventDefault();
        createModal.open({ storeId: storeId });
    });

    $('#enter-button').click(function (e) {
        e.preventDefault();
        var storeId = $('#Filter_StoreId').val();

        document.location.href = document.location.origin + document.location.pathname + '?storeId=' + storeId;
    })
});

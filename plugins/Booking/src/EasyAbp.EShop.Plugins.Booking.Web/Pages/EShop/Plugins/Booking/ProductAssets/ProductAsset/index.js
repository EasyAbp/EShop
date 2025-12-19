$(function () {

    var l = abp.localization.getResource('EasyAbpEShopPluginsBooking');

    var service = easyAbp.eShop.plugins.booking.productAssets.productAsset;
    var createModal = new abp.ModalManager(abp.appPath + 'EShop/Plugins/Booking/ProductAssets/ProductAsset/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'EShop/Plugins/Booking/ProductAssets/ProductAsset/EditModal');

    var dataTable = $('#ProductAssetTable').DataTable(abp.libs.datatables.normalizeConfiguration({
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
                                text: l('ProductAssetPeriods'),
                                action: function (data) {
                                    document.location.href = abp.appPath + 'EShop/Plugins/Booking/ProductAssets/ProductAssetPeriod?ProductAssetId=' + data.record.id;
                                }
                            },
                            {
                                text: l('Edit'),
                                visible: abp.auth.isGranted('EasyAbp.EShop.Plugins.Booking.ProductAsset.Update'),
                                action: function (data) {
                                    editModal.open({ id: data.record.id });
                                }
                            },
                            {
                                text: l('Delete'),
                                visible: abp.auth.isGranted('EasyAbp.EShop.Plugins.Booking.ProductAsset.Delete'),
                                confirmMessage: function (data) {
                                    return l('ProductAssetDeletionConfirmationMessage', data.record.id);
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
                title: l('ProductAssetAssetId'),
                data: "assetId"
            },
            {
                title: l('ProductAssetCurrency'),
                data: "currency"
            },
            {
                title: l('ProductAssetPrice'),
                data: "price"
            },
            {
                title: l('ProductAssetPeriodSchemeId'),
                data: "periodSchemeId"
            },
            {
                title: l('ProductAssetFromTime'),
                data: "fromTime",
                dataFormat: 'datetime'
            },
            {
                title: l('ProductAssetToTime'),
                data: "toTime",
                dataFormat: 'datetime'
            },
            {
                title: l('ProductAssetProductId'),
                data: "productId"
            },
            {
                title: l('ProductAssetProductSkuId'),
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

    $('#NewProductAssetButton').click(function (e) {
        e.preventDefault();
        createModal.open({ storeId: storeId });
    });

    $('#enter-button').click(function (e) {
        e.preventDefault();
        var storeId = $('#Filter_StoreId').val();

        document.location.href = document.location.origin + document.location.pathname + '?storeId=' + storeId;
    })
});

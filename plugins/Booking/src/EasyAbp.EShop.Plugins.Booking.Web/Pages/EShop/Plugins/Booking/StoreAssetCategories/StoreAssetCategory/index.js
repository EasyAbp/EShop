$(function () {

    var l = abp.localization.getResource('EasyAbpEShopPluginsBooking');

    var service = easyAbp.eShop.plugins.booking.storeAssetCategories.storeAssetCategory;
    var createModal = new abp.ModalManager(abp.appPath + 'EShop/Plugins/Booking/StoreAssetCategories/StoreAssetCategory/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'EShop/Plugins/Booking/StoreAssetCategories/StoreAssetCategory/EditModal');

    var dataTable = $('#StoreAssetCategoryTable').DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        autoWidth: false,
        scrollCollapse: true,
        order: [[0, "asc"]],
        ajax: abp.libs.datatables.createAjax(service.getList),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l('Edit'),
                                visible: abp.auth.isGranted('EasyAbp.EShop.Plugins.Booking.StoreAssetCategory.Update'),
                                action: function (data) {
                                    editModal.open({ id: data.record.id });
                                }
                            },
                            {
                                text: l('Delete'),
                                visible: abp.auth.isGranted('EasyAbp.EShop.Plugins.Booking.StoreAssetCategory.Delete'),
                                confirmMessage: function (data) {
                                    return l('StoreAssetCategoryDeletionConfirmationMessage', data.record.id);
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
                title: l('StoreAssetCategoryStoreId'),
                data: "storeId"
            },
            {
                title: l('StoreAssetCategoryAssetCategoryId'),
                data: "assetCategoryId"
            },
        ]
    }));

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewStoreAssetCategoryButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});

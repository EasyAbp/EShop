$(function () {

    var l = abp.localization.getResource('EasyAbpEShopPluginsBooking');

    var service = easyAbp.eShop.plugins.booking.grantedStores.grantedStore;
    var createModal = new abp.ModalManager(abp.appPath + 'EShop/Plugins/Booking/GrantedStores/GrantedStore/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'EShop/Plugins/Booking/GrantedStores/GrantedStore/EditModal');

    var dataTable = $('#GrantedStoreTable').DataTable(abp.libs.datatables.normalizeConfiguration({
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
                                visible: abp.auth.isGranted('EasyAbp.EShop.Plugins.Booking.GrantedStore.Update'),
                                action: function (data) {
                                    editModal.open({ id: data.record.id });
                                }
                            },
                            {
                                text: l('Delete'),
                                visible: abp.auth.isGranted('EasyAbp.EShop.Plugins.Booking.GrantedStore.Delete'),
                                confirmMessage: function (data) {
                                    return l('GrantedStoreDeletionConfirmationMessage', data.record.id);
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
                title: l('GrantedStoreStoreId'),
                data: "storeId"
            },
            {
                title: l('GrantedStoreAssetId'),
                data: "assetId"
            },
            {
                title: l('GrantedStoreAssetCategoryId'),
                data: "assetCategoryId"
            },
            {
                title: l('GrantedStoreAllowAll'),
                data: "allowAll"
            },
        ]
    }));

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewGrantedStoreButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});

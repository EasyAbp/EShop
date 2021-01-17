$(function () {

    var l = abp.localization.getResource('EasyAbpEShopStores');

    var service = easyAbp.eShop.stores.storeOwners.storeOwner;
    var storeService = easyAbp.eShop.stores.stores.store;
    var createModal = new abp.ModalManager(abp.appPath + 'EShop/Stores/StoreOwners/StoreOwner/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'EShop/Stores/StoreOwners/StoreOwner/EditModal');

    var dataTable = $('#StoreOwnerTable').DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        autoWidth: false,
        scrollCollapse: true,
        order: [[1, "asc"]],
        ajax: abp.libs.datatables.createAjax(service.getList, function () {
            return {storeId: storeId}
        }),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l('Edit'),
                                action: function (data) {
                                    editModal.open({id: data.record.id});
                                }
                            },
                            {
                                text: l('Delete'),
                                confirmMessage: function (data) {
                                    return l('StoreDeletionConfirmationMessage', data.record.id);
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
                data: "storeId",
                render: function (data, type, row, meta) {
                    var currentCell = $("#StoreOwnerTable").DataTable().cells({"row":meta.row, "column":meta.col}).nodes(0);
                    storeService.get(data).then(
                        x => {
                            $(currentCell).html(x.name);
                        });
                    return '...';
                }
            },
            {
                data: "ownerUserName"
            }
        ]
    }));

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewStoreOwnerButton').click(function (e) {
        e.preventDefault();
        createModal.open({storeId: storeId});
    });
});
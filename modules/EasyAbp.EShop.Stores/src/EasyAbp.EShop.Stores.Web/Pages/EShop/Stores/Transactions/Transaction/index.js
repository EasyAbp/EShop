$(function () {

    var l = abp.localization.getResource('EasyAbpEShopStores');

    var service = easyAbp.eShop.stores.transactions.transaction;
    var createModal = new abp.ModalManager(abp.appPath + 'EShop/Stores/Transactions/Transaction/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'EShop/Stores/Transactions/Transaction/EditModal');

    var dataTable = $('#TransactionTable').DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        autoWidth: false,
        scrollCollapse: true,
        order: [[1, "asc"]],
        ajax: abp.libs.datatables.createAjax(service.getList, function () {
            return { storeId: storeId }
        }),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l('Edit'),
                                visible: abp.auth.isGranted('Stores.Transaction.Update'),
                                action: function (data) {
                                    editModal.open({ id: data.record.id });
                                }
                            },
                            {
                                text: l('Delete'),
                                visible: abp.auth.isGranted('Stores.Transaction.Delete'),
                                confirmMessage: function (data) {
                                    return l('TransactionDeletionConfirmationMessage', data.record.id);
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
            { data: "storeId" },
            { data: "orderId" },
            { data: "transactionType" },
            { data: "actionName" },
            { data: "currency" },
            { data: "amount" },
        ]
    }));

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewTransactionButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});
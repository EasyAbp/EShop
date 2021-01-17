$(function () {

    var l = abp.localization.getResource('EasyAbpEShopProducts');

    var service = easyAbp.eShop.products.products.product;
    var createModal = new abp.ModalManager(abp.appPath + 'EShop/Products/Products/Product/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'EShop/Products/Products/Product/EditModal');

    var dataTable = $('#ProductTable').DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        autoWidth: false,
        scrollCollapse: true,
        ajax: abp.libs.datatables.createAjax(service.getList, function () {
            return { storeId: storeId, categoryId: categoryId, showHidden: true }
        }),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l('ProductSku'),
                                action: function (data) {
                                    document.location.href = document.location.origin + '/EShop/Products/Products/ProductSku?ProductId=' + data.record.id;
                                }
                            },
                            {
                                text: l('Edit'),
                                action: function (data) {
                                    editModal.open({ id: data.record.id });
                                }
                            },
                            {
                                text: l('Delete'),
                                confirmMessage: function (data) {
                                    return l('ProductDeletionConfirmationMessage', data.record.id);
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
            { data: "productGroupDisplayName" },
            { data: "uniqueName" },
            { data: "displayName" },
            { data: "inventoryStrategy" },
            { data: "sold" },
            { data: "isPublished" },
        ]
    }));

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewProductButton').click(function (e) {
        e.preventDefault();
        createModal.open({ storeId: storeId, categoryId: categoryId });
    });

    $('#enter-button').click(function (e) {
        e.preventDefault();
        var storeId = $('#Filter_StoreId').val();
        var categoryId = $('#Filter_CategoryId').val();

        document.location.href = document.location.origin + document.location.pathname + '?storeId=' + storeId + "&categoryId=" + categoryId;
    })
});
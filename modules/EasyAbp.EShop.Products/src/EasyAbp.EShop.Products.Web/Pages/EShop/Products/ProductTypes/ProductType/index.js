$(function () {

    var l = abp.localization.getResource('EasyAbpEShopProducts');

    var service = easyAbp.eShop.products.productTypes.productType;
    var createModal = new abp.ModalManager(abp.appPath + 'EShop/Products/ProductTypes/ProductType/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'EShop/Products/ProductTypes/ProductType/EditModal');

    var dataTable = $('#ProductTypeTable').DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        autoWidth: false,
        scrollCollapse: true,
        order: [[1, "asc"]],
        ajax: abp.libs.datatables.createAjax(service.getList),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l('Edit'),
                                action: function (data) {
                                    editModal.open({ id: data.record.id });
                                }
                            },
                            {
                                text: l('Delete'),
                                confirmMessage: function (data) {
                                    return l('ProductTypeDeletionConfirmationMessage', data.record.id);
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
            { data: "name" },
            { data: "displayName" },
            { data: "description" },
            { data: "multiTenancySide" },
        ]
    }));

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewProductTypeButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});
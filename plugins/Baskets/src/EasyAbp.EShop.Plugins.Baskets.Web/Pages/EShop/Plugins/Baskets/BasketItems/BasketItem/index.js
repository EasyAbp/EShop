$(function () {

    var l = abp.localization.getResource('EShopPluginsBaskets');

    var service = easyAbp.eShop.plugins.baskets.basketItems.basketItem;
    var createModal = new abp.ModalManager(abp.appPath + 'EShop/Plugins/Baskets/BasketItems/BasketItem/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'EShop/Plugins/Baskets/BasketItems/BasketItem/EditModal');

    var dataTable = $('#BasketItemTable').DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        autoWidth: false,
        scrollCollapse: true,
        order: [[1, "asc"]],
        ajax: abp.libs.datatables.createAjax(service.getList, function () {
            return { basketName: basketName, userId: userId }
        }),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l('Edit'),
                                visible: abp.auth.isGranted('EasyAbp.EShop.Plugins.Baskets.BasketItem.Update'),
                                action: function (data) {
                                    editModal.open({ id: data.record.id });
                                }
                            },
                            {
                                text: l('Delete'),
                                visible: abp.auth.isGranted('EasyAbp.EShop.Plugins.Baskets.BasketItem.Delete'),
                                confirmMessage: function (data) {
                                    return l('BasketItemDeletionConfirmationMessage', data.record.id);
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
            { data: "basketName" },
            { data: "userId" },
            { data: "storeId" },
            { data: "productId" },
            { data: "productSkuId" },
            { data: "quantity" },
            { data: "mediaResources" },
            { data: "productUniqueName" },
            { data: "productDisplayName" },
            { data: "skuName" },
            { data: "skuDescription" },
            { data: "currency" },
            { data: "unitPrice" },
            { data: "totalPrice" },
            { data: "totalDiscount" },
            { data: "inventory" },
            { data: "isInvalid" },
        ]
    }));

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewBasketItemButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});
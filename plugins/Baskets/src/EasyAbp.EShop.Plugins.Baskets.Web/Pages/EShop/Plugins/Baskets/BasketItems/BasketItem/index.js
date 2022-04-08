$(function () {

    var localStorageItemKey = "EShopBasket:" + basketName;

    var l = abp.localization.getResource('EasyAbpEShopPluginsBaskets');
    
    var serverSide = abp.setting.getBoolean('EasyAbp.EShop.Plugins.Baskets.EnableServerSideBaskets')
        && abp.auth.isGranted('EasyAbp.EShop.Plugins.Baskets.BasketItem');

    var service = easyAbp.eShop.plugins.baskets.basketItems.basketItem;
    var createModal = new abp.ModalManager(abp.appPath + 'EShop/Plugins/Baskets/BasketItems/BasketItem/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'EShop/Plugins/Baskets/BasketItems/BasketItem/EditModal');

    var configuration = {
        processing: true,
        paging: true,
        searching: false,
        autoWidth: false,
        scrollCollapse: true,
        order: [[1, "asc"]],
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l('Edit'),
                                action: function (data) {
                                    editModal.open({ basketName: basketName, id: data.record.id });
                                }
                            },
                            {
                                text: l('Delete'),
                                confirmMessage: function (data) {
                                    return l('BasketItemDeletionConfirmationMessage', data.record.id);
                                },
                                action: function (data) {
                                    if (serverSide) {
                                        service.delete(data.record.id)
                                            .then(function () {
                                                abp.notify.info(l('SuccessfullyDeleted'));
                                                dataTable.ajax.reload();
                                            });
                                    } else {
                                        var cachedItems = JSON.parse(localStorage.getItem(localStorageItemKey)) || [];
                                        cachedItems.splice(cachedItems.findIndex(x => x.id === data.record.id), 1);
                                        localStorage.setItem(localStorageItemKey, JSON.stringify(cachedItems));
                                        location.reload();
                                    }
                                }
                            }
                        ]
                }
            },
            { data: "basketName" },
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
    };

    var dataTable;
    
    if (serverSide) {
        configuration.serverSide = true;
        configuration.ajax = abp.libs.datatables.createAjax(service.getList, function () {
            return { basketName: basketName, userId: userId }
        });
        
        var clientSideItems = JSON.parse(localStorage.getItem(localStorageItemKey)) || [];

        // Move client-side basket items to server-side.
        if (clientSideItems.length > 0) {
            localStorage.setItem(localStorageItemKey, JSON.stringify([]));
            createManyServerSideBasketItems(clientSideItems);
        }

        dataTable = $('#BasketItemTable').DataTable(abp.libs.datatables.normalizeConfiguration(configuration));
    } else {
        configuration.serverSide = false;
        var cachedItems = JSON.parse(localStorage.getItem(localStorageItemKey)) || [];
        if (cachedItems.length > 0) {
            service.generateClientSideData({ items: cachedItems }).then(function (result) {
                configuration.data = result.items
                dataTable = $('#BasketItemTable').DataTable(abp.libs.datatables.normalizeConfiguration(configuration));
            });
        } else {
            configuration.data = [];
            dataTable = $('#BasketItemTable').DataTable(abp.libs.datatables.normalizeConfiguration(configuration));
        }
    }

    createModal.onResult(function () {
        if (serverSide) {
            dataTable.ajax.reload();
        } else {
            location.reload();
        }
    });

    editModal.onResult(function () {
        if (serverSide) {
            dataTable.ajax.reload();
        } else {
            location.reload();
        }
    });
    
    function createManyServerSideBasketItems(items, autoReloadDataTable = true) {
        var item = items.shift();
        service.create(item, {
            success: function () {
                if (items.length > 0) {
                    createManyServerSideBasketItems(items);
                } else if (autoReloadDataTable) {
                    dataTable.ajax.reload();
                }
            }
        })
    }

    $('#NewBasketItemButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});
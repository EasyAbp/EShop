$(function () {

    var l = abp.localization.getResource('EasyAbpEShopProducts');

    var service = easyAbp.eShop.products.products.product;
    var createModal = new abp.ModalManager(abp.appPath + 'EShop/Products/Products/ProductSku/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'EShop/Products/Products/ProductSku/EditModal');
    var changeInventoryModal = new abp.ModalManager(abp.appPath + 'EShop/Products/Products/ProductSku/ChangeInventoryModal');

    var dataTable = $('#ProductSkuTable').DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        autoWidth: false,
        scrollCollapse: true,
        order: [[1, "asc"]],
        ajax: function (requestData, callback, settings) {
            if (callback) {
                service.get(productId, storeId).then(function (result) {
                    callback({
                        recordsTotal: result.productSkus.length,
                        recordsFiltered: result.productSkus.length,
                        data: fillProductSkusContentDescription(result).productSkus
                    });
                });
            }
        },
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l('Edit'),
                                visible: abp.auth.isGranted('EasyAbp.EShop.Products.Product.Update'),
                                action: function (data) {
                                    editModal.open({ productId: productId, productSkuId: data.record.id, storeId: storeId });
                                }
                            },
                            {
                                text: l('ProductInventory'),
                                visible: abp.auth.isGranted('EasyAbp.EShop.Products.ProductInventory.Update'),
                                action: function (data) {
                                    changeInventoryModal.open({ productId: productId, productSkuId: data.record.id, storeId: storeId });
                                }
                            },
                            {
                                text: l('Delete'),
                                visible: abp.auth.isGranted('EasyAbp.EShop.Products.Product.Delete'),
                                confirmMessage: function (data) {
                                    return l('ProductDeletionConfirmationMessage', data.record.id);
                                },
                                action: function (data) {
                                    service.deleteSku(productId, data.record.id, storeId)
                                        .then(function () {
                                            abp.notify.info(l('SuccessfullyDeleted'));
                                            dataTable.ajax.reload();
                                        });
                                }
                            }
                        ]
                }
            },
            { data: "code" },
            { data: "contentDescription" },
            { data: "price" },
            { data: "inventory" },
            { data: "sold" },
        ]
    }));

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    changeInventoryModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewProductSkuButton').click(function (e) {
        e.preventDefault();
        createModal.open({ storeId: storeId, productId: productId });
    });
    
    function fillProductSkusContentDescription(product) {
        let attributeOptionNames = [];
        for (let i in product.productAttributes) {
            let attribute = product.productAttributes[i];
            for (let j in attribute.productAttributeOptions) {
                let option = attribute.productAttributeOptions[j];
                attributeOptionNames[option.id] = option.displayName;
            }
        }
        for (let i in product.productSkus) {
            let sku = product.productSkus[i];
            let options = [];
            for (let j in sku.attributeOptionIds) {
                let optionId = sku.attributeOptionIds[j];
                options.push(attributeOptionNames[optionId]);
            }
            sku.contentDescription = options.join(',');
        }
        return product;
    }
});
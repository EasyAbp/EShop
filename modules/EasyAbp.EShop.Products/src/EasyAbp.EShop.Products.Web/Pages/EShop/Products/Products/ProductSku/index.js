$(function () {

    var l = abp.localization.getResource('EasyAbpEShopProducts');

    var service = easyAbp.eShop.products.products.product;
    var createModal = new abp.ModalManager(abp.appPath + 'EShop/Products/Products/ProductSku/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'EShop/Products/Products/ProductSku/EditModal');

    var dataTable = $('#ProductSkuTable').DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        autoWidth: false,
        scrollCollapse: true,
        order: [[1, "asc"]],
        ajax: function (requestData, callback, settings) {
            if (callback) {
                service.get(productId).then(function (result) {
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
                                action: function (data) {
                                    editModal.open({ productId: productId, productSkuId: data.record.id, storeId: storeId });
                                }
                            },
                            {
                                text: l('Delete'),
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
            { data: "contentDescription" },
            { data: "price" },
            { data: "inventory" },
        ]
    }));

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
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
            let attributeOptionIds = JSON.parse(sku.serializedAttributeOptionIds);
            for (let j in attributeOptionIds) {
                let optionId = attributeOptionIds[j];
                options.push(attributeOptionNames[optionId]);
            }
            sku.contentDescription = options.join(',');
        }
        return product;
    }
});
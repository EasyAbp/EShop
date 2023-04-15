$(function () {

    $("#PromotionFilter :input").on('input', function () {
        dataTable.ajax.reload();
    });

    $('#PromotionFilter div').addClass('col-sm-3').parent().addClass('row');

    var getFilter = function () {
        var input = {};
        $("#PromotionFilter")
            .serializeArray()
            .forEach(function (data) {
                if (data.value != '') {
                    input[abp.utils.toCamelCase(data.name.replace(/PromotionFilter./g, ''))] = data.value;
                }
            })
        return input;
    };

    var l = abp.localization.getResource('EasyAbpEShopPluginsPromotions');

    var service = easyAbp.eShop.plugins.promotions.promotions.promotion;
    var createModal = new abp.ModalManager(abp.appPath + 'EShop/Plugins/Promotions/Promotions/Promotion/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'EShop/Plugins/Promotions/Promotions/Promotion/EditModal');

    var dataTable = $('#PromotionTable').DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,//disable default searchbox
        autoWidth: false,
        scrollCollapse: true,
        order: [[0, "asc"]],
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
                                visible: abp.auth.isGranted('EasyAbp.EShop.Plugins.Promotions.Promotion.Update'),
                                action: function (data) {
                                    editModal.open({id: data.record.id});
                                }
                            },
                            {
                                text: l('Delete'),
                                visible: abp.auth.isGranted('EasyAbp.EShop.Plugins.Promotions.Promotion.Delete'),
                                confirmMessage: function (data) {
                                    return l('PromotionDeletionConfirmationMessage', data.record.id);
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
                title: l('PromotionStoreId'),
                data: "storeId"
            },
            {
                title: l('PromotionPromotionType'),
                data: "promotionType"
            },
            {
                title: l('PromotionUniqueName'),
                data: "uniqueName"
            },
            {
                title: l('PromotionDisplayName'),
                data: "displayName"
            },
            {
                title: l('PromotionFromTime'),
                data: "fromTime"
            },
            {
                title: l('PromotionToTime'),
                data: "toTime"
            },
            {
                title: l('PromotionDisabled'),
                data: "disabled"
            },
            {
                title: l('PromotionPriority'),
                data: "priority"
            },
        ]
    }));

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewPromotionButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
    $('.NewPromotionButton').click(function (e) {
        e.preventDefault();
        createModal.open({"ViewModel.storeId": storeId, "ViewModel.promotionType": $(this).attr('promotion-type')});
    });
});

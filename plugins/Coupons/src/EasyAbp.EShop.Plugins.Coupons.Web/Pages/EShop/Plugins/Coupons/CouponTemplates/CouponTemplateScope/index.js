$(function () {

    var l = abp.localization.getResource('EasyAbpEShopPluginsCoupons');

    var service = easyAbp.eShop.plugins.coupons.couponTemplates.couponTemplate;
    var createModal = new abp.ModalManager(abp.appPath + 'EShop/Plugins/Coupons/CouponTemplates/CouponTemplateScope/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'EShop/Plugins/Coupons/CouponTemplates/CouponTemplateScope/EditModal');

    var dataTable = $('#CouponTemplateScopeTable').DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        autoWidth: false,
        scrollCollapse: true,
        order: [[0, "asc"]],
        ajax: function (requestData, callback, settings) {
            if (callback) {
                service.get(couponTemplateId).then(function (result) {
                    callback({
                        recordsTotal: result.scopes.length,
                        recordsFiltered: result.scopes.length,
                        data: result.scopes
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
                                visible: abp.auth.isGranted('EasyAbp.EShop.Plugins.Coupons.CouponTemplate.Update'),
                                action: function (data) {
                                    editModal.open({ couponTemplateId: couponTemplateId, couponTemplateScopeId: data.record.id });
                                }
                            },
                            {
                                text: l('Delete'),
                                visible: abp.auth.isGranted('EasyAbp.EShop.Plugins.Coupons.CouponTemplate.Update'),
                                confirmMessage: function (data) {
                                    return l('CouponTemplateScopeDeletionConfirmationMessage', data.record.id);
                                },
                                action: function (data) {
                                    service.get(couponTemplateId).then(function (result) {
                                        var updateDto = result;
                                        for (var i in updateDto.scopes) {
                                            if (updateDto.scopes[i].id === data.record.id) {
                                                updateDto.scopes.splice(i, 1);
                                                service.update(couponTemplateId, updateDto)
                                                    .then(function () {
                                                        abp.notify.info(l('SuccessfullyDeleted'));
                                                        dataTable.ajax.reload();
                                                    });
                                                break;
                                            }
                                        }
                                    });
                                }
                            }
                        ]
                }
            },
            { data: "storeId" },
            { data: "productGroupName" },
            { data: "productId" },
            { data: "productSkuId" },
        ]
    }));

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewCouponTemplateScopeButton').click(function (e) {
        e.preventDefault();
        createModal.open({ couponTemplateId: couponTemplateId });
    });
});
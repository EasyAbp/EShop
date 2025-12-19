$(function () {

    var l = abp.localization.getResource('EasyAbpEShopPluginsCoupons');

    var service = easyAbp.eShop.plugins.coupons.couponTemplates.couponTemplate;
    var createModal = new abp.ModalManager(abp.appPath + 'EShop/Plugins/Coupons/CouponTemplates/CouponTemplate/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'EShop/Plugins/Coupons/CouponTemplates/CouponTemplate/EditModal');

    var dataTable = $('#CouponTemplateTable').DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        autoWidth: false,
        scrollCollapse: true,
        order: [[0, "asc"]],
        ajax: abp.libs.datatables.createAjax(service.getList),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l('CouponTemplateScope'),
                                visible: abp.auth.isGranted('EasyAbp.EShop.Plugins.Coupons.CouponTemplate.Update'),
                                action: function (data) {
                                    document.location.href = document.location.origin + '/EShop/Plugins/Coupons/CouponTemplates/CouponTemplateScope/?CouponTemplateId=' + data.record.id;
                                }
                            },
                            {
                                text: l('Edit'),
                                visible: abp.auth.isGranted('EasyAbp.EShop.Plugins.Coupons.CouponTemplate.Update'),
                                action: function (data) {
                                    editModal.open({ id: data.record.id });
                                }
                            },
                            {
                                text: l('Delete'),
                                visible: abp.auth.isGranted('EasyAbp.EShop.Plugins.Coupons.CouponTemplate.Delete'),
                                confirmMessage: function (data) {
                                    return l('CouponTemplateDeletionConfirmationMessage', data.record.id);
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
            { data: "couponType" },
            { data: "uniqueName" },
            { data: "displayName" },
            { data: "description" },
            { data: "usableDuration" },
            { data: "usableBeginTime", dataFormat: 'datetime' },
            { data: "usableEndTime", dataFormat: 'datetime' },
            { data: "conditionAmount" },
            { data: "discountAmount" },
            { data: "currency" },
            { data: "isUnscoped" },
        ]
    }));

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewCouponTemplateButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});
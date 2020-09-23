$(function () {

    var l = abp.localization.getResource('EasyAbpEShopPlugins');

    var service = easyAbp.eShop.plugins.coupons.coupons.coupon;
    var createModal = new abp.ModalManager(abp.appPath + 'EShop/Plugins/Coupons/Coupons/Coupon/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'EShop/Plugins/Coupons/Coupons/Coupon/EditModal');

    var dataTable = $('#CouponTable').DataTable(abp.libs.datatables.normalizeConfiguration({
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
                                text: l('Edit'),
                                visible: abp.auth.isGranted('EasyAbp.EShop.Plugins.Coupons.Coupon.Update'),
                                action: function (data) {
                                    editModal.open({ id: data.record.id });
                                }
                            },
                            {
                                text: l('Delete'),
                                visible: abp.auth.isGranted('EasyAbp.EShop.Plugins.Coupons.Coupon.Delete'),
                                confirmMessage: function (data) {
                                    return l('CouponDeletionConfirmationMessage', data.record.id);
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
            { data: "couponTemplateId" },
            { data: "userId" },
            { data: "orderId" },
            { data: "usableBeginTime" },
            { data: "usableEndTime" },
            { data: "usedTime" },
            { data: "discountedAmount" },
        ]
    }));

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewCouponButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});
$(function () {

    var l = abp.localization.getResource('StoreApproval');

    var service = easyAbp.eShop.plugins.storeApproval.storeApplications.storeApplication;
    var createModal = new abp.ModalManager(abp.appPath + 'StoreApproval/StoreApplications/StoreApplication/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'StoreApproval/StoreApplications/StoreApplication/EditModal');

    var dataTable = $('#StoreApplicationTable').DataTable(abp.libs.datatables.normalizeConfiguration({
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
                                visible: abp.auth.isGranted('StoreApproval.StoreApplication.Update'),
                                action: function (data) {
                                    editModal.open({ id: data.record.id });
                                }
                            },
                            {
                                text: l('Delete'),
                                visible: abp.auth.isGranted('StoreApproval.StoreApplication.Delete'),
                                confirmMessage: function (data) {
                                    return l('StoreApplicationDeletionConfirmationMessage', data.record.id);
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
            { data: "applicantId" },
            { data: "approvalStatus" },
            { data: "storeName" },
            { data: "businessCategory" },
            { data: "address" },
            { data: "unifiedCreditCode" },
            { data: "houseNumber" },
            { data: "businessLicenseImage" },
            { data: "name" },
            { data: "idNumber" },
            { data: "idCardFrontImage" },
            { data: "idCardBackImage" },
            { data: "storeImage" },
            { data: "note" },
        ]
    }));

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewStoreApplicationButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});
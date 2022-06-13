$(function () {
    const l = abp.localization.getResource('EasyAbpEShopEShopSample');
    const orderService = easyAbp.eShop.orders.orders.order;
    const eShopPaymentService = easyAbp.eShop.payments.payments.payment;
    const paymentService = easyAbp.paymentService.payments.payment;
    const accountService = easyAbp.paymentService.prepayment.accounts.account;

    $('#CreateOrderButton').click(function (e) {
        e.preventDefault();
        if (!cakeProductSkuId) return
        let btn = $(this)
        btn.buttonBusy(true);
        orderService.create({
            storeId: storeId,
            orderLines: [{
                productId: cakeProductId,
                productSkuId: cakeProductSkuId,
                quantity: 1
            }]
        }).then(function () {
            abp.message.success(l('OrderCreated'))
                .then(function () {
                    window.location.reload();
                })
        }).catch(function () {
            btn.buttonBusy(false);
        });
    });

    let redirectToOrderHistory = function () {
        document.location.href = document.location.origin + '?showOrderHistory=true'
    }

    $('#CancelOrderButton').click(function (e) {
        e.preventDefault();
        orderService.cancel(orderId, {})
            .then(function () {
                abp.message.success(l('OrderCanceled'))
                    .then(function () {
                        redirectToOrderHistory()
                    })
            });
    });

    $('#TopUpButton').click(function (e) {
        e.preventDefault();
        accountService.changeBalance(accountId, {changedBalance: 1.00})
            .then(function () {
                abp.message.success(l('TopUpSucceeded'))
                    .then(function () {
                        window.location.reload();
                    })
            });
    });

    $('#PayForOrderButton').click(function (e) {
        e.preventDefault();
        if (currentBalance < totalPrice) {
            abp.message.error(l('InsufficientBalance'))
                .then(function () {
                    window.location.reload();
                })
            return
        }
        eShopPaymentService.create({
            paymentMethod: "Prepayment",
            orderIds: [orderId]
        }).then(function () {
            getPaymentIdAndPay()
        });
    });

    let getPaymentIdAndPay = function () {
        orderService.get(orderId).then(function (order) {
            if (!order) return
            if (!order.paymentId) {
                setTimeout(getPaymentIdAndPay, 500)
                return
            }
            paymentService.pay(order.paymentId, {
                extraProperties: {
                    "AccountId": accountId
                }
            }).then(function () {
                abp.message.success(l('PaymentSucceeded'))
                    .then(function () {
                        redirectToOrderHistory()
                    })
            })
        })
    }

    let updateTimeToAutoCancel = function () {
        $('#timeToAutoCancel').text(new Date(secondsToAutoCancel * 1000).toISOString().substring(11, 19))
    }

    let cancelCurrentPayment = function () {
        if (!orderId) return
        orderService.get(orderId).then(function (order) {
            if (order.paymentId) {
                paymentService.cancel(order.paymentId)
            }
        })
    }

    function tryGoToOrderHistoryTab() {
        if (location.search.indexOf('showOrderHistory=true') < 0) return
        $('#OrderHistoryTab-tab').tab("show")
        history.pushState(null, null, location.origin)
        // document.location.href = document.location.href.split('#')[0]
    }

    let init = function () {
        tryGoToOrderHistoryTab()
        updateTimeToAutoCancel()
        cancelCurrentPayment()
    }

    init()

    let interval = setInterval(function () {
        if (secondsToAutoCancel <= 0) {
            clearInterval(interval)
            return
        }
        secondsToAutoCancel--
        updateTimeToAutoCancel()
    }, 1000);
});
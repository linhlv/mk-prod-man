(function () {
    'use strict';

    var controllerId = 'quotationController';

    window.app.controller(controllerId, ctrl);

    ctrl.$inject = ['$scope', 'quotationSvc', 'alerts'];

    function ctrl($scope, quotationSvc, alerts) {
        var vm = this;
        vm.quotation = {
            companyName: '',
            rateOfExchange: 21000,
            total: function () {
                var t = 0;
                if (vm.quotation.quotationItems && vm.quotation.quotationItems.length) {
                    for (var i = 0; i < vm.quotation.quotationItems.length; i++) {
                        t += (vm.quotation.quotationItems[i].price * vm.quotation.quotationItems[i].rate);
                    }

                    return t;
                }

                return 0;
            }
        }
        
        vm.isLoading = true;
        vm.isSaving = true;
        vm.isRemoving = true;

        vm.remove = remove;
        vm.save = save;
        vm.exportExcel = exportExcel;
        vm.hasQuantity = false;

        allQuotation();

        vm.recalculate = function (qi) {
            //Price FOB = (Price + (Price x (Rate/100)))/Rate Of Exchange
            qi.priceFOB = (qi.price + (qi.price * (qi.rate / 100))) / vm.quotation.rateOfExchange;
            //
            qi.packingCBM = (qi.cartonMeasurementW * qi.cartonMeasurementD * qi.cartonMeasurementH) / 1000000;
            qi.cartonQuantity = qi.quantity / qi.packingPCSSE;
            qi.amount = qi.quantity * qi.priceFOB;
            qi.totalCBM = qi.cartonQuantity * qi.packingCBM;
            vm.quotation.totalOrderQuantity = 0;
            vm.quotation.totalCartonQuantity = 0;
            vm.quotation.totalCBM = 0;
            vm.quotation.totalAmount = 0;
            _.each(vm.quotation.quotationItems, function(item, idx) {
                vm.quotation.totalOrderQuantity += parseFloat(item.quantity);
                vm.quotation.totalCartonQuantity +=parseFloat( item.cartonQuantity);
                vm.quotation.totalCBM += parseFloat(item.totalCBM);
                vm.quotation.totalAmount += parseFloat(item.amount);
            });
        }


        function allQuotation() {
            vm.isLoading = true;
            quotationSvc.allQuotation().then(function (data) {
                if (!data.hasQuantity) {
                    vm.quotation.hasQuantity = false;
                } else {
                    vm.quotation.hasQuantity = data.hasQuantity;
                }
                vm.quotation.companyName = data.companyName;
                vm.quotation.attn = data.attn;
                vm.quotation.quotationDate = new Date( data.quotationDate);
                vm.quotation.rateOfExchange = data.rateOfExchange;
                vm.quotation.quotationItems = data.quotationItems;
                _.each(data.quotationItems, function (item, idx) {
                    if (!vm.quotation.quotationItems[idx].quantity) {
                        vm.quotation.quotationItems[idx].quantity = 1;
                    }
                    vm.recalculate(vm.quotation.quotationItems[idx]);
                });
                vm.isLoading = false;
                vm.hasQuantity = vm.quotation.hasQuantity;
            }, function (error) {
                vm.isLoading = false;
            });
        }

       
        function save() {
            vm.isSaving = true;
            vm.quotation.hasQuantity = vm.hasQuantity;
            quotationSvc.save(vm.quotation).then(function (data) {
                alerts.success("Quotation saved!");
                vm.isSaving = false;
            }, function (error) {
                vm.isSaving = false;
            });
        }

        function remove(quotationItem) {
            vm.isRemoving = true;
            quotationSvc.removeQuotationItemByProductId(quotationItem).then(function (data) {
                vm.quotation.quotationItems = _.without(
                               vm.quotation.quotationItems,
                               _.findWhere(vm.quotation.quotationItems, { itemId: data.itemId })
                           );

                alerts.success("Quotation item is removed!");

                vm.isRemoving = false;
            }, function (error) {
                vm.isRemoving = false;
            });
        }

        function exportExcel() {
            quotationSvc.exportExcel().then(function (data) {
                console.log(data);
                alerts.success("Quotation exported to excel!");
                vm.isSaving = false;
            }, function (error) {
                vm.isSaving = false;
            });
        }
    }
})();


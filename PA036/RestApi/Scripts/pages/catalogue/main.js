var myApp = angular.module('myApp', ['ng-admin']);

myApp.config(['NgAdminConfigurationProvider', function (NgAdminConfigurationProvider) {
    var nga = NgAdminConfigurationProvider;
    var admin = nga.application('Catalogue')
        .baseApiUrl('http://localhost:50455/api/'); // main API endpoint
    
    
    var products = nga.entity("Products")
        .identifier(nga.field('id'));
    var name = nga.field('name')
        .isDetailLink(true)
        .label('Name');
    var stockCount = nga.field('stockCount', 'number')
        .label('Count in stock');
    var unitCost = nga.field('unitCost', 'float')
        .label('Cost per unit');

    products.listView().fields([
        nga.field('id'),
        name,
        stockCount,
        unitCost
    ]).listActions(['edit', 'delete'])
        .sortDir("ASC")
        .filters([
        nga.field('name')
            .label('This will be filter on name')
            .pinned(true)
    ]).perPage(50);

    products.editionView().fields([
        name,
        stockCount,
        unitCost
    ]);

    products.creationView().fields([
        name,
        stockCount,
        unitCost
    ]);

    admin.addEntity(products);
    nga.configure(admin);
}]);

// override default api mapping
myApp.config(['RestangularProvider', function (RestangularProvider) {
    
    RestangularProvider.addFullRequestInterceptor(function (element, operation, what, url, headers, params, httpConfig) {
        if (operation === 'getList') {

            // spread filters
            if (params._filters) {
                for (var filter in params._filters) {
                    var f = firstLetterToLower(filter);
                    params[f] = params._filters[filter];
                }
                delete params._filters;
            }

            // sorting
            params['sortField'] = params._sortField || 'id';
            delete params._sortField;
            params['sortDir'] = params._sortDir || 'ASC';
            delete params._sortDir;

            // pagination
            params['page'] = params._page || 1;
            params['perPage']= params._perPage || 50;
            delete params._page;
            delete params._perPage;
        }
        return { params: params };
    });
}]);


function firstLetterToLower(string) {
    if (!string || !string.length) {
        return;
    }

    return string.charAt(0).toLowerCase() + string.slice(1);
}
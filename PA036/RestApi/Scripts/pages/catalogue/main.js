var myApp = angular.module('myApp', ['ng-admin']);

myApp.config(['NgAdminConfigurationProvider', function (NgAdminConfigurationProvider) {
    var nga = NgAdminConfigurationProvider;
    var admin = nga.application('Catalogue')
        .baseApiUrl('http://localhost:50455/api/'); // main API endpoint
    
    var products = nga.entity("Products")
        .identifier(nga.field('id'));

    var store = nga.entity('Stores')
        .identifier(nga.field('id'));


    storeSettings(store, nga);
    admin.addEntity(store);

    productsSettings(products, nga, admin);

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

function productsSettings(products, nga, admin) {
    var name = nga.field('name')
    .isDetailLink(true)
    .label('Name');
    var stockCount = nga.field('stockCount', 'number')
        .label('Count in stock');
    var unitCost = nga.field('unitCost', 'float')
        .label('Cost per unit');
    var storeReference = nga.field('storeId', 'reference')
        .targetEntity(admin.getEntity('Stores'))
        .targetField(
            nga.field('name')
        )
        .isDetailLink(true)
        .label('Store');
    products.listView().fields([
        nga.field('id'),
        name,
        stockCount,
        unitCost,
        storeReference
    ]).listActions(['edit', 'delete'])
        .sortDir("ASC")
        .filters([
        nga.field('name')
            .label('Name')
            .pinned(true)
        ]).perPage(50);

    products.editionView().fields([
        name,
        stockCount,
        unitCost,
        storeReference
    ]);

    products.creationView().fields([
        name,
        stockCount,
        unitCost,
        storeReference
    ]);
}
function storeSettings(store, nga) {
    var name = nga.field('name')
        .isDetailLink(true)
        .label('Name');
    var city = nga.field('city')
        .label('City');
    var state = nga.field('state')
        .label('State');

    store.listView().fields([
        nga.field('id'),
        name,
        city,
        state
    ]).listActions(['edit', 'delete'])
        .sortDir("ASC")
        .filters([
        nga.field('name')
            .label('Name')
            .pinned(true)
        ]).perPage(50);

    store.editionView().fields([
        name,
        city,
        state
    ]);

    store.creationView().fields([
        name,
        city,
        state
    ]);
}
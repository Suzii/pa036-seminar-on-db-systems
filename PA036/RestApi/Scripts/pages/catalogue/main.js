var myApp = angular.module('myApp', ['ng-admin']);
myApp.config(['NgAdminConfigurationProvider', function (NgAdminConfigurationProvider) {
    var nga = NgAdminConfigurationProvider;
    var admin = nga.application('Vyjebany katalog (v anglictine: Fucking Catalogue)')
        .baseApiUrl('http://localhost:50455/api/'); // main API endpoint
    
    
    var products = nga.entity("Products")
        .identifier(nga.field('Id'));
    var name = nga.field('Name')
        .isDetailLink(true)
        .label('Name');
    var stockCount = nga.field('StockCount', 'number')
        .label('Count in stock');
    var unitCost = nga.field('UnitCost', 'float')
        .label('Cost per unit');

    products.listView().fields([
        nga.field('Id'),
        name,
        stockCount,
        unitCost,
    ]).listActions(['edit', 'delete'])
        .sortDir("ASC")
        .filters([
        nga.field('Name')
            .label('This will be filter on name')
            .pinned(true),
    ]).perPage(50);
    products.editionView().fields([
        name,
        stockCount,
        unitCost,
    ])
    products.creationView().fields([
        name,
        stockCount,
        unitCost,
    ])
    admin.addEntity(products)
    nga.configure(admin);
}]);
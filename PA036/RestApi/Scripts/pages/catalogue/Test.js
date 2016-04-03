$(document).ready(function () {
    $.getJSON('api/products', function(data) {
        window.alert('Products loaded, check out the console');
        console.log('GET to products controller: ', data);
    });    
});
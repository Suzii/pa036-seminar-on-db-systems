$(document).ready(function () {
    $.getJSON('api/products', function(data) {
        window.alert('Products loaded: ' + data);
        console.log('GET to products controller: ', data);
    });    
});
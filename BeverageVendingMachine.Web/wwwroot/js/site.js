// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

window.addEventListener("load", (event) => {
    initData();
});

function initData() {
    $.ajax({
        url: '/api/TerminalApi/GetCoins',
        type: 'get',
        success: function (coins) {
            console.log('initCoins', coins);
            initCoins(coins);

            $.ajax({
                url: '/api/TerminalApi/GetStorageItems',
                type: 'get',
                success: function (products) {
                    console.log('initProducts', products);
                    initProducts(products);
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert("error" + XMLHttpRequest.responseText);
                }
            })
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("error" + XMLHttpRequest.responseText);
        }
    })
}
function initCoins(coins) {
    if (!coins) return;

    var htmlCoinsList = document.getElementById('coins-list');
    coins.forEach(function (coin) {
        console.log('coin', coin);
        var htmlCoin = document.createElement('li');
        htmlCoin.className = 'coins-list-item coin';
        htmlCoin.innerHTML = coin.value;
        htmlCoinsList.appendChild(htmlCoin);
    });
}
function initProducts(products) {
    if (!products) return;

    var htmlProductsList = document.getElementById('products-list');
    htmlProductsList.innerHTML = '';
    products.forEach(function (product) {
        addProductToPage(htmlProductsList, product);
    });

    var leftStorageSpace = 8 - products.length;
    if (leftStorageSpace > 0) {
        for (var x = 0; x < leftStorageSpace; x++) {
            addProductToPage(htmlProductsList);
        }
    }
}
function addProductToPage(htmlProductsList, product) {
    console.log('addProductToPage', htmlProductsList, product);
    var htmlProductLi = document.createElement('li');
    htmlProductLi.className = 'products-list-item';
    htmlProductLi.id = product ? 'products-list-item__' + product.id : 'products-list-item__default';

    var htmlProductImg = document.createElement('img');
    htmlProductImg.className = 'products-list-item__image';
    htmlProductImg.src = product ? product.imageUrl : 'https://nato.cdnartwhere.eu/cdn/ff/oca4fwSi7ZMflFF5-LRcenPXoZTDpZSTkwLZEvZtQIw/1607780582/public/default_images/default-image.jpg';
    htmlProductImg.alt = 'product image';
    htmlProductImg.draggable = false;

    var htmlProductTitle = document.createElement('span');
    htmlProductTitle.className = 'product-text-color products-list-item__title';
    htmlProductTitle.innerHTML = product ? product.name : 'Product name';

    var htmlProductCost = document.createElement('span');
    htmlProductCost.className = 'product-text-color products-list-item__cost';
    htmlProductCost.innerHTML = product ? product.cost + ' ₽' : 'Cost';

    var htmlProductAmount = document.createElement('span');
    htmlProductAmount.className = 'product-text-color products-list-item__amount';
    htmlProductAmount.innerHTML = product ? 'x' + product.storageQuantity : 'Quantity';

    htmlProductLi.appendChild(htmlProductImg);
    htmlProductLi.appendChild(htmlProductTitle);
    htmlProductLi.appendChild(htmlProductCost);
    htmlProductLi.appendChild(htmlProductAmount);
    htmlProductsList.appendChild(htmlProductLi);
}



function makePurchase() {
    console.log('makePurchase');
}
function releaseChange() {
    console.log('releaseChange');
}
function makePurchaseAndReleaseChange() {
    console.log('makePurchaseAndReleaseChange');
}
function importItems(){
    console.log('importItems');
}
function addNewItem() {

}
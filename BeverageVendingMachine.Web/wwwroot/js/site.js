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
            initCoins(coins);

            $.ajax({
                url: '/api/TerminalApi/GetStorageItems',
                type: 'get',
                success: function (products) {
                    initProducts(products);
                    initUserButtons();
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
    htmlCoinsList.innerHTML = '';
    coins.forEach(function (coin) {
        var htmlCoin = document.createElement('li');
        htmlCoin.className = 'coins-list-item coin';
        htmlCoin.innerHTML = coin.value;
        htmlCoin.onclick = function () { depositCoin(coin) };
        htmlCoinsList.appendChild(htmlCoin);
    });
}
function initProducts(products) {
    if (!products) return;

    var htmlProductsList = document.getElementById('products-list');
    htmlProductsList.innerHTML = '';
    products.forEach(function (product) {
        addProductHtmlToPage(htmlProductsList, product);
    });

    var leftStorageSpace = 8 - products.length;
    if (leftStorageSpace > 0) {
        for (var x = 0; x < leftStorageSpace; x++) {
            addProductHtmlToPage(htmlProductsList);
        }
    }
}
function addProductHtmlToPage(htmlProductsList, product) {
    var htmlProductLi = document.createElement('li');
    htmlProductLi.className = 'products-list-item';
    if (product) {
        if (product.isSelected) htmlProductLi.classList.add('selected');
        else htmlProductLi.classList.remove('selected');
    }
    htmlProductLi.id = product ? 'products-list-item__' + product.id : 'products-list-item__default';
    htmlProductLi.onclick = function () {
        if (product) {
            if (product.isSelected) unselectPurchaseItem(product);
            else selectPurchaseItem(product);
        }
    };

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
    htmlProductCost.innerHTML = product ? product.cost : 'Cost';// + ' ₽'

    var htmlProductAmount = document.createElement('span');
    htmlProductAmount.className = 'product-text-color products-list-item__amount';
    console.log(product);
    htmlProductAmount.innerHTML = product ? 'x' + product.storageQuantity : 'Quantity';

    htmlProductLi.appendChild(htmlProductImg);
    htmlProductLi.appendChild(htmlProductTitle);
    htmlProductLi.appendChild(htmlProductCost);
    htmlProductLi.appendChild(htmlProductAmount);
    htmlProductsList.appendChild(htmlProductLi);
}
function initUserButtons() {
    var htmlProductsList = document.getElementById('products-list');
    var selectedProduct = htmlProductsList.querySelector('.products-list-item.selected');
    var depositedAmount = document.getElementById('coins-info__deposited-amount-value').innerHTML;
    var changeAmount = document.getElementById('coins-info__change-amount-value').innerHTML;
    console.log('depositedAmount, changeAmount, productPrice, productAmount', depositedAmount, changeAmount, productPrice, productAmount);

    if (parseInt(changeAmount) > 0) document.getElementById('interface-button__release-change').classList.add('visible');
    if (selectedProduct) {
        var productPrice = selectedProduct.querySelector('.products-list-item__cost').innerHTML;
        var productAmount = selectedProduct.querySelector('.products-list-item__amount').innerHTML.substring(1);

        if (parseInt(depositedAmount) > parseInt(productPrice)) {
            document.getElementById('interface-button__make-purchase').classList.add('visible');
            document.getElementById('interface-button__make-purchase-and-release-change').classList.add('visible');
        }
    }
}
function hideInterfaceButtons() {
    document.querySelectorAll('.interface-button').forEach(function (button) { button.classList.remove('visible'); });
}

function makeAjaxRequestAndUpdate(url, data) {
    $.ajax({
        type: "POST",
        url: url,
        data: data,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (updateData) {
            handleUpdateData(updateData);
            initUserButtons();
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("error" + XMLHttpRequest.responseText);
        }
    });
}
function depositCoin(coin) {
    hideInterfaceButtons();
    makeAjaxRequestAndUpdate("api/TerminalApi/depositCoin", JSON.stringify(coin.id));
}
function selectPurchaseItem(product) {
    hideInterfaceButtons();
    makeAjaxRequestAndUpdate("api/TerminalApi/selectPurchaseItem", JSON.stringify(product.id));
}
function unselectPurchaseItem() {
    hideInterfaceButtons();
    makeAjaxRequestAndUpdate("api/TerminalApi/unselectPurchaseItem");
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

function handleUpdateData(updateData) {
    if (!updateData) return;
    if (updateData.depositedAmount) document.getElementById('coins-info__deposited-amount-value').innerHTML = updateData.depositedAmount;
    if (updateData.changeAmount) document.getElementById('coins-info__change-amount-value').innerHTML = updateData.changeAmount;
    if (updateData.coins) initCoins(updateData.coins);
    if (updateData.products) initProducts(updateData.products);
}
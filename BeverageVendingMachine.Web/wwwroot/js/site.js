window.addEventListener("load", (event) => {
    initData();
});
function initData() {
    $.ajax({
        url: '/api/TerminalApi/GetUpdateData',
        type: 'get',
        success: function (updateData) {
            console.log('initData', updateData);
            handleUpdateData(updateData);
            initUserButtons();
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("error" + XMLHttpRequest.responseText);
        }
    });
}
function handleUpdateData(updateData) {
        console.log(' handleUpdateData updateData', updateData.changeAmount);
    if (!updateData) return;

    document.getElementById('coins-info__deposited-amount-value').innerHTML = updateData.depositedAmount;
    document.getElementById('coins-info__change-amount-value').innerHTML = updateData.changeAmount;

    if (updateData.coins) initCoins(updateData.coins);
    if (updateData.products) initProducts(updateData.products);
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
    var depositedAmount = parseInt(document.getElementById('coins-info__deposited-amount-value').innerHTML);
    var changeAmount = parseInt(document.getElementById('coins-info__change-amount-value').innerHTML);
    console.log('depositedAmount, changeAmount, productPrice, productAmount', depositedAmount, changeAmount, productPrice, productAmount);

    if (changeAmount > 0) document.getElementById('interface-button__release-change').classList.add('visible');
    if (selectedProduct) {
        var productPrice = parseInt(selectedProduct.querySelector('.products-list-item__cost').innerHTML);
        var productAmount = selectedProduct.querySelector('.products-list-item__amount').innerHTML.substring(1);

        if (depositedAmount >= productPrice) {
            document.getElementById('interface-button__make-purchase').classList.add('visible');
            if (changeAmount > 0) document.getElementById('interface-button__make-purchase-and-release-change').classList.add('visible');
        }
    }
}
function hideInterfaceButtons() {
    document.querySelectorAll('.interface-button').forEach(function (button) { button.classList.remove('visible'); });
}

function makeAjaxRequestAndUpdateData(url, data) {
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
    makeAjaxRequestAndUpdateData("api/TerminalApi/depositCoin", JSON.stringify(coin.id));
}
function selectPurchaseItem(product) {
    var depositedAmount = parseInt(document.getElementById('coins-info__deposited-amount-value').innerHTML);
    if (depositedAmount >= product.cost) {
        hideInterfaceButtons();
        makeAjaxRequestAndUpdateData("api/TerminalApi/selectPurchaseItem", JSON.stringify(product.id));
    }
}
function unselectPurchaseItem() {
    hideInterfaceButtons();
    makeAjaxRequestAndUpdateData("api/TerminalApi/unselectPurchaseItem");
}

function closeModal() {
    var modal = document.getElementById('terminal-modal');
    modal.classList.remove('visible');
    document.getElementById('terminal-modal-content').innerHTML = '';
}
function openModal() {
    var modal = document.getElementById('terminal-modal');
    modal.classList.add('visible');
}

function makePurchase() {
    console.log('makePurchase', test);
}
function releaseChange() {
    $.ajax({
        url: '/api/TerminalApi/releaseChange',
        type: 'get',
        success: function (changeCoins) {
            initData();
            openModal();
            initReleaseChangeModal(changeCoins);
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            initData();
            alert("error" + XMLHttpRequest.responseText);
        }
    });
}
function initReleaseChangeModal(changeCoins) {
    if (!changeCoins) return;
    console.log(changeCoins);
    var terminalModalContentEl = document.getElementById('terminal-modal-content');
    var h3El = document.createElement('h3');
    h3El.className = "welcome-title modal-text-color";
    terminalModalContentEl.append(h3El);

    var coinsListContainerEl = document.createElement('div');
    coinsListContainerEl.className = "coins-list-container";
    var coinsListEl = document.createElement('ul');
    coinsListEl.className = "reset-ul-list coins-list";
    coinsListEl.id = "modal-coins-list";

    var changeAmount = 0.00; 
    Object.keys(changeCoins.coinDenominationsQuantity).forEach(function (coinDenomination) {
        var coin = changeCoins.coinDenominationsQuantity[coinDenomination];
        console.log('coin', coin);
        changeAmount += coinDenomination * changeCoins.coinDenominationsQuantity[coinDenomination];
        var coinEl = document.createElement('li');
        coinEl.className = 'coins-list-item__container';

        var coinContainerEl = document.createElement('div');
        coinContainerEl.className = 'coins-list-item coin';
        coinContainerEl.innerHTML = parseInt(coinDenomination);

        var coinQuantityEl = document.createElement('span');
        coinQuantityEl.className = "coins-list-item__quantity modal-text-color";
        coinQuantityEl.innerHTML = "x" + changeCoins.coinDenominationsQuantity[coinDenomination];

        coinEl.appendChild(coinContainerEl);
        coinEl.appendChild(coinQuantityEl);
        coinsListEl.appendChild(coinEl);
    });
    h3El.innerHTML = "Your change: " + changeAmount;
    coinsListContainerEl.append(coinsListEl);
    terminalModalContentEl.append(coinsListContainerEl);
}

function makePurchaseAndReleaseChange() {
    console.log('makePurchaseAndReleaseChange');
}
function importItems(){
    console.log('importItems');
}
function addNewItem() {

}
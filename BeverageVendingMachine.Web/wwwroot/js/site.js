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
            initButtons();
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("error" + XMLHttpRequest.responseText);
        }
    });
}

function isAdmin() {
    console.log('window.location.pathname', window.location.href);
    var urlArr = window.location.href.split('?');
    console.log('urlArr ', urlArr);

    if (urlArr.length > 1 && urlArr[1] !== '') {
        console.log('params found', urlArr[1]);
        if (urlArr[1].indexOf('secretKey') > -1) return true;
    }
    return false;
}
function handleUpdateData(updateData) {
    if (!updateData) return;

    if (document.getElementById('coins-info__deposited-amount-value')) document.getElementById('coins-info__deposited-amount-value').innerHTML = updateData.depositedAmount;
    if (document.getElementById('coins-info__change-amount-value')) document.getElementById('coins-info__change-amount-value').innerHTML = updateData.changeAmount;
    if (updateData.coins) initCoins(updateData.coins);
    if (updateData.products) initProducts(updateData.products);
}
function initCoins(coins) {
    if (!coins) return;

    var coinsListEl = document.getElementById('coins-list');
    coinsListEl.innerHTML = '';
    coins.forEach(function (coin) {
        var coinEl = document.createElement('li');
        coinEl.className = 'coins-list-item coin';
        if (coin.isBlocked) coinEl.classList.add('blocked')

        coinEl.innerHTML = coin.value;
        coinEl.id = 'coins-list-item__' + coin.id;
        if (isAdmin()) coinEl.onclick = function () { togglePickCoin(coin) };
        else coinEl.onclick = function () { depositCoin(coin) };
        coinsListEl.appendChild(coinEl);
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
function addProductHtmlToPage(htmlProductsList, product, isModal) {
    var htmlProductLi = document.createElement('li');
    htmlProductLi.className = 'products-list-item';
    if (product) {
        if (product.isSelected) htmlProductLi.classList.add('selected');
        else htmlProductLi.classList.remove('selected');
    }
    htmlProductLi.id = product ? 'products-list-item__' + product.id : 'products-list-item__default';

    if (isModal) htmlProductLi.id = 'modal__' + htmlProductLi.id;
    else {
        if (isAdmin())
            htmlProductLi.onclick = function () {
                if (product) {
                    if (product.isSelected) adminUnselectPurchaseItem(product);
                    else adminSelectPurchaseItem(product);
                }
            };
        else htmlProductLi.onclick = function () {
            if (product) {
                if (product.isSelected) unselectPurchaseItem(product);
                else selectPurchaseItem(product);
            }
        };
    }

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
function initButtons() {
    if (isAdmin()) initAdminButtons();
    else initUserButtons();
}
function initUserButtons() {
    var htmlProductsList = document.getElementById('products-list');
    var selectedProduct = htmlProductsList.querySelector('.products-list-item.selected');
    var depositedAmount = parseInt(document.getElementById('coins-info__deposited-amount-value').innerHTML);
    var changeAmount = parseInt(document.getElementById('coins-info__change-amount-value').innerHTML);
    console.log('depositedAmount, changeAmount, productPrice, productAmount', depositedAmount, changeAmount, productPrice, productAmount);

    if (changeAmount > 0) document.getElementById('interface-button__release-change').classList.add('visible');
    else document.getElementById('interface-button__release-change').classList.remove('visible');
    if (selectedProduct) {
        var productPrice = parseInt(selectedProduct.querySelector('.products-list-item__cost').innerHTML);
        var productAmount = selectedProduct.querySelector('.products-list-item__amount').innerHTML.substring(1);

        if (depositedAmount >= productPrice) {
            document.getElementById('interface-button__make-purchase').classList.add('visible');
            if (changeAmount > 0) document.getElementById('interface-button__make-purchase-and-release-change').classList.add('visible');
            else document.getElementById('interface-button__make-purchase-and-release-change').classList.remove('visible');
        }
        else {
            document.getElementById('interface-button__make-purchase').classList.remove('visible');
            document.getElementById('interface-button__make-purchase-and-release-change').classList.remove('visible');
        }
    }
    else {
        document.getElementById('interface-button__make-purchase').classList.remove('visible');
        document.getElementById('interface-button__make-purchase-and-release-change').classList.remove('visible');
    }
}
function initAdminButtons() {
    console.log('initAdminButtons');
}
function hideInterfaceButtons() {
    document.querySelectorAll('.interface-button').forEach(function (button) { button.classList.remove('visible'); });
}

function makeAjaxRequestAndUpdateData(type, url, data, callback) {
    $.ajax({
        type: type,
        url: url,
        data: data,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (!callback) {
                handleUpdateData(response);
                initButtons();
            }
            else callback(response);
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            initData();
            alert("error" + XMLHttpRequest.responseText);
        }
    });
}
function depositCoin(coin) {
    hideInterfaceButtons();
    makeAjaxRequestAndUpdateData("post", "api/TerminalApi/depositCoin", JSON.stringify(coin.id));
}
function togglePickCoin(coin) {
    if (!coin) return;

    var coinEl = document.getElementById('coins-list-item__' + coin.id);
    unpickOtherCoins(coinEl);

    if (coinEl.classList.contains('selected')) {
        coinEl.classList.remove('selected');
        hideCoinBlockButtons();
    }
    else {
        coinEl.classList.add('selected');
        showCoinBlockButtons(coinEl.classList.contains('blocked'));
    }
}
function showCoinBlockButtons(isBlockedCoin) {
    var blockButtonEl = document.getElementById('interface-button__block-coin');
    var unblockButtonEl = document.getElementById('interface-button__unblock-coin');
    if (isBlockedCoin) {
        blockButtonEl.classList.add('active', 'visible', 'blocked');
        unblockButtonEl.classList.remove('active', 'blocked');
        unblockButtonEl.classList.add('visible');
    }
    else {
        unblockButtonEl.classList.add('active', 'visible', 'blocked');
        blockButtonEl.classList.remove('active', 'blocked');
        blockButtonEl.classList.add('visible');
    }
}
function hideCoinBlockButtons() {
    var blockButtonEl = document.getElementById('interface-button__block-coin');
    var unblockButtonEl = document.getElementById('interface-button__unblock-coin');
    blockButtonEl.classList.remove('active', 'visible', 'blocked');
    unblockButtonEl.classList.remove('active', 'visible', 'blocked');
}
function unpickOtherCoins(currentCoinEl) {
    var coinsListEl = document.getElementById('coins-list');
    coinsListEl.querySelectorAll('.coins-list-item').forEach(function (coinEl) {
        if (currentCoinEl && coinEl.innerHTML !== currentCoinEl.innerHTML)
            coinEl.classList.remove('selected');
    });
}
function selectPurchaseItem(product) {
    var depositedAmount = parseInt(document.getElementById('coins-info__deposited-amount-value').innerHTML);
    if (depositedAmount >= product.cost) {
        hideInterfaceButtons();
        makeAjaxRequestAndUpdateData("post", "api/TerminalApi/selectPurchaseItem", JSON.stringify(product.id));
    }
}
function adminUnselectPurchaseItem() {
    console.log('adminUnselectPurchaseItem');
}
function unselectPurchaseItem() {
    hideInterfaceButtons();
    makeAjaxRequestAndUpdateData("post", "api/TerminalApi/unselectPurchaseItem");
}
function adminSelectPurchaseItem() {
    console.log('adminSelectPurchaseItem');
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
    var callback = function (response) {
        initData();
        openModal();
        initPurchasedItemModal(response.purchaseItem);
    };
    makeAjaxRequestAndUpdateData("get", "api/TerminalApi/makePurchase", null, callback);
}
function initPurchasedItemModal(purchaseItem) {
    if (!purchaseItem) return;

    console.log(purchaseItem);
    var terminalModalContentEl = document.getElementById('terminal-modal-content');
    var h3El = document.createElement('h3');
    h3El.className = "welcome-title modal-text-color";
    terminalModalContentEl.append(h3El);

    var productsContainerEl = document.createElement('div');
    productsContainerEl.className = "products-container";
    var productsListEl = document.createElement('ul');
    productsListEl.className = "reset-ul-list products-list";
    productsListEl.id = "modal-products-list";
    addProductHtmlToPage(productsListEl, purchaseItem, true);

    h3El.innerHTML = "Here is your beverage. Bye! Have a great time!";
    productsContainerEl.append(productsListEl);
    terminalModalContentEl.append(productsContainerEl);
}
function releaseChange() {
    var callback = function (response) {
        initData();
        openModal();
        initReleaseChangeModal(response.change);
    };
    makeAjaxRequestAndUpdateData("get", "api/TerminalApi/releaseChange", null, callback);
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
    var callback = function (response) {
        initData();
        openModal();
        initReleaseChangeModal(response.change);
        initPurchasedItemModal(response.purchaseItem);
    };
    makeAjaxRequestAndUpdateData("get", "api/TerminalApi/releasePurchaseItemAndChange", null, callback);
}



function blockCoinDenomination() {
    var blockButtonEl = document.getElementById('interface-button__block-coin');
    if (blockButtonEl.classList.contains('blocked')) return;

    var selectedCoin = document.querySelector('.coins-list-item.coin.selected');
    var callback = function (response) {
        if (response === 1) showCoinBlockButtons(true);
        selectedCoin.classList.add('blocked');
    };
    makeAjaxRequestAndUpdateData("post", "api/AdminTerminalApi/blockCoinDenomination", JSON.stringify(selectedCoin.id.replace("coins-list-item__", "")), callback);
}
function unblockCoinDenomination() {
    var unblockButtonEl = document.getElementById('interface-button__unblock-coin');
    if (unblockButtonEl.classList.contains('blocked')) return;

    var selectedCoin = document.querySelector('.coins-list-item.coin.selected');
    var callback = function (response) {
        if (response === 1) showCoinBlockButtons(false);
        selectedCoin.classList.remove('blocked');
    };
    makeAjaxRequestAndUpdateData("post", "api/AdminTerminalApi/unblockCoinDenomination", JSON.stringify(selectedCoin.id.replace("coins-list-item__", "")), callback);
}
function importItems(){
    console.log('importItems');
}
function addNewItem() {

}
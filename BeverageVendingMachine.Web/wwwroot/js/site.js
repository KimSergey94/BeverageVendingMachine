// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

window.addEventListener("load", (event) => {

});

function initProducts() {
    return $.ajax({
        url: '/',
        type: 'post',
        success: function (data) {
            
        }
    })
}
function initCoins() {

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
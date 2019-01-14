function getAntiForgeryTokenHeaders() {
    // used in Json Ajax requests
    var token = getAntiForgeryToken();
    var headers = {};
    headers[token.name] = token.value;

    return headers;
};
function getAntiForgeryToken() {
    return { name: '__RequestVerificationToken', value: $('input[name=__RequestVerificationToken]').first().val() };
};

function getUserTokenHeader() {
    // used in Json Ajax requests
    var token = getUserToken();
    var headers = {};
    headers[token.name] = token.value;

    return headers;
};
function getUserToken() {
    var token=getLocalToken();
    return { name: 'Authorization', value: "Bearer " + token };
};
function getLocalToken()
{
    return localStorage["sessionStorageBackup"];
};
function clearLocalToken() {
    localStorage["sessionStorageBackup"]=null;
};

function saveToLocalStorage(storageName, valuetoStore) {
    if (Modernizr.localstorage) {
        localStorage[storageName] = valuetoStore;
    }
    else {
        $.cookie.defaults = {};
        $.removeCookie(storageName);
        $.cookie(storageName, valuetoStore);
    }
};

function getFromLocalStorage(storageName) {
    var value='';
    if (Modernizr.localstorage) {
        value = localStorage[storageName];
    }
    else {
        value=$.cookie(storageName);
    }

    return value;
}

function isUserLogged() {
    return (getFromLocalStorage("userToken")!=undefined &&
            getFromLocalStorage("userCustId")!=undefined &&
            getFromLocalStorage("userFirstName")!=undefined);
}

function userLogout() {
    $('.logged').hide();
    $('.logged').hide();
    $('.notlogged').show();
    $('#aSubmitRegister').attr('disabled', false);
    reset();
}

function userLogged(name,custid) {
    $('.notlogged').hide();
    $('.logged').show();
    $('.logged').show();
    $('#mfirstname').html(name);
    $("#mfirstnamelink").attr("href", "/Customer/Index");
}

function runToTop() {
    $('body,html').animate({ scrollTop: 0 }, 800); return false;
};
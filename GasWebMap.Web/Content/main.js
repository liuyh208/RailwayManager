$(document).ready(function () {

    //$.get("api/menu", function (data, status) {
    //    alert("Data: " + data + "\nStatus: " + status);
    //});
});


function addtab(title, href) {
    $('#divcenter').tabs('add', {
        title: title,
        href: href,
        cache: false,
        closable: true,
        tools: [
            {
                iconCls: 'icon-mini-refresh',
                handler: function () {
                    alert('refresh');
                }
            }
        ]
    });
}

function gettab(title) {

    return $('#divcenter').tabs('getTab', title);
}

function selecttab(title,url) {
    $('#divcenter').tabs('select', title);
    var tab = gettab(title);
    $('#divcenter').tabs('update', {
        tab: tab,
        options: {
            href: url
        }
    });
}


function showView(name) {

    $.ajax({
        type: "GET",
        url: '/' + name,
        cache: false,
        dataType: "html",
        success: function (data, status) {
            $('#dlg22').html(data);
        }
    });
}





function showFunction(title, url) {

    var tab = gettab(title);
    if (tab) {
        selecttab(title,url);
    } else {
        addtab(title, url);
    }
}

function showDialog(title, href, w, h) {
    $('#dlg').dialog({
        title: title,
        width: w,
        height: h,
        closed: false,
        cache: false,
        href: href,
        modal: true
    });
}

function closeDlg() {
    $('#dlg').dialog('close');
}



//向后台提交数据
function postData(url, data, sucessFun, errorFun) {
    var jsondata = data;
    if (data != null) {
        jsondata = $.toJSON(data);
    }
    $.ajax({
        type: 'POST',

        url: '../' + url,
        dataType: "json",
        contentType: 'application/json',
        data: jsondata,
        success: sucessFun,
        error: errorFun
    });
}


function putData(url, data, sucessFun, errorFun) {
    var jsondata = data;
    if (data != null) {
        jsondata = $.toJSON(data);
    }
    $.ajax({
        type: 'PUT',
        url: '../' + url,
        dataType: "json",
        contentType: 'application/json',
        data: jsondata,
        success: sucessFun,
        error: errorFun
    });
}

function deleteData(url, data, sucessFun, errorFun) {
    var jsondata = {
        type: 'DELETE',
        url: '..' + url,
        dataType: "json",
        contentType: 'application/json',
        success: sucessFun,
        error: errorFun
    };
    if (data != null) {
        jsondata.data = $.toJSON(data);
    }
    $.ajax(jsondata);
}

function getData(url, data, sucessFun, errorFun) {
    //var jsondata = $.toJSON(data);
    $.ajax({
        type: 'GET',
        url: '../' + url,
        cache: false,
        dataType: "json",
        contentType: 'application/json',
        data: data,
        success: sucessFun,
        error: errorFun
    });
}

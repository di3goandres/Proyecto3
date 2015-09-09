var paginaMaestra = 'UtilidadesSession.aspx/';


$(function () {

    loadMenu();
});

function loadMenu() {

    DoJsonRequestHModi(paginaMaestra, 'ConsultarMenuUsuarioArbol', LoadMenuArbol, '{}');
    $(document).bind("contextmenu", function (e) {
        return false;
    });
}



function LoadMenuArbol(jsonrequest) {
    var data = jsonrequest.d;
    if (data.OK == "se presento un error consultando el menu de usuario.") {
        AlertUI("Cargar Menu", data.OK);
        document.location.target = "self";
        document.location.href = '../../Default.aspx';
        return;
    }
    if (data.OK == "SESSIONEND") {

        AlertUI('.:Información', 'Su session a Finalizado, por favor ingrese nuevamente', function () {
            document.location.target = "self";
            document.location.href = '../Default.aspx';
        });

        return;
    }
    if (data.OK == "OK") {
        var perfil = jsonrequest.d.Perfil.toString();
        var esDesarrollo = jsonrequest.d.EsDesarrollo;
        var items = jsonrequest.d.Menu;

        var divMenuAll = $("#menu-ul");
        //divMenuAll.append($('<li class="left"></li>'));
        var insertarMenu = $(' <ul class="nav navbar-nav"></ul>');
        divMenuAll.append();
        for (var i = 0; i < items.length; i++) {
            insertarMenu.append(armarArbol(items[i]));
        }
        divMenuAll.append(insertarMenu);
        //jQuery('ul.menu-ppl').supersubs({ minWidth: 10, maxWidth: 40, extraWidth: 1 }).superfish();
        $("#dialog-Busy").dialog('close');
    }
}


function armarArbol(data) {

    if (data.Hijos == null) {
        var menuLinks = $("<li><a  id=menu" + data.ID_OPERACION + '" href="' + ((data.Hijos != null) ? '#' + data.ID_OPERACION : data.URL) + '">' + data.NOMBRE + '</a></li>');

        return menuLinks;

    }
    else if (data.Hijos != null) { //si tiene hijos   
        var menuLink = $("<li class='dropdown'><a  class='dropdown-toggle' data-toggle='dropdown' role='button' aria-expanded='false' id=menu'" + data.ID_OPERACION + '" href="' + ((data.Hijos != null) ? '#' + data.ID_OPERACION : data.URL) + '">' + data.NOMBRE + '<span class="caret"></span></a><li>');

        //var menuUL = $('<ul/>');
        var menuUL = $("<ul class='dropdown-menu' role='menu'></ul>");

        var menuDropdown = $('<ul/>');
        var items = [];
        for (var i = 0; i < data.Hijos.length; i++) {
            if (data.Hijos[i] != null) { // si tiene hijos, no es hoja de arbol                                      
                menuUL.append(armarArbol(data.Hijos[i]));
            } else { // si es una hoja del arbol                    
                var menuLI = '<li><a href="#" onclick="' + data.URL + '">' + data.NOMBRE + '</a></li>';
                items.push(menuLI);
            

            }

        }
        menuUL.append(items.join(''));
        menuLink.append(menuUL);
        return menuLink;

    }
}





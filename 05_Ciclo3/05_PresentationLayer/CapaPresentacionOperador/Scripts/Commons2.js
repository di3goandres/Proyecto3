var fileNamesUploader;
var varType = "POST";
var varUrl;
var varData;
var varContentType = "application/json; charset=utf-8";
var varDataType = "json";
var varProcessData;
var serviceSucceeded;
var serviceError;
var selectRow;
var num_added = 0;
var added = 0;
var all_data = {};

$(function () {
    //    $('#error-message').hide();;
    //$('#error-messagel').hide();
    serviceError = defaultError;
    $(".button").button();
    $(".datepicker").datepicker({ dateFormat: 'dd/mm/yy' });
    loadProperties();
    $(window).bind('resize', function () {
        //$('.grid').setGridWidth($('#gridFrame').width(),true);
    }).trigger('resize');
    $(window).scrollLeft("100px");
    $('.search-form').hide();
    $('.filter-button').click(function () {
        $(this).parent().parent('.list-form').children('.search-form').toggle();
    });
    $('.clean-button').click(function () {
        var searchForm = $(this).parent().parent('.list-form').children('.search-form');
        searchForm.children('.filter-table').empty();
    });

    
 
});


//Método encargado de mostrar mensaje de error en la comunicación json
//
function defaultError(xhr, status, error) {
    AlertUI(".:ERROR", status.toUpperCase() + ": " + error);
}
var varType;


//function CallService() {

//    //    var basic = getMain(varData);
//    //    basic.seguimiento = jQuery.parseJSON(GetSeguimiento());
//    //    varData = $.toJSON(basic);
//    $.ajax({
//        type: varType, //GET or POST or PUT or DELETE verb
//        url: varUrl, // Location of the service
//        data: varData, //Data sent to server
//        contentType: varContentType, // content type sent to server
//        dataType: varDataType, //Expected data format from server
//        processdata: varProcessData, //True or False
//        success: function (data) {//On Successfull service call
//            var mydata = getMain(data);
//            if (mydata.status == 2) {
//                AlertUI(".:INFO", mydata.userMessage);
//                return;
//            } else if (mydata.status == 3) {
//                AlertUI(".:ERROR", mydata.userMessage);
//                return;
//            }
//            if (mydata.status != "OK") {
//                AlertUI(".:INFO", mydata.userMessage);
//                return;
//            }
//            if (serviceSucceeded != null && serviceSucceeded != undefined) {
//                //                var main = getMain(data);
//                //                var object = jQuery.parseJSON(main);
//                serviceSucceeded(mydata);
//            }
//        },
//        error: function (a, b, c) {
//            serviceError(a, b, c);
//            //serviceError(jQuery.parseJSON(getMain(data.responseText)));
//        } // When Service call fails
//    });
//}


function getMain(dObj) {
    if (dObj.hasOwnProperty('d'))
        return dObj.d;
    else
        return dObj;
}

/////Load a basic grid without grouping
/////
//function loadGrid(myGrid, pager, caption, mydata, colNames, colModel, onSelectedRow, ondblClickRow, onRightClickRow, height, rowNum, multiselect) {
//    var num = (rowNum == undefined) ? 10 : rowNum;
//    loadGridGrouping(myGrid, pager, caption, mydata, colNames, colModel, onSelectedRow, ondblClickRow, onRightClickRow, false, null, false, false, height, num, multiselect)
//}

/////Load a grid with grouping
/////
//function loadGridGrouping(myGrid, pager, caption, mydata, colNames, colModel, onSelectedRow, ondblClickRow, onRightClickRow, grouping, groupingView, footerrow, userDataOnFooter, height, rowNum, multiselect) {

//    if (multiselect == undefined)
//        multiselect = false;
//    myGrid.jqGrid(
//    {
//        data: mydata.rows,
//        datatype: "local",
//        height: height,

//        colNames: colNames,
//        colModel: colModel,
//        index: 'Id',
//        viewrecords: true,

//        ondblClickRow: ondblClickRow,
//        onSelectRow: onSelectedRow,
//        onRightClickRow: onRightClickRow,

//        ///Pager
//        pager: pager,
//        rownumbers: false,

//        gridview: true,
//        rowNum: rowNum, 
//          rowTotal: mydata.totalRecords, rowList: [10, 20, 30],
//        caption: caption, 

//        editurl: 'clientArray',
//        grouping: grouping,
//        groupingView: groupingView,
//        footerrow: footerrow,
//        userDataOnFooter: userDataOnFooter,
//        multiselect: multiselect
//    });

//    ///Add search row
//    ///
//    myGrid.jqGrid('filterToolbar', { stringResult: true, searchOnEnter: false });

//    //Set width to grids.
//    ///
//    //$('.grid').setGridWidth($('#gridFrame').width(),true);
//    $('.grid').setGridWidth(890);
//}

function loadProperties() {
    ///Add delete config from all jqgrid components
    ///
    jQuery.extend(jQuery.jgrid.del,
    {
        caption: "Eliminar",
        msg: "Esta seguro de que desea eliminar?",
        bSubmit: "Eliminar",
        bCancel: "Cancelar",
        processData: "Eliminando...",
        closeAfterDel: true,
        ajaxDelOptions: { contentType: "application/json" },
        serializeDelData: function (postData) { //Seralize data
            if (selectRow == undefined) return;
            var delData = { entity: { Id: selectRow.Id} };
            return delData;
        },
        afterSubmit: function (response, postdata) { //After submit for errors control
            var json = response.responseText;
            var result = eval("(" + (eval("(" + json + ")")).d + ")");
            var retBool = result.Success != undefined ? true : false;
            var retMessage = result.Success != undefined ? result.Success : (result.Error != undefined ? result.Error : "Error, retorno no valido.");
            //if (!retBool) console.log(result.FullMessage)
            return [retBool, retMessage, null];
        }
    });

    ///Add navigator config from all jqgrid componentss
    ///
    jQuery.extend(jQuery.jgrid.nav,
    {
        edittext: "Editar",
        edittitle: "Editar",
        addtext: "Agregar",
        addtitle: "Agregar",
        deltext: "Eliminar",
        deltitle: "Eliminar",
        searchtext: "Buscar",
        searchtitle: "Buscar",
        refreshtext: "Refrescar",
        refreshtitle: "Refrescar",
        alertcap: "Alerta",
        alerttext: "Por favor seleccione un elemento de la tabla",
        viewtext: "Visualizar",
        viewtitle: "Visualizar"

    });

    ///Add add-edit config from all jqgrid components
    ///
    jQuery.extend(jQuery.jgrid.edit,
    {
        addCaption: "Agregar",
        editCaption: "Editar",
        bSubmit: "Guardar",
        bCancel: "Cancelar",
        bClose: "Cerrar",
        saveData: "Los datos han cambiado, desea guardar estos cambios?",
        bYes: "Si",
        bNo: "No",
        bExit: "Cancelar",
        processData: "Processing...",
        zIndex: 9999,
        width: 500,
        ///Form actions
        reloadAfterSubmit: false,
        closeAfterAdd: true,
        closeAfterEdit: true,

        //        afterSubmit: function (response, postdata) { //After submit for errors control
        //            var json = response.responseText;
        //            var result = eval("(" + (eval("(" + json + ")")).d + ")");
        //            var retBool = result.Success != undefined ? true : false;
        //            var retMessage = result.Success != undefined ? result.Success : (result.Error != undefined ? result.Error : "Error, retorno no valido.");
        //            postdata.Id = result.Id;
        //            //if (!retBool) console.log(result.FullMessage);
        //            return [retBool, retMessage, postdata.id];
        //        },
        ajaxEditOptions: { contentType: "application/json" },

        serializeEditData: function (postData) {
            //            var entity;
            //            var data = jQuery.extend(true, {}, postData);
            //            if (data.Id != null && data.Id < 1)
            //                delete data.Id;
            //            delete data.id;
            //            delete data.oper;
            //            entity = { entity: data }; // GetSeguimiento() };
            return $.toJSON(postData);
        }
    });
    $.jgrid.edit.msg.required = "es obligatorio";

}
function BuildSearchForm(searchOptions, searchForm, searchFunction) {
    var i = 0;
    var table = '<table class="dummy-search">';

    var tr = '<tr>';

    ///Se cargan los campos en los que se realizará la busqueda
    ///
    tr += '<td>';
    tr += '<select class="field-select">';
    tr += '<option value="0">';
    tr += 'Seleccione una opción';
    tr += '</option>';
    if (searchOptions.fields != undefined && searchOptions.fields != null)
        for (i = 0; i < searchOptions.fields.length; i++) {
            tr += '<option value="' + searchOptions.fields[i].Name + '">';
            tr += searchOptions.fields[i].Title;
            tr += '</option>';
        }
    tr += '</select>';
    tr += '</td>';

    tr += '<td>';
    tr += '<select class="comparer-select">';
    tr += '<option value="0">';
    tr += 'Seleccione una opción';
    tr += '</option>';
    for (i = 0; i < searchOptions.comparers.length; i++) {
        tr += '<option value="' + searchOptions.comparers[i].Key + '">';
        tr += searchOptions.comparers[i].Value;
        tr += '</option>';
    }
    tr += '</select>';
    tr += '</td>';

    ///Valor del filtro
    ///
    tr += '<td>';
    tr += '<input type="text" class="field-text"></input>';
    tr += '</td>';
    ///Boton Agregar
    tr += '<td>';
    tr += '<input type="button" value="Agregar" class="button add-filter-button"></input>';
    tr += '</td>';

    tr += '</tr>';
    table += tr;
    table += '</table>';

    searchForm.append(table);
    searchForm.append('<table class="filter-table"><tr></tr></table>');
    searchForm.append('<div class="button-bar"><input type="button" value="Buscar" class="button search-button"/></div>');
    searchForm.children(".search-button").click(searchFunction);

    $('.add-filter-button').unbind('click');
    $('.add-filter-button').click(function () {
        var searchForm = $(this).parent().parent().parent().parent().parent().parent();
        var test = $(this).closest('.search-form');
        var clone = $(this).parent().parent().parent().html().replace("Agregar", "Quitar").replace("add-filter-button", "remove-filter-button");
        $(".filter-table").prepend(clone);
        $('.remove-filter-button').unbind('click');
        $('.remove-filter-button').click(function () {
            $(this).parent().parent().remove()
        });
        //$(".filter-table").children().attr("disabled", "disabled");
    });
    $(".button").button();
}



//Muestra un mensaje de errror.
function OnError(msg) {
    alert(msg.responseText);
}

/// realiza validacion de texto o numeros segun se le pase en el primer parametro  "Letras" ó "Numeros"
//permite el backspace y teclas de funciones
function EvaluarTexto(cadena, obj, e) {
    opc = false;
    tecla = (document.all) ? e.keyCode : e.which;
    //console.log("tecla: "+ tecla.toString());
    if ((tecla == 8) || (tecla == 0))
        opc = true;  // permite el backspace y teclas de funciones
    if (cadena == "Letras") {
        if ((tecla == 32) || (tecla == 164) || (tecla == 165) || (tecla == 241) || (tecla == 209))// permite el espacio en blanco, ñ, Ñ 
            opc = true;
        if ((tecla > 64 && tecla < 91) || (tecla > 96 && tecla < 123))
            opc = true;
    }
    if (cadena == "Numeros") {
        if (tecla > 47 && tecla < 58)
            opc = true;
                        //if (obj.value.search("[.*]") == -1 && obj.value.length != 0)// permite un punto en la cadena
                            //if (tecla == 46)
                            //    opc = true;
    }
    if (cadena == "NumerosPuntos") {
        if (tecla > 45 && tecla < 58)
            opc = true;
        //if (obj.value.search("[.*]") == -1 && obj.value.length != 0)// permite un punto en la cadena
        //if (tecla == 46)
        //    opc = true;
    }
    return opc;
}

function evitarTexto(obj, e) {
    opc = false;
    tecla = (document.all) ? e.keyCode : e.which;
    //console.log("tecla: "+ tecla.toString());
    if ((tecla == 8) || (tecla == 0))
        opc = false;  //no  permite el backspace y teclas de funciones
   
        if ((tecla == 32) || (tecla == 164) || (tecla == 165) || (tecla == 241) || (tecla == 209))// permite el espacio en blanco, ñ, Ñ 
            opc = false;
        if ((tecla > 64 && tecla < 91) || (tecla > 96 && tecla < 123))
            opc = false;
   
    
        if (tecla > 47 && tecla < 58)
            opc = false;
        //                if (obj.value.search("[.*]") == -1 && obj.value.length != 0)// permite un punto en la cadena
        //                    if (tecla == 46)
        //                        opc = true;
    
    return opc;
}

function formatDate(input) {
    var datePart = input.match(/\d+/g),
  year = datePart[2], // get only two digits
  month = datePart[1],
  day = datePart[0];

    return month + '/' + day + '/' + year;
}

function formatoInternacional(input) {
    var datePart = input.match(/\d+/g),
  year = datePart[2], // get only two digits
  month = datePart[1],
  day = datePart[0];

    return year + '-' + month + '-' + day;
}

// evalua si la cadena de texto cumple con un formato  de fecha "dd/mm/yyyy" y si esa fecha es valida
function esFechaValida(fecha) {

    var numDias = 31;
    var separadoruno = fecha.substring(2, 3);
    var separadordos = fecha.substring(5, 6);
    var dia = parseInt(fecha.substring(0, 2), 10);
    var mes = parseInt(fecha.substring(3, 5), 10);
    var anio = parseInt(fecha.substring(6), 10);


    switch (mes) {
        case 1:
        case 3:
        case 5:
        case 7:
        case 8:
        case 10:
        case 12:
            numDias = 31;
            break;
        case 4: case 6: case 9: case 11:
            numDias = 30;
            break;
        case 2:
            if (comprobarSiBisisesto(anio)) { numDias = 29 } else { numDias = 28 };
            break;
        default:
            return false;
    }

    if (dia > numDias || dia == 0) {
        return false;
    }

    if (separadoruno != '/' || separadordos != '/') return false;
    return true;
}


function comprobarSiBisisesto(anio) {
    if ((anio % 100 != 0) && ((anio % 4 == 0) || (anio % 400 == 0))) {
        return true;
    }
    else {
        return false;
    }
}

// funcion valida que una fech inicial no sea mayor a una fecha final
function DateCompare(fechaInicial, fechaFinal) {
    // dd/mm/yyyy
    fec = fechaInicial.split("/");
    fec1 = fechaFinal.split("/");

    if (fec[2] > fec1[2]) {
        return -1; // si año fecha inicial mayor q fecha final retorna -1 error
    }

    if (fec[2] == fec1[2]) { // si los años son iguales se valida el mes
        if (fec[1] > fec1[1]) {
            return -1; // si mes fecha inicial mayor q mes fecha final retorna -1 error
        }

        if (fec[1] == fec1[1]) { // si los meses son iguales se valida el dia
            if (fec[0] > fec1[0]) {
                return -1; // si dia fecha inicial mayor q dia fecha final retorna -1 error
            }
        }

    }
    return 1; // no hubo error retorna 1
}

//***********************************************************************
//***********************************************************************


//
function DoJsonRequestHModi(page, WebMethod, SuccessFunction, Data) {
    OnBusy();
    jQuery.ajax({
    error: OnError,
    timeout: 900000, //15 minutos
    url: "../paginas/UtilidadesSession.aspx/" + WebMethod,
    data: Data, dataType: 'json', type: 'POST', contentType: "application/json; charset=utf-8",
    success: SuccessFunction
});
}

//Hace un request a un web method usando Jquery, Ajax y Serialización JSON Sin invokar BusyBox
function DoJsonRequestH(page, WebMethod, SuccessFunction, Data) {
    jQuery.ajax({
        error: OnError,
        timeout: 900000, //15 minutos
        url:  page + WebMethod,
        data: Data, dataType: 'json', type: 'POST', contentType: "application/json; charset=utf-8",
        success: SuccessFunction
    });
}
//Hace un request a un web method usando Jquery, Ajax y Serialización JSON
function DoJsonRequest(page, WebMethod, SuccessFunction, Data) {
    ShowBusy();
    jQuery.ajax({
        error: OnError,
        timeout: 900000, //15 minutos
        url: page + WebMethod,
        data: Data, dataType: 'json', type: 'POST', contentType: "application/json; charset=utf-8",
        success: SuccessFunction
    });
}

//Muestra un mensaje
function AlertUI(title, text, onClose) {

    //$("#dialog-message").dialog("destroy");
    $("#dialog-message").attr("title", title);
    $("#dialog-message-text").html(text);

    $("#dialog-message").dialog({
        modal: true, resizable: false, draggable: false,
        buttons: {
            Ok: function () {
                $(this).dialog('close');
            }
        },
        close: function (event, ui) {

            if (onClose != undefined && onClose != null) {
                onClose();
            }
        }
    });
}

function AlertUIFOCUS(title, text, onClose) {

    //$("#dialog-message").dialog("close");
    $("#dialog-message").attr("title", title);
    $("#dialog-message-text").html(text);

    $("#dialog-message").dialog({
        modal: true, resizable: false, draggable: false,
        buttons: {
            Ok: function () {
                $(this).dialog('close');
                onClose();
            }
        },
        close: function (event, ui) {

            if (onClose != undefined && onClose != null) {
                onClose();
            }
        }
    });
}
//Muestra un mensaje tipo Confirm
function ConfirmUI(title, text, onContinuar) {

    $("#dialog-message").attr("title", title);
    $("#dialog-message-text").html(text);
    $("#dialog").dialog("destroy");

    $("#dialog-message").dialog({
        modal: true, resizable: false, draggable: false,
        buttons: {
            'Continuar': function () {
                $(this).dialog('close');
                onContinuar();
            },
            'Cancelar': function () {
                $(this).dialog('close');
            }
        },
        close: function (event, ui) {

        }
    });
}

// Llenar un select con los datos obtenidos del request
function fillSelect(ddl, data) {
    $(ddl).empty();
    var option = '<option value="' + "0" + '">' + "SELECCIONE UNA OPCIÓN" + '</option>';
    $(ddl).append(option);
    $.each(data, function (index, itemData) {
        option = '<option value="' + itemData.Id.toString() + '">' + itemData.Nombre + '</option>';
        $(ddl).append(option);
    });
}

/// <summary>
/// Da el siguiente formato a la cadena, primer letra mayuscula y las siguientes en minuscula (Capitalized)
/// </summary>
String.prototype.capitalize = function () { //v1.0
    return this.replace(/\w+/g, function (a) {
        return a.charAt(0).toUpperCase() + a.substr(1).toLowerCase();
    });
};


//Hace un request a un web method usando Jquery, Ajax y Serialización JSON bloqueando pantalla con Ocupado
function DoJsonRequestBusy(page, WebMethod, SuccessFunction, Data) {
    OnBusy();
    jQuery.ajax({
        error: function (jsonrequest) {
            $("#dialog-Busy").dialog('close');
            OnError(jsonrequest);
        },
        timeout: 900000, //15 minutos
        url: page + WebMethod,
        data: Data, dataType: 'json', type: 'POST', contentType: "application/json; charset=utf-8",
        success: function (jsonrequest) {
            $("#dialog-Busy").dialog('close');
            SuccessFunction(jsonrequest);
        }
    });
}



function DoJsonRequestSinBusy(page, WebMethod, SuccessFunction, Data) {
    OnBusy();
    jQuery.ajax({
        error: function (jsonrequest) {
            $("#dialog-Busy").dialog('close');
            OnError(jsonrequest);
        },
        timeout: 900000, //15 minutos
        url: page + WebMethod,
        data: Data, dataType: 'json', type: 'POST', contentType: "application/json; charset=utf-8",
        success: function (jsonrequest) {
           
            SuccessFunction(jsonrequest);
        }
    });
}

//Muestra dialog de ocupado.

function OnBusy() {
    $('.ui-dialog-titlebar-close').hide();
    $("#dialog-Busy").dialog({
        modal: true,
        resizable: false,
        draggable: false,
        //show: 'explode',
        //show: 'clip',      
        //show: 'fold',  
        close: function (event, ui) {
            //            $("select").show();
        }
    });
   
}

function redirect(url) {
    window.top.location.href = url;
}

function getSelectedRow(grid) {
    var rowid = grid.jqGrid('getGridParam', 'selrow');
    if (rowid == null) {
        AlertUI(".:Info", "Debe seleccionar una fila de la grilla");
        return null;
    }
    return grid.getRowData(rowid);
}
var fileUploadUrl = 'FileUpload.ashx';
var fileUploadProgress = DefaultFileUploadProgress;
var fileUploadFail = DefaultFileUploadFail;
var fileUploadSend = DefaultFileUploadSend;
var fileUploadDone = DefaultFileUploadDone;
var fileUploadAdd = DefaultFileUploadAdd;
var fileUploadInputClass = 'fileUploadInput';
var fileUploadListClass = 'file-fileupload-list';
var fileUploadSubmit = DefaultFileUploadSubmit;
var fileUploadIconFolder = "../imagenes/";
var esMensajeria = true;

function LoadUploadFiles(inputFileUpload, divFileUploadId) {
    $(divFileUploadId).empty();
    $(divFileUploadId).append('<div><ul class = "file-fileupload-list"></ul></div>');
   
    esMensajeria = false;

    $(inputFileUpload).fileupload({
        replaceFileInput: true,
        dataType: 'json',
        url: fileUploadUrl ,// + "&IDCarpeta="+nombreCarpetaActual,
        
        //submit:fileUploadSubmit,
        //ways:function(){alert("ways");},
        start: function (data, data1, data2) {
            //fileUploadStart(divFileUploadId) 
        },
        //        progressall:function(){$('#message p').html("Terminado...");},
        progress: function () {
            fileUploadProgress(divFileUploadId)
        },
        add: function (e, data) { fileUploadAdd(e, data, divFileUploadId) },
        fail: function (e, data) { fileUploadFail(e, data, divFileUploadId) },
        send: function (e, data) { fileUploadSend(e, data, divFileUploadId) },
        done: function (e, data) { fileUploadDone(e, data, divFileUploadId) }
    });
}


function LoadUploadFilesSend(inputFileUpload, divFileUploadId, fileUpMessage) {
    $(divFileUploadId).empty();
    $(divFileUploadId).append('<div><ul class = "file-fileupload-list"></ul></div>');

    esMensajeria = true;;


    $(inputFileUpload).fileupload({
        replaceFileInput: true,
        dataType: 'json',
        url: fileUpMessage,// + "&IDCarpeta="+nombreCarpetaActual,

        //submit:fileUploadSubmit,
        //ways:function(){alert("ways");},
        start: function (data, data1, data2) {
            //fileUploadStart(divFileUploadId) 
        },
        //        progressall:function(){$('#message p').html("Terminado...");},
        progress: function () {
            fileUploadProgress(divFileUploadId)
        },
        add: function (e, data) { fileUploadAdd(e, data, divFileUploadId) },
        fail: function (e, data) { fileUploadFail(e, data, divFileUploadId) },
        send: function (e, data) { fileUploadSend(e, data, divFileUploadId) },
        done: function (e, data) { fileUploadDone(e, data, divFileUploadId) }
    });
}


function DefaultFileUploadSubmit(e, data) {
}

function DefaultFileUploadAdd(e, data, fileDivUploadId) {
    $.each(data.files, function (index, file) {
        var nombresinEspacios = '';
        var x = 0;
        //        for ( x = 0; x < file.name.length; x++) {
        //            if (file.name.charAt(x) != ' ')
        //                nombresinEspacios += file.name.charAt(x);
        //        }
        //  var filename = file.name.replace(/^\s+|\s+$/g, '').replace('.', '-').replace('#', '-').replace(' ', '');
        var filename = file.name.ReplaceAll(".", "-").ReplaceAll("|", "-").ReplaceAll("&", "-").ReplaceAll(" ", "-").ReplaceAll("$", "-").ReplaceAll("^", "-").ReplaceAll("{", "-").ReplaceAll("}", "-").ReplaceAll("[", "-").ReplaceAll("]", "").ReplaceAll("(", "-").ReplaceAll(")", "-").ReplaceAll('"', "-").ReplaceAll("'", "-").ReplaceAll("+", "-").ReplaceAll("#", "").ReplaceAll("@", "");
       
        if ($("." + filename).length != 0) {
           
            $(fileDivUploadId + ' .' + filename).remove('.upload-done');
            $(fileDivUploadId + ' .' + filename + ' .upload-done-img').remove();
            $(fileDivUploadId + ' .' + filename + ' .error-file-img').remove();
            $(fileDivUploadId + ' .' + filename + ' .loading-file-img').remove();
            $(fileDivUploadId + ' .' + filename).prepend('<img class="loading-file-img" src="' + fileUploadIconFolder + 'ajax-loader_2.gif" alt="Verificando virus" />');
        }
        else {
           
            //$(fileDivUploadId + " .file-fileupload-list").append('<li class = "file-item"><div class="' + filename + '"><span><img class="loading-file-img" src="' + fileUploadIconFolder + 'ajax-loader2.gif" alt="Verificando virus" /></span>&nbsp;<a href="#" class ="remove-file-item">Eliminar</a> &nbsp;<span class="file-item">' + file.name + '</span> <label class="actual-process">Inició la carga del archivo</label></div></li>');
            $(fileDivUploadId + " .file-fileupload-list").append('<li class = "file-item"><div class="' + filename + '"><span><img class="loading-file-img" src="' + fileUploadIconFolder + 'ajax-loader2.gif" alt="Verificando virus" /></span>&nbsp; &nbsp;<span class="file-item">' + file.name + '</span> <label class="actual-process">Inició la carga del archivo</label></div></li>');

        }
        AddRemoveHandler();
    });
    data.submit();
}

function DefaultFileUploadProgress(fileDivUploadId) {

}

function DefaultFileUploadFail(e, data, fileDivUploadId) {
    $.each(data.files, function (index, file) {
        var nombresinEspacios = '';
        var x = 0;
        //        for (x = 0; x < file.name.length; x++) {
        //            if (file.name.charAt(x) != ' ')
        //                nombresinEspacios += file.name.charAt(x);
        //        }
        //  var filename = file.name.replace(/^\s+|\s+$/g, '').replace('.', '-').replace('#', '-').replace(' ', '');
        // var filename = nombresinEspacios.replace(' ', '').replace('.', '-').replace('#', '-').replace('(', '-').replace(')', '-'); //file.name.replace(/^\s+|\s+$/g, '').replace('.', '-').replace('#', '-').replace(' ', '');
        var filename = file.name.ReplaceAll(".", "-").ReplaceAll("|", "-").ReplaceAll("&", "-").ReplaceAll(" ", "-").ReplaceAll("$", "-").ReplaceAll("^", "-").ReplaceAll("{", "-").ReplaceAll("}", "-").ReplaceAll("[", "-").ReplaceAll("]", "").ReplaceAll("(", "-").ReplaceAll(")", "-").ReplaceAll('"', "-").ReplaceAll("'", "-").ReplaceAll("+", "-").ReplaceAll("#", "").ReplaceAll("@", "");
       
        $(fileDivUploadId + ' .' + filename + ' .upload-done').remove();
        $(fileDivUploadId + ' .' + filename + ' .upload-done-img').remove();
        $(fileDivUploadId + ' .' + filename + ' .loading-file-img').remove();
        $(fileDivUploadId + ' .' + filename + ' .error-file-img').remove();

        $('.' + filename + ' .actual-process').text('Error en la carga del archivo, es posible que el tamaño del archivo sea muy grande.');
        $(fileDivUploadId + ' .' + filename).prepend('<img class="error-file-img" src="' + fileUploadIconFolder + 'close.png" alt="Error en la carga de archivos" />');
        $(fileDivUploadId + ' .' + filename).append('<button class="remove-file-item btn btn-error"><i class="fa fa-ban"></i><span>Eliminar</span></button>');
    });

    AddRemoveHandler();
}

function DefaultFileUploadSend(e, data, fileDivUploadId) {
    $.each(data.files, function (index, file) {
        var nombresinEspacios = '';
        var x = 0;
      
        var filename = file.name.ReplaceAll(".", "-").ReplaceAll("|", "-").ReplaceAll("&", "-").ReplaceAll(" ", "-").ReplaceAll("$", "-").ReplaceAll("^", "-").ReplaceAll("{", "-").ReplaceAll("}", "-").ReplaceAll("[", "-").ReplaceAll("]", "").ReplaceAll("(", "-").ReplaceAll(")", "-").ReplaceAll('"', "-").ReplaceAll("'", "-").ReplaceAll("+", "-").ReplaceAll("#", "").ReplaceAll("@", "");
       
        $(fileDivUploadId + ' .' + filename + ' .actual-process').text('Se está realizando la verificación de virus.');
    });

}
function DefaultFileUploadDone(e, data, fileDivUploadId) {
    var result = data.result;
    var archivos = result.Archivos;
    if (result.Estado == "ERROR") {
        AlertUI(".:ERROR", result.Mensaje);
    }
    if (result.Archivos != undefined && result.Archivos != null) {
        /// Inicialmente se admitia realizar cargas de multiples archivos, esto se adaptó para q fueran archivos únicos
        ///
        var error = false;
        $.each(result.Archivos, function (index, file) {
            var nombresinEspacios = '';
            var x = 0;
          
            var filename = file.Original.ReplaceAll(".", "-").ReplaceAll("|", "-").ReplaceAll("&", "-").ReplaceAll(" ", "-").ReplaceAll("$", "-").ReplaceAll("^", "-").ReplaceAll("{", "-").ReplaceAll("}", "-").ReplaceAll("[", "-").ReplaceAll("]", "").ReplaceAll("(", "-").ReplaceAll(")", "-").ReplaceAll('"', "-").ReplaceAll("'", "-").ReplaceAll("+", "-").ReplaceAll("#", "").ReplaceAll("@", "");
            //removeRepetidos(fileDivUploadId, filename);


            $(fileDivUploadId + ' .' + filename + ' .upload-done-img').remove();
            $(fileDivUploadId + ' .' + filename + ' .upload-done').remove();
            $(fileDivUploadId + ' .' + filename + ' .loading-file-img').remove();
            $(fileDivUploadId + ' .' + filename + ' .error-file-img').remove();
           

            if (result.ArchivosMaximum != undefined && result.ArchivosMaximum != null && $.inArray(file.Original, result.ArchivosMaximum) == 0) {
                $(fileDivUploadId + ' .' + filename + ' .actual-process').text('Este archivo supera el tamaño máximo permitido.');
                error = true;
            }
            if (result.ArchivosMime != undefined && result.ArchivosMime != null && $.inArray(file.Original, result.ArchivosMime) == 0) {
                $(fileDivUploadId + ' .' + filename + ' .actual-process').text('El no tiene un tipo MIME valido.');
                error = true;
            }
            if (result.ArchivosVirus != undefined && result.ArchivosVirus != null && $.inArray(file.Original, result.ArchivosVirus) == 0) {
                $(fileDivUploadId + ' .' + filename + ' .actual-process').text('El antivirus borró el archivo, es posible que contenga virus.');
                error = true;
            }
            if (error == false) {


                $(fileDivUploadId + ' .' + filename).append('<i class="icon-axa_31 iconGray"></i>');
                $(fileDivUploadId + ' .' + filename + ' .actual-process').text( file.tamanioArchivo/1024  +"KB");
                $(fileDivUploadId + ' .' + filename).append('<input type="hidden" class="upload-done" name="doneFile" value="' + file.Generated + '">');
               
             
                //$(fileDivUploadId + ' .' + filename).append('<button class="remove-file-item btn btn-error"><i class="fa fa-ban"></i><span>Eliminar</span></button>');
            //    $(fileDivUploadId + ' .' + filename).append('<button class="remove-file-item btn btn-error"><i class="fa fa-ban"></i><span>Eliminar</span></button>');

                $(fileDivUploadId + ' .' + filename).prepend('<img class="upload-done-AddRemoveHandler()img" src="' + fileUploadIconFolder + 'ok.png" alt="A la espera de carga de los archivos" />');
                if (!esMensajeria) {


                    AlertUI(".:Información", result.Mensaje, function () {
                        AddRemoveHandler();
                        traerDatosCarpeta();
                    });
                } else {

                    $(fileDivUploadId + ' .' + filename).append('<button class="remove-file-item btn btn-error"><i class="fa fa-ban"></i><span>Eliminar</span></button>');
                }
            } else {
                $(fileDivUploadId + ' .' + filename).prepend('<img class="error-file-img" src="' + fileUploadIconFolder + 'close.png" alt="error en la carga de archivos" />');
                AlertUI("ERROR", result.Mensaje);
            }
        });
    }
    AddRemoveHandler();
    if (!esMensajeria) {
        traerDatosCarpeta();
    }
}

function GetLoadFilenames(fileDivUploadId) {
    var names = "";
    if ($(fileDivUploadId + ' .loading-file-img').length.toString() != "0") {
        AlertUI("Espere por favor...", "Existen archivos que aún están cargando...");
        return null;
    }
    $(fileDivUploadId + ' .upload-done').each(function (a) {
        names += $(this).val() + ",";
    });
    return names
}


//funcion para remover un item de la lista.
function AddRemoveHandler() {
    $('.remove-file-item').unbind("click");
    $('.remove-file-item').click(function () {
        $(this).parents('.file-item:first').remove();
    });
}

function removeItem() {

    $(this).parents('.file-item:first').remove();
}

function removeRepetidos(fileDivUploadId, filename) {
    var names = "";
    
    if ($(fileDivUploadId + ' .upload-done').length.toString() != "0") {

    }

    var r = GetLoadFilenames(fileDivUploadId);
    $(fileDivUploadId + ' .file-item').each(function (a) {
        names +=  $(this).parent().attr('class')+ ",";
    });
   

    var todos = names.split(',');


    for (var i = 0; i < todos.length; i++) {
        if (todos[i] == filename) {
            $("#" + todos[i]).parents('.file-item').remove();
        }
    }

    




}


function removeALL(d) {
    var names = GetLoadFilenames(d);
    var todos = names.split(',');
    for (var i = 0; i < todos.length; i++) {
        $(d+" ul li:contains('"+todos[i]+"')").remove();
            //$("#" + todos[i]).parents('.file-item:first').remove();
        
    }
}
function GetLoadFilenamesConvenio(fileDivUploadId) {
    var names = "";
    if ($(fileDivUploadId + ' .loading-file-img').length.toString() != "0") {
        AlertUI("Espere por favor...", "Existen archivos que aún están cargando...");
        return null;
    }
    $(fileDivUploadId + ' .upload-done').each(function (a) {
        names += $(this).val() + "|" + $(this).parent().attr('class').split('-')[0] + ",";
    });
    return names
}



function validarEmail(sEmail) {
    // filtros
    var emailFilter = /^.+@.+\..{2,}$/;
    var illegalChars = /[\(\)\<\>\,\;\:\\\/\"\[\]]/
    // condição
    if (!(emailFilter.test(sEmail)) || sEmail.match(illegalChars)) {
        return false;
    } else {
        return true;

    }
}

function Refresh() {
    location.reload();
}


function imposeMaxLength(Object, MaxLen) {
    return (Object.value.length <= MaxLen);
}

if (!Array.prototype.indexOf) {
    Array.prototype.indexOf = function (obj, fromIndex) {
        if (fromIndex == null) {
            fromIndex = 0;
        } else if (fromIndex < 0) {
            fromIndex = Math.max(0, this.length + fromIndex);
        }
        for (var i = fromIndex, j = this.length; i < j; i++) {
            if (this[i] === obj)
                return i;
        }
        return -1;
    };
}

if (!String.prototype.format) {
    String.prototype.format = function () {
        var content = this;
        for (var i = 0; i < arguments.length; i++) {
            var replacement = '{' + i + '}';
            content = content.replace(replacement, arguments[i]);
        }
        return content;
    };
}

if (!String.prototype.ReplaceAll) {
    String.prototype.ReplaceAll = function (CadenaAnterior, CadenaNueva) {
        var especiales = ['|', '&', '%', '.', '$', '^', '{', '}', '[', ']', '(', ')', '.', '+', '*', '\\', '?'];
        var expresion;

        if (especiales.indexOf(CadenaAnterior) != -1)
            expresion = new RegExp('\\' + CadenaAnterior, 'gi');
        else expresion = new RegExp(CadenaAnterior, 'gi');

        var cadena = this.toString();
        var coincidencias = cadena.match(expresion);
        if (coincidencias != null) {
            for (var i = 0; i < coincidencias.length; i++) {
                cadena = cadena.replace(CadenaAnterior, CadenaNueva);
            }
        }

        return cadena;
    }

}


if (!String.prototype.trim) {

    String.prototype.trim = function () {
        return this.replace(/^\s+|\s+$/g, '');
    }
}




function obtenerRadioSeleccionado(formulario, nombre) {
    elementos = document.getElementById(formulario).elements;
    longitud = document.getElementById(formulario).length;
    for (var i = 0; i < longitud; i++) {
        if (elementos[i].name == nombre && elementos[i].type == "radio" && elementos[i].checked == true) {
            return elementos[i];
        }
    }
    return false;
}


function ObtenerValorCheckBoxSeleccionados(formulario, nombre, Lista) {
    elementos = document.getElementById(formulario).elements;
    var seleccionados = new Array();
    var nombrePP = ""
    longitud = document.getElementById(formulario).length;
    for (var i = 0; i < longitud; i++) {
        if (Lista) {
            if (elementos[i].name == nombre && elementos[i].type == 'radio' && elementos[i].checked == true) {
                seleccionados.push({ valor: elementos[i].value, nombre: elementos[i].name });
            }
        } else {
            if (elementos[i].name == nombre && elementos[i].type == 'radio' && elementos[i].checked == true) {
                nombrePP = elementos[i].value;
            }

        }
    }
    if (Lista) {
        return seleccionados;
    } else {
        return nombrePP;
    }


}



function ObtenerValorCheckBoxSeleccionadosTypeCheck(formulario, nombre, Lista) {
    elementos = document.getElementById(formulario).elements;
    var seleccionados = new Array();
    var nombrePP = ""
    longitud = document.getElementById(formulario).length;
    for (var i = 0; i < longitud; i++) {
        if (Lista) {
            if (elementos[i].name == nombre && elementos[i].type == 'checkbox' && elementos[i].checked == true) {
                seleccionados.push({ valor: elementos[i].value, nombre: elementos[i].name });
            }
        } else {
            if (elementos[i].name == nombre && elementos[i].type == 'checkbox' && elementos[i].checked == true) {
                nombrePP = elementos[i].value;
            }

        }
    }
    if (Lista) {
        return seleccionados;
    } else {
        return nombrePP;
    }


}
function seleccionarRadioButtonConValorEspecifico(formulario, nombre, valorSeleccionar) {
    elementos = document.getElementById(formulario).elements;
    longitud = document.getElementById(formulario).length;
    for (var i = 0; i < longitud; i++) {
        if (elementos[i].name == nombre && elementos[i].type == 'radio' && elementos[i].value == valorSeleccionar) {
            elementos[i].checked = true
        }

    }

}
function desMarcarHabilitar(formulario, nombre, habilitar) {

    elementos = document.getElementById(formulario).elements;
    longitud = document.getElementById(formulario).length;
    for (var i = 0; i < longitud; i++) {
        if (elementos[i].name == nombre && elementos[i].type == "radio") {
            elementos[i].checked = false;
            if (!habilitar) {
                elementos[i].disabled = true;
            }
            else {
                elementos[i].disabled = false;

            }

        }
    }
}
function desMarcarHabilitarY(formulario, nombre, habilitar, type) {

    elementos = document.getElementById(formulario).elements;
    longitud = document.getElementById(formulario).length;
    for (var i = 0; i < longitud; i++) {
        if (elementos[i].name == nombre && elementos[i].type == type) {

            if (!habilitar) {
                elementos[i].disabled = true;
            }
            else {
                elementos[i].disabled = false;

            }

        }
    }
}

function hablitarTextos(formulario, nombre, habilitar) {
    elementos = document.getElementById(formulario).elements;
    longitud = document.getElementById(formulario).length;
    for (var i = 0; i < longitud; i++) {
        if (elementos[i].name == nombre ) {

            if (!habilitar) {
                elementos[i].disabled = true;
                elementos[i].value="";
            }
            else {
                elementos[i].disabled = false;

            }

        }
    }
}
function desMarcarHabilitarCheckbox(formulario, nombre, habilitar) {

    elementos = document.getElementById(formulario).elements;
    longitud = document.getElementById(formulario).length;
    for (var i = 0; i < longitud; i++) {
        if (elementos[i].name == nombre && elementos[i].type == "checkbox") {
            elementos[i].checked = false;
            if (!habilitar) {
                elementos[i].disabled = true;
            }
            else {
                elementos[i].disabled = false;

            }

        }
    }
}
function validarDatoTipoString(data) {
    var datos = $(data).val();
    if (datos.trim().length == 0 || datos.toString().trim() == "") {
        return true;
    }
    else {
        return false;
    }
}




function validardDatoTipoSelect(data) {
    var datos = $(data).val();
    if (datos == "0") {
        return true;
    }
    else {
        return false;
    }

}
   
function focusItem(data) {
    $(data).focus();
}



function agregarOpcionesAlSelect(id, NOMBRE_TEXTO, ELEMENTO) {
    var option = '<option value="' + id + '">' + NOMBRE_TEXTO + '</option>';
    ($(ELEMENTO).append(option));
}


// Email Validation
function isValidEmailAddress(emailAddress) {
    var pattern = new RegExp(/^(("[\w-\s]+")|([\w-]+(?:\.[\w-]+)*)|("[\w-\s]+")([\w-]+(?:\.[\w-]+)*))(@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$)|(@\[?((25[0-5]\.|2[0-4][0-9]\.|1[0-9]{2}\.|[0-9]{1,2}\.))((25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\.){2}(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\]?$)/i);
 
    if (!pattern.test(emailAddress)) {
        return false;
    }
    return true;
}




function validarYAgregarDatos(data, tipo, mensaje, Nombre, parametro) {

    if (tipo == "input") {
        if (!validarDatoTipoString(data)) {
            parametro[Nombre] = $(data).val();
            return true
        } else {
            AlertUIFOCUS('.:Info', mensaje, function () { focusItem(data) });
            return false;
        }

    }
    if (tipo == "fecha") {
        if (!validarDatoTipoString(data)) {
            var fec = $(data).val();
            fec = formatoInternacional(fec);
            parametro[Nombre] = fec;
            return true
        } else {
            AlertUIFOCUS('.:Info', mensaje, function () { focusItem(data) });
            return false;
        }

    }
    if (tipo == "select") {
        if (!validardDatoTipoSelect(data)) {
            parametro[Nombre] = $(data).val();

            return true;

        } else {
            AlertUIFOCUS('.:Info', mensaje, function () { focusItem(data) });
            return false;
        }

    }
    if (tipo == "email") {
        EmailContacto = $(data).val();
        if (!isValidEmailAddress(EmailContacto)) {
            AlertUIFOCUS(".:Info", " La dirección de correo: '" + EmailContacto + "' es incorrecta.", function () { focusItem(data) });
            return false;
        }
        else {
            parametro[Nombre] = $(data).val();
            return true;
        }
    }
}

function evitarCopiarEnCampos(data) {

    var ctrlDown = false;
    var ctrlKey = 17, vKey = 86, cKey = 67;

    $(document).keydown(function (e) {
        if (e.keyCode == ctrlKey) ctrlDown = true;
    }).keyup(function (e) {
        if (e.keyCode == ctrlKey) ctrlDown = false;
    });

    $(data).keydown(function (e) {
        if (ctrlDown && (e.keyCode == vKey || e.keyCode == cKey)) return false;
    });
}


function marcarCheckBox(data, Marcar) {
    if (Marcar) {
        $("#" + data).prop('checked', true);
    } else {
        $("#" + data).prop('checked', false);

    }
}

function isChecked(data) {
    var dato = $("#" + data).is(':checked');
    return dato;
}
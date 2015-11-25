var pagina = "EnviarMensaje.aspx/";
var gridId = "#DatosUsuarios";
var gridPagerId = "#DatosUsuariosPagerL";

$(function () {

    //$("#Mensaje").hide();

    $('#ValidarTipoIdentificacion').prop('disabled', false);
    $("#ValidarNumeroIdentificacion").prop('disabled', false);
    $("#ValidarNumeroIdentificacion").val("");
    $("#ValidarTipoIdentificacion").val("0");
    TraerInformacionInicial();
    grillaUsarios();
    $("#AgregarUsuario").hide()
    $("#EnviarMensaje").button().click(function () {
        validarEnviarMensaje();
    });
    $("#BotonValidarRegistro").button().click(function () {
        ValidarRegistroUsuario();
    });

    $("#CerraValidarAgregar").button().click(function () {
        $("#AgregarUsuario").dialog('close');
    });
    LoadUploadFilesSend('#fileUploadSolicitud', '#ArchivosAdjuntosDiv', 'FileUploadMessage.ashx');




});


function Agregar() {


    $("#ValidarTipoIdentificacion").val("0");
    $("#ValidarNumeroIdentificacion").val("");

  
    $("#AgregarUsuario").dialog({
        width: 600, height: 250, modal: true, resizable: false, draggable: true, title: 'Agregar Persona' , closeOnEscape: true,
        open: function (event, ui) { $(".ui-dialog-titlebar-close", ui.dialog).show() }
    });

}


function ObtenerUsuarios() {
    var myIDs = $(gridId).jqGrid('getDataIDs');

    var mydata = new Array();
    // Recorremos renglon a renglon
    for (var i = 0; i < myIDs.length; i++) {
        // nos traemos renglon a renglon           
        var myRow = $(gridId).jqGrid('getRowData', myIDs[i]);

        mydata.push(
       {

           ID: myRow.ID,
           idTIpoIdentificacion: myRow.idTIpoIdentificacion,
           tipoIdentificacion: myRow.tipoIdentificacion,
           numeroIdentificacion: myRow.numeroIdentificacion,
           mismo: myRow.mismo


       });

    }
    return mydata;

}

    function Borrar() {

        var rowid = $(gridId).jqGrid('getGridParam', 'selrow');

        if ((rowid == null) || (rowid == undefined)) {
            AlertUI(".:Eliminar", "Debe seleccionar un registro por favor.");
            return false;
        }
        $(gridId).delRowData(rowid);
        $(gridId).trigger('reloadGrid');


    }

    function validarEnviarMensaje() {

        var parametros = {};

        var usuarios = ObtenerUsuarios();

        if (usuarios=="undefined"|| usuarios.length == 0) {
            AlertUI(".:Info", "por favor ingresar por lo menos un usuario.");
            return;
        }
        parametros.usuarios = usuarios;
        var n = false;
        n = validarYAgregarDatos("#Asunto", "input", "Ingrese el asunto del mensaje, por favor", "Asunto", parametros);
        if (!n) return;

        n = validarYAgregarDatos("#cuerpoMensaje", "input", "Ingrese el mensaje que desea enviar, por favor", "cuerpoMensaje", parametros);
        if (!n) return;
        var filenames = GetLoadFilenames('#ArchivosAdjuntosDiv');

    
        if (filenames == null) {
            return;
        }
        if (filenames == null || filenames == "") {
            AlertUI(".:Info", "por favor ingresar por lo menos, un archivo adjunto.");
            return;
        }
        parametros.FILENAMES = filenames;

        var Pasar = $.toJSON(parametros);
        DoJsonRequestBusy(pagina, 'EnviarMensaje', resultadoEnviarMensaje, Pasar);

   
    }



    function resultadoEnviarMensaje(jsonrequest) {
      
        var data = jsonrequest.d;
        if (data.OK == "OK") {
      
            AlertUI(".:Información", data.mensaje.toString(), function () {
                removeALL("#ArchivosAdjuntosDiv");
                grillaUsarios();
                $("#Asunto").val("");
                $("textarea#cuerpoMensaje").val("");
                return;
            });
        }
        else {
            AlertUI(".:Información", data.mensaje.toString());
            return
        }
    }

    function TraerInformacionInicial() {
        DoJsonRequestBusy(pagina, "TraerInformacionInicial", cargarDatosInicales, '{}');
    }





    function cargarDatosInicales(jsonrequest) {
        var data = jsonrequest.d;
        if (data.Ok == "Error Consultando información inicial.") {
            AlertUI(".:Error", data.mensaje);
            return false;
        }
        if (data.Ok == "OK") {
            
            fillSelect($("#ValidarTipoIdentificacion"), data.TIPOIDENTIFICACION);

            

        }
    }



    function ValidarRegistroUsuario() {
        var parametros = {};
        var parametrosNOpasan = {}
        var n = false;

        n = validarYAgregarDatos("#ValidarTipoIdentificacion", "select", "Seleccione un tipo de identificación", "TIPO_IDENTIFICACION", parametros);
        if (!n) return;
        n = validarYAgregarDatos("#ValidarNumeroIdentificacion", "input", "Ingrese el numero de identificacion por favor", "NUMERO_IDENTIFICACION", parametros);
        if (!n) return;


        var Pasar = $.toJSON(parametros);
        DoJsonRequestBusy(pagina, 'validar', resultadoValidar, Pasar);
    }

    function resultadoValidar(jsonrequest) {
  
   
        var data = jsonrequest.d;
        if (data.OK == "OK") {

            var totalActual = $(gridId).getGridParam("reccount") +1;

            var mydata2 = [
            {

                ID: totalActual,
                idTIpoIdentificacion: data.TIPO_IDENTIFICACION,
                tipoIdentificacion: data.COMPLETO,
                numeroIdentificacion: data.NUMERO_IDENTIFICACION,
                mismo: data.MISMO


            }];

            for (var i = 0; i < mydata2.length; i++)
                jQuery(gridId).jqGrid('addRowData', mydata2[i].ID, mydata2[i]);
            /**
            SE PROCEDE A ARMAR EL MENSAJE PARA ENVIAR.
            */
        
            $(gridId).trigger('reloadGrid');
            ObtenerUsuarios();


            $("#AgregarUsuario").dialog('close');
            //$("#BotonValidarRegistro").hide();
            AlertUI(".:Información", data.mensaje.toString());
            //$("#Mensaje").show();
            //$('#ValidarTipoIdentificacion').prop('disabled', true);
            //$("#ValidarNumeroIdentificacion").prop('disabled', true);


            //AlertUI(".:Información", data.mensaje.toString(), function () {

            //    return;
            //});
        }
        else {
            AlertUI(".:Información", data.mensaje.toString(), function () {

            });
        
        }
    }







    function cambioDepartamento(datOrigen, DataDestino) {
        //secccion para los dropdownlist de las  departamntos y municipios
        $('#' + datOrigen).change(function () {
            $('#' + DataDestino).empty(); //se limpia para agregar los datos del departamento seleccionado
            if ($('#' + datOrigen).val() == "0") {
                $('#' + DataDestino).empty();   //se limpia tambien los datos del municipio 
                var option = '<option value="' + "0" + '">' + "Seleccione una opción" + '</option>';
                ($('#' + DataDestino).append(option));
            } else {
                var nommbreDepartamento = $('#' + datOrigen).val();

                var parametros = {};
                parametros.identificador = nommbreDepartamento;
                parametros.select = DataDestino;
                //var parametros = '{"identificador":"' + nommbreDepartamento + '"}';
                $('#' + DataDestino).empty();
                var Pasar = $.toJSON(parametros);
                DoJsonRequestBusy(pagina, 'CargarDatosDropDownMunicipio', CargarDatosParametricosMunicipio, Pasar);
            }
        });
    }


    function CargarDatosParametricosMunicipio(jsonrequest) {
        var respuesta = jsonrequest.d;
        fillSelect("#" + respuesta.select, respuesta.items);

    }


    /**Grilla para validar los usuarios*/



    function grillaUsarios() {


        jQuery(gridId).jqGrid('GridUnload');
        jQuery(gridId).jqGrid({
            height: "100%",
            loadtext: "Cargando datos...",

            multiselect: false,
            pager: gridPagerId,
            //        caption: "Estados",
            emptyrecords: "Sin registros",
            rownumbers: true,
            //        width: "100%",
            shrinkToFit: false,
            width: "100%",
            datatype: "local",
            rowNum: 10,
            rowList: [10, 20, 30],
            viewsortcols: [true, 'horizontal', true],
            loadonce: false,
            viewrecords: true,
            colNames: ['ID', 'idTIpoCedula', 'Tipo de Identificación', 'Nímero de Identificación', 'Mismo'],
            colModel: [
                    {
                        name: 'ID', Index: 'ID', sortable: false, width: 100, editable: true, edittype: 'text', hidden: true, editoptions: { readonly: true },
                        editrules: { edithidden: true, required: true }
                    },
                    {
                        name: 'idTIpoIdentificacion', Index: "idTIpoIdentificacion", hidden: true, sortable: false, align: "center", width: 60, editable: true, edittype: 'text', editoptions: { readonly: true },
                        editrules: { edithidden: true, required: true }
                    },
                    {
                        name: 'tipoIdentificacion', Index: "tipoIdentificacion", sortable: false, align: "center", width: 250, editable: true, edittype: 'text', hidden: false, editoptions: { readonly: true },
                        editrules: { edithidden: true, required: true }
                    },
                    {
                        name: 'numeroIdentificacion', Index: "numeroIdentificacion", sortable: false, align: "center", width: 350, editable: true, edittype: 'text', hidden: false, editoptions: { readonly: true },
                        editrules: { edithidden: true, required: true }
                    },
                     {
                         name: 'mismo', Index: 'mismo', sortable: false, width: 100, editable: true, edittype: 'text', hidden: true, editoptions: { readonly: true },
                         editrules: { edithidden: true, required: true },
                         editoptions: { value: "True:False" }, formatter: "checkbox",
                     },

            ],

            onRightClickRow: function (rowid, iRow, iCol, e, id) {
                //AlertUI("Probando", "con ID" + rowid);



            },

            jsonReader: {
                root: "d.rows",
                page: "d.page",
                total: "d.total",
                records: "d.records"
            },


            sortname: 'Nombre',
            sortorder: 'ASC',
            sortable: false,
            caption: "Personas para Enviar Mensaje",
            //loadComplete: function (data) { if (data.d.status == 2) { AlertUI(".:INFO", data.d.userMessage); } else if (data.d.status == 3) { AlertUI(".:ERROR", data.d.userMessage); } },
            loadError: function (xhr, status, error) { AlertUI(".:ERROR", status.toUpperCase() + ": " + error); },
            prmNames: { page: 'numPage', rows: 'numRows', sort: 'colName', order: 'sortOrder', search: 'isSearch', nd: 'nd', id: 'id', oper: 'oper', editoper: 'edit', addoper: 'add', deloper: 'del', totalrows: 'totalrows' },
   

        }).navGrid(gridPagerId, {
            edit: false, add: false, del: false, search: false, view: false, refresh: false
        }).navButtonAdd(gridPagerId, {
            caption: "Agregar", buttonicon: "ui-icon-person",
            onClickButton: function (data) {Agregar(); },
            position: "last", title: "Agregar", cursor: "pointer"
   
        }).navButtonAdd(gridPagerId, {
            caption: "Eliminar", buttonicon: "ui-icon-trash",
            onClickButton: function (data) { Borrar();},
            position: "last", title: "Eliminar", cursor: "pointer"
        });
       


    }


    /**fin grilla*/
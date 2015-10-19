var pagina = "Documentos.aspx/";
var actual = "";
var esCrear = false;
var nombreCarpetaActual = "";
$(function () {

    $("#EditarCarpeta").hide();

    $("#EditarCrearCarpeta").button().click(function () {
        eventocambiarNombreCarpeta();
    });

    $("#CerrarEditarCrearCarpeta").button().click(function () {

        $("#EditarCarpeta").dialog('close')
    });
    traerDatosCarpeta();
    $("#CancelarCargue").button().click(function () {
        removeALL('#ArchivosAdjuntosDiv');
        $("#cargarArchivos").dialog('close')
    });


    //$('#capa').load('index.html');

    $('#jstree1').jstree();



    LoadUploadFiles('#fileUploadSolicitud', '#ArchivosAdjuntosDiv');
    $("#cargarArchivos").hide();
});



function AbrirCargarAchivos() {
    var parametros = {};
    var n = false;
    parametros.id = actual;
    var Pasar = JSON.stringify(parametros);
    DoJsonRequestBusy(pagina, "GuardarCarpetaActual", terminoGuardarCarpeta, Pasar);
}

function terminoGuardarCarpeta(jsonrequest) {
    var data = jsonrequest.d;
    if (data.status == "OK") {
        $('#cargarArchivos').dialog({
            width: 480, height: 280, modal: true, resizable: false, draggable: false, title: 'Cargar archivos en carpeta Carpeta', closeOnEscape: false,
            //open: function (event, ui) { $(".ui-dialog-titlebar-close", ui.dialog).show() }
        });
    } else {
        $('#cargarArchivos').dialog('close');
        AlertUI(".:Información", data.mensaje.toString());
        return
    }

}

    function eventocambiarNombreCarpeta() {
        var parametros = {};
        var n = false;
        n = validarYAgregarDatos("#textCarpeta", "input", "Ingrese el nombre de la carpeta", "NuevoNombre", parametros);
        if (!n) return;
        parametros.anterior = nombreCarpetaActual;
        parametros.Escrear = esCrear;
        parametros.identificador = actual;


        var Pasar = JSON.stringify(parametros);
        DoJsonRequestBusy(pagina, 'CrearEditarDocumentos', resultadoCarpeta, Pasar);
    }


    function resultadoCarpeta(jsonrequest) {
        var data = jsonrequest.d;
        if (data.status == "OK") {
            $("#EditarCarpeta").dialog('close');
            $("#jstree2").jstree('destroy');
            traerDatosCarpeta();
            AlertUI(".:Información", data.mensaje.toString(), function () {
           
            });
        }
        else {
            AlertUI(".:Información", data.mensaje.toString());
            return;
        }
    }


    function cambiarNombreCarpeta(Crear) {
        if (Crear) {
       
            esCrear = true;
            $("#EditarCrearCarpeta").val("Crear Carpeta");
            $('#EditarCarpeta').dialog({
                width: 450, height: 280, modal: true, resizable: false, draggable: false, title: 'Crear Carpeta', closeOnEscape: false,
                open: function (event, ui) { $(".ui-dialog-titlebar-close", ui.dialog).show() }
            });
        } else {
            $("#EditarCrearCarpeta").val("Renombrar");
            $("#textCarpeta").val(nombreCarpetaActual);
            EsEditar = false;
      
            $('#EditarCarpeta').dialog({
                width: 450, height: 280, modal: true, resizable: false, draggable: false, title: 'Renombrar Carpeta', closeOnEscape: false,
                open: function (event, ui) { $(".ui-dialog-titlebar-close", ui.dialog).show() }
            });
        }
    }


    function traerDatosCarpeta() {

        DoJsonRequestBusy(pagina, "TraerInformacionInicial", cargarDatosInicales, '{}');
        //DoJsonRequestBusy(pagina, "TraerDatosNombreCarpeta", cargarDatosInicales, '{}');
    }


    function customMenu(node) {
        // The default set of all items
        var items = {};
        if (node.type == "file") {
            items = {
                renameItem: { // The "rename" menu item
                    label: "Renombrar",
                    action: function () { },
                    icon: "glyphicon glyphicon-pencil"
                },
                deleteItem: { // The "delete" menu item
                    label: "Borrar",
                    action: function () { AlertUI(".:Información", "Sin implementar") },
                    icon: "glyphicon glyphicon-floppy-remove"
                },
            
                downloadtem: { // The "delete" menu item
                    label: "Descargar",
                    action: function () {

                        var ventana = window.open('Download.aspx?ID=' + node.id + "&IDC="+node.parent, "Descarga", "");

                    },
                    icon: "glyphicon glyphicon glyphicon-download-alt"
                }



            };

        } else if (node.type == "folder") {


            items = {
                renameItem: { // The "rename" menu item
                    label: "Renombrar",
                    action: function () {
                        cambiarNombreCarpeta(false);


                    },
                    icon: "glyphicon glyphicon-pencil"
                },
                deleteItem: { // The "delete" menu item
                    label: "Borrar",
                    action: function () { AlertUI(".:Información", "Sin implementar") },
                    icon: "glyphicon glyphicon-floppy-remove"
                },
                addItem: { // The "delete" menu item
                    label: "Adicionar un Archivo",
                    action: function () {
                        AbrirCargarAchivos();
                    },
                    icon: "glyphicon glyphicon-cloud-upload"
                },

                addFolder: { // The "delete" menu item
                    label: "Adicionar una Carpeta",
                    action: function () { cambiarNombreCarpeta(true); },
                    icon: "glyphicon glyphicon-floppy-disk"
                }
            };


        } else if (node.type == "Inicial") {

            items = {
           
                addItem: { // The "delete" menu item
                    label: "Adicionar un Archivo",
                    action: function () {

                        AbrirCargarAchivos();
                    },
                    icon: "glyphicon glyphicon-cloud-upload"
                },

                addFolder: { // The "delete" menu item
                    label: "Adicionar una Carpeta",
                    action: function () { cambiarNombreCarpeta(true); },
                    icon: "glyphicon glyphicon-floppy-disk"
                }
            };

        }

        if ($(node).hasClass("glyphicon-file")) {
            // Delete the "delete" menu item
            delete items.deleteItem;
        }

        return items;
    }
    function cargarDatosInicales(jsonrequest) {
        $("#jstree2").jstree('destroy');
        var data = jsonrequest.d;
        $("#dialog-Busy").dialog('close');
        if (data.status == "Error Consultando información inicial.") {
            AlertUI(".:Error", data.mensaje);
        
            return false;
        }
        if (data.status == "OK") {
            var datos = data.arbol;
            $("#actual").text(data.NombreCarpeta);
            $('#jstree2')

                .on('changed.jstree', function (e, data) {
                    var i, j, r = [];
                    for (i = 0, j = data.selected.length; i < j; i++) {
                        r.push(data.instance.get_node(data.selected[i]).id + data.instance.get_node(data.selected[i]).type);
                        actual = data.instance.get_node(data.selected[i]).id;
                        nombreCarpetaActual  = (data.instance.get_node(data.selected[i])).text;
                    }
                    $('#event_result').html('Selected: ' + r.join(', '));
                })


                .jstree({
                    'core': {
                        'data': datos



                    },
                    'plugins': ["contextmenu", "themes", "types"], contextmenu: {
                        items: customMenu

                   
                    }
                    , "themes": { "stripes": true }
                    , "types": {
                        "file": {
                            //"icon": "glyphicon glyphicon-flash"
                        },
                        "folder": {
                            //"icon": "glyphicon glyphicon-ok"
                        },
                        "Inicial": {
                            //"icon": "glyphicon glyphicon-ok"
                        }
                    }

                });




        }


    }
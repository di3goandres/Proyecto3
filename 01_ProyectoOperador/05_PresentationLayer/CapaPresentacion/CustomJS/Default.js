var pagina = "Default.aspx/";

var gridEstadosId = "#Datos";
var gridEstadosPagerId = "#pagerL";

var gridEstadosIdE = "#Table1";
var gridEstadosPagerIdE = "#Div2";

var gridPlanesIdE = "#Table2";
var gridPlanesPagerIdE = "#Div4";

var gridServiciosIdE = "#Table3";
var gridServiciosPagerIdE = "#Div6";

var gridServiciosDisdE = "#Table4";
var gridServiciosPagerDisdE = "#Div8";

var gridCasosdE = "#DatosC";
var gridCasosPagerIdE = "#PagerC";

$(function () {
    DoJsonRequestBusy(pagina, 'TraerinformacionInicial', datosIniciales, {});
    LoadUploadFiles('#fileUploadSolicitud', '#ArchivosAdjuntosDiv');

  
});

function datosIniciales(jsonrequest) {
    var data = jsonrequest.d;

    if (data.OK) {
        AlertUI(".:Info", "No tiene los permisos necesarios para estar en esta pagina.");
        //, function () {

        //    document.location.target = "self";
        //    document.location.href = '../Paginas/Default.aspx';
        //})
    }
    
}

<!DOCTYPE html>
<html lang="en">
<head>
    <!-- Meta, title, CSS, favicons, etc. -->
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="description" content="Bootstrap, a sleek, intuitive, and powerful mobile first front-end framework for faster and easier web development.">
    <meta name="keywords" content="HTML, CSS, JS, JavaScript, framework, bootstrap, front-end, frontend, web development">
    <meta name="author" content="Axa Colpatria">
    <title>Seguimientos estado
    </title>
    <!-- Bootstrap core CSS -->
    <link href="css/normalize.css" rel="stylesheet">
    <link href="css/bootstrap/bootstrap.min.css" rel="stylesheet">
    <link href="css/fonts.css" rel="stylesheet">
    <link href="css/axa_icons.css" rel="stylesheet">
    <link href="css/toolkit.css" rel="stylesheet">
    <link href="css/site/autos.css" rel="stylesheet">
    <!-- HTML5 shim and Respond.js for IE8 support of HTML5 elements and media queries -->
    <!--[if lt IE 8]>
      <link href="css/bootstrap/bootstrap-ie7.css" rel="stylesheet">
      <link href="css/ie/ie7.css" rel="stylesheet">
      <script src="js/ie/html5shiv.min.js"></script>
      <script src="js/ie/ie7.js"></script>
      <link href="http://externalcdn.com/respond-proxy.html" id="respond-proxy" rel="respond-proxy" />
      <script src="js/bootstrap/respond.min.js"></script>
      <![endif]-->
</head>
<body class="animated fadeIn">
    <div class="container">

        <section class="text-left">
            <h2 class="title">Seguimiento reclamo</h2>
            <p>Información para el intermediario</p>
            <div class="divider"></div>
        </section>


        <div class="clear"></div>
        <section class="quoteContent">
            <div class="container">

                <address>
                    <span><strong class="colorBlue">No. de Seguimiento:</strong>
                        <h2 class="subTit">1112344</h2>
                    </span>
                    <br>
                    <span><strong class="colorBlue">No. de obligación:</strong> 555555</span><br>
                    <span><strong class="colorBlue">Nombre Completo:</strong> Deivid Alexander Osorio de Axa</span><br>
                    <span><strong class="colorBlue">Documento:</strong> 80-090.766</span><br>
                    <span>Bogotá, D.C</span>
                </address>
                <div class="row">
                    <br>
                    <h4 class="uppercase subtitle"><i class="iconSubtitulos icon-axa_67"></i>&nbsp;Datos del siniestro</h4>
                    <!-- formulario datos personales-->
                    <br>
                    <form data-toggle="validator" role="form" novalidate="true">


                        <div class="row">


                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                <div class="form-group has-feedback ">
                                    <label for="exampleInputEmail1" class="labelhalf">Estado del siniestro</label>

                                    <div class="ui-widget arrow-caret">
                                        <select class="combobox form-control">
                                            <option value="VCO">Validación Coberturas</option>
                                            <option value="ANS">Analisis de siniestro</option>
                                            <option value="ASA">Asignación Ajustador</option>
                                            <option value="SDC">Solicitud Documentos  </option>
                                            <option value="PPG">En proceso de pago</option>
                                            <option value="PAG">Pagado</option>
                                            <option value="OBJ">Objetado</option>



                                        </select>
                                    </div>
                                    <span class="glyphicon form-control-feedback" aria-hidden="true"></span>
                                    <span class="help-block with-errors"></span>
                                </div>
                            </div>
                        </div>
                        <div class="row">





                            <div class="row">
                                <div class="col-md-6 col-sm-12 col-xs-12">
                                    <div class="form-group has-feedback">
                                        <label for="exampleInputEmail1" class="labelhalf">Fecha asignación ajustador</label>
                                        <div class="ui-widget half25 arrow-caret">
                                            <select class="form-control combobox">
                                                <option value="">Mes.</option>
                                                <option value="c.c">Agosto</option>
                                                <option value="NIT">Septiembre</option>
                                            </select>
                                        </div>
                                        <div class="ui-widget half25 arrow-caret">
                                            <select class="combobox form-control">
                                                <option value="">Día</option>
                                                <option value="c.c">01</option>
                                                <option value="NIT">02</option>
                                            </select>
                                        </div>
                                        <div class="ui-widget half25 arrow-caret">
                                            <select class="combobox form-control">
                                                <option value="">Año</option>
                                                <option value="c.c">1985</option>
                                                <option value="NIT">1986</option>
                                            </select>
                                        </div>
                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                <br>
                                <div class="col-xs-12">
                                    <div class="form-group has-feedback ">
                                        <label for="exampleInputEmail1">Observaciones</label>

                                        <textarea required class="form-control" placeholder="Observaciones Adicionales"></textarea>
                                        <span class="glyphicon form-control-feedback" aria-hidden="true"></span>
                                        <span class="help-block with-errors"></span>
                                    </div>
                                </div>
                            </div>




                        </div>

                        <div class="clear"></div>

                        <div class="row">
                            <div class="col-xs-12">
                                <div class="form-group">
                                    <label for="exampleInputFile">Adjuntar documentación de soporte &nbsp;</label>
                                    <span class="btn btn-primary fileinput-button">
                                        <i class="icon-axa_32"></i>
                                        <span>Add files... </span>
                                        <input type="file" name="files[]" multiple="">
                                    </span>
                                    <div class="clear"></div>
                                    <table role="presentation" class="table table-striped clearfix">
                                        <tbody class="files">

                                            <tr class="template-upload fade in">
                                                <td>
                                                    <i class="icon-axa_31 iconGray"></i>
                                                </td>
                                                <td>
                                                    <p class="name">foto.jpg</p>
                                                    <strong class="error text-danger label label-danger hidden">File type not allowed</strong>
                                                </td>
                                                <td>
                                                    <p class="size">1.78 KB</p>
                                                    <div class="progress progress-striped active" role="progressbar" aria-valuemin="0" aria-valuemax="100" aria-valuenow="0">
                                                        <div class="progress-bar progress-bar-success" style="width: 0%;"></div>
                                                    </div>
                                                </td>
                                                <td>


                                                    <button class="btn btn-error">
                                                        <i class="fa fa-ban"></i>
                                                        <span>Eliminar</span>
                                                    </button>

                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>

                        <div class="clear"></div>
                        <div class="row">
                        </div>
                </div>










                <div class="clear"></div>
                <div class="row">
                    <div class="text-center">
                        <a class="btn btn-cancel pull-left"><i class="icon-axa_25"></i>Cancelar </a>
                    </div>
                    <div class="text-center">
                        <a class="btn btn-default pull-right" data-toggle="modal" data-target="#AxaModal">Guardar Siniestro <i class="icon-axa_24"></i></a>
                    </div>
                </div>
                </form>
            </div>
        </section>


    </div>



    <!-- Modal -->
    <div class="modal fade" id="AxaModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog modal-sm" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="myModalLabel">Información</h4>
                </div>
                <div class="modal-body text-center">
                    Registro existente
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-primary" data-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
    </div>
    </div>
      </div>
      <!--end modales-->
</body>
<script src="js/jquery/jquery-1.11.3.js"></script>
<script src="js/jquery/jquery-ui.js"></script>
<script src="js/bootstrap/bootstrap.js"></script>
<script src="js/modernizr.js"></script>
<script src="js/bootstrap/validator.min.js"></script>
<script src="js/slick/slick.min.js" type="text/javascript"></script>
<script src="js/toolkit.js"></script>
<script src="js/site/autos.js"></script>
<script>
    $(function () {
        $('[data-toggle="tooltip"]').tooltip();
    })

</script>
</html>

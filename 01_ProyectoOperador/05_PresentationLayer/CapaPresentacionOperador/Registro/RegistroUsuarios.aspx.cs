using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Uniandes.AccesoDatos.Menu;
using Uniandes.Controlador;
using Uniandes.Entity;
using Uniandes.Utilidades;

public partial class Registro_RegistroUsuarios : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static object CrearUsuario(string NombresI, string NombresII, string ApellidosI, string ApellidosII, int TIPO_IDENTIFICACION, string NUMERO_IDENTIFICACION,

        int MunExpedicion, string fechaExpedicion, string Genero, string fechaNacimiento, int MunNacimiento, int Nacionalidad, int munResidencia, string DireccionResidencia,

        string telefono,
       string Email, string passwordQuestion, string SecurityAnswer)
    {

        string PERFILP = "USUARIOS";
        string Retorno = "";
        string status = "";
        Centralizador.Service1Client serviciocentralizador = new Centralizador.Service1Client();
        Centralizador.Entity.Usuario nuevoUsuario = new Centralizador.Entity.Usuario();

        nuevoUsuario.primerNombre = NombresI;
        nuevoUsuario.segundoNombre = NombresII;
        nuevoUsuario.segundoApellido = ApellidosII;
        nuevoUsuario.primerApellido = ApellidosI;
        nuevoUsuario.idTipoIdentificacion = TIPO_IDENTIFICACION;
        nuevoUsuario.numeroIdentificacion = NUMERO_IDENTIFICACION;
        nuevoUsuario.idMunicipioExpedicionDocumento = MunExpedicion;


        nuevoUsuario.fechaExpedicion = Convert.ToDateTime(fechaExpedicion,CultureInfo.InvariantCulture);
        nuevoUsuario.genero = Genero;
        nuevoUsuario.fechaNacimiento = Convert.ToDateTime(fechaNacimiento,CultureInfo.InvariantCulture);
        nuevoUsuario.idMunicipioNacimiento = MunNacimiento;

        nuevoUsuario.idPaisNacionalidad = Nacionalidad;
        nuevoUsuario.idMunicipioResidencia = munResidencia;
        nuevoUsuario.idMunicipioNotificacionCorrespondencia = munResidencia;
        nuevoUsuario.direccionNotificacionCorrespondencia = DireccionResidencia;
        nuevoUsuario.direccionResidencia = DireccionResidencia;
        nuevoUsuario.idMunicipioLaboral = munResidencia;
        nuevoUsuario.estadoCivil = "S";
        nuevoUsuario.correoElectronico = Email;
        nuevoUsuario.telefono = telefono;
        nuevoUsuario.idOperador = 1;
        var resultado = serviciocentralizador.ValidarExistenciaUsuario(nuevoUsuario);





        if (!resultado)
        {

            #region creacion de usuario en en sistema operador
            //


            MembershipUser a = Membership.GetUser(NUMERO_IDENTIFICACION);

            string porEmail = string.Empty;
            porEmail = Membership.GetUserNameByEmail(Email);
            if (a == null && string.IsNullOrEmpty(porEmail))
            {
                #region ("Creacion")
                MembershipCreateStatus createStatus;
                MembershipUser newUser =
                           Membership.CreateUser(NUMERO_IDENTIFICACION, NUMERO_IDENTIFICACION,
                                                 Email, passwordQuestion,
                                                 SecurityAnswer, true,
                                                 out createStatus);

                switch (createStatus)
                {
                    case MembershipCreateStatus.Success:
                        Roles.AddUserToRole(NUMERO_IDENTIFICACION, PERFILP);

                        Paciente nuevoPaciente = new Paciente();
                        nuevoPaciente.nombres_paciente = NombresI;
                        nuevoPaciente.apellidos_paciente = ApellidosI;
                        nuevoPaciente.ident_paciente = NUMERO_IDENTIFICACION;
                        nuevoPaciente.tipo_id = TIPO_IDENTIFICACION;
                        nuevoPaciente.genero_paciente = 2;

                        nuevoPaciente.direccion_paciente = DireccionResidencia;
                        nuevoPaciente.telefono_paciente = telefono;
                        nuevoPaciente.movil_paciente = telefono;
                        nuevoPaciente.mail_paciente = Email;
                        nuevoPaciente.userId = newUser.ProviderUserKey.ToString();
                        nuevoPaciente.fecha_nacimiento = DateTime.Now;

                       // PacienteDao pd = new PacienteDao();
                        //var nuevo = pd.registrarPacienteNuevo(nuevoPaciente);
                        var Usuarioregistrado = serviciocentralizador.RegistrarUsuario(nuevoUsuario);
                        DaoUsuario registroAPP = new DaoUsuario();
                        var usuaripoRegistrarApp =  registroAPP.RegistrarUsuario(nuevoPaciente.userId, Usuarioregistrado.UUID.ToString());

                        var enviar = new Correos().EnviarEmailCreacionDeUsuario(Email);

                        status = "OK";
                        Retorno = "La cuenta del usuario, ha sido creada con exito";

                        break;

                    case MembershipCreateStatus.DuplicateUserName:
                        status = "Existe";
                        Retorno = "Ya existe un usuario con ese nombre de usuario";
                        //CreateAccountResults.Text = "Ya existe un usuario con ese nombre de usuario";//"There already exists a user with this username.";
                        break;

                    case MembershipCreateStatus.DuplicateEmail:
                        status = "Duplicado";
                        Retorno = "Ya existe un usuario con este email.";// "There already exists a user with this email address.";
                        break;

                    case MembershipCreateStatus.InvalidEmail:
                        status = "email";
                        Retorno = "La dirección de correo electrónico que nos ha facilitado en inválida.";//"There email address you provided in invalid.";
                        break;

                    case MembershipCreateStatus.InvalidPassword:
                        status = "password";
                        Retorno = "La contraseña que ha proporcionado no es válido. Debe ser de siete caracteres y tener al menos un carácter no alfanumérico.";//"The password you provided is invalid. It must be seven characters long and have at least one non-alphanumeric character.";
                        break;

                    default:
                        status = "Error";
                        Retorno = "Hubo un error desconocido, la cuenta de usuario no fue creado.";//"There was an unknown error; the user account was NOT created.";
                        break;
                }
                #endregion
            }
            else
            {
                if (a != null)
                {
                    status = "Existe";
                    Retorno = "El nombre de usuario ya existe.";
                }
                //        CreateAccountResults.Text = "El usuario ya existe";

                if (!string.IsNullOrEmpty(porEmail))
                {
                    status = "EmailCambiar";
                    Retorno = "Ingrese por favor una dirección de correo electrónico diferente.";
                }
            }
#endregion

        }
        else
        {

            Retorno = "El usuario Se encuentra registrado en el centralizador";
            status = "RegistradoCentralizador"; 

        }
        return new
        {
            status = status,
            mensaje = Retorno
        };
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static object TraerinformacionInicial()
    {

        try
        {

            // var perfies = (int)ModelLayer.Bansat.Prefijo_TIPO_PARAMETROS.TIPO_PARAMETROS.PERFILES;

            return new
            {
                Ok = "OK",
                PREGUNTAS = __getPreguntas(),
                TIPOIDENTIFICACION = _GetParametrosIdentificacion(),
                DEPARTAMENTOS = _GetDepartamentos(),
                Generos = _getGeneros(),
                Paises = _getPaises()

            };



        }

        catch (Exception ex)
        {
            AppLog.Write(" Error obteniendo la informacion Inicial. ", AppLog.LogMessageType.Error, ex, "BansatLog");

            return new
            {
                OK = "Error Consultando información inicial.",
                mensaje = ex.Message + ex.StackTrace
            };
        }
    }


    public static object _GetParametrosIdentificacion()
    {


        var resultado = new TipoidentificacionDao().obtenerTipos();
        var retorno = resultado.Select(x => new
        {
            Id = x.id_tipoId,
            Nombre = x.nombre_tipoId.ToUpper()
        });

        return retorno;
    }
    public static object __getPreguntas()
    {


        var resultado = new DaoPreguntas().ObtenerPreguntas();
        var retorno = resultado.Select(x => new
        {
            Id = x.id,
            Nombre = x.pregunta
        });

        return retorno;
    }

    public static object _GetDepartamentos()
    {


        var resultado = new DepartamentosDao().ObtenerDepartamentos();
        var retorno = resultado.Select(x => new
        {
            Id = x.IdDepartamento,
            Nombre = x.NombreDepartamento.ToUpper()
        });

        return retorno;
    }


    public static object _GetMunicipiuosDepartamentos(int idDepartametnto)
    {


        var resultado = new DepartamentosDao().ObtenerMunicipiosDepartamentos(idDepartametnto);
        var retorno = resultado.Select(x => new
        {
            Id = x.IdMunicipio,
            Nombre = x.NombreMunicipio.ToUpper()
        });

        return retorno;
    }

    public static object _getPaises()
    {


        var resultado = new DepartamentosDao().obtenerPaises();
        var retorno = resultado.Select(x => new
        {
            Id = x.IdPais,
            Nombre = x.NombrePais.ToUpper()
        });

        return retorno;
    }

    public static object _getGeneros()
    {

        List<TipoGenero> generos = new List<TipoGenero>();

        generos.Add(new TipoGenero()
        {
            Id = "M",
            Genero = "Masculino"

        });
        generos.Add(new TipoGenero()
        {
            Id = "F",
            Genero = "Femenino"

        });

        var retorno = generos.Select(x => new
        {
            Id = x.Id,
            Nombre = x.Genero.ToUpper()
        });

        return retorno;

    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static object CargarDatosDropDownMunicipio(int identificador, string select)
    {
        return new
        {
            status = "ok",
            items = _GetMunicipiuosDepartamentos(identificador),
            select = select


        };
    }


    public class TipoGenero
    {
        public string Id { get; set; }
        public string Genero { get; set; }
    }


}
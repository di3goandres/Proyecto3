using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Centralizador.Entity
{
    public class Usuario
    {
        public string UUID { get; set; }
        public int idTipoIdentificacion { get; set; }
        public string numeroIdentificacion { get; set; }
        public int idMunicipioExpedicionDocumento { get; set; }
        public int idDepartamentoExpedicionDocumento { get; set; }

        public string NombreMunicipioExpedicionDocumento { get; set; }
        public DateTime fechaExpedicion { get; set; }
        public string primerApellido { get; set; }
        public string segundoApellido { get; set; }
        public string primerNombre { get; set; }
        public string segundoNombre { get; set; }
        public string genero { get; set; }
        public DateTime fechaNacimiento { get; set; }
        public int idDepartamentoNacimiento { get; set; }
        public int idMunicipioNacimiento { get; set; }
        public string NombreMunicipioNacimiento { get; set; }

        public int idPaisNacionalidad { get; set; }
        public string NombrePaisNacionalidad { get; set; }
        public int idDepartamentoResidencia { get; set; }
        public int idMunicipioResidencia { get; set; }

        public string NombreMunicipioResidencia { get; set; }

        public string direccionResidencia { get; set; }
        public int idDepartamentoNotificacionCorrespondencia { get; set; }
        public int idMunicipioNotificacionCorrespondencia { get; set; }

        public string NombreMunicipioNotificacionCorrespondencia { get; set; }

        public string direccionNotificacionCorrespondencia { get; set; }
        public string telefono { get; set; }
        public string correoElectronico { get; set; }
        public int idDepartamentoLaboral { get; set; }
        public int idMunicipioLaboral { get; set; }

        public string NombreMunicipioLaboral { get; set; }

        public string estadoCivil { get; set; }
        public Nullable<int> idOperador { get; set; }



    }
}

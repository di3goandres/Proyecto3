﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Centralizador.Entity;

namespace Centralizador.DAO
{
    public class MapeadorUsuario
    {

        public static Usuario MapUsuarioToBizEntity(tb005_RRUS entidad)
        {

            return new Usuario

            {
                UUID = entidad.UID.ToString(),
                idTipoIdentificacion = entidad.idTipoIdentificacion == default(int) ? default(int) : entidad.idTipoIdentificacion,
                numeroIdentificacion = entidad.numeroIdentificacion,
                idDepartamentoExpedicionDocumento = entidad.tb003_MUNICIPIO.tb002_DEPARTAMENTO.idDepartamento,

                idMunicipioExpedicionDocumento = entidad.idMunicipioExpedicionDocumento == default(int) ? default(int) : Convert.ToInt32(entidad.idMunicipioExpedicionDocumento),
                NombreMunicipioExpedicionDocumento = entidad.tb003_MUNICIPIO.nombreMunicipio,
                fechaExpedicion = (entidad.fechaExpedicion) == default(DateTime) ? default(DateTime) : Convert.ToDateTime(entidad.fechaExpedicion),
                primerApellido = entidad.primerApellido,
                segundoApellido = entidad.segundoApellido,
                primerNombre = entidad.primerNombre,
                segundoNombre = entidad.segundoNombre,
                genero = entidad.genero,
                fechaNacimiento = entidad.fechaNacimiento,
                idMunicipioNacimiento = entidad.idMunicipioNacimiento,
                idDepartamentoNacimiento = entidad.tb003_MUNICIPIO3.tb002_DEPARTAMENTO.idDepartamento,

                NombreMunicipioNacimiento = entidad.tb003_MUNICIPIO3.nombreMunicipio,
                idPaisNacionalidad =entidad.idPaisNacionalidad,
                NombrePaisNacionalidad = entidad.tb001_PAIS.nombrePais,
                idMunicipioResidencia = entidad.idMunicipioResidencia,
                idDepartamentoResidencia = entidad.tb003_MUNICIPIO4.tb002_DEPARTAMENTO.idDepartamento,

                NombreMunicipioResidencia = entidad.tb003_MUNICIPIO4.nombreMunicipio,
                direccionResidencia = entidad.direccionResidencia,
                idMunicipioNotificacionCorrespondencia = entidad.idMunicipioNotificacionCorrespondencia,
                idDepartamentoNotificacionCorrespondencia = entidad.tb003_MUNICIPIO1.tb002_DEPARTAMENTO.idDepartamento,

                NombreMunicipioNotificacionCorrespondencia = entidad.tb003_MUNICIPIO1.nombreMunicipio,
                direccionNotificacionCorrespondencia = entidad.direccionNotificacionCorrespondencia,
                telefono = entidad.telefono,
                correoElectronico = entidad.correoElectronico,
                idMunicipioLaboral = entidad.idMunicipioLaboral,
                idDepartamentoLaboral = entidad.tb003_MUNICIPIO2.tb002_DEPARTAMENTO.idDepartamento,

                NombreMunicipioLaboral = entidad.tb003_MUNICIPIO2.nombreMunicipio,
                estadoCivil = entidad.estadoCivil,
               



            };

        }


        public static tb005_RRUS MapUsuarioFromBizEntity(Usuario entidad)
        {
            Guid g = Guid.NewGuid();
            return new tb005_RRUS
            {
                UID = g,
                idTipoIdentificacion = entidad.idTipoIdentificacion == default(int) ? default(int) : entidad.idTipoIdentificacion,
                numeroIdentificacion = entidad.numeroIdentificacion,
                idMunicipioExpedicionDocumento = entidad.idMunicipioExpedicionDocumento == default(int) ? default(int) : Convert.ToInt32(entidad.idMunicipioExpedicionDocumento),
                fechaExpedicion = (entidad.fechaExpedicion) == default(DateTime) ? default(DateTime) : Convert.ToDateTime(entidad.fechaExpedicion),
                primerApellido = entidad.primerApellido,
                segundoApellido = entidad.segundoApellido,
                primerNombre = entidad.primerNombre,
                segundoNombre = entidad.segundoNombre,
                genero = entidad.genero,
                fechaNacimiento = entidad.fechaNacimiento,
                idMunicipioNacimiento = entidad.idMunicipioNacimiento,
                idPaisNacionalidad = entidad.idPaisNacionalidad,
                idMunicipioResidencia = entidad.idMunicipioResidencia,
                direccionResidencia = entidad.direccionResidencia,
                idMunicipioNotificacionCorrespondencia = entidad.idMunicipioNotificacionCorrespondencia,
                direccionNotificacionCorrespondencia = entidad.direccionNotificacionCorrespondencia,
                telefono = entidad.telefono,
                correoElectronico = entidad.correoElectronico,
                idMunicipioLaboral = entidad.idMunicipioLaboral,
                estadoCivil = entidad.estadoCivil,
                idOperador = entidad.idOperador
            };
        }

    }
}

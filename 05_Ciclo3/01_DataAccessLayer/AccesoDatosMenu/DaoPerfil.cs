using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Linq.SqlClient;
using System.Linq;
using System.Text;

namespace Uniandes.Centralizador.AccesoDatos.Menu
{
    public class DaoPerfil
    {
        public bool InsertPerfil(string Nombre, String Descripcion, String Prefijo)
        {

            using (AccesoDatosDataContext ctx = new AccesoDatosDataContext(ConfigurationManager.ConnectionStrings["UniandesConnectionString"].ConnectionString))
            {
                PERFIL nueva = new PERFIL();
                nueva.NOMBRE_PERFIL = Nombre;
                nueva.DESCRIPCION = Descripcion;
                nueva.PREFIJO = Prefijo;

                ctx.PERFIL.InsertOnSubmit(nueva);
                ctx.SubmitChanges();
            }

            return true;

        }


        public bool ActualizarPerfil(string Nombre, String Descripcion, int idPerfil)
        {

            using (AccesoDatosDataContext ctx = new AccesoDatosDataContext(ConfigurationManager.ConnectionStrings["UniandesConnectionString"].ConnectionString))
            {
                var oldentity = (from d in ctx.PERFIL
                                 where d.ID_PERFIL == idPerfil
                                 select d);

                if (oldentity.Any())
                {
                    oldentity.First().NOMBRE_PERFIL = Nombre;
                    oldentity.First().DESCRIPCION = Descripcion;
                    ctx.Refresh(System.Data.Linq.RefreshMode.KeepCurrentValues, oldentity);
                    ctx.SubmitChanges();
                }

            }

            return true;

        }


        public bool DeletePerfil(int IdPerfil)
        {

            using (AccesoDatosDataContext ctx = new AccesoDatosDataContext(ConfigurationManager.ConnectionStrings["UniandesConnectionString"].ConnectionString))
            {
                var operacionesPerfilEliminar = (from d in ctx.PERFIL_OPERACION
                                                 where d.ID_PERFIL == IdPerfil
                                                 select d).ToList();

                if (operacionesPerfilEliminar.Any())
                {
                    ctx.PERFIL_OPERACION.DeleteAllOnSubmit(operacionesPerfilEliminar);
                }
                var eliminarPerfil = (from d in ctx.PERFIL
                                      where d.ID_PERFIL == IdPerfil
                                      select d).ToList();
                if (eliminarPerfil.Any())
                {
                    ctx.PERFIL.DeleteAllOnSubmit(eliminarPerfil);
                }
                ctx.SubmitChanges();


            }

            return true;

        }

        public List<PERFIL> GetPerfiles(int PageIndex, int pageSize, ref int totalRecords)
        {

            var Resultados = new List<PERFIL>();
            int skip = ((PageIndex - 1) * pageSize);

            using (AccesoDatosDataContext ctx = new AccesoDatosDataContext(ConfigurationManager.ConnectionStrings["UniandesConnectionString"].ConnectionString))
            {
                var estados = (from d in ctx.PERFIL
                               select d).Distinct();
                if (estados.Any())
                {
                    estados = estados.Skip(skip).Take(pageSize);
                    Resultados = estados.ToList();

                }
            }
            return Resultados;

        }



        public bool AsociarPerfilMenu(int IdPerfil, int idMenu)
        {
            List<PERFIL_OPERACION> insertar = new List<PERFIL_OPERACION>();

            using (AccesoDatosDataContext ctx = new AccesoDatosDataContext(ConfigurationManager.ConnectionStrings["UniandesConnectionString"].ConnectionString))
            {
                var menu = (from d in ctx.OPERACION

                            where d.ID_OPERACION_PADRE == idMenu || d.ID_OPERACION == idMenu
                            select d).Distinct();


                if (menu.Any())
                {
                    foreach (var data in menu)
                    {
                        insertar.Add(new PERFIL_OPERACION()
                        {
                            ID_PERFIL = IdPerfil,
                            ID_OPERACION = data.ID_OPERACION,

                        });
                    }
                    ctx.PERFIL_OPERACION.InsertAllOnSubmit(insertar);
                    ctx.SubmitChanges();


                }
            }
            return true;

        }

        public bool DesasociarPerfilMenu(int IdPerfil, int IdMenu)
        {

            using (AccesoDatosDataContext ctx = new AccesoDatosDataContext(ConfigurationManager.ConnectionStrings["UniandesConnectionString"].ConnectionString))
            {
                var operacionesPerfilEliminar = (from d in ctx.PERFIL_OPERACION
                                                 where d.ID_PERFIL == IdPerfil
                                                 select d).ToList();

                if (operacionesPerfilEliminar.Any())
                {
                    ctx.PERFIL_OPERACION.DeleteAllOnSubmit(operacionesPerfilEliminar);
                }
                ctx.SubmitChanges();
            }

            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idMenu"></param>
        /// <returns></returns>
        public List<PERFIL> consultaPerfilesAsociadosAMenu(int idMenu)
        {
            List<PERFIL> retorno = new List<PERFIL>();

            using (AccesoDatosDataContext ctx = new AccesoDatosDataContext(ConfigurationManager.ConnectionStrings["UniandesConnectionString"].ConnectionString))
            {
                var operacionMenu = (from operacin in ctx.OPERACION
                                     where operacin.ID_OPERACION_PADRE == null
                                     && operacin.ID_OPERACION == idMenu
                                     select operacin.ID_OPERACION);

                var idperfil = (from d in ctx.PERFIL_OPERACION

                                where d.ID_OPERACION == idMenu
                                select d.ID_PERFIL).Distinct().ToList();

                var PERFILES = (from perfil in ctx.PERFIL
                                where idperfil.Contains(perfil.ID_PERFIL)
                                select perfil);

                if (PERFILES.Any())
                {
                    foreach (var data in PERFILES)
                    {
                        retorno.Add(new PERFIL()
                        {
                            ID_PERFIL = data.ID_PERFIL,
                            NOMBRE_PERFIL = data.NOMBRE_PERFIL
                        });
                    }
                }
            }
            return retorno;
        }


        /// <summary>
        /// Verifica si el perfil del uisuario tiene acceso a la URL o pagina
        /// </summary>
        /// <param name="Perfil">perfil del usuario</param>
        /// <param name="Pagina">Nombre de la pagina a la cual esta ingresando</param>
        /// <returns>True si tiene acceso, false si no lo tiene</returns>
        public bool PerfilTieneAcceso(string Perfil, string Pagina)
        {


            using (AccesoDatosDataContext ctx = new AccesoDatosDataContext(ConfigurationManager.ConnectionStrings["UniandesConnectionString"].ConnectionString))
            {

                //var idPerfil = (from d in ctx.PERFIL
                //                where d.PREFIJO == Perfil
                //                select d.ID_PERFIL).First();

                //var idoperacion = (from d in ctx.OPERACION
                //                   where d.URL == Pagina
                //                   select d.ID_OPERACION).First();

                //var existe = (from d in ctx.PERFIL_OPERACION
                //              where d.ID_PERFIL == idPerfil
                //              && d.ID_OPERACION == idoperacion
                //              select d).ToList();
                var existe = (from d in ctx.PERFIL_OPERACION
                              where d.PERFIL.PREFIJO == Perfil
                              && SqlMethods.Like(d.OPERACION.URL, "%" + Pagina + "%")
                              select d).ToList();

                if (existe.Any())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }

    }
}

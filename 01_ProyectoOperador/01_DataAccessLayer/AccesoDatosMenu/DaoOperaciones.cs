using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using Operador.Entity;

namespace Uniandes.Centralizador.AccesoDatos.Menu
{
    public class DaoOperaciones
    {

        /// <summary>
        /// PARA ARMAR EL MENU POR EL PERFIL ASOCIADO
        /// </summary>
        /// <param name="prefijoPerfil"></param>
        /// <returns></returns>
        public List<Operacion> ConsultarOperacionesMenuPorPrefijoPerfil(string prefijoPerfil)
        {
            try
            {
                var Resultados = new List<Operacion>();



                using (AccesoDatosDataContext ctx = new AccesoDatosDataContext(ConfigurationManager.ConnectionStrings["UniandesConnectionString"].ConnectionString))
                {
                    var estados = (from d in ctx.OPERACION
                                   join u in ctx.PERFIL_OPERACION on d.ID_OPERACION equals u.ID_OPERACION
                                   join per in ctx.PERFIL on u.ID_PERFIL equals per.ID_PERFIL
                                   where per.PREFIJO == prefijoPerfil && d.VISIBLE_MENU == "S"
                                   select d).Distinct();

                    if (estados.Any())
                    {
                        foreach (var operacion in estados)
                        {
                            Resultados.Add(MapeadorOperaciones.MapOperacionesToBizEntity(operacion));
                        }
                    }
                }

                return Resultados;
            }
            catch (Exception ex)
            {

                throw new Exception("Error tratando de Obtener listado de OperacionesPorPerfil.", ex);
            }
        }

        /// <summary>
        /// sE CONSULTA EL MENU PARA AGREGAR O QUITAR FUNCIONALIDADES
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRows"></param>
        /// <returns></returns>
        public List<Operacion> ConsultarMenuCabecera(int PageIndex, int pageSize, ref int totalRows)
        {
            var Resultados = new List<Operacion>();
            int skip = ((PageIndex - 1) * pageSize);

            using (AccesoDatosDataContext ctx = new AccesoDatosDataContext(ConfigurationManager.ConnectionStrings["UniandesConnectionString"].ConnectionString))
            {
                var estados = (from d in ctx.OPERACION

                               where d.ID_OPERACION_PADRE == null
                               select d).Distinct();
                if (estados.Any())
                {
                    estados = estados.Skip(skip).Take(pageSize);

                    foreach (var operacion in estados)
                    {
                        Resultados.Add(MapeadorOperaciones.MapOperacionesToBizEntity(operacion));
                    }
                }
            }
            return Resultados;


        }


        //Agrego el menu
        public bool AgregarMenuCabecera(String NombreMenu, String Url, Boolean EsPadre, int MenuPadre)
        {


            using (AccesoDatosDataContext ctx = new AccesoDatosDataContext(ConfigurationManager.ConnectionStrings["UniandesConnectionString"].ConnectionString))
            {
                OPERACION nueva = new OPERACION();
                nueva.VISIBLE_MENU = "S";
                nueva.URL = Url;
                if (!EsPadre)
                {
                    nueva.ID_OPERACION_PADRE = MenuPadre;
                }
                nueva.NOMBRE = NombreMenu;

                ctx.OPERACION.InsertOnSubmit(nueva);


                ctx.SubmitChanges();

                InsertarOperacionAlPerfil(MenuPadre, nueva.ID_OPERACION);
            }
            return true;

        }

        public void InsertarOperacionAlPerfil(int idOperacionPadre, int idOperacionhija)
        {
            List<PERFIL_OPERACION> insertar = new List<PERFIL_OPERACION>();

            using (AccesoDatosDataContext ctx = new AccesoDatosDataContext(ConfigurationManager.ConnectionStrings["UniandesConnectionString"].ConnectionString))
            {

                var Perfiles = (from perfiles in ctx.PERFIL_OPERACION
                                where perfiles.ID_OPERACION == idOperacionPadre
                                select perfiles.ID_PERFIL).Distinct();

                if (Perfiles.Any())
                {
                    foreach (var datos in Perfiles.ToList())
                    {
                        insertar.Add(new PERFIL_OPERACION()
                        {
                            ID_PERFIL = datos.Value,
                            ID_OPERACION = idOperacionhija,
                        });
                    }
                }


                ctx.PERFIL_OPERACION.InsertAllOnSubmit(insertar);
                ctx.SubmitChanges();
            }
        }





        //cONSULTAR LO HIJOS DEL MENU
        public List<Operacion> ConsultarMenuHijosdeCabecera(int PageIndex, int pageSize, ref int totalRows, int idPadre)
        {
            var Resultados = new List<Operacion>();
            int skip = ((PageIndex - 1) * pageSize);

            using (AccesoDatosDataContext ctx = new AccesoDatosDataContext(ConfigurationManager.ConnectionStrings["UniandesConnectionString"].ConnectionString))
            {
                var estados = (from d in ctx.OPERACION

                               where d.ID_OPERACION_PADRE == idPadre
                               select d).Distinct();
                if (estados.Any())
                {
                    estados = estados.Skip(skip).Take(pageSize);
                    foreach (var operacion in estados)
                    {
                        Resultados.Add(MapeadorOperaciones.MapOperacionesToBizEntity(operacion));
                    }
                }
            }
            return Resultados;


        }



        //Para desactivar cualquier menu o item de menu
        public bool DesactivarMenuCabeceraOHijo(Boolean Activar, int idMenu)
        {

            try
            {
                using (AccesoDatosDataContext ctx = new AccesoDatosDataContext(ConfigurationManager.ConnectionStrings["UniandesConnectionString"].ConnectionString))
                {
                    var oldentity = (from d in ctx.OPERACION
                                     where d.ID_OPERACION == idMenu
                                     select d).FirstOrDefault();

                    if (oldentity != null)
                    {
                        oldentity.VISIBLE_MENU = Activar == true ? "S" : "N";
                        ctx.Refresh(System.Data.Linq.RefreshMode.KeepCurrentValues, oldentity);
                        ctx.SubmitChanges();
                    }

                }
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }


        }


        public bool ActualizarDescripcionesMenu(int IDMenu, String NombreMenu, String UrlMenu)
        {
            try
            {
                using (AccesoDatosDataContext ctx = new AccesoDatosDataContext(ConfigurationManager.ConnectionStrings["UniandesConnectionString"].ConnectionString))
                {
                    var oldentity = (from d in ctx.OPERACION
                                     where d.ID_OPERACION == IDMenu
                                     select d).FirstOrDefault();

                    if (oldentity != null)
                    {
                        oldentity.NOMBRE = NombreMenu;
                        oldentity.URL = UrlMenu;

                        ctx.Refresh(System.Data.Linq.RefreshMode.KeepCurrentValues, oldentity);
                        ctx.SubmitChanges();
                    }

                }
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        #region Menu - perfiles
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Perfil"></param>
        /// <param name="idMenuOperacionPadre"></param>
        /// <returns></returns>
        public bool InsertarOperacionesPerfil(String Perfil, int idMenuOperacionPadre)
        {

            using (AccesoDatosDataContext ctx = new AccesoDatosDataContext(ConfigurationManager.ConnectionStrings["UniandesConnectionString"].ConnectionString))
            {
                List<PERFIL_OPERACION> insertar = new List<PERFIL_OPERACION>();
                // Consultar el menu
                var perfilP = (from perfil in ctx.PERFIL
                               where perfil.PREFIJO == Perfil
                               select perfil).First().ID_PERFIL;


                var hijos = (from d in ctx.OPERACION
                             where d.ID_OPERACION_PADRE == idMenuOperacionPadre
                             select d);

                if (hijos != null)
                {
                    foreach (var data in hijos)
                    {
                        insertar.Add(new PERFIL_OPERACION()
                        {
                            ID_PERFIL = perfilP,
                            ID_OPERACION = data.ID_OPERACION,

                        });
                    }
                }
                insertar.Add(new PERFIL_OPERACION()
                {
                    ID_PERFIL = perfilP,
                    ID_OPERACION = idMenuOperacionPadre,
                });
                ctx.PERFIL_OPERACION.InsertAllOnSubmit(insertar);
                ctx.SubmitChanges();


            }
            return true;
        }
        public bool DeleteOperacionesPerfil(String Perfil, int idMenuOperacionPadre)
        {

            using (AccesoDatosDataContext ctx = new AccesoDatosDataContext(ConfigurationManager.ConnectionStrings["UniandesConnectionString"].ConnectionString))
            {
                List<PERFIL_OPERACION> insertar = new List<PERFIL_OPERACION>();
                // Consultar el menu
                int idPerfil = 0;
                var perfilesP = (from perfil in ctx.PERFIL
                                 where perfil.PREFIJO.Contains(Perfil)
                                 select perfil);//.First().ID_PERFIL;
                if (perfilesP.Any())
                {

                    idPerfil = perfilesP.First().ID_PERFIL;
                }

                var hijos = (from d in ctx.OPERACION
                             where d.ID_OPERACION_PADRE == idMenuOperacionPadre || d.ID_OPERACION == idMenuOperacionPadre
                             select d.ID_OPERACION).ToList();

                var operfil = (from po in ctx.PERFIL_OPERACION
                               join perfiles in ctx.PERFIL on po.ID_PERFIL equals perfiles.ID_PERFIL
                               join oper in ctx.OPERACION on po.ID_OPERACION equals oper.ID_OPERACION
                               where perfiles.ID_PERFIL == idPerfil
                     && hijos.Contains(oper.ID_OPERACION)

                               select po).ToList();



                ctx.PERFIL_OPERACION.DeleteAllOnSubmit(operfil);
                ctx.SubmitChanges();


            }
            return true;
        }


        public bool consultaExisteEnbaseDatos(int perfil, int idMenuOperacionPadre)
        {
            using (AccesoDatosDataContext ctx = new AccesoDatosDataContext(ConfigurationManager.ConnectionStrings["UniandesConnectionString"].ConnectionString))
            {
                var operfil = (from po in ctx.PERFIL_OPERACION
                               where po.ID_PERFIL == perfil && po.ID_OPERACION == idMenuOperacionPadre
                               select po);

                if (operfil.Any())
                    return true;
                else
                    return false;

            }


        }
        #endregion
    }
}

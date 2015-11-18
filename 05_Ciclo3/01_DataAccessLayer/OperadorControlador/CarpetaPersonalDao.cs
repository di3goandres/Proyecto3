using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Operador.Entity;

namespace Uniandes.Controlador
{
    public class CarpetaPersonalDao
    {

        /// <summary>
        /// Registra una carpeta en el FTP personal del usuario.
        /// </summary>
        /// <param name="carpeta"></param>
        /// <returns></returns>
        public bool RegistrarCarpetaPersonal(CarpetaPersonal carpeta)
        {

            try
            {
                using (OperadorDataContext ctx = new OperadorDataContext())
                {

                    bool totalExiste = validarExisteCarpeta(carpeta.NombreCarpeta, carpeta.IdCarpetaPersonal);

                    if (totalExiste)
                    {
                        //No se crea en base de datos por que exite con un nombre identico
                        return false;
                    }
                    else
                    {
                        var entidad = MapeadorCarpetaPersonal.MapCarpetaFromBizEntity(carpeta);
                        entidad.idCarpetaPadre = entidad.idCarpetaPadre == 0 ? null : entidad.idCarpetaPadre;

                        ctx.tblCarpetaPersonal.InsertOnSubmit(entidad);
                        ctx.SubmitChanges();

                        var retorno = MapeadorCarpetaPersonal.MapCarpetaToBizEntity(entidad);
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



        public bool ActualizarCarpetaPersonal(CarpetaPersonal carpeta)
        {

            try
            {
                using (OperadorDataContext ctx = new OperadorDataContext())
                {


                    var datos = (from cp in ctx.tblCarpetaPersonal
                                 where cp.idCarpetaPersonal == carpeta.IdCarpetaPersonal
                                 select cp).FirstOrDefault();

                    if (datos != null)
                    {
                        datos.NombreCarpeta = carpeta.NombreCarpeta;
                        ctx.Refresh(System.Data.Linq.RefreshMode.KeepCurrentValues, datos);
                        ctx.SubmitChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Validar Existencia Carpeta
        /// </summary>
        /// <param name="nombreCarpeta"></param>
        /// <param name="idCarpeta"></param>
        /// <returns></returns>
        public bool validarExisteCarpeta(String nombreCarpeta, Decimal? idCarpeta)
        {

            try
            {
                using (OperadorDataContext ctx = new OperadorDataContext())
                {



                    if (idCarpeta == null)
                    {
                        var carpeta = (from cp in ctx.tblCarpetaPersonal
                                       where cp.idCarpetaPadre == (decimal?)null
                                       && cp.NombreCarpeta.ToUpper() == nombreCarpeta.ToUpper()
                                       select cp);

                        if (carpeta.Any())
                        {
                            //si existen 
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        var actual = (from cp in ctx.tblCarpetaPersonal
                                      where cp.idCarpetaPersonal == idCarpeta
                                      select cp).First();


                        if (actual.idCarpetaPadre == null)
                        {
                            var carpeta = (from cp in ctx.tblCarpetaPersonal
                                           where cp.idCarpetaPadre == (decimal?)null
                                                && cp.NombreCarpeta.ToUpper() == nombreCarpeta.ToUpper()
                                           select cp);

                            if (carpeta.Any())
                            {
                                //si existen 
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else {
                            var carpeta = (from cp in ctx.tblCarpetaPersonal
                                           where cp.idCarpetaPadre == actual.idCarpetaPadre
                                                && cp.NombreCarpeta.ToUpper() == nombreCarpeta.ToUpper()
                                           select cp);

                            if (carpeta.Any())
                            {
                                //si existen 
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
            catch (Exception e)
            {
                return false;
            }
        }


        /// <summary>
        /// obtiene todas las carpetas por usuario pero las retorna en forma de arbol
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<CarpetaPersonal> ObtenerCarpetasPorUsuario(String userId)
        {

            List<CarpetaPersonal> Resultados = new List<CarpetaPersonal>();
            List<CarpetaPersonal> ResultadosConPath = new List<CarpetaPersonal>();


            try
            {
                using (OperadorDataContext ctx = new OperadorDataContext())
                {


                    var cPersonal = (from cp in ctx.tblCarpetaPersonal
                                     where cp.userIdApplicacion == userId
                                     select cp);


                    if (cPersonal.Any())
                    {
                        foreach (var operacion in cPersonal)
                        {
                            Resultados.Add(MapeadorCarpetaPersonal.MapCarpetaToBizEntity(operacion));
                        }
                    }
                }
                foreach (var data in Resultados)
                {
                    data.PathTotal = fullPathPorCarpeta(data.IdCarpetaPersonal);
                    ResultadosConPath.Add(data);

                }


                CargarListaHijos(ResultadosConPath);
                var Resultado = ResultadosConPath.Where(x => x.idCarpetaPadre == 0).OrderBy(x => x.NombreCarpeta).ToList();
                return Resultado;

            }
            catch (Exception e)
            {
                throw e;
            }


        }


        /// <summary>
        /// Obtiene todas las carptas del usuario, adicionalmente se le coloca el full path de cada carpeta. y las entrega todas, sin forma 
        /// de arbol
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<CarpetaPersonal> ObtenerTodasCarpetasPorUsuario(String userId)
        {
            List<CarpetaPersonal> Resultados = new List<CarpetaPersonal>();
            List<CarpetaPersonal> ResultadosConPath = new List<CarpetaPersonal>();
            try
            {
                using (OperadorDataContext ctx = new OperadorDataContext())
                {


                    var cPersonal = (from cp in ctx.tblCarpetaPersonal
                                     where cp.userIdApplicacion == userId
                                     select cp);


                    if (cPersonal.Any())
                    {
                        foreach (var operacion in cPersonal)
                        {
                            Resultados.Add(MapeadorCarpetaPersonal.MapCarpetaToBizEntity(operacion));
                        }
                    }
                }
                foreach (var data in Resultados)
                {
                    data.PathTotal = fullPathPorCarpeta(data.IdCarpetaPersonal);
                    ResultadosConPath.Add(data);

                }


                // CargarListaHijos(ResultadosConPath);
                // var Resultado = ResultadosConPath.Where(x => x.idCarpetaPadre == 0).OrderBy(x => x.NombreCarpeta).ToList();
                return ResultadosConPath;

            }
            catch (Exception e)
            {
                throw e;
            }


        }


        /// <summary>
        /// Obtiene todas las carpetas del usuario y adiciona coloca el full path de ellas
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="idCarpetaPersonal"></param>
        /// <returns></returns>
        public List<CarpetaPersonal> ObtenerCarpetasPorUsuarioCarpeta(String userId, long? idCarpetaPersonal)
        {

            List<CarpetaPersonal> Resultados = new List<CarpetaPersonal>();
            List<CarpetaPersonal> ResultadosConPath = new List<CarpetaPersonal>();



            try
            {
                using (OperadorDataContext ctx = new OperadorDataContext())
                {

                    // tblCarpetaPersonal cpersonal = new tblCarpetaPersonal();
                    //si se pasa null trae los padres de lo contrario traera los Hijos
                    if (idCarpetaPersonal == null)
                    {
                        var cPersonal = (from cp in ctx.tblCarpetaPersonal
                                         where cp.userIdApplicacion == userId &&
                                          cp.idCarpetaPadre == (decimal?)null

                                         select cp);


                        if (cPersonal.Any())
                        {
                            foreach (var operacion in cPersonal)
                            {
                                Resultados.Add(MapeadorCarpetaPersonal.MapCarpetaToBizEntity(operacion));
                            }
                        }
                    }
                    else
                    {


                        var cPersonal = (from cp in ctx.tblCarpetaPersonal
                                         where cp.userIdApplicacion == userId &&
                                          cp.idCarpetaPadre == idCarpetaPersonal
                                         select cp);


                        if (cPersonal.Any())
                        {
                            foreach (var operacion in cPersonal)
                            {
                                Resultados.Add(MapeadorCarpetaPersonal.MapCarpetaToBizEntity(operacion));
                            }
                        }

                    }

                }

                //  var ResultadoCarpeta = Resultados.Where(x => x.idCarpetaPadre == 0).OrderBy(x => x.NombreCarpeta).ToList();
                foreach (var data in Resultados)
                {
                    data.PathTotal = fullPathPorCarpeta(data.IdCarpetaPersonal);
                    ResultadosConPath.Add(data);

                }
                return ResultadosConPath;


            }
            catch (Exception e)
            {
                throw e;
            }


        }




        /// <summary>
        /// Carga en los carpetas padres sus carpetas hijas
        /// </summary>
        /// <param name="carpetaBiz"></param>
        private static void CargarListaHijos(List<CarpetaPersonal> carpetaBiz)
        {
            try
            {
                foreach (var padre in carpetaBiz)
                {

                    List<CarpetaPersonal> hijos = new List<CarpetaPersonal>();
                    foreach (var hijo in carpetaBiz)
                    {
                        if (hijo.idCarpetaPadre == padre.IdCarpetaPersonal)
                            hijos.Add(hijo);
                    }

                    if (hijos.Count > 0)
                    {
                        CarpetaPersonal op = carpetaBiz.FirstOrDefault(x => x.IdCarpetaPersonal == padre.IdCarpetaPersonal);
                        op.Hijos = new List<CarpetaPersonal>();
                        op.Hijos.AddRange(hijos);

                        CargarListaHijos(hijos);
                    }
                }

            }
            catch (Exception exc) { throw exc; }
        }



        /// <summary>
        /// retorna el full path de una carpeta
        /// </summary>
        /// <param name="idCarpeta"></param>
        /// <returns></returns>
        public string fullPathPorCarpeta(decimal? idCarpeta)
        {
            string fullName = string.Empty;
            using (OperadorDataContext ctx = new OperadorDataContext())
            {


                var cPersonal = (from cp in ctx.tblCarpetaPersonal
                                 where cp.idCarpetaPersonal == idCarpeta
                                 select cp);


                if (cPersonal.Any())
                {
                    foreach (var operacion in cPersonal)
                    {
                        fullName += fullPathPorCarpeta(operacion.idCarpetaPadre) + @"\" + operacion.NombreCarpeta;
                    }
                }
            }

            return fullName;
        }




    }



}

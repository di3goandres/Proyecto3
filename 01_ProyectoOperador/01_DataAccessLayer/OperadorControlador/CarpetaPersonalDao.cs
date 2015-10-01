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


        public bool validarExisteCarpeta(String nombreCarpeta, Decimal idCarpeta)
        {

            try
            {
                using (OperadorDataContext ctx = new OperadorDataContext())
                {


                    var carpeta = (from cp in ctx.tblCarpetaPersonal
                                   where cp.idCarpetaPersonal == idCarpeta
                                   && cp.NombreCarpeta == nombreCarpeta
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
            catch (Exception e)
            {
                return false;
            }
        }



        public List<CarpetaPersonal> ObtenerCarpetasPorUsuario(String userId)
        {

            List<CarpetaPersonal> Resultados = new List<CarpetaPersonal>();


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
                CargarListaHijos(Resultados);
                var Resultado = Resultados.Where(x => x.idCarpetaPadre == 0).OrderBy(x => x.NombreCarpeta).ToList();
                return Resultado;

            }
            catch (Exception e)
            {
                throw e;
            }


        }

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
    
    }

     

}

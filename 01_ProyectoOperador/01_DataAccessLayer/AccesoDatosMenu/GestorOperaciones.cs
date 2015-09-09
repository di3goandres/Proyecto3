using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uniandes.Entity;

namespace Uniandes.Centralizador.AccesoDatos.Menu
{
    public class GestorOperaciones
    {
        public List<Operacion> ConsultarOperacionesMenuPorPrefijoPerfil(List<string> prefijoPerfil)
        {
            try

            {
                List<Operacion> retorno = new List<Operacion>();
                foreach (var prefijo in prefijoPerfil)
                {
                    List<Operacion> listoperciones = new DaoOperaciones().ConsultarOperacionesMenuPorPrefijoPerfil(prefijo);

                    CargarListaHijos(listoperciones);

                    var Resultado = listoperciones.Where(x => x.ID_OPERACION_PADRE == null).OrderBy(x => x.NOMBRE).ToList();
                    retorno = Resultado;
                }
                return retorno;

            }
            catch (Exception exc)
            {
                throw new Exception("Error obteniendo VerificarOperacionPorPrefijoPerfil", exc);
            }
        }

        private static void CargarListaHijos(List<Operacion> operacionesBiz)
        {
            try
            {
                foreach (var padre in operacionesBiz)
                {
                    List<Operacion> hijos = new List<Operacion>();
                    foreach (var hijo in operacionesBiz)
                    {
                        if (hijo.ID_OPERACION_PADRE == padre.ID_OPERACION)
                            hijos.Add(hijo);
                    }

                    if (hijos.Count > 0)
                    {
                        Operacion op = operacionesBiz.FirstOrDefault(x => x.ID_OPERACION == padre.ID_OPERACION);
                        op.Hijos = new List<Operacion>();
                        op.Hijos.AddRange(hijos);

                        CargarListaHijos(hijos);
                    }
                }

            }
            catch (Exception exc) { throw exc; }
        }

    }
}

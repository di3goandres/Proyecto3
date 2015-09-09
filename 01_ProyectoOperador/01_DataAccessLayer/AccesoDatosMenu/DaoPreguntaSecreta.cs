using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Uniandes.Centralizador.AccesoDatos.Menu
{
    public class DaoPreguntas
    {
        public List<PreguntasSecreta> ObtenerPreguntas()
        {
            try
            {
                List<PreguntasSecreta> listaRetorno = new List<PreguntasSecreta>();
                using (AccesoDatosDataContext ctx = new AccesoDatosDataContext(ConfigurationManager.ConnectionStrings["UniandesConnectionString"].ConnectionString))
                {
                    var DATOS = (from c in ctx.PreguntasSecreta
                                 select c);
                    if (DATOS.Any())
                    {
                        listaRetorno = DATOS.ToList();
                    }
                }

                return listaRetorno;
            }
            catch (Exception ex)
            {
                throw;
            }


        }
    }
}

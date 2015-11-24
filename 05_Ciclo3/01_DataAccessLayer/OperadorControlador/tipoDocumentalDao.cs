using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uniandes.Controlador
{
    public class tipoDocumentalDao
    {
        public decimal obtenerTipoLogiaDocumental(String codigo)
        {
            decimal retorno = 0;

            try
            {
                using (OperadorDataContext ctx = new OperadorDataContext())
                {


                    var id = (from cp in ctx.tbl_tipoDocumento
                              where
                               cp.codigo == codigo
                              select cp);

                    if (id.Any())
                    {
                        retorno = id.First().idTipoDocumento;
                    }
                }
                return retorno;

            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}

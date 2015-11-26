using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Centralizador.DAO
{
    public class SoapMessageDao
    {

        public tbl011_SoapMessage obtenerUrl(int IdOperador)
        {


            try
            {

                tbl011_SoapMessage retorno = new tbl011_SoapMessage();
                using (CentralizadorDataContext ctx = new CentralizadorDataContext())
                {


                    var SoapMessage = (from cc in ctx.tbl011_SoapMessage
                                       where cc.idOperador == IdOperador
                                   select cc);


                    if (SoapMessage.Any())
                    {
                        retorno = SoapMessage.First();
                    }
                    return retorno;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        
        }
    }
}

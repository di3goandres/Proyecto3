using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Uniandes.Centralizador.AccesoDatos.Menu
{
    public class DaoActivity
    {

        public bool registrarLog(Guid UniqueIdentifierUser, String Pagina)
        {

            int idOperacion = 0;
            using (AccesoDatosDataContext ctx = new AccesoDatosDataContext(ConfigurationManager.ConnectionStrings["UniandesConnectionString"].ConnectionString))
            {
                var paginas = (from d in ctx.OPERACION

                               where d.URL.Contains(Pagina)
                               select d).Distinct();
                if (paginas.Any())
                {
                    idOperacion = paginas.First().ID_OPERACION;

                }

                Activity act = new Activity();

                act.ActivityDay = DateTime.Now;
                act.Id_Operacion = idOperacion;
                act.UserId = UniqueIdentifierUser;
                ctx.Activity.InsertOnSubmit(act);
                ctx.SubmitChanges();

            }

            return true;
        }
    }
}

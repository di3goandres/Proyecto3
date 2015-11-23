using Operador.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uniandes.Controlador;


namespace Uniandes.GestorMensajeria
{
    public class GestorMensajeria
    {

        public bool EnviarMensaje(TransferenciaMensajes transferenciaMensaje) {
            BandejaNotificacionesDAO dao = new BandejaNotificacionesDAO();

            return dao.EnviarMensaje(transferenciaMensaje);
            
        }


    }
}

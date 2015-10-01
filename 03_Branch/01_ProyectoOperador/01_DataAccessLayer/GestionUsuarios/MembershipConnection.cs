using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.Collections.Specialized;
using System.Reflection;

namespace Uniandes.GestionUsuarios
{
    public class MembershipConnection
    {

        public bool validarUsuarios(String Nombre, string pass)
        {
            CustomMemberShip c = new CustomMemberShip();
            return c.ValidateUser(Nombre, pass);

        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Security;
using Uniandes.Utilidades;

namespace Uniandes.GestionUsuarios
{
    public class CustomMemberShip : SqlMembershipProvider
    {
        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {
            base.Initialize(name, config);// Update the private connection string field in the base class.
            string connectionString = "ApplicationServices".GetFromConnStrings();
            // Set private property of Membership provider.
            var connectionStringField = GetType().BaseType.GetField("_sqlConnectionString", BindingFlags.Instance | BindingFlags.NonPublic);
            connectionStringField.SetValue(this, connectionString);
        }
    }
}

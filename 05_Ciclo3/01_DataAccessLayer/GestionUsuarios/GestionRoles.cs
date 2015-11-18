using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;

namespace Uniandes.GestionUsuarios
{
    public class GestionRoles
    {

        public List<String> GetRolesForUser(String UserName)
        {
            return Roles.GetRolesForUser(UserName).ToList();
        }

        public List<String> GetAllRoles()
        {
            return Roles.GetAllRoles().ToList();
        }

        public List<String> GetAllRollesExeptActual(string RolActual)
        {

            List<String> RolesExept = GetAllRoles();
            RolesExept.Remove(RolActual);
            return RolesExept;

        }

        public void AddUserToRole(string username, string roleName)
        {
            Roles.AddUserToRole(username, roleName);
        }
        public bool CreateRole(string roleName)
        {
            Roles.CreateRole(roleName);
            return true;
        }


        public bool RoleExists(string roleName)
        {

            return Roles.RoleExists(roleName);
        }


        public bool DeleteRole(string roleName)
        {
            var listaUsuarios = GetUsersInRole(roleName);

            if (listaUsuarios.Count() > 0) Roles.RemoveUsersFromRole(listaUsuarios.ToArray(), roleName);

            Roles.DeleteRole(roleName);
            return true;
        }

        public bool RemoveUserFromRole(string username, string roleNameRemo, string roleNameNew)
        {

            Roles.RemoveUserFromRole(username, roleNameRemo);
            Roles.AddUserToRole(username, roleNameNew);
            return true;

        }
        public List<String> GetUsersInRole(string roleName)
        {
            return Roles.GetUsersInRole(roleName).ToList();

        }


    }
}

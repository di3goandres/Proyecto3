using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Web.Security;
using System.Security.Cryptography;
using System.Threading;

namespace Uniandes.GestionUsuarios
{
    public class GestionUsuario : MembershipProvider
    {

        public static string GetMD5Hash(string value)
        {
            MD5 md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(value));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            MembershipUser usuario = Membership.GetUser(username);
            usuario.ChangePassword(oldPassword, newPassword);
            usuario.UnlockUser();
            return true;
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            MembershipUser u = Membership.GetUser(username);
            Boolean result = u.ChangePasswordQuestionAndAnswer(password,
                                              newPasswordQuestion,
                                              newPasswordAnswer);
            return result;
        }

        public override MembershipUser CreateUser(string username, string password, string email,
            string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            MembershipUser newUser =
                           Membership.CreateUser(username, password,
                                                email, passwordQuestion,
                                                passwordAnswer, isApproved, providerUserKey,
                                                 out status);



            return newUser;
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordReset
        {
            get { throw new NotImplementedException(); }
        }

        public override bool EnablePasswordRetrieval
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            return Membership.FindUsersByEmail(emailToMatch, pageIndex, pageSize, out totalRecords);
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            return Membership.FindUsersByName(usernameToMatch, pageIndex, pageSize, out totalRecords);

        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            return Membership.GetAllUsers(pageIndex, pageSize, out totalRecords);

        }

        public override int GetNumberOfUsersOnline()
        {
            return Membership.GetNumberOfUsersOnline();

        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            MembershipUser user = Membership.GetUser(username);
            return user;
        }

        public bool DeleteUser(string username)
        {

            return Membership.DeleteUser(username, true);

        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            return Membership.GetUserNameByEmail(email);
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredPasswordLength
        {
            get { throw new NotImplementedException(); }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresUniqueEmail
        {
            get { throw new NotImplementedException(); }
        }

        public override string ResetPassword(string username, string answer)
        {
            MembershipUser user = Membership.GetUser(username);
            string newPassword = user.ResetPassword(answer);
            return newPassword;
        }

        public override bool UnlockUser(string userName)
        {
            MembershipUser user = Membership.GetUser(userName);
            user.UnlockUser();
            return true;

        }

        public override void UpdateUser(MembershipUser user)
        {
            Membership.UpdateUser(user);

        }

        public override bool ValidateUser(string username, string password)
        {
            return Membership.ValidateUser(username, password);

        }
    }
}

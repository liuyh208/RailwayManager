using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using GasWebMap.Core;
using GasWebMap.Core.Data;
using GasWebMap.Domain;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;

namespace GasWebMap.Services.Responses
{
    internal class CustomAuthProvider : CredentialsAuthProvider
    {
        private User curUser;

        public override bool TryAuthenticate(IServiceBase authService, string userName, string password)
        {
            IRepository<User> rep = AppEx.Container.GetRepository<User>();

            curUser = rep.GetEntity(t => t.Name == userName && t.HashedPassword == password);
            if (curUser == null)
                return false;

            var session = (CustomUserSession) authService.GetSession(false);
            session.DepartmentID = curUser.DepartmentID;

            var rep1 = AppEx.Container.GetRepository<Department>();
            if (curUser.DepartmentID.HasValue)
            {
                var en = rep1.GetEntity(t => t.Id == curUser.DepartmentID.Value);
                if (en != null)
                {
                    session.Department = en.Name;
                }
            }
           
          
            IRepository<UserRole> rep2 = AppEx.Container.GetRepository<UserRole>();
            List<Guid> lst = rep2.GetEntities(t => t.UserID == curUser.Id).Select(t => t.RoleID).Distinct().ToList();

            IRepository<Role> repRole = AppEx.Container.GetRepository<Role>();
            var lstRoleName = new List<string>();
            foreach (Guid roleId in lst)
            {
                Role role = repRole.GetEntityByID(roleId);
                lstRoleName.Add(role.Name);
            }
            session.Roles = lstRoleName;
            session.IsAdmin = session.HasRole("管理员");
            session.Roles = lstRoleName;
            session.UserName = userName;
            session.UserID = curUser.Id;
            session.IsAuthenticated = true;
            FormsAuthentication.SetAuthCookie(userName, true);
           
            return true;
        }

        private string CheckUser(string userName, string password)
        {
            IRepository<User> rep = AppEx.Container.GetRepository<User>();

            curUser = rep.GetEntity(t => t.Name == userName && t.HashedPassword == password);
            if (curUser != null)
                return "成功";

            return "";
        }
    }
}
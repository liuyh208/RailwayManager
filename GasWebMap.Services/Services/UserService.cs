using System;
using System.Collections.Generic;
using System.Linq;
using GasWebMap.Core.Data;
using GasWebMap.Domain;
using GasWebMap.Services.Base;
using GasWebMap.Services.Responses;
using ServiceStack.ServiceInterface;

namespace GasWebMap.Services
{
    [Authenticate]
    public class UserService : ServiceBase
    {
        private IList<User> lstUser;

        public IList<User> Get(UserGet userGet)
        {
            IRepository<User> rep = GetRepository<User>();
            if (userGet.DepartID == Guid.Empty || userGet.DepartID == Guid.Parse("f693d47c-fa95-007f-8c39-564208c34886"))
            {
                lstUser = rep.GetEntities(t => t.Name != null).ToList();
            }
            else
            {
                lstUser = rep.GetEntities(t => t.DepartmentID == userGet.DepartID).ToList();
            }
            return lstUser;
        }

        public User Post(UserDto dto)
        {
            var u = new User
            {
                Name = dto.Name,
                HashedPassword = dto.Password,
                Email = dto.Email,
                Salt = "***",
                CreatedOn = DateTime.Now,
                Id = Guid.NewGuid(),
                Telephone=dto.Telephone,
                SMS=dto.Sms,
                DepartmentID = dto.DeaprtID
            };
            IRepository<User> rep = GetRepository<User>();
            rep.Add(u);
            return u;
        }

        public User Put(UserDto dto)
        {
            IRepository<User> rep = GetRepository<User>();
            User u = rep.GetEntityByID(dto.ID);
            if (u != null)
            {
                u.Id = dto.ID;
                u.Name = dto.Name;
                u.Telephone = dto.Telephone;
                u.SMS = dto.Sms;
                u.HashedPassword = dto.Password;
                u.Email = dto.Email;
                rep.Update(u);
                return u;
            }
            return new User();
        }


        public ResponseResult Delete(UserDelete users)
        {
            IRepository<User> rep = GetRepository<User>();
            var r=GetRepository<UserRole>();
            foreach (var u in users)
            {
                r.Delete(t=>t.UserID==u);
            }
            rep.DeleteByIDs(users);
            return ResponseResult.SuccessRes;
        }
    }
}
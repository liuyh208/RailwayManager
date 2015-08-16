using System;
using System.Collections.Generic;
using GasWebMap.Services.Base;
using ServiceStack.ServiceInterface.Auth;

namespace GasWebMap.Services.Responses
{
    public class CustomUserSession : AuthUserSession, ICustomSession
    {
        
        public Guid? UserID { get; set; }

        public Guid? DepartmentID { get; set; }

        public string Department { get; set; }

        /// <summary>
        ///     是否管理员
        /// </summary>
        /// <value>The admin.</value>
        public bool IsAdmin { get; set; }
    }
}
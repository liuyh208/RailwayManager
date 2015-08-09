using System;
using System.Collections.Generic;
using ServiceStack.ServiceInterface.Auth;

namespace GasWebMap.Services.Responses
{
    public class CustomUserSession : AuthUserSession
    {
        
        public Guid? UserID { get; set; }

        public Guid? DepartmentID { get; set; }

        /// <summary>
        ///     是否管理员
        /// </summary>
        /// <value>The admin.</value>
        public bool IsAdmin { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GasWebMap.Services.Base
{
    public interface ICustomSession
    {
         Guid? UserID { get; set; }

         Guid? DepartmentID { get; set; }

        /// <summary>
        ///     是否管理员
        /// </summary>
        /// <value>The admin.</value>
         bool IsAdmin { get; set; }
    }
}

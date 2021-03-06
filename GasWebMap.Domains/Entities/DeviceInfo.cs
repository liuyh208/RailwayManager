﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GasWebMap.Core.Data;
using ServiceStack.ServiceHost;

namespace GasWebMap.Domain
{

    public class DeviceInfo : EntityBase
    {
        public Guid? DepartmentID { get; set; }
        public int pOrder { get; set; }
        /// <summary>
        /// 车间
        /// </summary>
        /// <value>The workshop.</value>
        public string Workshop { get; set; }

        public string Name { get; set; }

        public string ProtectUnit { get; set; }

        public string IdentifyStyle { get; set; }

        public string DeviceType { get; set; }

        public string FactoryCode { get; set; }

        /// <summary>
        /// 精度
        /// </summary>
        /// <value>The device accuracy.</value>
        public string DeviceAccuracy { get; set; }

        public string Factory { get; set; }

        /// <summary>
        ///溯源周期
        /// </summary>
        /// <value>The cycle.</value>
        public int Cycle { get; set; }

        public DateTime? BuyDate { get; set; }

        /// <summary>
        /// 鉴定时间
        /// </summary>
        /// <value>The identify date.</value>
        public DateTime? IdentifyDate { get; set; }

        /// <summary>
        /// 有效期
        /// </summary>
        /// <value>The valid date.</value>
        public DateTime? ValidDate { get; set; }

        /// <summary>
        /// 鉴定单位
        /// </summary>
        /// <value>The identify factory.</value>
        public string IdentifyUnit { get; set; }

        public string Status { get; set; }

        /// <summary>
        /// 鉴定证书编号
        /// </summary>
        /// <value>The identify certificate no.</value>
        public string IdentifyCertificateNo { get; set; }

        public string Remark { get; set; }
    }
}
